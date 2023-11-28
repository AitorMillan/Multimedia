using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Devices.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;




// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace Multimedia
{

    public sealed partial class MainPage : Page
    {
        private Registro_Usuario datosusuario;
        private string usuario = "admin";
        private string password = "admin";

        private BitmapImage imagCheck = new BitmapImage(new Uri("ms-appx:///Assets/bien.png"));
        private BitmapImage imagCross = new BitmapImage(new Uri("ms-appx:///Assets/mal.png"));

        public MainPage()
        {
            this.InitializeComponent();
            datosusuario = new Registro_Usuario();

        }

        private void TextBoxUsuario_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (ComprobarEntrada(txtUsuario.Text, usuario))
            {
                passContra.Focus(FocusState.Programmatic);
            }
        }

        private void passContra_KeyUp(object sender, KeyRoutedEventArgs e)
        {

            if (ComprobarEntrada(passContra.Password, password))
            {
                btnLogin.Focus(FocusState.Programmatic);
            }
        }
        private async void OpenNewWindow()
        {
            CoreApplicationView newView = CoreApplication.CreateNewView();
            int newViewId = 0;

            await newView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Frame frame = new Frame();
                frame.Navigate(typeof(Registro_Usuario), null);
                Window.Current.Content = frame;
                // You have to activate the window in order to show it later.
                Window.Current.Activate();

                newViewId = ApplicationView.GetForCurrentView().Id;
            });

            bool viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newViewId);
        }

        private Boolean ComprobarEntrada(string valorIntroducido, string valorValido)
        {
            Boolean valido = false;
            if (valorIntroducido.Equals(valorValido))
            {
                valido = true;
            }
            else
            {

            // marcamos borde en rojo
            //componenteEntrada.Background = new SolidColorBrush(Colors.Red);
                lblError.Text = "El usuario o la contraseña introducidos son incorrectos";
                lblError.Visibility = Visibility.Visible;
            // imagen al lado de la entrada de usuario --> cross
            //imagenFeedBack.Source = imagCross;
                valido = false;
            }
            return valido;
        }

        private void RellenarCamposBien()
        {
            // Establecer colores e imágenes al hacer clic en el botón de inicio de sesión
            txtUsuario.Background = new SolidColorBrush(Colors.Green);
            imgCheckUsuario.Source = imagCheck;

            passContra.Background = new SolidColorBrush(Colors.Green);
            imgCheckContrasena.Source = imagCheck;
        }

        private void RellenarCamposMal()
        {
            // Establecer colores e imágenes al hacer clic en el botón de inicio de sesión
            txtUsuario.Background = new SolidColorBrush(Colors.Red);
            imgCheckUsuario.Source = imagCross;

            passContra.Background = new SolidColorBrush(Colors.Red);
            imgCheckContrasena.Source = imagCross;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (ComprobarEntrada(txtUsuario.Text, usuario)
                &&
                (ComprobarEntrada(passContra.Password, password)))
            {
                RellenarCamposBien();
            }else
            {
                RellenarCamposMal();
            }

            OpenNewWindow1();

        }

        private async void OpenNewWindow1()
        {
            CoreApplicationView newView = CoreApplication.CreateNewView();
            int newViewId = 0;

            await newView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Frame frame = new Frame();
                frame.Navigate(typeof(Principal), null);
                Window.Current.Content = frame;
                // You have to activate the window in order to show it later.
                Window.Current.Activate();

                newViewId = ApplicationView.GetForCurrentView().Id;
            });

            bool viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newViewId);
        }

        private void btnRegistro_Click(object sender, RoutedEventArgs e)
        {
            OpenNewWindow();
        }
    }
}
