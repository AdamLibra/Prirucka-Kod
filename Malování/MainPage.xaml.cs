using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.Storage;
using Windows.UI.Input.Inking;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;
using Windows.UI.Notifications;
using Microsoft.Toolkit.Uwp.Notifications;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Malování
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            myInkCanvas.InkPresenter.InputDeviceTypes = CoreInputDeviceTypes.Mouse | CoreInputDeviceTypes.Pen;
            SaveImage.Click += Button_Click;
            myInkCanvas.InkPresenter.StrokesCollected += InkPresenter_StrokesCollected;
            
        }
        
        async void Button_Click(object sender, RoutedEventArgs e)
        {
            IReadOnlyList<InkStroke> currentStrokes = myInkCanvas.InkPresenter.StrokeContainer.GetStrokes();
            if (currentStrokes.Count > 0)
            {
                FileSavePicker savePicker = new FileSavePicker()
                {
                    SuggestedStartLocation = PickerLocationId.Desktop,
                    SuggestedFileName = "MyInk",
                    DefaultFileExtension = ".png"
                };

                savePicker.FileTypeChoices.Add("PNG (*.png)", new List<string>() { ".png" });
                StorageFile file = await savePicker.PickSaveFileAsync();

                if (file != null)
                {
                    CachedFileManager.DeferUpdates(file);
                    IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.ReadWrite);
                    using (IOutputStream outputStream = stream.GetOutputStreamAt(0))
                    {
                        await myInkCanvas.InkPresenter.StrokeContainer.SaveAsync(outputStream);
                        await outputStream.FlushAsync();
                    }

                    stream.Dispose();
                    Windows.Storage.Provider.FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(file);
                    if (status == Windows.Storage.Provider.FileUpdateStatus.Complete)
                    {
                        // HOTOVO
                    }
                }
            }
            var content = new ToastContentBuilder()
             .AddText("Ukládání obrázku")
             .GetToastContent();

            var notif = new ToastNotification(content.GetXml());

            ToastNotificationManager.CreateToastNotifier().Show(notif);
        }

        async void InkPresenter_StrokesCollected(InkPresenter sender, InkStrokesCollectedEventArgs args)
        {
            MyList.Items.Clear();
            InkRecognizerContainer inkRecognizerContainer = new InkRecognizerContainer();
            if (inkRecognizerContainer != null)
            {
                IReadOnlyList<InkRecognitionResult> recognitionResults = await inkRecognizerContainer.RecognizeAsync(myInkCanvas.InkPresenter.StrokeContainer, InkRecognitionTarget.All);
                if (recognitionResults.Count > 0)
                    foreach (var result in recognitionResults)
                    {
                        IReadOnlyList<string> candidates = result.GetTextCandidates();
                        foreach (string candidate in candidates) MyList.Items.Add(candidate);
                    }
            }
        }
       
    }
}
