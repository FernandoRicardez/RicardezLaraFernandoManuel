using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace tablas10
{
    public partial class ViewController : UIViewController, IUITableViewDataSource, IUITableViewDelegate
    {
        #region variables

        List<String> cosas = new List<string>();


        #endregion

        #region constructors
        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        #endregion

        #region lyfeCicle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            TableView.Delegate = this;
            TableView.DataSource = this;

          

        }

        #region tableViewDataSource

        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {
            var cell = tableView.DequeueReusableCell("TableCell", indexPath);
            cell.TextLabel.Text = cosas[indexPath.Row];
            return cell;
        }

        [Export("numberOfSectionsInTableView:")]
        public nint NumberOfSections(UITableView tableView) => 1;

        public nint RowsInSection(UITableView tableView, nint section) => cosas.Count;

        #endregion

        #region tableViewDelegate


        #endregion

        #endregion

        #region userInteraction

        partial void btnAdd_Clicked(NSObject sender)
        {
            var alert = UIAlertController.Create("Select limit", null, UIAlertControllerStyle.Alert);

            for (int i = 1; i < 21; i++)
            {
                alert.AddAction(UIAlertAction.Create(i.ToString(), UIAlertActionStyle.Default, (UIAlertAction obj) =>tablaSeleccionada( int.Parse(obj.Title))));
           
            };
           
            PresentViewController(alert, true, null);



        }




        #endregion

        #region functionality
        void tablaSeleccionada(int num){
            var alert = UIAlertController.Create("Select limit", "if limit its an invalid number, 10 will be selected", UIAlertControllerStyle.Alert);

            alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, (UIAlertAction obj) =>
            {
                int limite;
                if (!int.TryParse(alert.TextFields[0].Text,out limite))
                {
                    limite = 10;
                }
                if(limite < 0)
                {
                    limite = 10;
                }

                cosas = new List<string>();
                for (int i = 0; i <= limite; i++)
                {
                    cosas.Add(num +"x " + i +" = " +(num*i) );
                }
                InvokeOnMainThread(()
                                   =>
                { TableView.ReloadData();});
            }));
            alert.AddTextField((UITextField obj) => obj.Text = num.ToString());

            PresentViewController(alert, true, TableView.ReloadData);
 }

        #endregion

    }
}
