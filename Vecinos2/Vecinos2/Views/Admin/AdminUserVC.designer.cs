// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Vecinos2
{
	[Register ("AdminUserVC")]
	partial class AdminUserVC
	{
		[Outlet]
		UIKit.UIButton BtnDisableAccount { get; set; }

		[Outlet]
		UIKit.UIBarButtonItem BtnEdit { get; set; }

		[Outlet]
		UIKit.UIButton BtnEnableAccount { get; set; }

		[Outlet]
		UIKit.UIImageView ImgProfilePicture { get; set; }

		[Outlet]
		UIKit.UILabel LblChange { get; set; }

		[Outlet]
		UIKit.UITextField TxtAddressAdmin { get; set; }

		[Outlet]
		UIKit.UITextField TxtNameAdmin { get; set; }

		[Outlet]
		UIKit.UITextField TxtPhoneAdmin { get; set; }

		[Outlet]
		UIKit.UIView ViewProfilePicture { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (TxtNameAdmin != null) {
				TxtNameAdmin.Dispose ();
				TxtNameAdmin = null;
			}

			if (TxtAddressAdmin != null) {
				TxtAddressAdmin.Dispose ();
				TxtAddressAdmin = null;
			}

			if (TxtPhoneAdmin != null) {
				TxtPhoneAdmin.Dispose ();
				TxtPhoneAdmin = null;
			}

			if (BtnDisableAccount != null) {
				BtnDisableAccount.Dispose ();
				BtnDisableAccount = null;
			}

			if (BtnEdit != null) {
				BtnEdit.Dispose ();
				BtnEdit = null;
			}

			if (BtnEnableAccount != null) {
				BtnEnableAccount.Dispose ();
				BtnEnableAccount = null;
			}

			if (ImgProfilePicture != null) {
				ImgProfilePicture.Dispose ();
				ImgProfilePicture = null;
			}

			if (LblChange != null) {
				LblChange.Dispose ();
				LblChange = null;
			}

			if (ViewProfilePicture != null) {
				ViewProfilePicture.Dispose ();
				ViewProfilePicture = null;
			}
		}
	}
}
