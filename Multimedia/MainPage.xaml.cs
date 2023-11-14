using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;



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
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                if (ComprobarEntrada(txtUsuario.Text, usuario,
                txtUsuario, imgCheckUsuario))
                {
                    // habilitar entrada de contraseña y pasarle el foco
                    passContra.IsEnabled = true;
                    passContra.Focus(FocusState.Programmatic);
                    // deshabilitar entrada de login
                    txtUsuario.IsEnabled = false;
                }
            }
        }
        private void passContra_KeyUp(object sender, KeyRoutedEventArgs e)
        {
           
            if (ComprobarEntrada(passContra.Password, password, passContra, imgCheckContrasena))
            {
                btnLogin.Focus(FocusState.Programmatic);
            }
        }

        private Boolean ComprobarEntrada(string valorIntroducido, string valorValido,
        Control componenteEntrada, Image imagenFeedBack)
        {
            Boolean valido = false;
            if (valorIntroducido.Equals(valorValido))
            {
                // borde y background en verde
                componenteEntrada.Background = new SolidColorBrush(Colors.Green);
                // imagen al lado de la entrada de usuario --> check
                imagenFeedBack.Source = imagCheck;
                valido = true;
            }
            else
            {
            // marcamos borde en rojo
            componenteEntrada.Background = new SolidColorBrush(Colors.Red);
            // imagen al lado de la entrada de usuario --> cross
            imagenFeedBack.Source = imagCross;
                valido = false;
            }
            return valido;
        }

        private async void VentanaPrincipal_Unloaded(object sender, RoutedEventArgs e)
        {
            var messageDialog = new MessageDialog("Gracias por usar nuestra aplicación...", "Despedida");
            await messageDialog.ShowAsync();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (ComprobarEntrada(txtUsuario.Text, usuario,
                    txtUsuario, imgCheckUsuario)
                &&
                ComprobarEntrada(passContra.Password, password,
                    passContra, imgCheckContrasena))
            {
                //Pantalla correspondiente, todavía por definir
            }
        }

    }
}
