using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PullToRefress11
{

    //This class is a Singleton 
    public sealed class CitiGroupManager
    {
        //Singleton 
        static readonly Lazy<CitiGroupManager> lazy = new Lazy<CitiGroupManager>(() => new CitiGroupManager());
        public static CitiGroupManager SharedInstance { get => lazy.Value; }

        //ClassVariables
        HttpClient httpClient;
        Dictionary<string, List<String>> countries;

        #region events

        public EventHandler<CountriesEventArgs> CountriesFetched;
        public EventHandler<ErrorEventArguments> CountriesFetchFalied;

        #endregion

        //Constructor
        CitiGroupManager() => httpClient = new HttpClient();

        //Functionality
        public Dictionary<string, List<String>> GetDefaultCountries()
        {
            var countriesJSON = File.ReadAllText("citites-incomplete.json");
            return JsonConvert.DeserializeObject<Dictionary<string, List<String>>>(countriesJSON);
        }

        public void FetchCountries()
        {
            Task.Factory.StartNew(FetchCountriesAsync);

            async Task FetchCountriesAsync()
            {
                try
                {
					
                    var countriesJSON = await httpClient.GetStringAsync("https://dl.dropbox.com/s/0adq8yw6vd5r6bj/cities.json?dl=0");
                    countries = JsonConvert.DeserializeObject<Dictionary<string, List<String>>>(countriesJSON);
                    //eventos events/Delegate, notificaciones via notificacion center o si estas dentro de un viewController con United segue   
                    var e = new CountriesEventArgs(countries);
                    CountriesFetched(this, e);
                    if (CountriesFetched == null)
                        return;
                    
                }
                catch (Exception ex)
                {
                    //eventos events/Delegate, notificaciones via notificacion center o si estas dentro de un viewController con United segues
                    Console.WriteLine("FetchCountiesAsync Failed " + ex.ToString());
                    if (CountriesFetchFalied == null)
                        return;
                    CountriesFetchFalied(this, new ErrorEventArguments(ex.Message));
                }
            }
        }
    }
}
    public class CountriesEventArgs : EventArgs
    {
        public Dictionary<string, List<String>> Countries { get; private set; }
        public CountriesEventArgs(Dictionary<string, List<string>> countries) => Countries = countries;
    }

public class ErrorEventArguments: EventArgs
{
    public String Error { get; private set; }
    public ErrorEventArguments(string error) => Error = error;
}