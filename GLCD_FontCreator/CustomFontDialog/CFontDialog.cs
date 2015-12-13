/*

Derived from CustomFontDialog by Syed Umar Anis (http://umaranis.wordpress.com/)

*/
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
using System.Text;
using System.Windows.Forms;

namespace GLCD_FontCreator.CustomFontDialog
{
  public partial class CFontDialog : Form
  {
    private Font m_selFont;

    public CFontDialog( PrivateFontCollection pfc )
    {
      InitializeComponent( );

      lstFont.PrivateFonts = pfc;
      lstFont.LoadFonts( );

      lstFont.SelectedFontFamilyChanged += lstFont_SelectedFontFamilyChanged;
      lstFont.SelectedFontFamily = FontFamily.GenericSansSerif;
      lstFont.AddSelectedFontToRecent( );
      txtSize.Text = Convert.ToString( 10 );
    }

    public void AddPrivateFonts( PrivateFontCollection pfc )
    {
      lstFont.PrivateFonts = pfc;
    }


    public new Font Font
    {
      get
      {
        // should use a variable - the label is disposed after clicking OK and the owner may as for the font only later
        return m_selFont; // lblSampleText.Font;
      }
      set
      {
        lstFont.AddFontToRecent( value.FontFamily );
        lstFont.SelectedFontFamily = value.FontFamily;
        txtSize.Text = value.Size.ToString( );
        chbBold.Checked = value.Bold;
        chbItalic.Checked = value.Italic;
        chbStrikeout.Checked = value.Strikeout;
      }
    }

    public void AddFontToRecentList( FontFamily ff )
    {
      lstFont.AddFontToRecent( ff );
    }

    private void lstFont_SelectedFontFamilyChanged( object sender, EventArgs e )
    {
      UpdateSampleText( );
    }

    private void lstSize_SelectedIndexChanged( object sender, EventArgs e )
    {
      if ( lstSize.SelectedItem != null )
        txtSize.Text = lstSize.SelectedItem.ToString( );
    }

    private void txtSize_TextChanged( object sender, EventArgs e )
    {
      if ( lstSize.Items.Contains( txtSize.Text ) )
        lstSize.SelectedItem = txtSize.Text;
      else
        lstSize.ClearSelected( );

      UpdateSampleText( );
    }


    private void txtSize_KeyDown( object sender, KeyEventArgs e )
    {
      switch ( e.KeyData ) {
        case Keys.D0:
        case Keys.D1:
        case Keys.D2:
        case Keys.D3:
        case Keys.D4:
        case Keys.D5:
        case Keys.D6:
        case Keys.D7:
        case Keys.D8:
        case Keys.D9:
        case Keys.End:
        case Keys.Enter:
        case Keys.Home:
        case Keys.Back:
        case Keys.Delete:
        case Keys.Escape:
        case Keys.Left:
        case Keys.Right:
          break;
        case Keys.Decimal:
        case ( Keys )190: //decimal point
          if ( txtSize.Text.Contains( "." ) ) {
            e.SuppressKeyPress = true;
            e.Handled = true;
          }
          break;
        default:
          e.SuppressKeyPress = true;
          e.Handled = true;
          break;
      }

    }

    private void UpdateSampleText( )
    {
      if ( lstFont.SelectedFontFamily == null ) return; // more init issues


      float size = txtSize.Text != "" ? float.Parse(txtSize.Text) : 1;
      // determine what is available and what is choosen
      bool fRegular = lstFont.SelectedFontFamily.IsStyleAvailable( FontStyle.Regular );
      bool fBold = lstFont.SelectedFontFamily.IsStyleAvailable( FontStyle.Bold );
      bool fItalic = lstFont.SelectedFontFamily.IsStyleAvailable( FontStyle.Italic );
      bool fStrike = lstFont.SelectedFontFamily.IsStyleAvailable( FontStyle.Strikeout );

      FontStyle style = (FontStyle) 99; // invalid to check later..

      if ( fRegular ) style = FontStyle.Regular;
      if ( chbBold.Checked && fBold ) style = FontStyle.Bold;
      if ( chbItalic.Checked && fItalic ) style = FontStyle.Italic;
      if ( chbStrikeout.Checked && fStrike ) style |= FontStyle.Strikeout; // this is OR'ed (underline would be too)

      if ( style == ( FontStyle )99 && fItalic ) style = FontStyle.Italic;
      if ( style == ( FontStyle )99 && fBold ) style = FontStyle.Bold;
      if ( style == ( FontStyle )99 ) {
        // still - seems like a bug...
        ;
      }


      if ( m_selFont == null ) {
        // on init components only
        m_selFont = lblSampleText.Font; // first assignment in init
      }
      Font tmp = m_selFont;
      m_selFont = new Font( lstFont.SelectedFontFamily, size, style );
      lblSelFont.Text = m_selFont.Name;
      lblSampleText.Font = m_selFont;
      tmp.Dispose( );
    }



    /// <summary>
    /// Handles CheckedChanged event for Bold, 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void chb_CheckedChanged( object sender, EventArgs e )
    {
      UpdateSampleText( );
    }

    private void btnOK_Click( object sender, EventArgs e )
    {
      lstFont.AddSelectedFontToRecent( );
    }

  }
}
