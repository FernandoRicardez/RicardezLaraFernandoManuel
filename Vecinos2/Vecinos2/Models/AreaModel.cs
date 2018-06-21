using System;
namespace Vecinos2.Models
{
    public class AreaModel
    {

		public string Name { get; set; }
		public string Description { get; set; }
		public string Uid { get; set; }
        public bool IsReservable { get; set; }
		public string PhotoUri { get; set; }
              
		public AreaModel(string name, string description, string uid, bool isReservable,string photoUri)
		{
			Name = name;
			Description = description;
			Uid = uid;
			IsReservable = isReservable;
			PhotoUri = photoUri; 
		}
	}
}
