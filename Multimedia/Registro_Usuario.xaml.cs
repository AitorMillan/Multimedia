using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public sealed partial class Registro_Usuario : Page
    {
        StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        string XMLFilePath = ApplicationData.Current.LocalFolder.Path + "/usuarios.xml";

        public Registro_Usuario()
        {
            this.InitializeComponent();
            PrepararArchivo();
        }

        private async void PrepararArchivo()
        {
            StorageFile file;

            try
            {
                file = await localFolder.GetFileAsync("usuarios.xml");
            }
            catch (FileNotFoundException)
            {
                // El archivo no existe en LocalFolder, así que lo copiamos desde el paquete
                StorageFile initialFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/usuarios.xml"));
                // Usa ReplaceExisting para sobrescribir si ya existe
                file = await initialFile.CopyAsync(localFolder, "usuarios.xml", NameCollisionOption.ReplaceExisting);
            }
        }


        private async void MostrarMensaje(string mensaje)
        {
            var mensajeDialog = new MessageDialog(mensaje, "Información");
            await mensajeDialog.ShowAsync();
        }

        private async Task<bool> comprobarUsuario(XmlDocument doc, String username)
        {
            XmlNodeList usersNodes = doc.SelectNodes("/Usuarios/Usuario");
            foreach (XmlNode node in usersNodes)
            {
                string nombre = node.Attributes["Username"].Value;
                if (nombre == username)
                {
                    // Crear y mostrar el cuadro de diálogo
                    MessageDialog dialog = new MessageDialog("Ya existe un usuario con el mismo nombre de usuario.", "Error al registrar usuario");
                    await dialog.ShowAsync();

                    return true; // Retorna falso si se encuentra el usuario
                }
            }

            doc.Save(XMLFilePath);
            return false;
        }

        private async void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            String username = txtBoxUsername.Text;
            String pwd = txtBoxContraseña.Text;
            
            if (username == "" || pwd == "")
            {
                MessageDialog dialog = new MessageDialog("El campo usuario o contraseña está vacío.", "Error al registrar usuario");
                await dialog.ShowAsync();
                return;
            }

            SHA256 sha = SHA256.Create();
            byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(pwd));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            doc.Load(XMLFilePath);
            if (!await comprobarUsuario(doc, username))
            {

                XmlElement user = doc.CreateElement("Usuario");
                user.SetAttribute("Username", username);
                user.SetAttribute("Pwd", builder.ToString());


                // Obtener el elemento raíz del documento XML
                XmlElement raiz = doc.DocumentElement;

                // Agregar el nuevo elemento Excursionista al elemento raíz
                raiz.AppendChild(user);

                // Guardar los cambios en el archivo XML
                doc.Save(XMLFilePath);
            }
        }
    }
}
