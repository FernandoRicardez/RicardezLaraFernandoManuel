using System;
using System.Collections.Generic;
using Foundation;
using LinqToTwitter;
using UIKit;
using System.Linq;
using System.Threading.Tasks;
using tablas10.Model;

namespace tablas10
{
    public partial class ViewController : UIViewController, IUITableViewDataSource, IUITableViewDelegate
    {
        #region variables

        //forlazy load


        List<String> cosas = new List<string>();
        TwitterContext twitterCtx;

        List<TweetModelForCell> tweets = new List<TweetModelForCell>(); 



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

            TableView.RowHeight = UITableView.AutomaticDimension;
            TableView.EstimatedRowHeight = 300;
            
            var tw = initTwitter();
          

        }

        #region tableViewDataSource

        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {

            var cell = tableView.DequeueReusableCell("TableCell", indexPath) as TwitTableCell;
            //cell.ProfileImage = UIImage.LoadFromData(NSData.FromUrl(NSUrl.FromString(tweets[indexPath.Row].ImgSource)));
            cell.UserName = tweets[indexPath.Row].UserName;
            cell.TweetText = tweets[indexPath.Row].TweetText;

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

            var alert = UIAlertController.Create("Select limit", "if limit its an invalid number, 10 will be selected", UIAlertControllerStyle.Alert);

            alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, (UIAlertAction obj) =>
            {
                var query = alert.TextFields[0].Text;
                var result = twitterAsync(query);

           
            }));
            alert.AddTextField((UITextField obj) => obj.Placeholder = "Buscar...");

            PresentViewController(alert, true, TableView.ReloadData);

     


            //var alert = UIAlertController.Create("Select limit", null, UIAlertControllerStyle.Alert);

            //for (int i = 1; i < 21; i++)
            //{
            //    alert.AddAction(UIAlertAction.Create(i.ToString(), UIAlertActionStyle.Default, (UIAlertAction obj) =>tablaSeleccionada( int.Parse(obj.Title))));
           
            //};
           
            //PresentViewController(alert, true, null);



        }

        async System.Threading.Tasks.Task initTwitter()
        {


            var auth = new ApplicationOnlyAuthorizer()
            {
                CredentialStore = new InMemoryCredentialStore
                {
                    ConsumerKey = "Wc30HCVdXdaNdmzQ3mnqGASzM",
                    ConsumerSecret = "fDcmPOzHEe2CWcpkIfQVyuZglvo52BiTTtSxPlDULmDYwmzkEB"

                }

            };

            await auth.AuthorizeAsync();


             twitterCtx = new TwitterContext(auth);
        }

        async System.Threading.Tasks.Task twitterAsync(string query)
        {
            
            var searchResponse =
           await
           (from search in twitterCtx.Search
            where search.Type == SearchType.Search &&
                  search.Query == query
            select search)
           .SingleOrDefaultAsync();


            if (searchResponse != null && searchResponse.Statuses != null)
            {

                    cosas = new List<string>();
                tweets = new List<TweetModelForCell>();

                searchResponse.Statuses.ForEach(tweet =>
                {
                    
                    cosas.Add(tweet.Text);

                    TweetModelForCell tweetModel = new TweetModelForCell(tweet.User.ProfileImageUrl, tweet.User.Name, tweet.Text);
                    tweets.Add(tweetModel);
                });
                InvokeOnMainThread(()=>{ 
                    TableView.ReloadData(); 
                });
            }

           
        }



        void tablaSeleccionada(int num){
            var alert = UIAlertController.Create("Select limit", "if limit its an invalid number, 10 will be selected", UIAlertControllerStyle.Alert);

            alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, (UIAlertAction obj) =>
            {
                var query = alert.TextFields[0].Text;

                InvokeOnMainThread(()
                                   =>
                { TableView.ReloadData();});
            }));
            alert.AddTextField((UITextField obj) => obj.Placeholder = "Buscar...");

            PresentViewController(alert, true, TableView.ReloadData);
 }

        #endregion

    }
}
