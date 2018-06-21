using System;
using System.Collections.Generic;
using Firebase.Database;
using Foundation;
using Vecinos2.Models; 

namespace Vecinos2.Controllers
{
    public class AnnouncmentsController
    {
        DatabaseReference announcementNode;
        public EventHandler<AnnouncementFetchedEventArgs> AnnouncementFetched;
        nuint handleReference; 
        
        public AnnouncmentsController()
        {
            announcementNode = AppDelegate.rootNode.GetChild("announcements");
        }

        public bool InsertAnnouncement(AnnouncementModel announcement)
        {
            
            object[] keys = { "name","description","postDate","expirationDate","user" };
            object[] values = { announcement.Title, announcement.Description, announcement.PostDate ,announcement.ExpirationDateTimeStamp, announcement.Username };
            var data = NSDictionary.FromObjectsAndKeys(values, keys, keys.Length);

            announcementNode.KeepSynced(true);
            announcementNode.GetChildByAutoId().SetValue<NSDictionary>(data, (error, reference) => { Console.WriteLine(error); });


            return true;
        }

        public void StopAnnouncementFirebaseListener()
        {
            announcementNode.RemoveObserver(handleReference);
        }

        //get all announcements from firebase
        public void getAllAnnouncemets()
        {
            if (AnnouncementFetched == null)
            {
                return;
            }

            AnnouncementFetchedEventArgs announcementArgs;
            List<AnnouncementModel> announcementsLst = new List<AnnouncementModel>();

            handleReference = announcementNode.ObserveEvent(DataEventType.Value, (DataSnapshot snapshot, string prevKey) => {

                if(!snapshot.Exists)
                {
                    announcementArgs = new AnnouncementFetchedEventArgs(new List<AnnouncementModel>());
                    AnnouncementFetched(this, announcementArgs);
                    return;
                }

                var data = snapshot.GetValue<NSDictionary>();

                announcementsLst = new List<AnnouncementModel>();
                if (data == null)
                {
                    announcementArgs = new AnnouncementFetchedEventArgs(new List<AnnouncementModel>());
                    AnnouncementFetched(this, announcementArgs);
                    return;
                }

                for (int i = 0; i < data.Values.Length; i++)
                {
                    var announcement = data.Values[i];

                    string uid = data.Keys[i]?.ToString();

                    string user = announcement.ValueForKey((NSString)"user")?.ToString();
                    string name = announcement.ValueForKey((NSString)"name")?.ToString();
                    string description = announcement.ValueForKey((NSString)"description")?.ToString();
                    string date = announcement.ValueForKey((NSString)"expirationDate")?.ToString(); 
                    string post = announcement.ValueForKey((NSString)"postDate")?.ToString();

                    // < currentDate()
                    if(CheckAnnouncement(date) < 0)
                    {
                        continue;
                    }

                    announcementsLst.Add(new AnnouncementModel(name, description, date, user,post));

                }
                announcementArgs = new AnnouncementFetchedEventArgs(announcementsLst);
                AnnouncementFetched(this, announcementArgs);
            });

        }

        int CheckAnnouncement(string date)
        {
            char[] array = date.ToCharArray();

            string year = array[0].ToString() + array[1].ToString() + array[2].ToString() + array[3].ToString();
            string month = array[4].ToString() + array[5].ToString();
            string day = array[6].ToString() + array[7].ToString();

            string theDate = $"{year}{month}{day}";

            string now = DateTime.Now.ToString("yyyyMMdd");

            //Console.WriteLine(theDate + " - " + now);

            return int.Parse(theDate) - int.Parse(now);

        }

        public class AnnouncementFetchedEventArgs : EventArgs
        {
            public List<AnnouncementModel> AnnouncementList { get; private set; }
            public AnnouncementFetchedEventArgs(List<AnnouncementModel> announcementList) => AnnouncementList = announcementList;
        }
    }
}
