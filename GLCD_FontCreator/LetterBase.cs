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

namespace GLCD_FontCreator
{
  /// <summary>
  /// Abstract class that serves as base for a file format specific letter 'creator' class
  ///  - Represents the byte array of one letter
  /// </summary>
  abstract class LetterBase : List<Byte>
  {
    protected Char m_c = '\0';      // the letter handled


    /// <summary>
    /// Character Digitizer
    ///   Creates the Byte stream for this character
    /// </summary>
    /// <param name="c">Character to digitize</param>
    /// <param name="fo">The FontOptimzer serving the bitmaps</param>
    /// <param name="target">Width target setting</param>
    /// <returns>The size of the created character</returns>
    abstract public Size CreateChar( Char c, FontOptimizer fo, FontOptimizer.WidthTarget target );

    /// <summary>
    /// Returns the formatted HexByte String representation of this character
    ///   Some pretty printing is applied
    /// </summary>
    /// <returns>A String of Hex Chars representing this character</returns>
    abstract public String GetBytes( );


    protected String hex( Byte b )
    {
      return String.Format( "0x{0,2:x2}, ", b );
    }

    protected String hex( UInt16 w )
    {
      return hex( ( Byte )( w >> 8 ) ) + hex( ( Byte )( w ) ); // MSB first
    }


  }
}
