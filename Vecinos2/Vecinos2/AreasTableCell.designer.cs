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
	[Register ("AreasTableCell")]
	partial class AreasTableCell
	{
		[Outlet]
		UIKit.UIImageView ImgAreaImage { get; set; }

		[Outlet]
		UIKit.UILabel LblAreaDescription { get; set; }

		[Outlet]
		UIKit.UILabel LblAreaName { get; set; }

		[Outlet]
		UIKit.UIView ViewAreaCell { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (LblAreaName != null) {
				LblAreaName.Dispose ();
				LblAreaName = null;
			}

			if (LblAreaDescription != null) {
				LblAreaDescription.Dispose ();
				LblAreaDescription = null;
			}

			if (ImgAreaImage != null) {
				ImgAreaImage.Dispose ();
				ImgAreaImage = null;
			}

			if (ViewAreaCell != null) {
				ViewAreaCell.Dispose ();
				ViewAreaCell = null;
			}
		}
	}
}
