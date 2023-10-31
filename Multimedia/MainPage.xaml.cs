using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace Multimedia
{
    Datos_Usuario datosusuario;
    private string usuario = "admin";
    private string password = "admin";
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            datosusuario = new Datos_Usuario();
        }

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            // se hará la comprobación al pulsar el "Enter"
            if (e.Key == Key.Return)
            {
                lblEstado.Content = "Se pulsó el enter ";
                if (ComprobarEntrada(txtUsuario.Text, usuario,
                txtUsuario, imgCheckUsuario))
                {
                    // habilitar entrada de contraseña y pasarle el foco
                    passContra.IsEnabled = true;
                    passContra.Focus();
                    // deshabilitar entrada de login
                    txtUsuario.IsEnabled = false;
                }
            }
        }

        private void imgAvatar_MouseEnter(object sender, MouseEventArgs e)
        {
            lblEstado.Content = "Entrando en el avatar y cambiando imagen";
            icono_login.Source = imagRollOver;
        }

        private void imgAvatar_MouseLeave(object sender, MouseEventArgs e)
        {
            lblEstado.Content = "Saliendo del avatar y restaurando la imagen";
            icono_login.Source = imagOriginal;
        }

        private void pnlDisenoPrincipal_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lblEstado.Content = "[" + e.GetPosition(this).X +
                "," + e.GetPosition((this)).Y + "]";
        }

        private void passContra_KeyUp(object sender, KeyEventArgs e)
        {
            lblEstado.Content = "Has pulsado la tecla <<" + e.Key.ToString() + ">>";
            if (ComprobarEntrada(passContra.Password, password,
            passContra, imgCheckContrasena))
                btnLogin.Focus();
        }


        private void VentanaPrincipal_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBox.Show("Gracias por usar nuestra aplicación...", "Despedida");
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (ComprobarEntrada(txtUsuario.Text, usuario,
                    txtUsuario, imgCheckUsuario)
                &&
                ComprobarEntrada(passContra.Password, password,
                    passContra, imgCheckContrasena))
            {
                datosusuario.Show();
                this.Hide();
            }
        }
        private Boolean ComprobarEntrada(string valorIntroducido, string valorValido,
        Control componenteEntrada, Image imagenFeedBack)
        {
            Boolean valido = false;
            if (valorIntroducido.Equals(valorValido))
            {
                // borde y background en verde
                componenteEntrada.BorderBrush = Brushes.Green;
                componenteEntrada.Background = Brushes.LightGreen;
                // imagen al lado de la entrada de usuario --> check
                imagenFeedBack.Source = imagCheck;
                valido = true;
            }
            else
            {
                // marcamos borde en rojo
                componenteEntrada.BorderBrush = Brushes.Red;
                componenteEntrada.Background = Brushes.LightCoral;
                // imagen al lado de la entrada de usuario --> cross
                imagenFeedBack.Source = imagCross;
                valido = false;
            }
            return valido;
        }

    }
}
