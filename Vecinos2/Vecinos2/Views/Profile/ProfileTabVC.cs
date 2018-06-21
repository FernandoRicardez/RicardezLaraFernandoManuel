using System;
using Vecinos2.Controllers;
using UIKit;
using Photos;
using Foundation;
using Vecinos2.Models;
using Firebase.Auth;

namespace Vecinos2.Views.Profile
{
    public partial class ProfileTabVC : UIViewController, IUIImagePickerControllerDelegate,IUITabBarDelegate
    {

		bool EditMode;
		UITapGestureRecognizer profileTapGesture;
        UserController userController;
        int flag = 0; 

		public ProfileTabVC(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            

            //Add events
			BtnEdit.Clicked += BtnEdit_Clicked;
			profileTapGesture = new UITapGestureRecognizer(tapGestureListenner){ Enabled = false};

            userController = new UserController();

            userController.ProfileImageFetched += UserController_ProfileImageFetched;


            userController.GetImageFromUser(AppDelegate.CurrentUser.Uid);

        }
              


		[Export("imagePickerController:didFinishPickingMediaWithInfo:")]
        public void FinishedPickingMedia(UIImagePickerController picker, NSDictionary info)
        {
            //TODO: falta enviarlo a FIREBASE
            var image = info[UIImagePickerController.OriginalImage] as UIImage;
            ImgProfilePicture.Image = image;
            flag = 1; 
            picker.DismissViewController(true, null);
        }

        [Export("imagePickerControllerDidCancel:")]
        public void Canceled(UIImagePickerController picker)
        {
            picker.DismissViewController(true, null);
            flag = 1; 
        }

        void UserController_ProfileImageFetched(object sender, UserGetImageEventArgs e)
        {
            ImgProfilePicture.Image = e.ProfileImage;
        }


		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
            if(flag != 1)
            {
                LoadData();
            }
		}

	

		void LoadData()
		{
            BtnEdit.Title = "Editar";
			LblChange.Hidden = true;
            TxtName.Enabled = false;
            TxtPhone.Enabled = false;
            TxtAddress.Enabled = false;
            ViewProfilePicture.AddGestureRecognizer(profileTapGesture);
            TxtName.Text = AppDelegate.CurrentUser.Name;
            TxtPhone.Text = AppDelegate.CurrentUser.Phone;
            TxtAddress.Text = AppDelegate.CurrentUser.Address;
            EditMode = false;
		}

		public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

		void BtnEdit_Clicked(object sender, EventArgs e)
		{
			EditMode = !EditMode;
			BtnEdit.Title = EditMode ? "Hecho" : "Editar";
			profileTapGesture.Enabled = EditMode;
			LblChange.Hidden = !EditMode;
			TxtName.Enabled = EditMode;
			TxtPhone.Enabled = EditMode;

            if(!EditMode)
            {
                UpdateUserProfile(); 
            }

		}

        void UpdateUserProfile()
        {
            UserModel user = AppDelegate.CurrentUser;
            user.Name = TxtName.Text;
            user.Address = TxtAddress.Text;
            user.Phone = TxtPhone.Text;

            userController.UpdateUserProfile(user, ImgProfilePicture.Image);
        }

		void tapGestureListenner()
		{
            UIAlertController alert = UIAlertController.Create(null, null, UIAlertControllerStyle.ActionSheet);
            alert.AddAction(UIAlertAction.Create("Abrir galeria de fotos", UIAlertActionStyle.Default, TryOpenGallery));
            alert.AddAction(UIAlertAction.Create("Cancelar", UIAlertActionStyle.Cancel, null));

            PresentViewController(alert, true, null);
		}

        void TryOpenGallery(UIAlertAction obj)
        {

            if (!UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.PhotoLibrary))
            {
                return;
            }

            CheckPhotoLibraryAuthorizationsStatus(PHPhotoLibrary.AuthorizationStatus);
        }

        void CheckPhotoLibraryAuthorizationsStatus(PHAuthorizationStatus authorizationStatus)
        {
            switch (authorizationStatus)
            {
                case PHAuthorizationStatus.NotDetermined:
                    // TODO: Pedir permiso para acceder

                    PHPhotoLibrary.RequestAuthorization(CheckPhotoLibraryAuthorizationsStatus);

                    break;
                case PHAuthorizationStatus.Restricted:
                    // TODO: Mostrar un mensaje diciendo que está restringido
                    InvokeOnMainThread(() => { ShowMessage("Error", "El recuerso no esa dispiie", NavigationController); });
                    break;
                case PHAuthorizationStatus.Denied:
                    // TODO: Mostrar un mensaje diciendo que el usuario denego
                    InvokeOnMainThread(() => { ShowMessage("Error", "El recuerso no esta disponible", NavigationController); });
                    break;
                case PHAuthorizationStatus.Authorized:
                   InvokeOnMainThread(() =>
                    {
                        var imagePickerController = new UIImagePickerController
                        {
                            SourceType = UIImagePickerControllerSourceType.PhotoLibrary,
                            Delegate = this
                        };
                        PresentViewController(imagePickerController, true, null);
                    });

                    break;
                default:
                    break;
            }
        }

        void ShowMessage(string title, string message, UIViewController fromViewController)
        {

            UIAlertController alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

            fromViewController.PresentViewController(alert, true, null);
        }

        partial void btnLogOut(NSObject sender)
        {
            NSError error;
            var signedOut = Auth.DefaultInstance.SignOut(out error);

            if (!signedOut)
            {
                AuthErrorCode errorCode;
                if (IntPtr.Size == 8) // 64 bits devices
                    errorCode = (AuthErrorCode)((long)error.Code);
                else // 32 bits devices
                    errorCode = (AuthErrorCode)((int)error.Code);

                // Posible error codes that SignOut method with credentials could throw
                // Visit https://firebase.google.com/docs/auth/ios/errors for more information
                switch (errorCode)
                {
                    case AuthErrorCode.KeychainError:
                    default:
                        // Print error
                        break;
                }
            }
            else
            {
                DismissViewController(true, null);
            }
        }

    }
}

