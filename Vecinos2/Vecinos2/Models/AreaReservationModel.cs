using System;
namespace Vecinos2.Models
{
	public class AreaReservationModel : IComparable<AreaReservationModel>
    {
		public long dateIndex { get; set; }
		public string reservationDate { get; }
		public string reservationHolderName { get; }


		public AreaReservationModel(string reservationDate, string reservationHolderName,long index)
		{
			this.reservationDate = reservationDate;
			this.reservationHolderName = reservationHolderName;
			this.dateIndex = index;
		}
      
		public int CompareTo(AreaReservationModel obj)
		{
			return dateIndex.CompareTo(obj.dateIndex);
		}
	}
}
