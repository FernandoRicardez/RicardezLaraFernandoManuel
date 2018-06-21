// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Vecinos2.Views
{
	[Register ("LoginVC")]
	partial class LoginVC
	{
		[Outlet]
		UIKit.UITextField TxtMail { get; set; }

		[Outlet]
		UIKit.UITextField TxtPassword { get; set; }

		[Action ("btnSignIn_TouchUpInside:")]
		partial void btnSignIn_TouchUpInside (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (TxtMail != null) {
				TxtMail.Dispose ();
				TxtMail = null;
			}

			if (TxtPassword != null) {
				TxtPassword.Dispose ();
				TxtPassword = null;
			}
		}
	}
}
