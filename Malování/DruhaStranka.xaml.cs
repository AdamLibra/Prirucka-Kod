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

// Dokumentaci k šabloně Prázdná aplikace najdete na adrese https://go.microsoft.com/fwlink/?LinkId=234238

namespace Malování
{
    /// <summary>
    /// Prázdná stránka, která se dá použít samostatně nebo se na ni dá přejít v rámci
    /// </summary>
    public sealed partial class DruhaStranka : Page
    {
        public DruhaStranka()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TextBlock textBlock1 = new TextBlock();
            textBlock1.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            textBlock1.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
            textBlock1.Text = "Ahoj světe!";
            textBlock1.FontSize = 40;
            mrizka.Children.Add(textBlock1);
        }
    }
}
