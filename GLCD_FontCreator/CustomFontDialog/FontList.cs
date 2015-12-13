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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Text;

using GLCD_FontCreator.GDI;

namespace GLCD_FontCreator.CustomFontDialog
{
  public partial class FontList : UserControl
  {

    public event EventHandler SelectedFontFamilyChanged;
    public RecentlyUsedList RecentlyUsed = new RecentlyUsedList(5);
    private int lastSelectedIndex = -1;

    private InstalledFontCollection ifc = new InstalledFontCollection();
    private PrivateFontCollection   pfc = null;
    private int m_numPrivFonts  = 0;

    public PrivateFontCollection PrivateFonts
    {
      get { return pfc; }
      set
      {
        pfc = value;
        UpdatePrivateFonts( );
      }
    }


    public FontList( )
    {
      InitializeComponent( );


      lstFont.Items.Add( "Section" ); //section entry for Recently Used
      lstFont.Items.Add( "Section" ); //section entry for All Fonts
    }


    private void UpdatePrivateFonts( )
    {
      //remove all priv fonts
      for ( int i=0; i<m_numPrivFonts; i++ ) {
        lstFont.Items.RemoveAt( AllFontsStartIndex ); // from top
      }
      m_numPrivFonts = 0; // as they are removed...

      if ( pfc != null ) {
        foreach ( FontFamily f in pfc.Families ) {
          try {
            if ( f.Name != null || f.Name != "" ) {
              Font addFont = null;
              // have to try different styles - as not all are provided in each family
              if ( f.IsStyleAvailable( FontStyle.Regular ) )
                addFont = new Font( f, 12, FontStyle.Regular );
              else if ( f.IsStyleAvailable( FontStyle.Italic ) )
                addFont = new Font( f, 12, FontStyle.Italic );
              else if ( f.IsStyleAvailable( FontStyle.Bold ) )
                addFont = new Font( f, 12, FontStyle.Bold );

              // others go in the bin ..
              if ( addFont != null && !addFont.GdiVerticalFont )
                // just add it
                lstFont.Items.Insert( AllFontsStartIndex + m_numPrivFonts, addFont );   m_numPrivFonts++;
              // handle recent items
              if ( RecentlyUsed.Contains(addFont.Name ) ) {
                RecentlyUsed.Update( addFont );
              }
            }
          }
          catch ( Exception e ) {
            System.Diagnostics.Debug.Print( "Ex font: {1} - {0}", f.Name, e.Message );
          }

        }
      }
    }


    public void LoadFonts( )
    {
      // add private ones first
      UpdatePrivateFonts( );

      // now the installed ones
      foreach ( FontFamily f in ifc.Families ) {
        try {
          if ( f.Name != null || f.Name != "" ) {
            Font addFont = null;
            // have to try different styles - as not all are provided in each family
            if ( f.IsStyleAvailable( FontStyle.Regular ) )
              addFont = new Font( f, 12, FontStyle.Regular );
            else if ( f.IsStyleAvailable( FontStyle.Italic ) )
              addFont = new Font( f, 12, FontStyle.Italic );
            else if ( f.IsStyleAvailable( FontStyle.Bold ) )
              addFont = new Font( f, 12, FontStyle.Bold );

            // others go in the bin ..
            if ( addFont != null && !addFont.GdiVerticalFont )
              lstFont.Items.Add( addFont );
          }
        }
        catch ( Exception e ) {
          System.Diagnostics.Debug.Print( "Ex font: {1} - {0}", f.Name, e.Message );
        }

      }

    }


    private const int RecentlyUsedSectionIndex = 0;
    private int AllFontsSectionIndex
    {
      get
      {
        return RecentlyUsed.Count + 1;
      }
    }


    private int AllFontsStartIndex
    {
      get
      {
        return RecentlyUsed.Count + 2;
      }
    }



    public FontFamily SelectedFontFamily
    {
      get
      {
        if ( lstFont.SelectedItem != null )
          return ( ( Font )lstFont.SelectedItem ).FontFamily;
        else
          return null;
      }
      set
      {
        if ( value == null ) lstFont.ClearSelected( );
        else {
          lstFont.SelectedIndex = IndexOf( value );
        }

      }
    }

    public int IndexOf( FontFamily ff )
    {
      for ( int i = 1; i < lstFont.Items.Count; i++ ) {
        if ( lstFont.Items[i].GetType( ) == typeof( String ) && ( ( string )lstFont.Items[i] == "Section" ) ) continue; // there are fonts and strings in this list..
        Font f = (Font)lstFont.Items[i];
        if ( f.FontFamily.Name == ff.Name ) {
          return i;
        }
      }

      return -1;
    }

    public void AddSelectedFontToRecent( )
    {
      if ( lstFont.SelectedIndex < 1 ) return;

      lstFont.SuspendLayout( );

      int tmpCount = RecentlyUsed.Count;

      RecentlyUsed.Add( ( Font )lstFont.SelectedItem ); // this is on top

      for ( int i = 1; i <= tmpCount; i++ ) {
        lstFont.Items.RemoveAt( 1 ); // remove all Recent ones
      }

      for ( int i = 0; i < RecentlyUsed.Count; i++ ) {
        lstFont.Items.Insert( i + 1, RecentlyUsed[i] ); // add all from Recent
      }

      lstFont.SelectedIndex = 1;

      lstFont.ResumeLayout( );

    }

    public void AddFontToRecent( FontFamily ff )
    {
      lstFont.SuspendLayout( );

      for ( int i = 1; i <= RecentlyUsed.Count; i++ ) {
        lstFont.Items.RemoveAt( 1 );
      }

      if ( IndexOf( ff ) >= 0 )
        RecentlyUsed.Add( ( Font )lstFont.Items[IndexOf( ff )] );

      for ( int i = 0; i < RecentlyUsed.Count; i++ ) {
        lstFont.Items.Insert( i + 1, RecentlyUsed[i] );
      }

      //lstFont.SelectedIndex = 1;

      lstFont.ResumeLayout( );

    }


    private void lstFont_DrawItem( object sender, DrawItemEventArgs e )
    {


      if ( e.Index == 0 ) {
        e.Graphics.FillRectangle( Brushes.AliceBlue, e.Bounds );
        Font font = new Font(DefaultFont, FontStyle.Bold | FontStyle.Italic);
        e.Graphics.DrawString( "Recently Used", font, Brushes.Purple, e.Bounds.X + 10, e.Bounds.Y + 3, StringFormat.GenericDefault );
      }
      else if ( e.Index == AllFontsStartIndex - 1 ) {
        e.Graphics.FillRectangle( Brushes.AliceBlue, e.Bounds );
        Font font = new Font(DefaultFont, FontStyle.Bold | FontStyle.Italic);
        e.Graphics.DrawString( "All Fonts", font, Brushes.Purple, e.Bounds.X + 10, e.Bounds.Y + 3, StringFormat.GenericDefault );
      }
      else {
        // Draw the background of the ListBox control for each item.
        e.DrawBackground( );

        Font font = (Font)lstFont.Items[e.Index];

        e.Graphics.DrawString( font.Name, font, Brushes.Black, e.Bounds, StringFormat.GenericDefault );
        //        gdiDrawing.RenderText( e.Graphics, font.Name, font.Name, Color.Black, e.Bounds, font.Size );

        // If the ListBox has focus, draw a focus rectangle around the selected item.
        e.DrawFocusRectangle( );
      }




    }

    private void lstFont_SelectedIndexChanged( object sender, EventArgs e )
    {

      if ( lstFont.SelectedIndex == RecentlyUsedSectionIndex || lstFont.SelectedIndex == AllFontsSectionIndex ) {
        lstFont.SelectedIndex = lastSelectedIndex;
      }
      else if ( lstFont.SelectedItem != null ) {
        if ( !txtFont.Focused ) {
          Font f = (Font)lstFont.SelectedItem;
          txtFont.Text = f.Name;
        }

        if ( SelectedFontFamilyChanged != null )
          SelectedFontFamilyChanged( lstFont, new EventArgs( ) );

        lastSelectedIndex = lstFont.SelectedIndex;
      }
    }

    private void txtFont_TextChanged( object sender, EventArgs e )
    {
      if ( !txtFont.Focused ) return;

      for ( int i = AllFontsStartIndex; i < lstFont.Items.Count; i++ ) {
        string str = ((Font)lstFont.Items[i]).Name;
        if ( str.StartsWith( txtFont.Text, true, null ) ) {
          lstFont.SelectedIndex = i;

          const uint WM_VSCROLL = 0x0115;
          const uint SB_THUMBPOSITION = 4;

          uint b = ((uint)(lstFont.SelectedIndex) << 16) | (SB_THUMBPOSITION & 0xffff);
          gdiNative.SendMessage( lstFont.Handle, WM_VSCROLL, b, 0 );

          return;
        }
      }
    }

    private void txtFont_MouseClick( object sender, MouseEventArgs e )
    {
      txtFont.SelectAll( );
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void lstFont_KeyDown( object sender, KeyEventArgs e )
    {
      // if you type alphanumeric characters while focus is on ListBox, it shifts the focus to TextBox.
      if ( Char.IsLetterOrDigit( ( char )e.KeyValue ) ) {
        txtFont.Focus( );
        txtFont.Text = ( ( char )e.KeyValue ).ToString( );
        txtFont.SelectionStart = 1;
      }


      // allows to move between sections using arrow keys
      switch ( e.KeyCode ) {
        case Keys.Left:
        case Keys.Up:
          if ( lstFont.SelectedIndex == AllFontsSectionIndex + 1 ) {
            lstFont.SelectedIndex = lstFont.SelectedIndex - 2;
            e.SuppressKeyPress = true;
          }
          break;
        case Keys.Down:
        case Keys.Right:
          if ( lstFont.SelectedIndex == AllFontsSectionIndex - 1 ) {
            lstFont.SelectedIndex = lstFont.SelectedIndex + 2;
            e.SuppressKeyPress = true;
          }
          break;

      }
    }

    // ensures that focus is lstFont control whenever the form is loaded
    private void FontList_Load( object sender, EventArgs e )
    {
      this.ActiveControl = lstFont;
    }


  }
}
