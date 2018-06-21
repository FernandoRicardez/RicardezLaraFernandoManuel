using System;
using Foundation;
using UIKit;
using Vecinos2.Models;
using Vecinos2.Controllers;
using System.Text.RegularExpressions;
using Firebase.Auth;


namespace Vecinos2.Views
{
    public partial class RegisterVC : UIViewController
    {
		UserController userController;

		public RegisterVC(IntPtr handle) : base(handle)
        {


        }      
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            TxtPassword.SecureTextEntry = true;
            TxtPasswordConfirmation.SecureTextEntry = true;

			userController = new UserController();
			userController.UserInserted += UserController_UserRegisted;


            TxtName.ShouldReturn = (textField) =>
            {
                TxtName.ResignFirstResponder();
                return true;
            };

            TxtMail.ShouldReturn = (textField) =>
            {
                TxtMail.ResignFirstResponder();
                return true;
            };

            TxtPassword.ShouldReturn = (textField) =>
            {
                TxtPassword.ResignFirstResponder();
                return true;
            };

            TxtPasswordConfirmation.ShouldReturn = (textField) =>
            {
                TxtPasswordConfirmation.ResignFirstResponder();
                return true;
            };

            TxtAddress.ShouldReturn = (textField) =>
            {
                TxtAddress.ResignFirstResponder();
                return true;
            };

            TxtPhone.ShouldReturn = (textField) =>
            {
                TxtPhone.ResignFirstResponder();
                return true;
            };

        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        bool ValidateFields()
		{
			if (TxtMail.Text == ""){
				ShowAlert("ERROR", "El correo no puede estar vacío.", null);
                return false;
			}
			if (TxtName.Text == ""){
                ShowAlert("ERROR", "El nombre no puede estar vacío.", null);
                return false;
            }
			if (TxtPhone.Text == ""){
                ShowAlert("ERROR", "El teléfono no puede estar vacío.", null);
                return false;
            }
			if (TxtAddress.Text == ""){
                ShowAlert("ERROR", "La dirección no puede estar vacía.", null);
                return false;
            }
			if (TxtPassword.Text == ""){
                ShowAlert("ERROR", "La contraseña no puede estar vacía.", null);
                return false;
            }
			if (TxtPasswordConfirmation.Text == ""){
                ShowAlert("ERROR", "La contraseña de verificación no puede estar vacía.", null);
                return false;
            }
			try
            {
                var addr = new System.Net.Mail.MailAddress(TxtMail.Text);

			}
            catch
            {
                ShowAlert("ERROR", "Ingrese un correo electrónico válido.", null);
                return false;
            }
			//TODO: Validate phone :
            if(!IsPhoneNumber(TxtPhone.Text))
            {
                ShowAlert("ERROR", "Ingrese un número telefónico válido.", null);
                return false;
            }


			if(TxtPassword.Text != TxtPasswordConfirmation.Text)
			{
                ShowAlert("ERROR", "Las contraseñas no coinciden.", null);
				return false;
			}
         
			return true;	
		}

        void InsertUser(string uid)
		{
			string mail = TxtMail.Text.ToLower();
            UserModel user = new UserModel( TxtName.Text, mail, uid, TxtAddress.Text, TxtPhone.Text, UserModel.userStatus.Pending);
            userController.InsertUser(user);

		}

		partial void BtnRegister_TouchUpInsideAsync(NSObject sender)
		{
			if (ValidateFields())
            {
				Auth.DefaultInstance.CreateUser(TxtMail.Text, TxtPassword.Text, AuthCreateUserResult);


            }
		}

		void AuthCreateUserResult(User user, NSError error)
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
					case AuthErrorCode.EmailAlreadyInUse:
                        ShowAlert("ERROR", "La cuenta de correo ingresada ya esta en uso.", null);
						break;
					case AuthErrorCode.InvalidEmail:
                        ShowAlert("ERROR", "Ingrese una direccion de correo valida.", null);
                        break;
					case AuthErrorCode.WeakPassword:
                        ShowAlert("ERROR", "Su contraseña es demasiado débil, debe incluir por lo menos seis cáracteres.", null);
                        break;
					default:
                        ShowAlert("ERROR", "No se pudo registrar el usuario intentelo más tarde.", null);
                        break;
                }

                return;
            }
   

			try
			{
				InsertUser(user.Uid);
            }
            catch (NSErrorException ex)
            {
				Console.WriteLine(ex.Message);
				ShowAlert("ERROR", "No se pudo registrar el usuario intentelo más tarde.",null);
                
            }
        }


        public static bool IsPhoneNumber(string number)
        {
            const string MatchPhonePattern =
                @"^(0|[1-9]\d*)$";
            Regex regex = new Regex(MatchPhonePattern);
            Match match = regex.Match(number);

            if (match.Success && number.Length <= 10 && number.Length >= 7)
            {
                return true;
            }
            return false;
        }
      
	

		partial void BtnCancel_TouchUpInside(NSObject sender)
		{
			DismissViewController(true, null);
		}

		void UserController_UserRegisted(object sender, UserInsertedEventArgs e)
		{
			bool wasInserted = e.WasInserted;
            if (wasInserted)
			{
				DismissViewController(true, null);
			}
            else
			{
				//TODO: alert error o usario repetido
			}
			Console.WriteLine(wasInserted);
		}


		void ShowAlert(string title, string message, Action func)
        {
            var okAlertController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
            okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

            InvokeOnMainThread(() => PresentViewController(okAlertController, true, func));

        }
	}
}

