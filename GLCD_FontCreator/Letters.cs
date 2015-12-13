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

namespace GLCD_FontCreator
{

  /// <summary>
  /// List of all letters
  /// </summary>
  class Letters : List<LetterBase>
  {
    private SizeTable m_sizeTable = new SizeTable();
    public SizeTable SizeTable { get { return m_sizeTable; } }

    private Func<LetterBase> m_letterFactory = null;

    /// <summary>
    /// The maximum Size used when merging all created characters
    /// </summary>
    public Size MaxSize = new Size(0,0);


    /// <summary>
    /// cTor: accepts the font specific Letter creator
    /// </summary>
    /// <param name="letter">A Letter Creator instance</param>
    public Letters( Func<LetterBase> letterFactory )
    {
      m_letterFactory = letterFactory;
    }


    /// <summary>
    /// Add one character to the List
    /// </summary>
    /// <param name="c">The character</param>
    /// <param name="fo">The FontOptimizer serving character bitmaps</param>
    /// <param name="target">Width target setting</param>
    /// <returns>The maxsize of all added characters</returns>
    public Size Add( Char c, FontOptimizer fo, FontOptimizer.WidthTarget target )
    {
      LetterBase l = m_letterFactory();
      Size s = l.CreateChar( c, fo, target );
      MaxSize.Width = Math.Max( MaxSize.Width, s.Width );
      MaxSize.Height = Math.Max( MaxSize.Height, s.Height );


      this.Add( l );
      m_sizeTable.Add( ( Byte )s.Width );

      return s;
    }


    /// <summary>
    /// Returns a String containing all Hex Bytes of all added characters
    ///   Some pretty printing is applied
    ///   NOTE: there is a comma after the last hexbyte caller must remove it if needed !!
    /// </summary>
    /// <returns>String containing all Hex Bytes of all added characters</returns>
    public String GetBytes( )
    {
      String ret = "";
      ret += String.Format( "\n" );
      ret += String.Format( "\t// font data\n" );
      foreach ( LetterBase l in this ) {
        ret += l.GetBytes( );
      }
      return ret;
    }

    /// <summary>
    /// Returns the total number of bytes used to represent all characters in the list
    /// </summary>
    /// <returns>Number of hex bytes created</returns>
    public int CodeSize( )
    {
      int cnt = 0;
      foreach ( LetterBase l in this ) {
        cnt += l.Count;
      }
      return cnt;
    }

  }// end class Letters



}
