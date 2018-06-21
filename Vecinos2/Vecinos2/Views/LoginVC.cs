using System;
using Firebase.Auth;
using Foundation;
using UIKit;
using Vecinos2.Controllers;
using Vecinos2.Models;
namespace Vecinos2.Views
{
    public partial class LoginVC : UIViewController
    {
        NSObject handle;
		UserController userController;

        public LoginVC(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
			TxtPassword.SecureTextEntry = true;
			userController = new UserController();
			userController.UserLoggedIn += UserController_UserLoggedIn;
			TxtPassword.ShouldReturn += TxtPassword_ShouldReturn;


            TxtMail.ShouldReturn = (textField) =>
            {
                TxtPassword.BecomeFirstResponder();
                return true;
            };

            TxtPassword.ShouldReturn = (textField) =>
            {
                TxtPassword.ResignFirstResponder();
                return true;
            };
        }



        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            handle = Auth.DefaultInstance.AddAuthStateDidChangeListener(HandleAuthStateDidChangeListener);

            UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.Default;
        }

		bool TxtPassword_ShouldReturn(UITextField textField)
		{
			if (textField.Text != "")
			{
				LogIn();

			}
			return true;
		}


        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            Auth.DefaultInstance.RemoveAuthStateDidChangeListener(handle);

        }
        

        partial void btnSignIn_TouchUpInside(NSObject sender)
        {
			LogIn();
			

        }

         void LogIn()
		{
			if (validateMail())
                Auth.DefaultInstance.SignIn(TxtMail.Text, TxtPassword.Text, SignInResult);
		}

		bool validateMail()
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(TxtMail.Text);
                return true;
            }
            catch
            {
                ShowAlert("ERROR", "Ingrese un email válido", null);
                return false;
            }

        }

        void HandleAuthStateDidChangeListener(Auth auth, User user)
        {
            //Console.WriteLine("DidChange");
        }

 


        void SignInResult(User user, NSError error)
        {         
            if (error != null)
            {
                AuthErrorCode errorCode;
                if (IntPtr.Size == 8) // 64 bits devices
                    errorCode = (AuthErrorCode)((long)error.Code);
                else // 32 bits devices
                    errorCode = (AuthErrorCode)((int)error.Code);

                // Posible error codes that SignIn method with email and password could throw
                // Visit https://firebase.google.com/docs/auth/ios/errors for more information
                switch (errorCode)
                {
                    case AuthErrorCode.OperationNotAllowed:
                    case AuthErrorCode.InvalidEmail:
                    case AuthErrorCode.UserDisabled:
                    case AuthErrorCode.WrongPassword:
                    default:
                        Console.WriteLine("Could not login!" + error.LocalizedDescription);

                        if(error.LocalizedDescription == "The user account has been disabled by an administrator.")
                        {
                            ShowAlert("ERROR", "La cuenta ha sido deshabilitada por el administrador", null);
                        }
                        else if(error.LocalizedDescription == "Network error (such as timeout, interrupted connection or unreachable host) has occurred.")
                        {
                            ShowAlert("ERROR DE CONEXIÓN", "No se tiene conexión a Internet", null);
                        }
                        else
                        {
                            ShowAlert("ERROR", "Usuario o contraseña incorrectos", null);
                        }


                        break;
                }

                return;
            }

			userController.getUser(user.Uid);
		

        }

		void UserController_UserLoggedIn(object sender, UserLoggedEventArgs e)
		{
			switch (e.LoginStatus)
			{
				case LoginStatus.Admin:
		            NavigateToAdminScreen();
					break;
				case LoginStatus.Pending:
					ShowAlert("ESPERA", "Tu cuenta necesita ser validada por un administrador.", null);
					break;
				case LoginStatus.Unauthorized:
					ShowAlert("ATENCIÓN", "Tu cuenta ha sido desactivada si crees que es un error contacta a administración", null);
					break;
				case LoginStatus.Error:
					ShowAlert("ERROR", "Hubo un error intentalo más tarde, si el problema persiste contacta a administración", null);
					break;
				case LoginStatus.Ok:
					NavigateToMainScreen();
					break;
					                
					
				default:
					break;
			}
			if (e.LoginStatus == LoginStatus.Admin)
			{
			}
            else
			{

				NavigateToMainScreen();
			}

		}
	
        void NavigateToAdminScreen()
		{
			PerformSegue("AdminLogInSegue", this);
		}

         void NavigateToMainScreen()
        {
            PerformSegue("LogInSegue", this);
        }

		void ShowAlert(string title, string message, Action func)
        {
            var okAlertController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
            okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

            InvokeOnMainThread(() => PresentViewController(okAlertController, true, func));

        }

   



    }
}