/*
Part of GLCD_FontCreator - Copyright 2015 Martin Burri

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GLCD_FontCreator.CustomFontDialog;
using GLCD_FontCreator.FontCreators;

namespace GLCD_FontCreator
{
  public partial class FontCreator : Form
  {
    // stores the FontCreator supported
    private Dictionary<String, Func<FontOptimizer,FCBase>> m_FCchooser = new  Dictionary<string, Func<FontOptimizer, FCBase>>();

    public FontCreator( )
    {
      InitializeComponent( );

      this.Text = String.Format( "{0} V {1}", Application.ProductName, Application.ProductVersion );

      // the default test char line
      c_testString = "";
      for ( Char c = ' '; c < Convert.ToChar(127); c++ ) {
        c_testString += c;
      }

      m_font = ( Font )fDlg.Font.Clone( );
      ValidateChars( );


      // ************ ADD NEW FONT CREATOR FORMATS HERE *********************
      // add known file formats
      comFileFormat.Items.Add( GLCD_FC2_Compatible.FontCreatorName );
      m_FCchooser.Add( GLCD_FC2_Compatible.FontCreatorName, GLCD_FC2_Compatible.GetInstance );
      // next one
      // comFileFormat.Items.Add( FC_Template.FontCreatorName );
      // m_FCchooser.Add( FC_Template.FontCreatorName, FC_Template.GetInstance );


      // finally set the default selected Save File Format
      comFileFormat.SelectedIndex = 0; // just select the first one as default

      // more init
      hScrTargetHeight.Value = 16; // start value Height target
      OptimizeFont( ); // optimize with default values
    }



    private String c_testString = "";
    private Font m_font = null;
    private FCBase FC = null;
    private FontOptimizer FO = null;
    private PrivateFontCollection MYFONTS = null; // contains loadable fonts
    private CFontDialog cfDlg = new CFontDialog(null);
    private AppSettings appSettings = new AppSettings();


    private void InitText( )
    {
      if ( String.IsNullOrEmpty( txMyText.Text ) ) {
        txTxFont.Text = c_testString;
      }
      else {
        txTxFont.Text = txMyText.Text;
      }
    }

    private void ShowFontProps( )
    {
      InitText( );
      txTxFont.Font = m_font;

      // get font props shown
      lbFontProps.Items.Clear( );
      String tx = "";
      tx = String.Format( "Bold                  {0}", m_font.Bold ); lbFontProps.Items.Add( tx );
      tx = String.Format( "Italic                {0}", m_font.Italic ); lbFontProps.Items.Add( tx );
      tx = String.Format( "Underline             {0}", m_font.Underline ); lbFontProps.Items.Add( tx );
      tx = String.Format( "Strikeout             {0}", m_font.Strikeout ); lbFontProps.Items.Add( tx );
      tx = String.Format( "Family                {0}", m_font.FontFamily.ToString( ) ); lbFontProps.Items.Add( tx );
      tx = String.Format( "Name                  {0}", m_font.Name ); lbFontProps.Items.Add( tx );
      tx = String.Format( "Charset               {0}", m_font.GdiCharSet ); lbFontProps.Items.Add( tx );
      tx = String.Format( "Height (line spacing) {0}", m_font.Height ); lbFontProps.Items.Add( tx );
      tx = String.Format( "Size [pts]            {0}", m_font.SizeInPoints ); lbFontProps.Items.Add( tx );
    }


    private void OptimizeFont( )
    {
      if ( m_TotalChars <= 0 ) return; // TODO complain..

      // use optimizer to get the height
      FO = new FontOptimizer( m_font, MYFONTS );
      FO.FirstChar = txFirstChar.Text[0];
      FO.CharCount = m_TotalChars;
      int tSize;
      if ( !int.TryParse( txTargetSize.Text, out tSize ) ) {
        tSize = 12; txTargetSize.Text = tSize.ToString( ); // fix if not a number..
      }
      FO.TargetHeight = tSize;
      FO.RemoveTop = cbxRemoveTop.Checked;
      FO.RemoveBottom = cbxRemoveBottom.Checked;

      FO.Optimize( ); // this should now reveal the font to use
      // carry into globals and GUI
      m_font = FO.FontToUse;
      ShowFontProps( );
      txFinalSize.Text = String.Format( "{0} x {1}", FO.MinimumRect.Width, FO.MinimumRect.Height );
      Clean( );
    }

    private void LoadFont( )
    {
      float tSize;
      if ( !float.TryParse( txTargetSize.Text, out tSize ) ) {
        tSize = 12; txTargetSize.Text = tSize.ToString( ); // fix if not a number..
      }
      m_font = new Font( cfDlg.Font.FontFamily, tSize, cfDlg.Font.Style );
      cfDlg.Font = m_font;
      // using a 'tuned font dialog that deals with the exception issue of the original one
      cfDlg.AddPrivateFonts( MYFONTS );
      if (  cfDlg.ShowDialog( this ) != DialogResult.Cancel ) {
        m_font = ( Font )cfDlg.Font.Clone();

        OptimizeFont( );
      }
    }

    private void LoadFontFile( )
    {
      String exlist = "";

      ofDlg.InitialDirectory = appSettings.FontDirPath;
      if ( ofDlg.ShowDialog( this ) != DialogResult.Cancel ) {
        if ( MYFONTS == null )
          MYFONTS = new PrivateFontCollection( );

        foreach ( String fn in ofDlg.FileNames ) {
          try {
            MYFONTS.AddFontFile( fn );
          }
          catch ( Exception ) {
            exlist += String.Format( "    {0}\n", Path.GetFileNameWithoutExtension( fn ) );
          }
        }
        if ( ! String.IsNullOrEmpty(exlist)) {
          MessageBox.Show( String.Format( "The font types of: \n{0}\n   are unfortunately not supported!", exlist ), "Add font file", MessageBoxButtons.OK );
        }
      }
    }


    private void LoadFontDirectory( )
    {
      String exlist = "";

      fbDlg.SelectedPath = appSettings.FontDirPath;
      if ( fbDlg.ShowDialog( this ) != DialogResult.Cancel ) {
        appSettings.FontDirPath = fbDlg.SelectedPath; appSettings.Save( );
        if ( MYFONTS == null )
          MYFONTS = new PrivateFontCollection( );

        foreach ( String fn in Directory.EnumerateFiles( fbDlg.SelectedPath, "*.ttf", SearchOption.AllDirectories  ) ) {
          try {
            MYFONTS.AddFontFile( fn );
          }
          catch ( Exception ) {
            exlist += String.Format( "    {0}\n", Path.GetFileNameWithoutExtension( fn ) );
          }
        }
        if ( !String.IsNullOrEmpty( exlist ) ) {
          MessageBox.Show( String.Format( "The font types of: \n{0}\n   are unfortunately not supported!", exlist ), "Add font file", MessageBoxButtons.OK );
        }
      }
    }


    private void SaveFontAs( )
    {
      if ( FO == null ) return;
      if ( m_TotalChars <= 0 ) return;

      if ( comFileFormat.SelectedItem == null ) return;
      if ( !m_FCchooser.ContainsKey( ( String )comFileFormat.SelectedItem ) ) return; // ERROR exit


      FC = m_FCchooser[( String )comFileFormat.SelectedItem]( FO ); // call the selected format instance factory

      String ret = "";
      FontOptimizer.WidthTarget widthTarget = FontOptimizer.WidthTarget.WT_None;
      if ( rbTrimMono.Checked )
        widthTarget = FontOptimizer.WidthTarget.WT_Mono;
      else if ( rbTrimMinimum.Checked )
        widthTarget = FontOptimizer.WidthTarget.WT_Minimum;

      ret = FC.FontFile( txFirstChar.Text[0], m_TotalChars, widthTarget );
      String fName = "";
      fName = FC.FontNameCreated + ".h";

      sfDlg.FileName = fName;
      sfDlg.InitialDirectory = appSettings.SaveDirPath;
      if ( sfDlg.ShowDialog( this ) != DialogResult.Cancel ) {
        using ( TextWriter tw = new StreamWriter( sfDlg.FileName, false ) ) {
          tw.Write( ret );
          FO.MakeThumbnail( sfDlg.FileName );
          txFontName.Text = String.Format( "File: {0} created, code size is {1} bytes", Path.GetFileName( sfDlg.FileName ), FC.CodeSize );
          appSettings.SaveDirPath = Path.GetDirectoryName( sfDlg.FileName ); appSettings.Save( );
        }
      }
    }



    private void Dirty( )
    {
      lblDirty.Visible = true;
    }

    private void Clean( )
    {
      lblDirty.Visible = false;
    }


    private void btToTest_Click( object sender, EventArgs e )
    {
      if ( m_TotalChars <= 0 ) return; // TODO complain..
      // create string
      String testString = "";
      int lastChar = Convert.ToChar( Convert.ToByte(txFirstChar.Text[0]) + m_TotalChars -  1);
      for ( Char c = txFirstChar.Text[0]; c <= lastChar; c++ ) {
        testString += c;
      }
      txMyText.Text = testString;
    }

    private void btClearTest_Click( object sender, EventArgs e )
    {
      txMyText.Text = "";
    }

    private void txMyText_TextChanged( object sender, EventArgs e )
    {
      InitText( );
    }

    private void txFirstChar_TextChanged( object sender, EventArgs e )
    {
      ValidateChars( );
      Dirty( );
    }

    private void txLastChar_TextChanged( object sender, EventArgs e )
    {
      ValidateChars( );
      Dirty( );
    }

    private void ValidateChars( )
    {
      if ( txFirstChar.TextLength > 0 ) {
        txFirstCharASC.Text = String.Format( "{0,2:D2}", Convert.ToByte( txFirstChar.Text[0] ) );

        if ( txLastChar.TextLength > 0 ) {
          txLastCharASC.Text = String.Format( "{0,2:D2}", Convert.ToByte( txLastChar.Text[0] ) );

          if ( Convert.ToByte( txLastChar.Text[0] ) >= Convert.ToByte( txFirstChar.Text[0] ) ) {
            txCharCount.Text = MakeTotalChars( ).ToString( );
          }
          else {
            txCharCount.Text = "-";
          }
        }
        else {
          txLastCharASC.Text = "-";
        }
      }
      else {
        txFirstCharASC.Text = "-";
      }


    }

    private int m_TotalChars = 0;

    private int MakeTotalChars( )
    {
      if ( ( txFirstChar.TextLength > 0 ) && ( txLastChar.TextLength > 0 ) ) {
        m_TotalChars = Convert.ToByte( txLastChar.Text[0] );
        m_TotalChars -= Convert.ToByte( txFirstChar.Text[0] );
        m_TotalChars++;
      }
      else {
        m_TotalChars = 0;
      }
      return m_TotalChars;
    }

    private void hScrTargetHeight_ValueChanged( object sender, EventArgs e )
    {
      txTargetSize.Text = hScrTargetHeight.Value.ToString( );
      Dirty( );
    }


    #region Menu & Buttons

    private void btNewFont_Click( object sender, EventArgs e )
    {
      LoadFont( );
    }

    private void btSaveFontAs_Click( object sender, EventArgs e )
    {
      SaveFontAs( );
    }

    private void btOptimizeFont_Click( object sender, EventArgs e )
    {
      OptimizeFont( );
    }

    private void loadFontToolStripMenuItem_Click( object sender, EventArgs e )
    {
      LoadFont( );
    }

    private void addFontfilesToolStripMenuItem_Click( object sender, EventArgs e )
    {
      LoadFontFile( );
    }

    private void AddFontDirectorytoolStripMenuItem_Click( object sender, EventArgs e )
    {
      LoadFontDirectory( );
    }

    private void saveAsToolStripMenuItem_Click( object sender, EventArgs e )
    {
      SaveFontAs( );
    }

    private void exitToolStripMenuItem_Click( object sender, EventArgs e )
    {
      Application.Exit( );
    }



    #endregion

  }
}
