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
using Windows.Storage;
using System.Xml;
using System.Security.Cryptography;
using System.Text;
using Windows.UI.StartScreen;




// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace Multimedia
{

    public sealed partial class MainPage : Page
    {
        private Registro_Usuario datosusuario;
        StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        string XMLFilePath = ApplicationData.Current.LocalFolder.Path + "/usuarios.xml";

        private BitmapImage imagCheck = new BitmapImage(new Uri("ms-appx:///Assets/bien.png"));
        private BitmapImage imagCross = new BitmapImage(new Uri("ms-appx:///Assets/mal.png"));

        public MainPage()
        {
            this.InitializeComponent();
            PrepararArchivo("usuarios.xml");
            PrepararArchivo("Comentarios.xml");
            datosusuario = new Registro_Usuario();

            saludo_inicial();

        }

        private async void PrepararArchivo(String nombreArchivo)
        {
            StorageFile file;

            try
            {
                file = await localFolder.GetFileAsync(nombreArchivo);
            }
            catch (FileNotFoundException)
            {
                // El archivo no existe en LocalFolder, así que lo copiamos desde el paquete
                StorageFile initialFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/"+nombreArchivo));
                // Usa ReplaceExisting para sobrescribir si ya existe
                file = await initialFile.CopyAsync(localFolder, nombreArchivo, NameCollisionOption.ReplaceExisting);
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


        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            String username = txtUsuario.Text;
            String pwd = passContra.Password;

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
            XmlNodeList usersNodes = doc.SelectNodes("/Usuarios/Usuario");
            foreach (XmlNode node in usersNodes)
            {
                String nombre = node.Attributes["Username"].Value;
                String contra = node.Attributes["Pwd"].Value;
                if (nombre == username && builder.ToString() == contra)
                {
                    lblError.Text = "Login exitoso";
                    lblError.Visibility = Visibility.Visible;
                    SessionState.Username = username;
                }

            }

            abrirPestañaPrincipal();

        }

        private async void abrirPestañaPrincipal()
        {
            CoreApplicationView newView = CoreApplication.CreateNewView();
            int newViewId = 0;
            int currentViewId = ApplicationView.GetForCurrentView().Id;
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

            if (viewShown)
            {
                await ApplicationViewSwitcher.SwitchAsync(newViewId, currentViewId, ApplicationViewSwitchingOptions.ConsolidateViews);
            }
        }

        private void btnRegistro_Click(object sender, RoutedEventArgs e)
        {
            OpenNewWindow();
        }

        private void passContra_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }

        private async void saludo_inicial()
        {
            MediaElement mediaElement = new MediaElement();
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
            Windows.Media.SpeechSynthesis.SpeechSynthesisStream stream = await
           synth.SynthesizeTextToStreamAsync("Estás en la aplicación de " +
           "ESITube 2023. Bienvenido!!!");
            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();
        }


    }
}
