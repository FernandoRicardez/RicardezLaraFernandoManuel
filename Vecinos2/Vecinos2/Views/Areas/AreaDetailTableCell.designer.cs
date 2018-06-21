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
	[Register ("AreaDetailTableCell")]
	partial class AreaDetailTableCell
	{
		[Outlet]
		UIKit.UILabel LblReservationDate { get; set; }

		[Outlet]
		UIKit.UILabel LblReservationHolder { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (LblReservationDate != null) {
				LblReservationDate.Dispose ();
				LblReservationDate = null;
			}

			if (LblReservationHolder != null) {
				LblReservationHolder.Dispose ();
				LblReservationHolder = null;
			}
		}
	}
}
