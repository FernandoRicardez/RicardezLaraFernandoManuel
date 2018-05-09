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
    public partial class ViewController : UIViewController, IUITableViewDataSource, IUITableViewDelegate, IUISearchBarDelegate
    {
        #region variables
		TwitterContext twitterCtx;
		List<TweetModelForCell> tweets = new List<TweetModelForCell>();
        List<TweetModelForCell> backgroundTweets = new List<TweetModelForCell>();
		Task twitterInitialized;
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

            TableView.Delegate = this;
            TableView.DataSource = this;
			SearchBar.Delegate = this;

            TableView.RowHeight = UITableView.AutomaticDimension;
            TableView.EstimatedRowHeight = 300;
            twitterInitialized = initTwitter();

                     
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

        public nint RowsInSection(UITableView tableView, nint section) => tweets.Count;
        #endregion


		#region tableViewDelegate
		[Export("scrollViewDidScroll:")]
		public  void ScrolledAsync(UIScrollView scrollView)
		{
			if (tweets.Count > 10)
			{
				var height = scrollView.Frame.Size.Height;
                var contentYoffset = scrollView.ContentOffset.Y;
                var distanceFromBottom = scrollView.ContentSize.Height - contentYoffset;
				if (distanceFromBottom < height)
				{

                    foreach (var tweet in backgroundTweets)
					{
						tweets.Add(tweet);
					}
					InvokeOnMainThread(() => TableView.ReloadData());
					var c = lazyTwitterAsync(SearchBar.Text);
            		
				}
            }
		}      
		#endregion

		#region SearchBarDelegate

        [Export("searchBar:textDidChange:")]
		public void TextChanged(UISearchBar searchBar, string searchText)
		{
			twitterAsync(searchText);
			lazyTwitterAsync(searchText);
		}
	

       
       
		#endregion

		#endregion

		#region userInteraction

		partial void btnAdd_Clicked(NSObject sender)
        {
            if (!string.IsNullOrEmpty(SearchBar.Text))
			{
				twitterAsync(SearchBar.Text);
			}

		}
		#endregion

		#region tweeterFunctions
  
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
		   (
			from search in twitterCtx.Search
			where search.Type == SearchType.Search && search.Query == query
				
            select search).SingleOrDefaultAsync();


            if (searchResponse != null && searchResponse.Statuses != null)
            {

                tweets = new List<TweetModelForCell>();
				backgroundTweets = new List<TweetModelForCell>();
				searchResponse.Statuses.ForEach(tweet =>
				                                tweets.Add(
					                                new TweetModelForCell(tweet.User.ProfileImageUrl, tweet.User.Name, tweet.Text, tweet.StatusID)));
				InvokeOnMainThread(() => TableView.ReloadData());
            }


        }

        //Method for getting tweets
		async System.Threading.Tasks.Task lazyTwitterAsync(string query)
        {         
            var searchResponse =
           await
           (from search in twitterCtx.Search
            where search.Type == SearchType.Search &&
                  search.Query == query && 
				search.MaxID == tweets[tweets.Count-1].IdFromTweet-1
            select search)
           .SingleOrDefaultAsync();

			backgroundTweets = new List<TweetModelForCell>();

            if (searchResponse != null && searchResponse.Statuses != null)
            {
				searchResponse.Statuses.ForEach(tweet => 
		                                backgroundTweets.Add(new TweetModelForCell(tweet.User.ProfileImageUrl, tweet.User.Name, tweet.Text,tweet.StatusID)));
				InvokeOnMainThread(() => TableView.ReloadData());
            } 
         
        }
  

        #endregion

    }
}
