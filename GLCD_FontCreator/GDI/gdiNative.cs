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
  static class gdiNative
  {

    public const int WM_FONTCHANGE = 0x001D;
    public const int HWND_BROADCAST = 0xffff;
    public const int LOGPIXELSX = 88;
    public const int LOGPIXELSY = 90;

    const string GDI32 = "gdi32.dll";
    const string USER32 = "user32.dll";
    const string KERNEL32 = "kernel32.dll";

    struct Rect
    {
      public long Left, Top, Right, Bottom;
      public Rect( Rectangle rect )
      {
        this.Left = rect.Left;
        this.Top = rect.Top;
        this.Right = rect.Right;
        this.Bottom = rect.Bottom;
      }
    }

    static uint ToGdiColor( Color color )
    {
      return ( uint )( color.R | color.G << 8 | color.B << 16 );
    }

    [DllImport( GDI32 )]
    internal static extern IntPtr CreateFont(
      int nHeight,
      int nWidth,
      int nEscapement,
      int nOrientation,
      int fnWeight,
      uint fdwItalic,
      uint fdwUnderline,
      uint fdwStrikeOut,
      uint fdwCharSet,
      uint fdwOutputPrecision,
      uint fdwClipPrecision,
      uint fdwQuality,
      uint fdwPitchAndFamily,
      string lpszFace
      );

    [DllImport( GDI32 )]
    internal static extern int AddFontResource( string lpszFilename );

    [DllImport( GDI32 )]
    internal static extern bool RemoveFontResource(string lpFileName  );


    [DllImport( GDI32 )]
    internal static extern int GetDeviceCaps( HandleRef hdc, int nIndex );
  

    [DllImport( GDI32 )]
    internal static extern IntPtr SelectObject( HandleRef hdc, IntPtr obj );

    [DllImport( GDI32 )]
    internal static extern bool DeleteObject( HandleRef obj );

    [DllImport( USER32, CharSet = CharSet.Auto )]
    static extern int DrawText( HandleRef hDC, string lpchText, int nCount, ref Rect lpRect, uint uFormat );
    internal static int DrawText( HandleRef hDC, string text, Rectangle rect, uint format )
    {
      var r = new Rect (rect);
      return DrawText( hDC, text, text.Length, ref r, format );
    }

    [DllImport( GDI32 )]
    static extern uint SetTextColor( HandleRef hdc, uint crColor );
    internal static uint SetTextColor( HandleRef hdc, Color color )
    {
      return SetTextColor( hdc, ToGdiColor( color ) );
    }

    [DllImport( GDI32 )]
    internal static extern uint SetBkMode( HandleRef hdc, int mode );

    [DllImport( USER32 )]
    public static extern IntPtr SendMessage( IntPtr hWnd, uint Msg, uint wParam, uint lParam );


    [DllImport( KERNEL32 )]
    internal static extern int MulDiv( int nNumber, int nNumerator, int nDenominator );



  }
}
