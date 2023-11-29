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
    public sealed partial class Musica : Page
    {
        public Musica()
        {
            this.InitializeComponent();
        }

        private void videoSeleccionado(String nombreVideo)
        {
            Frame.Navigate(typeof(ReproduccionVideos), new Tuple<String>(nombreVideo));
        }

        private void imgVideo1_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            videoSeleccionado(textBox1.Text);
        }

        private void textBox1_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            videoSeleccionado(textBox1.Text);
        }

        private void imgVideo2_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            videoSeleccionado(textBox2.Text);
        }

        private void textBox2_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            videoSeleccionado(textBox2.Text);
        }

        private void imgVideo3_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            videoSeleccionado(textBox3.Text);
        }

        private void textBox3_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            videoSeleccionado(textBox3.Text);
        }

        private void imgVideo4_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            videoSeleccionado(textBox4.Text);
        }

        private void textBox4_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            videoSeleccionado(textBox4.Text);
        }
    }
}
