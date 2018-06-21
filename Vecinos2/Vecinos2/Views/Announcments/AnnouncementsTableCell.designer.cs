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
	[Register ("AnnouncementsTableCell")]
	partial class AnnouncementsTableCell
	{
		[Outlet]
		UIKit.UILabel lblAnnouncementDate { get; set; }

		[Outlet]
		UIKit.UILabel lblAnnouncementDescription { get; set; }

		[Outlet]
		UIKit.UILabel lblAnnouncementTitle { get; set; }

		[Outlet]
		UIKit.UILabel lblAnnouncementUser { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (lblAnnouncementTitle != null) {
				lblAnnouncementTitle.Dispose ();
				lblAnnouncementTitle = null;
			}

			if (lblAnnouncementDescription != null) {
				lblAnnouncementDescription.Dispose ();
				lblAnnouncementDescription = null;
			}

			if (lblAnnouncementUser != null) {
				lblAnnouncementUser.Dispose ();
				lblAnnouncementUser = null;
			}

			if (lblAnnouncementDate != null) {
				lblAnnouncementDate.Dispose ();
				lblAnnouncementDate = null;
			}
		}
	}
}
