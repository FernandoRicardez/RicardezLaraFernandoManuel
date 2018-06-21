using System;
using System.Globalization;
using Foundation;
using UIKit;
using Vecinos2.Controllers;
using Vecinos2.Models;



namespace Vecinos2.Views.Announcments
{
    public partial class NewAnnouncementVC : UIViewController
    {

        AnnouncmentsController announcmentsController;
        //AnnouncementModel announcement;
        UIDatePicker datePicker; 

		public NewAnnouncementVC(IntPtr handle) : base(handle)
        {
        }
      
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            announcmentsController = new AnnouncmentsController();
            datePicker = new UIDatePicker();
            CreateDatePicker(); 
        }
        
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

		partial void BtnPostAnnouncement_TouchUpInside(NSObject sender)
		{
            if(ValidateFields())
            {
                if(DateFormat(TxtDatePicker.Text) == "")
                {
                    ShowAlert("Error", "El formato de fecha no es correcto", null);
                }
                else if(CheckAnnouncement(TxtDatePicker.Text) < 0)
                {
                    ShowAlert("Error", "Debe elegir una fecha de hoy en adelante", null);
                }
                else
                {
                    var insert = announcmentsController.InsertAnnouncement(
                    new AnnouncementModel(
                        TxtAnnouncementTitle.Text, //Title
                        TxtAnnouncementDesription.Text, //Description
                        DateFormat(TxtDatePicker.Text), //Expiration
                        AppDelegate.CurrentUser.Name, //User
                        DateTime.Now.ToString("yyyyMMddHHmmss"))); //DateNow()

                    if (insert)
                    {
                        TxtDatePicker.Enabled = true;
                        TxtAnnouncementTitle.Text = "";
                        TxtAnnouncementDesription.Text = "";
                        TxtDatePicker.Text = "";

                        ShowAlert("Confirmación", "Se realizó la publicación del aviso", null);
                    }
                    else
                        ShowAlert("Error", "Favor de llenar todos los campos", null);
                }

            }
            else
            {
                ShowAlert("Error", "Favor de llenar todos los campos", null);
            }


		}

        bool ValidateFields()
        {
            if (TxtAnnouncementTitle.Text != "" && TxtDatePicker.Text != "" && TxtAnnouncementDesription.Text != "")
                return true;
            else
                return false; 
        }

        int CheckAnnouncement(string date)
        {
            date = DateFormat(date);

            char[] array = date.ToCharArray();

            string year = array[0].ToString() + array[1].ToString() + array[2].ToString() + array[3].ToString();
            string month = array[4].ToString() + array[5].ToString();
            string day = array[6].ToString() + array[7].ToString();

            string theDate = $"{year}{month}{day}";

            string now = DateTime.Now.ToString("yyyyMMdd");

            //Console.WriteLine(theDate + " - " + now);

            return int.Parse(theDate) - int.Parse(now);

        }

        //datepicker event on textfield
        void CreateDatePicker()
        {
            datePicker.Mode = UIDatePickerMode.Date;
            datePicker.Locale = new NSLocale("es");
			datePicker.ValueChanged += (object sender, EventArgs e) => TxtDatePicker.Text = ((DateTime)datePicker.Date).ToString("D", new CultureInfo("es-ES"));
            var toolBar = new UIToolbar();
            toolBar.SizeToFit();

			var doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, SetDateToTextField);
            UIBarButtonItem[] barbuttonarr = { doneButton };
            toolBar.SetItems(barbuttonarr, false);

            TxtDatePicker.InputAccessoryView = toolBar;
            TxtDatePicker.InputView = datePicker; 

        }

        string DateFormat(string date)
        {
            try
            {
                DateTime myDate = DateTime.ParseExact(date, "D", new CultureInfo("es-ES"));
                var result = myDate.ToString("yyyyMMddHHmmss", new CultureInfo("es-ES"));
                return result; 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ""; 
            }
        }

        void SetDateToTextField(object sender, EventArgs e)
        {
            //Console.WriteLine(((DateTime)datePicker.Date).ToString("yyyyMMddHHmmss"));
            TxtDatePicker.Text = ((DateTime)datePicker.Date).ToString("D", new CultureInfo("es-ES"));
            //TxtDatePicker.Enabled = false;
            this.View.EndEditing(true);
        }

        void ShowAlert(string title, string message, Action func)
        {
            var okAlertController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
            okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

            InvokeOnMainThread(() => PresentViewController(okAlertController, true, func));

        }
	}
}

