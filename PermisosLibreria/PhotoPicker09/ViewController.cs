using System;
using Photos;
using UIKit;
using Foundation;
using AVFoundation;

namespace PhotoPicker09
{
    public partial class ViewController : UIViewController, IUIImagePickerControllerDelegate
    {
        #region Variables

        UITapGestureRecognizer editTapGesture;
        UITapGestureRecognizer tapTapGesture;


        #endregion

        #region Constructor
        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
        #endregion

        #region LifeCicle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            InitializeComponents();
        }
        #endregion

        #region UserInteractions

        void ShowOptions(UITapGestureRecognizer gesture)
		{
			var alert = UIAlertController.Create(null, null, UIAlertControllerStyle.ActionSheet);
			
			alert.AddAction(UIAlertAction.Create("Open library", UIAlertActionStyle.Default, TryOpenLibrary));
            alert.AddAction(UIAlertAction.Create("Camera", UIAlertActionStyle.Default, CameraSelected));
            alert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null));

            PresentViewController(alert, true, null);

        }
        void TryOpenLibrary(UIAlertAction obj)
        {
            Console.WriteLine("photo");

            if (!UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.PhotoLibrary))
            {
                //print msg 
                return;
            }
            CheckPhotoLibraryAuthorizationStatus(PHPhotoLibrary.AuthorizationStatus);

        }


        void CameraSelected(UIAlertAction obj)
        {

            if (!UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.Camera))
            {
                ShowMessage("Error","Your device camera is not available",NavigationController);
                return;
            }

            CheckCameraAuthorizationStatus(AVCaptureDevice.GetAuthorizationStatus(AVMediaType.Video));


        }

        void CheckCameraAuthorizationStatus(AVAuthorizationStatus auth)
        {
            switch (auth)
            {
                case AVAuthorizationStatus.Authorized:
                    InvokeOnMainThread(() =>
                    {

                        var imagePicker = new UIImagePickerController
                        {
                            SourceType = UIImagePickerControllerSourceType.Camera,
                            Delegate = this
                        };
                        PresentViewController(imagePicker, true, null);

                    });
                    break;

                case AVAuthorizationStatus.Denied:
                    InvokeOnMainThread(() =>
                    {
                        ShowMessage("Alert!", "No", NavigationController);
                    });
                    break;

                case AVAuthorizationStatus.NotDetermined:
                    
                    AVCaptureDevice.RequestAccessForMediaType(AVMediaType.Video,(bool access)=> CheckCameraAuthorizationStatus(auth));
                    break;
                case AVAuthorizationStatus.Restricted:
                    InvokeOnMainThread(() =>
                    {
                        ShowMessage("Alert!", "No", NavigationController);
                    }); 
                    break;

                default:
                    break;
            }
        }

        void CheckPhotoLibraryAuthorizationStatus(PHAuthorizationStatus authorizationStatus)
        {
            switch (authorizationStatus)
            {
                case PHAuthorizationStatus.NotDetermined:
                    //Pedir permiso
                    PHPhotoLibrary.RequestAuthorization(CheckPhotoLibraryAuthorizationStatus);
                    break;
                case PHAuthorizationStatus.Restricted:
                    InvokeOnMainThread(() =>
                    {
                        ShowMessage("Alert!", "No", NavigationController);
                    });break;
                case PHAuthorizationStatus.Denied:
                    InvokeOnMainThread(() =>
                    {
                        ShowMessage("Alert!", "No", NavigationController);
                    });
                    break;
                case PHAuthorizationStatus.Authorized:
                    //Open photo library
                    InvokeOnMainThread(() =>
                    {

                        var imagePicker = new UIImagePickerController
                        {
                            SourceType = UIImagePickerControllerSourceType.PhotoLibrary,
                            Delegate = this
                        };
                        PresentViewController(imagePicker, true, null);

                    });
                    break;
                default:
                    break;

            }
        }

        void InitializeComponents()
        {
            LblEdit.Hidden = true;
            LblTap.Hidden = true;

            editTapGesture = new UITapGestureRecognizer(ShowOptions) { Enabled = true };
            ProfileView.AddGestureRecognizer(editTapGesture);

            tapTapGesture = new UITapGestureRecognizer(ShowOptions) { Enabled = false };
            BottomView.AddGestureRecognizer(tapTapGesture);
        }

        partial void BtnEdit_Clicked(Foundation.NSObject sender)
        {

        }

        #endregion

        #region UIImagePickerControllerDelegate

        [ExportAttribute("imagePickerControllerDidCancel:")]
        public void Canceled(UIImagePickerController picker)
        {
                picker.DismissViewController(true,null);
        }
        [ExportAttribute("imagePickerController:didFinishPickingMediaWithInfo:")]
        public void FinishedPickingMedia(UIImagePickerController picker, NSDictionary info)
        {
            var image = info.ValueForKey(UIImagePickerController.OriginalImage) as UIImage;
            ImgProfile.Image = image;
            picker.DismissViewController(true,null);
        }

        void ShowMessage(string title,string message,UIViewController fromViewController)
        {
            var alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert   );

            alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));

            fromViewController.PresentViewController(alert,true,null);
                            
        }
        #endregion
    }
}
