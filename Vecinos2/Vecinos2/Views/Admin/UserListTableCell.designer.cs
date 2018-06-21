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
	[Register ("UserListTableCell")]
	partial class UserListTableCell
	{
		[Outlet]
		UIKit.UILabel LblAddress { get; set; }

		[Outlet]
		UIKit.UILabel LblMail { get; set; }

		[Outlet]
		UIKit.UILabel LblName { get; set; }

		[Outlet]
		UIKit.UILabel LblPhone { get; set; }

		[Outlet]
		UIKit.UILabel LblStatus { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (LblMail != null) {
				LblMail.Dispose ();
				LblMail = null;
			}

			if (LblName != null) {
				LblName.Dispose ();
				LblName = null;
			}

			if (LblPhone != null) {
				LblPhone.Dispose ();
				LblPhone = null;
			}

			if (LblStatus != null) {
				LblStatus.Dispose ();
				LblStatus = null;
			}

			if (LblAddress != null) {
				LblAddress.Dispose ();
				LblAddress = null;
			}
		}
	}
}
