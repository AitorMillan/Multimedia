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

            if (e.Parameter is Tuple<string> data)
            {
                String numJugadores = data.Item1;
                iniciarVideo(numJugadores);

                //Controlamos que cuando se cambie de pagina se pare la musica
                this.Frame.Navigated += MyFrame_NavigatedFrom;
            }
        }
    }
}
