// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Vecinos2.Views.Announcments
{
	[Register ("NewAnnouncementVC")]
	partial class NewAnnouncementVC
	{
		[Outlet]
		UIKit.UITextField TxtAnnouncementDesription { get; set; }

		[Outlet]
		UIKit.UITextField TxtAnnouncementTitle { get; set; }

		[Outlet]
		UIKit.UITextField TxtDatePicker { get; set; }

		[Action ("BtnPostAnnouncement_TouchUpInside:")]
		partial void BtnPostAnnouncement_TouchUpInside (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (TxtAnnouncementDesription != null) {
				TxtAnnouncementDesription.Dispose ();
				TxtAnnouncementDesription = null;
			}

			if (TxtAnnouncementTitle != null) {
				TxtAnnouncementTitle.Dispose ();
				TxtAnnouncementTitle = null;
			}

			if (TxtDatePicker != null) {
				TxtDatePicker.Dispose ();
				TxtDatePicker = null;
			}
		}
	}
}
