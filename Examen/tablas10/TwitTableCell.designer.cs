// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace tablas10
{
    [Register ("TwitTableCell")]
    partial class TwitTableCell
    {
        [Outlet]
        UIKit.UIImageView imgUserProfileImage { get; set; }


        [Outlet]
        UIKit.UILabel lblTweetText { get; set; }


        [Outlet]
        UIKit.UILabel lblUserScreenName { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (imgUserProfileImage != null) {
                imgUserProfileImage.Dispose ();
                imgUserProfileImage = null;
            }

            if (lblTweetText != null) {
                lblTweetText.Dispose ();
                lblTweetText = null;
            }

            if (lblUserScreenName != null) {
                lblUserScreenName.Dispose ();
                lblUserScreenName = null;
            }
        }
    }
}