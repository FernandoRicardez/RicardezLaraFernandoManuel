// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;

namespace Vecinos2
{
	public partial class AreasTableCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString("AreasTableCell");

        public string AreaTitle
        {
            get { return LblTitle.Text; }
            set { LblTitle.Text = value; }
        }

        public string AreaDescription
		{
			get { return LblDescription.Text; }
			set { LblDescription.Text = value; }
		}

		public UIImage AreaImage
		{
			get { return ImgAreaImage.Image; }
			set { ImgAreaImage.Image = value; }
		}

        
      
		public bool isReservable 
		{ get; set;
		}

        public string ReservableLabel
		{
			get { return LblResevable.Text; }
			set { LblResevable.Text = value; }

		}
		
		public AreasTableCell (IntPtr handle) : base (handle)
		{
		}

	}
}