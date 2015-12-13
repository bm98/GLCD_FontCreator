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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLCD_FontCreator.FontCreators
{
  /// <summary>
  /// A Font Creator for a specific font file format
  ///  - GLCD with 'buggy' shift bits
  ///  - Compatible with popular FontCreator2.0 rsp. 2.1 from F. Maximilian Thiele
  /// </summary>
  class GLCD_FC2_Compatible : FCBase
  {

    #region Infrastructure

    private const String c_fcName = "GLCD_FC2_compatible";
    /// <summary>
    /// Returns the name of the instance
    /// </summary>
    static public new String FontCreatorName { get { return c_fcName; } }
    /// <summary>
    /// Returns a new instance of this implementation
    /// </summary>
    /// <returns>A new specific FontCreator instance</returns>
    static public new FCBase GetInstance( FontOptimizer fo )
    {
      return new GLCD_FC2_Compatible( fo );
    }

    #endregion


    #region Letter class - Implements the font file specific Letter creator


    /// <summary>
    /// Allows the receiver to create Letter instances 
    /// </summary>
    /// <returns>A new instance of Letter</returns>
    public override LetterBase LetterFactory( )
    {
      return new Letter( );
    }

    /// <summary>
    /// Provides the bitmap scan and code output for one letter
    /// </summary>
    public new class Letter : LetterBase
    {
      private int  m_bytesPerCol = 1;


      /// <summary>
      /// Character Digitizer
      ///   Creates the Byte stream for this character
      /// </summary>
      /// <param name="c">Character to digitize</param>
      /// <param name="fo">The FontOptimzer serving the bitmaps</param>
      /// <param name="target">Width target setting</param>
      /// <returns>The size of the created character</returns>
      public override Size CreateChar( Char c, FontOptimizer fo, FontOptimizer.WidthTarget target )
      {
        m_c = c;

        Bitmap fontImage = fo.GetMapForChar(c, target);

        // GET and ADD BITS...
        // from Top to bottom - then from left to right
        m_bytesPerCol = ( fontImage.Height + 7 ) / 8; // get height in bytes

        for ( int b = 0; b < m_bytesPerCol; b++ ) {
          // per byte row
          for ( int x = 0; x < fontImage.Width; x++ ) {
            // left -> right

            Byte col = 0;
            Byte bit = 1; // paint bit moves from LSB to MSB in the y -loop

            for ( int y = b * 8; y < ( b + 1 ) * 8; y++ ) {
              if ( y < fontImage.Height ) {
                // top -> down ??
                Color pix = fontImage.GetPixel(x,y);
                if ( pix.R > FontOptimizer.BLANKTX ) // red on black painting
                  col |= bit;
                bit <<= 1;
              }
              else {
                // catch overrun - but shift in (supposed bug in original must be retained...)
                col <<= 1; // shift in
              }
            } // for y
            this.Add( col );
          }// for x
        }// for b

        Size nS = fontImage.Size;
        fontImage.Dispose( );
        return nS;
      }

      /// <summary>
      /// Returns the formatted HexByte String representation of this character
      ///   Some pretty printing is applied
      /// </summary>
      /// <returns>A String of Hex Chars representing this character</returns>
      public override String GetBytes( )
      {
        String ret = "";

        int perLine = 0; int totalOut = 0;
        int lItems = this.Count / m_bytesPerCol;
        foreach ( Byte b in this ) {
          if ( ( perLine % lItems ) == 0 ) ret += String.Format( "\t" ); // is start of line
          ret += String.Format( "{0}", hex( b ) );
          perLine++; totalOut++;
          // start a line if needed
          if ( ( perLine % lItems ) == 0 ) {
            if ( totalOut < this.Count ) ret += String.Format( " \n" ); // NL only if not last line
            perLine = 0;
          }
        }
        ret += String.Format( "  // char ({0,3:D3}) '{1}'\n\n", Convert.ToByte( m_c ), m_c ); // describe this fragment and NL

        return ret;
      }
    }// class Letter

    #endregion

    /// <summary>
    /// cTor: Accepts a FontOptimizer fully setup to serve bitmaps of the selected font and size
    /// </summary>
    /// <param name="fo">The initialized FontOptimizer</param>
    public GLCD_FC2_Compatible( FontOptimizer fo ) : base( fo ) { }


    /// <summary>
    /// Creates a complete font file and returns it as String
    /// </summary>
    /// <param name="firstChar">The character to start with</param>
    /// <param name="charCount">The number of characters to include by inc. the Chars Ordinal (ASCII 7bit only)</param>
    /// <param name="widthTarget">Width target setting</param>
    /// <returns>The created font file as String</returns>
    public override String FontFile( Char firstChar, int charCount, FontOptimizer.WidthTarget widthTarget )
    {
      String ret = "";

      // store args
      FirstChar = firstChar;
      CharCount = charCount;

      m_letters = new Letters( LetterFactory ); // create font machine.. and submit the factory
      // create string
      int lastChar = Convert.ToChar( Convert.ToByte(FirstChar) + CharCount -  1);
      for ( Char c = FirstChar; c <= lastChar; c++ ) {

        Size s = m_letters.Add( c, m_fo, widthTarget ); // --> this will essentially capture all characters bits via Letter instance

        if ( m_firstSize.Height == 0 ) m_firstSize = s; // collect size information - init in 1st round

        Monospace = Monospace & ( s == m_firstSize ); // gets false if sizes are different
      }
      // final size from collected bit patterns
      Width = m_letters.MaxSize.Width;
      Height = m_letters.MaxSize.Height;

      UInt16 totalSize = 6; // descriptor code size 
      if ( !Monospace ) totalSize += ( UInt16 )m_letters.SizeTable.Count; // fixed fonts don't have a size table
      totalSize += ( UInt16 )m_letters.CodeSize( );
      CodeSize = totalSize;

      // start output
      FontNameCreated = ModName( );

      // create the file (collect all in one String)
      ret += Header( );
      // descriptor
      ret += Descriptor( );

      // code portion
      ret += CodeStart( totalSize );
      if ( !Monospace ) ret += m_letters.SizeTable.GetBytes( ); // fixed fonts don't have a size table

      // all font code now
      ret += m_letters.GetBytes( );
      // remove last comma from hex bytes
      int lc = ret.LastIndexOf(',');
      if ( lc != -1 ) ret = ret.Remove( lc, 1 );

      // write the code trailer
      ret += Trailer( );

      return ret; // Done
    }


    /// <summary>
    /// Code snips containing descriptors
    ///   e.g. #include and #define
    /// </summary>
    /// <returns>A string containing the needed items</returns>
    protected override String Descriptor( )
    {
      /*

      #include <inttypes.h>
      #include <avr/pgmspace.h>

      #ifndef NEW_FONT_H
      #define NEW_FONT_H

      #define NEW_FONT_WIDTH 12
      #define NEW_FONT_HEIGHT 18

      */
      String ret = "";
      String modName = Name.ToUpper( ).Replace( " ", "_" );
      ret += String.Format( "#include <inttypes.h>\n" );
      ret += String.Format( "#include <avr/pgmspace.h>\n" );
      ret += String.Format( "\n" );
      ret += String.Format( "#ifndef _{0}_H_\n", modName );
      ret += String.Format( "#define _{0}_H_\n", modName );
      ret += String.Format( "\n" );
      ret += String.Format( "#define {0}_WIDTH  {1,-3}\n", modName, Width );
      ret += String.Format( "#define {0}_HEIGHT {1,-3}\n", modName, Height );
      ret += String.Format( "\n" );

      return ret;
    }


    /// <summary>
    /// The code start section 
    ///   e.g. keywords and identifiers uses to wrap the const bytes
    /// </summary>
    /// <param name="totalSize"></param>
    /// <returns>A string containing the needed items</returns>
    protected override String CodeStart( UInt16 totalSize )
    {
      /*

      static uint8_t new_Font[] PROGMEM = {
          0x03, 0xF0, // size
          0x0C, // width
          0x12, // height
          0x20, // first char
          0x21, // char count

      */
      String ret = "";
      // this is AVR (Arduino) style to creae fonts in prog. memory
      ret += String.Format( "static const uint8_t {0}[] PROGMEM = {{\n", FontNameCreated ); // Escape { to print it
      if ( Monospace )
        ret += String.Format( "\t{0} // size is 0 - Monospace font\n", hex( ( UInt16 )0 ) ); // mono needs size = 0
      else
        ret += String.Format( "\t{0} // size\n", hex( totalSize ) );

      ret += String.Format( "\t{0} // width\n", hex( ( Byte )Width ) );
      ret += String.Format( "\t{0} // height\n", hex( ( Byte )Height ) );
      ret += String.Format( "\t{0} // first char\n", hex( ( Byte )FirstChar ) );
      ret += String.Format( "\t{0} // char count\n", hex( ( Byte )CharCount ) );

      ret += String.Format( "\n" );

      return ret;
    }


    /// <summary>
    /// Code snips containing comment header of the file
    ///   all descriptive comment used to provide some human readable information in the font file 
    /// </summary>
    /// <returns>A string containing the needed items</returns>
    protected override String Header( )
    {
      // create the header here
      String ret = "";
      Char lastChar = Convert.ToChar( Convert.ToByte(m_fo.FirstChar) + CharCount - 1);

      // write some header
      ret += String.Format( "//\n" );
      ret += String.Format( "// Created by {1} on {0}\n", DateTime.Now.ToShortDateString( ), FontCreatorName );
      ret += String.Format( "//\n" );
      ret += String.Format( "//   Font Name:  {0}\n", FontNameCreated );
      ret += String.Format( "//   Orig. Name: {0}\n", Name );
      ret += String.Format( "//\n" );
      ret += String.Format( "//   Start Char: {0,3:D3} '{1}'\n", Convert.ToByte( FirstChar ), FirstChar );
      ret += String.Format( "//   End Char:   {0,3:D3} '{1}'\n", Convert.ToByte( lastChar ), lastChar );
      ret += String.Format( "//   # Chars:    {0}\n", CharCount );
      ret += String.Format( "//\n" );
      ret += String.Format( "//   Height:     {0}\n", Height );
      ret += String.Format( "//   Width:      {0}\n", Width );
      ret += String.Format( "//\n" );
      ret += String.Format( "//   Monospace:  {0}\n", Monospace.ToString( ) );
      ret += String.Format( "//   Bold:       {0}\n", m_fo.FontToUse.Bold.ToString( ) );
      ret += String.Format( "//   Italic:     {0}\n", m_fo.FontToUse.Italic.ToString( ) );
      ret += String.Format( "//   Underline:  {0}\n", m_fo.FontToUse.Underline.ToString( ) );
      ret += String.Format( "//   Strikeout:  {0}\n", m_fo.FontToUse.Strikeout.ToString( ) );
      ret += String.Format( "//\n" );
      ret += String.Format( "//   Codesize:   {0}\n", CodeSize );
      ret += String.Format( "//\n" );

      return ret;
    }


    /// <summary>
    /// Code snips containing the trailer of the file
    ///  e.g. closing brackets, #endif for the defines started in the Descriptor
    /// </summary>
    /// <returns>A string containing the needed items</returns>
    protected override String Trailer( )
    {
      String ret = "";

      // write some footer
      ret += String.Format( "\n" );
      ret += String.Format( "}};\n" ); // Escape } to print it
      ret += String.Format( "\n" );
      ret += String.Format( "#endif\n" );
      ret += String.Format( "\n" );

      return ret;
    }




  }
}
