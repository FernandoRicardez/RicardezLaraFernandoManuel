using Foundation;
using UIKit;
using Firebase.Core;
using Firebase.Database;
using Firebase.Storage;
using System;
using System.Globalization;
using Vecinos2.Models;
using ObjCRuntime;

namespace Vecinos2
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations

        public override UIWindow Window
        {
            get;
            set;
        }
        
        public static UserModel CurrentUser { get; set; }
        public static DatabaseReference rootNode { get; set; }


        //FIREBASE STORAGE.
        // Get a reference to the storage service, using the default Firebase App


        // Create a root reference
        public static StorageReference rootRefStorage { get; set; }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
			// Override point for customization after application launch.
			// If not required for your application you can safely delete this method
			App.Configure();

            Database.DefaultInstance.PersistenceEnabled = true; 
            rootNode = Database.DefaultInstance.GetRootReference();

            var storage = Storage.DefaultInstance;
            rootRefStorage = storage.GetRootReference();
            return true;
        }

        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations(UIApplication application, [Transient] UIWindow forWindow)
        {
            return UIInterfaceOrientationMask.Portrait;
        }

        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.
        }

        public override void WillTerminate(UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }

        public static double GetUtcTimestamp()
        {
            return double.Parse(DateTime.UtcNow.ToString("yyyyMMddHHmmss"));
        }

        public static string ConvertUnformattedUtcDateToCurrentDate(string utcDate)
        {
            var date = System.DateTime.ParseExact(utcDate, "yyyyMMddHHmmss", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
            return date.ToString("MM/dd/yy");
        }

        public static double GetUtcTimeStampFromString(string date)
        {
            DateTime tempDate = DateTime.ParseExact(date, "yyyyMMddHHmmss", CultureInfo.InvariantCulture); 
            return double.Parse(tempDate.ToString("yyyyMMddHHmmss"));
        }
        
        public static void SetScreen(UIViewController param)
		{
		   	
		}
    }
}

