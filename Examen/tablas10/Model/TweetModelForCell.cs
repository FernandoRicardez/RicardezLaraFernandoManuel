using System;
namespace tablas10.Model
{
    public class TweetModelForCell
    {

        public string ImgSource { get;}
        public string UserName { get; }
        public string TweetText { get; }
        public TweetModelForCell(string imgSource, string userName, string tweet)
        {
            ImgSource = imgSource;
            UserName = userName;
            TweetText = tweet;
        }
    }
}
