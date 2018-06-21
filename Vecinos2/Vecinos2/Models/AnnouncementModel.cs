using System;
namespace Vecinos2.Models
{
    public class AnnouncementModel
    {
        public string Title { get; }
        public string Description { get; }
        public double ExpirationDateTimeStamp { get; }
        public string Username { get; }
        public string PostDate { get; }

        public AnnouncementModel(string Title, string Description, string Expiration, string Username, string PostDate)
        {
            this.Title = Title;
            this.Description = Description;
            this.ExpirationDateTimeStamp = AppDelegate.GetUtcTimeStampFromString(Expiration);
            this.Username = Username;
            this.PostDate = PostDate;
        }
    }
}
