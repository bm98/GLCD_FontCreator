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
  /// An abstract class which provides the frame for a specific FontCreator implementation
  ///  - Different formats can be implemented deriving from this Base
  /// </summary>
  abstract class FCBase
  {
    #region Infrastructure (IMPLEMENT)

    /// <summary>
    /// Returns the name of the instance
    /// </summary>
    static public String FontCreatorName { get; }
    /// <summary>
    /// Returns a new instance of this implementation
    ///  - must be implemented and hide this one
    /// </summary>
    /// <returns>A new specific FontCreator instance</returns>
    static public FCBase GetInstance( FontOptimizer fo ) { return null; }

    #endregion


    #region Provided (DON'T TOUCH - at least not lighthearted ..)

    // Public attributes of this font - usually populated while processing
    public String Name { get; set; }
    public String FontNameCreated { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public Boolean Monospace { get; set; }
    public Char FirstChar { get; set; }
    public int CharCount { get; set; }
    public int CodeSize { get; set; }

    // locals
    protected FontOptimizer  m_fo;  // a font optimizer serving letter bitmaps
    protected Letters m_letters;    // the list of letters created
    protected Size m_firstSize = new Size(0,0);


    /// <summary>
    /// cTor: Accepts a FontOptimizer fully setup to serve bitmaps of the selected font and size
    /// </summary>
    /// <param name="fo">The initialized FontOptimizer</param>
    public FCBase( FontOptimizer fo )
    {
      m_fo = fo;
      // derive from font
      Name = m_fo.FontToUse.Name;
      FontNameCreated = Name;
      Width = ( int )( m_fo.FontToUse.SizeInPoints + 0.5F ); // preliminary ..
      Height = ( int )( m_fo.FinalHeight );
      Monospace = true; // init
    }


    /// <summary>
    /// Creates a code compatible name for the code section
    /// </summary>
    /// <returns>A C/C++ compatible identifier from the Font name</returns>
    protected String ModName( )
    {
      // fname([Xx]Y)[_b][_i]_SSS_EEE
      // Sans_Serif17x24_b_i_032_065  

      // clean some invalid code symbol chars blanks, dashes etc.
      String ret = Name.Replace(" ", "_");
      ret = ret.Replace( "-", "_" );
      ret = ret.Replace( "+", "_" );
      ret = ret.Replace( "%", "_" );
      ret = ret.Replace( "(", "_" );
      ret = ret.Replace( ")", "_" );
      ret = ret.Replace( ",", "_" );
      ret = ret.Replace( ";", "_" );


      if ( Monospace )
        ret += "x" + Width.ToString( );
      ret += Height.ToString( );
      if ( m_fo.FontToUse.Bold )
        ret += "_b";
      if ( m_fo.FontToUse.Italic )
        ret += "_i";
      ret += String.Format( "_{0,3:D3}", Convert.ToByte( m_fo.FirstChar ) );
      int lastCharNum = Convert.ToByte(m_fo.FirstChar) + CharCount - 1;
      ret += String.Format( "_{0,3:D3}", lastCharNum );

      return ret;
    }

    // formatting helpers
    protected String hex( Byte b )
    {
      return String.Format( "0x{0,2:x2}, ", b );
    }

    protected String hex( UInt16 w )
    {
      return hex( ( Byte )( w >> 8 ) ) + hex( ( Byte )( w ) );
    }

    #endregion


    #region Font Creator Specific  (IMPLEMENT)

    // *******************  Items to be implemented for a specific font file **************

    /// <summary>
    /// Allows the receiver to create Letter instances 
    /// </summary>
    /// <returns>A new instance of Letter</returns>
    abstract public LetterBase LetterFactory( );

    #region Letter class - Implements the font file specific Letter creator

    /// <summary>
    /// Provides the bitmap scan and code output for one letter
    /// </summary>
    abstract public class Letter { };

    #endregion

    /// <summary>
    /// Creates a complete font file and returns it as String
    /// </summary>
    /// <param name="firstChar">The character to start with</param>
    /// <param name="charCount">The number of characters to include by inc. the Chars Ordinal (ASCII 7bit only)</param>
    /// <param name="widthTarget">Width target setting</param>
    /// <returns>The created font file as String</returns>
    abstract public String FontFile( Char firstChar, int charCount, FontOptimizer.WidthTarget widthTarget );

    /*
    
    in general a font file is created by concatenating

    Header + Descriptor + CodeStart + CODE from Letters + Trailer

    */


    /// <summary>
    /// Code snips containing comment header of the file
    ///   all descriptive comment used to provide some human readable information in the font file 
    /// </summary>
    /// <returns>A string containing the needed items</returns>
    abstract protected String Header( );

    /// <summary>
    /// Code snips containing descriptors
    ///   e.g. #include and #define
    /// </summary>
    /// <returns>A string containing the needed items</returns>
    abstract protected String Descriptor( );

    /// <summary>
    /// The code start section 
    ///   e.g. keywords and identifiers uses to wrap the const bytes
    /// </summary>
    /// <param name="totalSize"></param>
    /// <returns>A string containing the needed items</returns>
    abstract protected String CodeStart( UInt16 totalSize );

    /// <summary>
    /// Code snips containing the trailer of the file
    ///  e.g. closing brackets, #endif for the defines started in the Descriptor
    /// </summary>
    /// <returns>A string containing the needed items</returns>
    abstract protected String Trailer( );

    #endregion


  }
}
