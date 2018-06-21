using System;
namespace Vecinos2.Models
{
    public class UserModel
    {

		public string Name { get; set; }
		public string Mail { get; }
		public string Uid { get; }
        public string Address { get; set; }
        public string Phone { get; set; }
		public userStatus UserStatus { get; }

        public enum userStatus
		{
            UnAuthorized=0,
			Authorized=2,
			Pending=1,
            Admin=3
		}
	      
		public UserModel( string name, string mail, string uid, string address, string phone, userStatus userStatus)
		{
			Name = name;
			Mail = mail;
			Uid = uid;
			Address = address;
			Phone = phone;
			UserStatus = userStatus;
		}
	}
}
