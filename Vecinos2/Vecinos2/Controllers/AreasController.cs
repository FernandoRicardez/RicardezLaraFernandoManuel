using System;
using Foundation;
using Firebase.Database;
using Vecinos2.Models;
using Firebase.Storage;
using System.Threading.Tasks;
using UIKit;
using System.Drawing;
using System.Collections.Generic;
using System.Globalization;

namespace Vecinos2.Controllers
{
	public class AreasController
	{
		DatabaseReference areasNode;
		DatabaseReference reservationsNode;
		public EventHandler<AreasFetchedEventArgs> AreasFetched;
		public EventHandler<AreaReservationFetchedEventArgs> areaReservationFetched;
		public EventHandler<AreaReservationPostedEventArgs> areaReservationPosted;
        nuint handleReference; 
      
       
	    public AreasController()
		{
			areasNode = AppDelegate.rootNode.GetChild("areas");
			reservationsNode = AppDelegate.rootNode.GetChild("reservations");
		}

        public void StopAreasFirebaseListener()
        {
            areasNode.RemoveObserver(handleReference);
        }

        public void getAllAreas()
		{
            if (AreasFetched == null)
			{
				return;
			}

			AreasFetchedEventArgs areasArgs; 
			List<AreaModel> areas = new List<AreaModel>();

            handleReference = areasNode.ObserveEvent(DataEventType.Value, (DataSnapshot snapshot, string prevKey) => {
				var data = snapshot.GetValue<NSDictionary>();
                areas = new List<AreaModel>();
                if (data == null)
				{
					areasArgs = new AreasFetchedEventArgs(new List<AreaModel>());
                    AreasFetched(this, areasArgs);
					return;
				}
				for (int i = 0; i < data.Values.Length;i++)
				{
					var area = data.Values[i];

					string uid = data.Keys[i]?.ToString();
                    
					string name = area.ValueForKey((NSString)"name")?.ToString();
					string description = area.ValueForKey((NSString)"description")?.ToString();
					string photoUri = area.ValueForKey((NSString)"PhotoUri")?.ToString();
					int reservable = (int)(area.ValueForKey((NSString)"isReservable") as NSNumber);
					bool isReservable = reservable != 0;

					areas.Add(new AreaModel(name, description, uid, isReservable, photoUri));
					         
				}
				areasArgs = new AreasFetchedEventArgs(areas);
                AreasFetched(this, areasArgs);
			});
		

		}

		public void ReserveAreaForDate(string areaUid, DateTime date)
		{
			CultureInfo culture = new CultureInfo("es-mx");
			string dateString = date.ToString("dddd, MMMM dd, yyyy", culture);
			long dateArea = long.Parse(date.ToString("yyyyMMdd"));

			if (areaReservationPosted == null)
			{
				return;
			}

			reservationsNode.GetChild(areaUid).GetQueryOrderedByChild("date").GetQueryEqualToValue((NSNumber)dateArea).ObserveSingleEvent(DataEventType.Value, (DataSnapshot snapshot) =>
			{
				AreaReservationPostedEventArgs postedEventArgs;

				if (snapshot.Exists)
				{
					postedEventArgs = new AreaReservationPostedEventArgs("Esta fecha ya esta ocupada seleccione otra fecha!");
					areaReservationPosted(this, postedEventArgs);
					return;
				}
				else
				{
					object[] keys = { "reservationDateString", "holderName", "date" };
					object[] values = { dateString, AppDelegate.CurrentUser.Name, dateArea };
					var data = NSDictionary.FromObjectsAndKeys(values, keys, keys.Length);

					reservationsNode.KeepSynced(true);
					reservationsNode.GetChild(areaUid).GetChildByAutoId().SetValue<NSDictionary>(data, (error, reference) =>
					{
						if (error != null)
						{
							Console.WriteLine("Error doing reservation");
						}
			        });

	            }
            
			});
            
			
		}

        public void GetFutureReservationsForArea(string areaUid)
		{
			if (areaReservationFetched==null)
			{
				return;
			}
			long query = long.Parse( DateTime.Now.ToString("yyyyMMdd"));
   
			AreaReservationFetchedEventArgs fetchedEventArgs;
			List<AreaReservationModel> areaReservations;

			reservationsNode.GetChild(areaUid).GetQueryOrderedByChild("date").GetQueryStartingAtValue((NSNumber)query).ObserveEvent(DataEventType.Value, (DataSnapshot snapshot) =>
			{
				areaReservations = new List<AreaReservationModel>();
				if(!snapshot.Exists)
				{
					fetchedEventArgs = new AreaReservationFetchedEventArgs(new List<AreaReservationModel>());
                    areaReservationFetched(this, fetchedEventArgs);
                    return;
				}
				var data = snapshot.GetValue<NSDictionary>();
                if (data == null)
                {
                    fetchedEventArgs = new AreaReservationFetchedEventArgs(new List<AreaReservationModel>());
                    areaReservationFetched(this, fetchedEventArgs);
                    return;
                }
                for (int i = 0; i < data.Values.Length; i++)
                {
                    var area = data.Values[i];

					var reservationDate = area.ValueForKey((NSString)"reservationDateString")?.ToString();
					var holderName = "Reservado por " + area.ValueForKey((NSString)"holderName")?.ToString();
					var dateindex = long.Parse( area.ValueForKey((NSString)"date")?.ToString());

					areaReservations.Add(new AreaReservationModel(reservationDate, holderName,dateindex));
                }
				areaReservations.Sort();
                fetchedEventArgs = new AreaReservationFetchedEventArgs(areaReservations);
                areaReservationFetched(this, fetchedEventArgs);


			});
        
		}

		public class AreasFetchedEventArgs : EventArgs
        {
            public List<AreaModel> AreasList { get; private set; }
            public AreasFetchedEventArgs(List<AreaModel> areasList) => AreasList = areasList;
        }
		public class AreaReservationFetchedEventArgs : EventArgs
        {
            public List<AreaReservationModel> AreaReservations { get; private set; }
            public AreaReservationFetchedEventArgs(List<AreaReservationModel> areaReservations) => AreaReservations = areaReservations;
        }
		public class AreaReservationPostedEventArgs : EventArgs
        {
            public string insertMesssage { get; private set; }
			public AreaReservationPostedEventArgs(string message) => insertMesssage = message;
        }

      
      
                                                     
	}
}