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
    public sealed partial class Navigace : Page
    {
        public Navigace()
        {
            this.InitializeComponent();
            ObsahStranky.Navigate(typeof(MainPage)); //Nastaví po načtení výchozí stránku na MainPage
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {

            if (args.IsSettingsSelected)
            {
                //když se klikne na nastavení, zde něco udělej
            }
            else
            {
                NavigationViewItem polozka = args.SelectedItem as NavigationViewItem; // do proměné "polozka" uloží to na co uživatel kliknul

                switch (polozka.Tag.ToString())
                {
                    case "hlStranka":
                        ObsahStranky.Navigate(typeof(MainPage));
                        break;
                    case "vedlejsiStranka":
                        ObsahStranky.Navigate(typeof(DruhaStranka));
                        break;
                    case "oAplikaciStranka":
                        //můžeme si vytvořit další stránku a přesměrovat se na ni zde.
                        break;
                    default:
                        break;
                }
            }
        }

        //Tato metoda se zavolá pokaždé co klikneme na Back tlačítko v navigaci

        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            try
            {
                ObsahStranky.GoBack();
            }
            catch (Exception)
            {
                
            }
        }
    }
}
