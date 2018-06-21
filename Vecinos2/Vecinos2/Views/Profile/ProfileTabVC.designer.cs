// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Vecinos2.Views.Profile
{
	[Register ("ProfileTabVC")]
	partial class ProfileTabVC
	{
		[Outlet]
		UIKit.UIBarButtonItem BtnEdit { get; set; }

		[Outlet]
		UIKit.UIImageView ImgProfilePicture { get; set; }

		[Outlet]
		UIKit.UILabel LblChange { get; set; }

		[Outlet]
		UIKit.UITextField TxtAddress { get; set; }

		[Outlet]
		UIKit.UITextField TxtName { get; set; }

		[Outlet]
		UIKit.UITextField TxtPhone { get; set; }

		[Outlet]
		UIKit.UIView ViewProfilePicture { get; set; }

		[Action ("btnLogOut:")]
		partial void btnLogOut (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (BtnEdit != null) {
				BtnEdit.Dispose ();
				BtnEdit = null;
			}

			if (ImgProfilePicture != null) {
				ImgProfilePicture.Dispose ();
				ImgProfilePicture = null;
			}

			if (LblChange != null) {
				LblChange.Dispose ();
				LblChange = null;
			}

			if (TxtAddress != null) {
				TxtAddress.Dispose ();
				TxtAddress = null;
			}

			if (TxtName != null) {
				TxtName.Dispose ();
				TxtName = null;
			}

			if (TxtPhone != null) {
				TxtPhone.Dispose ();
				TxtPhone = null;
			}

			if (ViewProfilePicture != null) {
				ViewProfilePicture.Dispose ();
				ViewProfilePicture = null;
			}
		}
	}
}
