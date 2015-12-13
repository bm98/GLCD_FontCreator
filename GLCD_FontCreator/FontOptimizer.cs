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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

using System.Diagnostics;
using System.Drawing.Text;

namespace GLCD_FontCreator
{
  class FontOptimizer
  {
    public enum WidthTarget{
      WT_None = 0, // use as given by the font (creating the font as is)
      WT_Mono,     // use the minimum cutout for the charset (creating kind of a monospace font)
      WT_Minimum,  // use the minimum per char (creating a variable spaced font)

    };


    public const int BLANKTX = 20; // pixel values below are considered as not used


    public Font FontToUse;

    // test item
    public Char FirstChar { get; set; }
    public int CharCount { get; set; }

    // guiding targets
    public int TargetHeight { get; set; }
    public Boolean RemoveTop { get; set; }
    public Boolean RemoveBottom { get; set; }

    // outcome
    public int ScanlineStart { get; set; } // first scanline optimized
    public int ScanlineEnd { get; set; } // last scanline optimized
    public int FinalHeight { get; set; }
    public Rectangle MinimumRect { get; set; } // the minimum rectangle to cutout all chars


    private PrivateFontCollection m_myFonts = null;

    /// <summary>
    /// cTor: gets the font to optimize and added fonts by the user
    /// </summary>
    /// <param name="font">The font to optimize</param>
    /// <param name="myFonts">The user set of fonts</param>
    public FontOptimizer( Font font, PrivateFontCollection myFonts )
    {
      // init stuff
      FontToUse = ( Font )font.Clone( );
      RemoveTop = false;
      RemoveBottom = false;
      FirstChar = '0';
      CharCount = 10;
      ScanlineStart = 0;
      ScanlineEnd = 0;
      m_myFonts = myFonts;
    }


    private Font NewFont( Font oldFont, float newSize )
    {
      Font nf = new Font(oldFont.FontFamily, newSize, oldFont.Style, oldFont.Unit );
      if ( ( nf.Name != oldFont.Name ) && ( m_myFonts != null) ) {
        // seems the font is not in the installed ones..
        // so find it in the provided ones if they were provided (should...)
        foreach ( FontFamily ff in m_myFonts.Families ) {
          if ( ff.Name == oldFont.Name )
            nf = new Font( ff, newSize, oldFont.Style, oldFont.Unit );
        }
      }
      return nf;
    }

    /// <summary>
    /// Returns a Red on Black bitmap from a String
    /// no trimming is applied
    /// </summary>
    /// <param name="font">The font to use</param>
    /// <param name="s">The string to paint</param>
    /// <returns></returns>
    private Bitmap GetStringBitmap( Font font, String s )
    {
      // get the size needed to paint the string in one line with the font given
      Bitmap fontImage = new Bitmap( 1600, 200 );// biggg to have all printed on one line which is needed for long strings and large fonts
      Graphics g = Graphics.FromImage( fontImage );
      StringFormat newStringFormat = new StringFormat();
      newStringFormat.Alignment = StringAlignment.Near;
      newStringFormat.LineAlignment = StringAlignment.Near;
      newStringFormat.Trimming = StringTrimming.None;
      Size preferredSize = Size.Ceiling(g.MeasureString( s, font, new Size(1600,200), newStringFormat ));
      g.Dispose( );

      // get a new bitmap with optimal size for painting stuff
      fontImage = new Bitmap( preferredSize.Width, preferredSize.Height );
        g = Graphics.FromImage( fontImage );
        g.ResetClip( );
        g.Clear( Color.Black );
        // set text drawing props to make the best capturing later
        g.TextContrast = 0;
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
        g.DrawString( s, font, Brushes.Red, new Point( 0, 0 ) ); // draw red on black for capturing
        g.Flush( System.Drawing.Drawing2D.FlushIntention.Sync ); // get things finished
      g.Dispose( );

      //fontImage.Save( "TEST.png" ); // save the temp result
      return fontImage;
    }


    /// <summary>
    /// Returns X and Width of the painted area as rectangle
    /// </summary>
    /// <param name="fontImage">The bitmap of the image to identify</param>
    /// <returns>The minimum rectangle for X and Width (Y and Height are from global)</returns>
    private Rectangle GetMinimumWidthRect( Bitmap fontImage )
    {
      // set max width to start
      int scancolstart = 0;
      int scancolend = fontImage.Width - 1;
      // minimum width
      scancolend = 0; // init to track
      for ( int x = 0; x < fontImage.Width; x++ ) {
        // test the scancol for blank columns
        Boolean blankcol = true;
        for ( int y = 0; y < fontImage.Height; y++ ) {
          blankcol &= ( fontImage.GetPixel( x, y ).R < BLANKTX ); // compare to threshold
        }

        // determine minimum width on each scanrow
        if ( blankcol && ( scancolend == 0 ) ) {
          scancolstart = x; // move over left blank rows
        }
        if ( !blankcol ) {
          // not blank is moving the endcol and disabling the startcol move
          scancolend = x;
        }
      }
      // scancolstart is the last empty col - i.e. take the next one 
      scancolstart++;

      if ( scancolend == 0 ) {
        // there was NO painted col i.e. an empty bitmap
        scancolstart = fontImage.Width / 2; // return a vertical col in the middle of the map
        scancolend = scancolstart; 
      }
      return new Rectangle( scancolstart, ScanlineStart, scancolend - scancolstart + 1, FinalHeight );
    }


    /// <summary>
    /// Returns Y and Height of the painted area as rectangle
    /// </summary>
    /// <param name="fontImage">The bitmap of the image to identify</param>
    /// <returns>The minimum rectangle for Y and Height (X and Width are bitmap dimensions</returns>
    private Rectangle GetMinimumHeightRect( Bitmap fontImage )
    {
      // set max height to start
      int scanlinestart = 0;
      int scanlineend = 0;

      for ( int y = 0; y < fontImage.Height; y++ ) {
        // test the scanline for blank rows
        Boolean blankline = true;
        for ( int x = 0; x < fontImage.Width; x++ ) {
          blankline &= ( fontImage.GetPixel( x, y ).R < BLANKTX ); // compare to threshold
        }

        if ( blankline && ( scanlineend == 0 ) ) {
          scanlinestart = y; // move over top blank lines while there was no filled line detected
        }
        if ( !blankline ) {
          // not blank is setting and moving the endline
          scanlineend = y;
        }
      }
      // scanlinestart is the last empty row - i.e. take the next one 
      scanlinestart++;

      if ( scanlineend == 0 ) {
        // there was NO painted row i.e. an empty bitmap
        scanlinestart = fontImage.Height / 2; // return a horizontal row in the middle of the map
        scanlineend = scanlinestart;
      }
      // width defaults to the full width as we don't know better here..
      return new Rectangle( 0, scanlinestart, fontImage.Width, scanlineend - scanlinestart + 1 );
    }


    /// <summary>
    /// Returns the painted rectangle for this character
    /// </summary>
    /// <param name="fontImage">The bitmap of the image to identify</param>
    /// <returns>The minimum rectangle</returns>
    private Rectangle GetMinimumRect( Bitmap fontImage )
    {
      Rectangle hRect = GetMinimumHeightRect(fontImage);
      Rectangle wRect = GetMinimumWidthRect(fontImage);

      return new Rectangle( wRect.X, hRect.Y, wRect.Width, hRect.Height ); // combine the two
    }


    // determine the scanstart and end of a string with the given font
    // returns height deviation from target (pos = to big; neg = to small)
    private int TryFontHeight( Font font, String s, int targetHeight )
    {

      Bitmap fontImage = GetStringBitmap( font, s );

      Rectangle hRect = GetMinimumHeightRect(fontImage);

      // backup according to setting
      int startline = hRect.Y;
      int endline = startline + hRect.Height - 1;
      if ( !RemoveTop )
        startline = 0;
      if ( !RemoveBottom )
        endline = fontImage.Height - 1;
      fontImage.Dispose( ); // done with the bitmap

      // check outcome - assuming a incremental scan only...
      int height = endline - startline + 1;
      if ( height >= targetHeight ) {
        // save globals
        ScanlineStart = startline;
        ScanlineEnd = endline;
        FontToUse = new Font( font, font.Style );
        FinalHeight = height;
      }
      return ( height - targetHeight );
    }


    /// <summary>
    /// Determines the fontsize to use to achieve the target height 
    ///   for the char set given (FirstChar, CharCount)
    ///   respecting the Optimization flags (RemoveTop, RemoveBottom)
    /// Determines the minimum character width needed to fit all
    /// Stores font and scanline information for further use
    /// </summary>
    /// <returns>The final height delta achieved</returns>
    public int Optimize( )
    {
      int retVal = 0;

      // create string
      String testString = "";
      int lastChar = Convert.ToChar( Convert.ToByte(FirstChar) + CharCount -  1);
      for ( Char c = FirstChar; c <= lastChar; c++ ) {
        testString += c;
      }

      // scan for the first size that meets the conditions (just increment the font and test)
      float fSize = TargetHeight/2; // start with something which is too small due to linespaceing
      Font tryFont = NewFont(FontToUse, fSize );
      retVal = TryFontHeight( tryFont, testString, TargetHeight );
      while ( retVal < 0 ) {
        // approach the target with finer steps...
        if ( retVal == -1F ) 
          fSize += 0.125F; // min steps in fonts (I think)
        else if ( retVal == -2F )
          fSize += 0.125F;
        else if ( retVal == -3F )
          fSize += 0.25F;
        else if ( retVal == -4F )
          fSize += 0.5F;
        else
          fSize += 2.0F;
        //Debug.Print( "Optimize to {0} retVal: {1} newSize {2}\n", TargetHeight, retVal, fSize );
        tryFont = NewFont( FontToUse, fSize );
        retVal = TryFontHeight( tryFont, testString, TargetHeight );
      }
      tryFont.Dispose( );

      // now we have the font to use i.e. the height match
      // establish charwidth information
      Rectangle tmpRect = new Rectangle( 1000, ScanlineStart, 1, FinalHeight ); // init small to grow later..
      for ( Char c = FirstChar; c <= lastChar; c++ ) {
        Bitmap fontImage = GetStringBitmap( FontToUse, Convert.ToString(c) ); // raw image
        if ( tmpRect.X == 1000 )
          tmpRect.X = fontImage.Width / 2; // clutch to start in the middle of the rect with Unions
        Rectangle wRect = GetMinimumWidthRect(fontImage); // determine the horizontal bounds (vertical ones are from global)
        fontImage.Dispose( );

        tmpRect = Rectangle.Union( tmpRect, wRect ); // get outer bounds of the two
      }
      MinimumRect = tmpRect; // make global

      return retVal;
    }



    /// <summary>
    /// Save a PNG thumbnail using the global settings into the file with given name (add png)
    /// </summary>
    /// <param name="fName">Filepath/name to save</param>
    public void MakeThumbnail( String fName )
    {
      // create string
      String testString = "";
      int lastChar = Convert.ToChar( Convert.ToByte(FirstChar) + CharCount -  1);
      for ( Char c = FirstChar; c <= lastChar; c++ ) {
        testString += c;
      }

      Bitmap fontImage = GetStringBitmap( FontToUse, testString );
      fontImage.Save( fName + ".png", ImageFormat.Png );
      fontImage.Dispose( );
    }


    // 
    /// <summary>
    /// Returns a bitmap for this character with current height applied 
    /// </summary>
    /// <param name="c">The character to paint</param>
    /// <param name="target">The target width to apply</param>
    /// <returns>A Red on Black bitmap of the character</returns>
    public Bitmap GetMapForChar( Char c, WidthTarget target )
    {
      // if target width == 0 get minimum width

      Bitmap fontImage = GetStringBitmap( FontToUse, Convert.ToString(c) );

      // init for WT_Mono
      Rectangle cutRect = MinimumRect;
      if ( target == WidthTarget.WT_None ) {
        cutRect.X = 0; cutRect.Width = fontImage.Width; // full width
      }
      else if ( target == WidthTarget.WT_Minimum ) {
        if ( c == ' ' ) {
          // special treatment for blank in minimize - it contains no points to detect the width...
          cutRect.X = 0; cutRect.Width = fontImage.Width; // use WT_None and provide the original width
        }
        else {
          // for the minimum target we need to find it first
          Rectangle wRect = GetMinimumWidthRect(fontImage);
          cutRect.X = wRect.X; cutRect.Width = wRect.Width;
        }
      }

      // this fail for chars that are smaller than the MinimumSize (Mono) 
      //  i.e. cut is out of Width bounds of the fontImage 
      try {
        Bitmap nBT = fontImage.Clone( cutRect, fontImage.PixelFormat ); // return cutout
        fontImage.Dispose( );
        return nBT;

      } catch {
        // cut is out of Width bounds of the fontImage
        Bitmap nBT = new Bitmap(cutRect.Width, cutRect.Height );// blow up to expected monosize
        Graphics g = Graphics.FromImage(nBT);
          g.Clear( Color.Black );
          g.DrawImage( fontImage, 0, 0 ); // paint from existing to new 
          g.Flush( System.Drawing.Drawing2D.FlushIntention.Sync );
        g.Dispose( );
        return nBT; 
      }
    }



  }
}
