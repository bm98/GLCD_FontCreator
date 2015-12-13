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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GLCD_FontCreator.GDI
{
  class gdiDrawing
  {
/*** CURENTLY NOT USED ***/
    public static void RenderText( IDeviceContext hdc, string text, string fontFamily, Color color, Rectangle region, float size )
    {
      // create the handle of DC
      var h = new HandleRef (null, hdc.GetHdc ());
      // create the font
      int emsize = - gdiNative.MulDiv ((int)size, gdiNative.GetDeviceCaps (h, gdiNative.LOGPIXELSY), 72);

      var p = new HandleRef (null, gdiNative.CreateFont (emsize, 0, 0, 0, 0, 0, 0, 0, 1/*Ansi_encoding*/, 0, 0, 4, 0, fontFamily));
      try {
        // use the font in the DC
        gdiNative.SelectObject( h, p.Handle );
        // set the background to transparent
        gdiNative.SetBkMode( h, 1 );
        // set the color of the text
        gdiNative.SetTextColor( h, color );
        // draw the text to the region
        gdiNative.DrawText( h, text, region, 0x0100 );
      }
      finally {
        // release the resources
        gdiNative.DeleteObject( p );
        hdc.ReleaseHdc( );
      }
    }

    

  }
}
