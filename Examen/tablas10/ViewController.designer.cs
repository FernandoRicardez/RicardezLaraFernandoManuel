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
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        UIKit.UITableView TableView { get; set; }


        [Action ("btnAdd_Clicked:")]
        partial void btnAdd_Clicked (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
            if (TableView != null) {
                TableView.Dispose ();
                TableView = null;
            }
        }
    }
}