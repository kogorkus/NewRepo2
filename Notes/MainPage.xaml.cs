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
using Windows.Storage;
using Windows.UI.Popups;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace Notes
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            FileList.Text = "";
        }
        StorageFolder localFolder;
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            localFolder = ApplicationData.Current.LocalFolder;
            StorageFolder storageFolder = KnownFolders.PicturesLibrary;
            IReadOnlyList<StorageFile> files = await storageFolder.GetFilesAsync();
            foreach (StorageFile file in files)
            {
                var props = await file.GetBasicPropertiesAsync();
                FileList.Text += ("[" + file.Name + "]\n");
                FileList.Text += ("Размер " + (props.Size /1024.0).ToString("F2")  + " Кб\n" );
            }
        }

        private async void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            
            await localFolder.CreateFileAsync("settings.txt", CreationCollisionOption.ReplaceExisting);
            TbEnter.Text = localFolder.Path.ToString();
            StorageFile firstFile = await localFolder.GetFileAsync("settings.txt");
            await FileIO.WriteTextAsync(firstFile, "texttexttext");
            TbEnter.Text = firstFile.ToString();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            StorageFile firstFile = await localFolder.GetFileAsync("settings.txt");
            TbEnter.Text = await FileIO.ReadTextAsync(firstFile);
            await new MessageDialog("Files`s opend").ShowAsync();
        }
    }
}
