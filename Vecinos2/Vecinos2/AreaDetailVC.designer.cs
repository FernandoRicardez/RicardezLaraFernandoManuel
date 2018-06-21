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
	[Register ("AreaDetailVC")]
	partial class AreaDetailVC
	{
		[Outlet]
		UIKit.UIBarButtonItem BtnAddReservation { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (BtnAddReservation != null) {
				BtnAddReservation.Dispose ();
				BtnAddReservation = null;
			}
		}
	}
}
