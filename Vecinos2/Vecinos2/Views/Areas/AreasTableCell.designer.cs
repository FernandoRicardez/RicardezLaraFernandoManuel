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
		UIKit.UILabel LblDescription { get; set; }

		[Outlet]
		UIKit.UILabel LblResevable { get; set; }

		[Outlet]
		UIKit.UILabel LblTitle { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ImgAreaImage != null) {
				ImgAreaImage.Dispose ();
				ImgAreaImage = null;
			}

			if (LblDescription != null) {
				LblDescription.Dispose ();
				LblDescription = null;
			}

			if (LblTitle != null) {
				LblTitle.Dispose ();
				LblTitle = null;
			}

			if (LblResevable != null) {
				LblResevable.Dispose ();
				LblResevable = null;
			}
		}
	}
}
