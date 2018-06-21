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
	[Register ("RegisterVC")]
	partial class RegisterVC
	{
		[Outlet]
		UIKit.UITextField TxtAddress { get; set; }

		[Outlet]
		UIKit.UITextField TxtMail { get; set; }

		[Outlet]
		UIKit.UITextField TxtName { get; set; }

		[Outlet]
		UIKit.UITextField TxtPassword { get; set; }

		[Outlet]
		UIKit.UITextField TxtPasswordConfirmation { get; set; }

		[Outlet]
		UIKit.UITextField TxtPhone { get; set; }

		[Action ("BtnCancel_TouchUpInside:")]
		partial void BtnCancel_TouchUpInside (Foundation.NSObject sender);

		      
		[Action ("BtnRegister_TouchUpInside:")]
		partial void BtnRegister_TouchUpInsideAsync (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (TxtName != null) {
				TxtName.Dispose ();
				TxtName = null;
			}

			if (TxtMail != null) {
				TxtMail.Dispose ();
				TxtMail = null;
			}

			if (TxtPassword != null) {
				TxtPassword.Dispose ();
				TxtPassword = null;
			}

			if (TxtPasswordConfirmation != null) {
				TxtPasswordConfirmation.Dispose ();
				TxtPasswordConfirmation = null;
			}

			if (TxtAddress != null) {
				TxtAddress.Dispose ();
				TxtAddress = null;
			}

			if (TxtPhone != null) {
				TxtPhone.Dispose ();
				TxtPhone = null;
			}
		}
	}
}
