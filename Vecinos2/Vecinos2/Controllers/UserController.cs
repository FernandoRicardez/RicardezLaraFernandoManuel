using System;
using Foundation;
using Firebase.Database;
using Vecinos2.Models;
using Firebase.Storage;
using System.Threading.Tasks;
using UIKit;
using System.Drawing;
using System.Collections.Generic;

namespace Vecinos2.Controllers
{
	public class UserController
	{
		DatabaseReference userNode;
		DatabaseReference userReference;
		StorageReference imagesRef;

		public EventHandler<UserInsertedEventArgs> UserInserted;
		public EventHandler<UserLoggedEventArgs> UserLoggedIn;
		public EventHandler<UserGetImageEventArgs> ProfileImageFetched;
		public EventHandler<GetUsersEventArgs> UsersFetched;
        

		public UserController()
		{

			userNode = AppDelegate.rootNode.GetChild("users");
			imagesRef = AppDelegate.rootRefStorage.GetChild("images");
		}

		public void UpdateUserProfile(UserModel user, UIImage image)
		{
			Task.Factory.StartNew(UpdateUser);


			void UpdateUser()
			{
				try
				{
					userReference = userNode.GetChild(user.Uid);

					userReference.ObserveSingleEvent(DataEventType.Value, (snapshot) =>
					{
						if (!snapshot.Exists)
						{

							return;
						}
						object[] keys = { "name", "address", "phone", "mail", "uid", "status" };
						object[] values = { user.Name, user.Address, user.Phone, user.Mail, user.Uid, user.UserStatus };
						var data = NSDictionary.FromObjectsAndKeys(values, keys, keys.Length);

						userNode.KeepSynced(true);
						userReference.SetValue<NSDictionary>(data, (error, reference) =>
						{
							if (error != null)
							{
								Console.WriteLine("Aqui es el error ");
							}
						});


						if (image == null)
							return;

						var profileImageRef = imagesRef.GetChild($"/{user.Uid}/profile.jpg");

						var imageMetadata = new StorageMetadata
						{
							ContentType = "image/jpeg"
						};

						image = ResizeImage(image, 170, 170);

						profileImageRef.PutData(image.AsJPEG(), imageMetadata, (metadata, error) =>
						{
							if (error != null)
							{
								Console.WriteLine("Error");
							}
						});

					});
				}
				catch (Exception ex)
				{
					//Evento de error
					Console.WriteLine(ex.Message);
				}
			}

		}

		public UIImage ResizeImage(UIImage sourceImage, float width, float height)
		{
			UIGraphics.BeginImageContext(new SizeF(width, height));
			sourceImage.Draw(new RectangleF(0, 0, width, height));
			var resultImage = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();
			return resultImage;
		}

		public void GetImageFromUser(string id)
		{
			if (ProfileImageFetched == null)
				return;

			StorageReference profileImageRef = AppDelegate.rootRefStorage.GetChild($"images/{id}/profile.jpg");

			profileImageRef.GetData(1 * 1024 * 1024, HandleStorageGetDataCompletion);

			void HandleStorageGetDataCompletion(NSData data, NSError error)
			{
				if (error != null)
				{
					// Uh-oh, an error occurred!
					return;
				}

				// Data for "images/island.jpg" is returned
				var profileImage = UIImage.LoadFromData(data);

				var okEvent = new UserGetImageEventArgs(profileImage);
				ProfileImageFetched(this, okEvent);
			}


		}


		public void getAllUsers()
		{

			try
			{
				if (UsersFetched == null)
				{
					return;
				}

				GetUsersEventArgs getUsersEventArgs;
				List<UserModel> users = new List<UserModel>();



                userNode.ObserveEvent(DataEventType.Value, (DataSnapshot snapshot, string prevKey) =>
				{
					var data = snapshot.GetValue<NSDictionary>();
					users = new List<UserModel>();
					if (data == null)
					{
						getUsersEventArgs = new GetUsersEventArgs(new List<UserModel>());
						UsersFetched(this, getUsersEventArgs);
						return;
					}
					for (int i = 0; i < data.Values.Length; i++)
					{
						var area = data.Values[i];
						UserModel.userStatus userStatus = (UserModel.userStatus)int.Parse((area.ValueForKey((NSString)"status")?.ToString()));

                        if (userStatus == UserModel.userStatus.Admin)
					                continue;
						


						string uid = data.Keys[i]?.ToString();

						string name = area.ValueForKey((NSString)"name")?.ToString();

						string mail = area.ValueForKey((NSString)"mail")?.ToString();
						string address = area.ValueForKey((NSString)"address")?.ToString();
						string phone = area.ValueForKey((NSString)"phone")?.ToString();
                                          
                        

						users.Add(new UserModel(name,mail,uid,address,phone,userStatus));

					}
					getUsersEventArgs = new GetUsersEventArgs(users);
					UsersFetched(this, getUsersEventArgs);

				});
			}
			catch (Exception ex)
			{
                Console.WriteLine(ex.Message);
			}
		}


        public void setStatusForUser(string uid,UserModel.userStatus status)
		{
			int newStatus = (int)status;
			userNode.GetChild($"{uid}/status").SetValue<NSNumber>((NSNumber)newStatus  );

		}


		public void getUser(string uid)
		{

			Task.Factory.StartNew(getUserAsync);


			async Task getUserAsync()
			{
				try
				{


					userReference = userNode.GetChild(uid);

					userReference.ObserveEvent(DataEventType.Value, (snapshot) =>
					{

						if (UserLoggedIn == null)
						{
							return;
						}

						if (snapshot.Exists)
						{

							var data = snapshot.GetValue<NSDictionary>();

							//var key = snapshot.Key;
							var name = data["name"]?.ToString();
							var mail = data["mail"]?.ToString();
							var addreess = data["address"]?.ToString();
							var phone = data["phone"]?.ToString();
							UserModel.userStatus userStatus = (Vecinos2.Models.UserModel.userStatus)int.Parse((data["status"] as NSNumber).StringValue);


							UserModel loggedUser = new UserModel(name, mail, uid, addreess, phone, userStatus);
							if (loggedUser.UserStatus >= UserModel.userStatus.Authorized)
							{
								AppDelegate.CurrentUser = loggedUser;

								//Event return ok
								UserLoggedEventArgs okEvent;
                                if (loggedUser.UserStatus != UserModel.userStatus.Admin)
								{
									okEvent = new UserLoggedEventArgs(LoginStatus.Ok);
                                    UserLoggedIn(this, okEvent);
								}
                                else
								{
									okEvent = new UserLoggedEventArgs(LoginStatus.Admin);
                                    UserLoggedIn(this, okEvent);
								}





							}
							else
							{
								//Event return unauthorized	
								if (loggedUser.UserStatus == UserModel.userStatus.Pending)
								{
									var pendingEvent = new UserLoggedEventArgs(LoginStatus.Pending);
									UserLoggedIn(this, pendingEvent);
								}
								else
								{
									var unauthorizedEvent = new UserLoggedEventArgs(LoginStatus.Unauthorized);
									UserLoggedIn(this, unauthorizedEvent);
								}
							}




						}
						else
						{
							//Return wrongLogin
							var wrongUserEvent = new UserLoggedEventArgs(LoginStatus.WrongLogin);
							UserLoggedIn(this, wrongUserEvent);
						}
					});

				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					var errorEvent = new UserLoggedEventArgs(LoginStatus.Error);
					UserLoggedIn(this, errorEvent);
				}


			}
		}




		public void InsertUser(UserModel user)
		{
			Task.Factory.StartNew(InsertUserAsync);


			async Task InsertUserAsync()
			{
				try
				{
					userReference = userNode.GetChild(user.Uid);

					userReference.ObserveSingleEvent(DataEventType.Value, (snapshot) =>
					{
						if (UserInserted == null)
						{
							return;
						}
						if (snapshot.Exists)
						{
							var e = new UserInsertedEventArgs(false);
							UserInserted(this, e);


							return;
						}
						else
						{
							object[] keys = { "name", "address", "phone", "mail", "uid", "status" };
							object[] values = { user.Name, user.Address, user.Phone, user.Mail, user.Uid, user.UserStatus };
							var data = NSDictionary.FromObjectsAndKeys(values, keys, keys.Length);

							userNode.KeepSynced(true);
							userReference.SetValue<NSDictionary>(data, (error, reference) =>
							{
								if (error != null)
								{
									Console.WriteLine("error");
								}
							});


							var profileImageRef = imagesRef.GetChild($"/{user.Uid}/profile.jpg");

							UIImage image = new UIImage("profile.jpg");

							NSUrl photoUrl = new NSUrl(NSBundle.MainBundle.PathForResource("profile", "jpg"));

							var imageMetadata = new StorageMetadata
							{
								ContentType = "image/jpeg"
							};

							profileImageRef.PutData(image.AsJPEG(), imageMetadata, (metadata, error) =>
							{
								if (error != null)
								{
									Console.WriteLine("Error");
								}
							});




							var de = new UserInsertedEventArgs(true);
							UserInserted(this, de);
						}
					});
				}
				catch (Exception ex)
				{
					//Evento de error
					Console.WriteLine(ex.Message);
				}
			}


		}


	}
}

public class UserInsertedEventArgs : EventArgs
{
	public bool WasInserted { get; private set; }
	public UserInsertedEventArgs(bool wasInserted) => WasInserted = wasInserted;
}

public class UserLoggedEventArgs : EventArgs
{
	public LoginStatus LoginStatus { get; private set; }
	public UserLoggedEventArgs(LoginStatus loginStatus) => LoginStatus = loginStatus;
}

public class UserGetImageEventArgs : EventArgs
{
    public UIImage ProfileImage { get; private set; }
    public UserGetImageEventArgs(UIImage profileImage) => ProfileImage = profileImage;
}
public class GetUsersEventArgs : EventArgs
{
    public List<UserModel> ProfileImage { get; private set; }
    public GetUsersEventArgs(List<UserModel> profileImage) => ProfileImage = profileImage;
}


public enum LoginStatus
{

	Error = 0,
	WrongLogin = 1,
	Unauthorized = 2,
	Pending = 3,
	Ok = 4,
    Admin = 5
}