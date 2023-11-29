using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace Multimedia
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Principal : Page
    {
        public Principal()
        {
            this.InitializeComponent();

            SystemNavigationManager.GetForCurrentView().BackRequested += opcionVolver;

            //tamaño mínimo de la ventana
            ApplicationView.GetForCurrentView().SetPreferredMinSize
            (new Size(320, 320));
            ApplicationView.GetForCurrentView().VisibleBoundsChanged
           += MainPage_VisibleBoundsChanged;

        }

        private void MainPage_VisibleBoundsChanged(ApplicationView sender, object args)
        {
            var Width = ApplicationView.GetForCurrentView().VisibleBounds.Width;
            //grande: menú abierto y lo demás
            if (Width >= 720)
            {
                sView.DisplayMode = SplitViewDisplayMode.CompactInline;
                sView.IsPaneOpen = true;
            }
            //se oculta el menú
            else if (Width >= 360)
            {
                sView.DisplayMode = SplitViewDisplayMode.CompactOverlay;
                sView.IsPaneOpen = false;
            }
            //se ocultan los iconos de menú
            else
            {
                sView.DisplayMode = SplitViewDisplayMode.Overlay;
                sView.IsPaneOpen = false;
            }

        }

        private void opcionVolver(object sender, BackRequestedEventArgs e)
        {
           
            if (fmMain.BackStack.Any())
            {
                fmMain.GoBack();
            }
        }


        private void btnMusica_Click(object sender, RoutedEventArgs e)
        {
            fmMain.Navigate(typeof(Musica));
        }

        private void btnTrailers_Click(object sender, RoutedEventArgs e)
        {
            fmMain.Navigate(typeof(Trailers));
        }

        private void btnVideoJuegos_Click(object sender, RoutedEventArgs e)
        {
            fmMain.Navigate(typeof(Videojuegos));
        }

        private void btnRecetas_Click(object sender, RoutedEventArgs e)
        {
            fmMain.Navigate(typeof(Recetas));
        }

        private void btnManualidades_Click(object sender, RoutedEventArgs e)
        {
            fmMain.Navigate(typeof(Manualidades));
        }

        private void desplegar_Click(object sender, RoutedEventArgs e)
        {
            sView.IsPaneOpen = !sView.IsPaneOpen;
        }
    }
}
