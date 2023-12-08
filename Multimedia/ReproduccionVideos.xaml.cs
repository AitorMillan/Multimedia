using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Xml;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Text;
using Windows.UI.Popups;
using Windows.System;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace Multimedia
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class ReproduccionVideos : Page
    {
        ObservableCollection<Comentario> Comentarios { get; set; }
        string XMLFilePath = ApplicationData.Current.LocalFolder.Path + "/Comentarios.xml";
        public ReproduccionVideos()
        {
            this.InitializeComponent();
            Comentarios = new ObservableCollection<Comentario>();
            listaComentarios.ItemsSource = Comentarios;
        }

        public void iniciarVideo(String nombreVideo)
        {
            mediaElementVideo.Source = new Uri("ms-appx:///Assets/" + nombreVideo + ".mp4");

        }


        private void MyFrame_NavigatedFrom(object sender, NavigationEventArgs e)
        {
            mediaElementVideo.Pause();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is Tuple<string, string> data)
            {
                String nombreVideo = data.Item1;
                txtDescripcion.Text = data.Item2;

                iniciarVideo(nombreVideo);
                Comentarios.Clear();
                cargarComentarios(nombreVideo);

                txtBlockTitulo.Text = nombreVideo;

                //Controlamos que cuando se cambie de pagina se pare la musica
                this.Frame.Navigated += MyFrame_NavigatedFrom;
            }
        }

        private void cargarComentarios(String nombreVideo)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(XMLFilePath);

            XmlNodeList comentNodes = doc.SelectNodes("/Comentarios/Comentario");
            foreach (XmlNode node in comentNodes)
            {
                if (node.Attributes["Video"]?.Value == nombreVideo)
                {
                    Comentario comentario = new Comentario
                    {
                        Texto = node.Attributes["Texto"]?.Value,
                        User = node.Attributes["User"]?.Value
                    };
                    Comentarios.Add(comentario);
                }
            }

        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }

        }

        private async void btnDescargaVideos_Click(object sender, RoutedEventArgs e)
        {
            // Aquí deberías tener la lógica para obtener el nombre del video que estás reproduciendo
            string nombreVideo = txtBlockTitulo.Text;

            // Llamamos al método de descarga
            await DescargarVideo(nombreVideo);
        }

        // Método para descargar el video
        private async Task DescargarVideo(string nombreVideo)
        {
            try
            {
                // Obtener la carpeta "Assets"
                StorageFolder assetsFolder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Assets");

                // Obtener el archivo del video desde la carpeta "Assets"
                StorageFile videoFile = await assetsFolder.GetFileAsync(nombreVideo + ".mp4");

                // Configurar el cuadro de diálogo para la ubicación de descarga
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.Downloads;
                savePicker.FileTypeChoices.Add("Video", new[] { ".mp4" });
                savePicker.SuggestedFileName = nombreVideo;

                // Mostrar el cuadro de diálogo y obtener la ubicación seleccionada por el usuario
                StorageFile destino = await savePicker.PickSaveFileAsync();

                // Copiar el archivo al destino seleccionado por el usuario
                if (destino != null)
                {
                    await videoFile.CopyAndReplaceAsync(destino);
                    MostrarMensaje("Descarga exitosa");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al descargar el video: {ex.Message}");
            }
        }

        private async void MostrarMensaje(string mensaje)
        {
            var mensajeDialog = new Windows.UI.Popups.MessageDialog(mensaje, "Información");
            await mensajeDialog.ShowAsync();
        }

        private void btnEnviar_Click(object sender, RoutedEventArgs e)
        {
            String texto = txtComentario.Text.Trim();
            String username = SessionState.Username;

            XmlDocument doc = new XmlDocument();

            if (texto == "")
            {
                return;
            }
            Comentario nuevoComentario = new Comentario()
            {
                Texto = texto,
                User = username
            };

            Comentarios.Add(nuevoComentario);
            doc.Load(XMLFilePath);
            XmlElement coment = doc.CreateElement("Comentario");
            coment.SetAttribute("Video", txtBlockTitulo.Text);
            coment.SetAttribute("User", username);
            coment.SetAttribute("Texto", texto);


            // Obtener el elemento raíz del documento XML
            XmlElement raiz = doc.DocumentElement;

            // Agregar el nuevo elemento Excursionista al elemento raíz
            raiz.AppendChild(coment);

            // Guardar los cambios en el archivo XML
            doc.Save(XMLFilePath);
            }
        }
    }


