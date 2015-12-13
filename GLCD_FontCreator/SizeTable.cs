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

namespace GLCD_FontCreator
{
  class SizeTable : List<Byte>
  {

    public String GetBytes( )
    {
      String ret = "";
      ret += String.Format( "\n" );
      ret += String.Format( "\t// char widths\n" );

      int perLine = 0;
      foreach ( Byte b in this ) {
        if ( ( perLine % 16 ) == 0 ) ret += String.Format( "\t" ); // start of line
        ret += String.Format( "{0}", hex( b ) );
        perLine++;
        if ( ( perLine % 16 ) == 0 ) {
          if ( perLine < this.Count ) ret += String.Format( " \n" );
          perLine = 0;
        }
      }
      ret += String.Format( "\n" );

      return ret;
    }

    private String hex( Byte b )
    {
      return String.Format( "0x{0,2:x2}, ", b );
    }



  }
}
