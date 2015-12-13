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
using System.Drawing;
using System.Text;

namespace GLCD_FontCreator.CustomFontDialog
{
  /// <summary>
  /// A custom collection for maintaining recently used lists of any kind. For example, recently used fonts, color etc.
  /// List with limited size which is given by MaxSize. As list grows beyond MaxSize, oldest item is removed.
  /// New items are added at the top of the list (at index 0), existing items move down.
  /// If added item is already there in the list, it is moved to the top (at index 0).
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class RecentlyUsedList : List<Font>
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="maxSize">As list grows longer than max size, oldest item will be dropped.</param>
    public RecentlyUsedList( int maxSize )
    {
      this.maxSize = maxSize;
    }

    private int maxSize;

    public int MaxSize
    {
      get
      {
        return maxSize;
      }
    }

    public bool Contains( String itemName )
    {
      foreach ( Font f in this ) {
        if ( f.Name == itemName )
          return true;
      }
      return false;
    }


    public void Update( Font item )
    {
      foreach ( Font f in this ) {
        if ( f.Name == item.Name )
          this[IndexOf( f )] = item;
      }
    }


    public new void Add( Font item )
    {
      int i = this.IndexOf(item);
      if ( i != -1 ) this.RemoveAt( i );

      if ( this.Count == MaxSize ) this.RemoveAt( this.Count - 1 );

      this.Insert( 0, item );
    }

  }
}
