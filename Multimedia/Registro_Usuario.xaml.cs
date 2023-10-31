using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace Multimedia
{
    public class Usuario
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }

        public Usuario(string nombre, string apellidos, string correo, string contraseña)
        {
            Nombre = nombre;
            Apellidos = apellidos;
            Correo = correo;
            Contraseña = contraseña;
        }
    }
    public sealed partial class Registro_Usuario : Page
    {
        public Registro_Usuario()
        {
            this.InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NombreApellidos_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                lblEstado.DataContext = "Se pulsó el enter ";
                Contraseña.IsEnabled = true;
                Contraseña.Focus(FocusState.Programmatic);
            }
        }

        private void Username_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                lblEstado.DataContext = "Se pulsó el enter ";
                Contraseña.IsEnabled = true;
                Contraseña.Focus(FocusState.Programmatic);
            }
        }

        private void Correo_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                lblEstado.DataContext = "Se pulsó el enter ";
                Contraseña.IsEnabled = true;
                Contraseña.Focus(FocusState.Programmatic);
            }
        }
    }
}
