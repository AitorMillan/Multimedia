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

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace Multimedia
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class ReproduccionVideos : Page
    {
        public ReproduccionVideos()
        {
            this.InitializeComponent();
        }

        public void iniciarVideo(String nombreCancion)
        {
            // Obtenemos la canción, ajustamos su sonido, la ponemos en bucle y la reproducimos
            mediaElementVideo.Source = new Uri("ms-appx:///Assets/" + nombreCancion + ".mp4");

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

                txtBlockTitulo.Text = nombreVideo;

                //Controlamos que cuando se cambie de pagina se pare la musica
                this.Frame.Navigated += MyFrame_NavigatedFrom;
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

    }

}

