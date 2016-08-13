// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace ParallaxPageView
{
    [Register ("DataViewController")]
    partial class DataViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel dataLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (dataLabel != null) {
                dataLabel.Dispose ();
                dataLabel = null;
            }
        }
    }
}