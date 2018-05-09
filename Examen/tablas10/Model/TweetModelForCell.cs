using System;
namespace tablas10.Model
{
    public class TweetModelForCell
    {

		public ulong IdFromTweet { get; }
        public string ImgSource { get; }
        public string UserName { get; }
        public string TweetText { get; }
        public TweetModelForCell(string imgSource, string userName, string tweet, ulong id)
        {
			IdFromTweet = id;
            ImgSource = imgSource;
            UserName = userName;
            TweetText = tweet;
        }
    }
}
