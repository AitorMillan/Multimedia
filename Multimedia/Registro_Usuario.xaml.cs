using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
        public string Username { get; set; }
        public string Contraseña { get; set; }

        public Usuario(string nombre, string username, string contraseña)
        {
            Nombre = nombre;
            Username = username;
            Contraseña = contraseña;
        }
    }
    public sealed partial class Registro_Usuario : Page
    {
        private List<Usuario> usuarios = new List<Usuario>();

        public Registro_Usuario()
        {
            this.InitializeComponent();
        }

        private bool UsuarioYaRegistrado(string nombreyapellidos, string username)
        {
            // Verificar si ya existe un usuario con el mismo nombre/apellidos o username
            return usuarios.Any(u => u.Nombre == nombreyapellidos || u.Username == username);
        }

        private void GuardarUsuario()
        {
            // Obtener datos de los campos
            string nombreyapellidos = "si";
            string username = Username.Text;
            string contraseña = Contraseña.Text;

            // Validar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(nombreyapellidos) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(contraseña))
            {
                MostrarMensaje("Por favor, complete todos los campos.");
                return;
            }

            // Verificar si el usuario ya está registrado
            if (UsuarioYaRegistrado(nombreyapellidos, username))
            {
                MostrarMensaje("Este usuario ya está registrado.");
                return;
            }

            // Crear un nuevo usuario
            Usuario nuevoUsuario = new Usuario(nombreyapellidos, username, contraseña);

            // Agregar el nuevo usuario a la lista
            usuarios.Add(nuevoUsuario);

            // Aquí puedes guardar la lista de usuarios en algún lugar (por ejemplo, en un archivo)
            // GuardarUsuariosEnArchivo();

            // Mostrar un mensaje de éxito
            MostrarMensaje("Usuario registrado con éxito.");

            // También puedes realizar otras acciones, como navegar a otra página, dependiendo de tus necesidades.
        }


        private async void MostrarMensaje(string mensaje)
        {
            var mensajeDialog = new MessageDialog(mensaje, "Información");
            await mensajeDialog.ShowAsync();
        }

        private void NombreApellidos_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            // Verificar si la tecla presionada es "Enter"
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                
            }
        }

        private void Username_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            // Verificar si la tecla presionada es "Enter"
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                
            }
        }

        private void Contraseña_PreviewKeyDown(object sender, KeyRoutedEventArgs e)
        {
            // Verificar si la tecla presionada es "Enter"
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                
            }
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            GuardarUsuario();
        }
    }
}
