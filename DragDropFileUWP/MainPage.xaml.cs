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

using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DragDropFileUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            fillTree1();
        }

        private void fillTree1()
        {
            TreeViewNode node = new TreeViewNode { Content = "Root", IsExpanded = true };
            treeView1.RootNodes.Add(node);
        }

        private void grid_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
            e.DragUIOverride.Caption = "Open";
        }

        private async void grid_Drop(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                var items = await e.DataView.GetStorageItemsAsync();
                if (items.Count > 0)
                {
                    List<StorageFile> filesToOpen = new List<StorageFile>();
                    for (int i = 0; i < items.Count; i++)
                    {
                        var storageFile = items[i] as StorageFile;
                        string filePath = storageFile.Path;

                        TreeViewNode node = new TreeViewNode { Content = filePath };
                        treeView1.RootNodes[0].Children.Add(node);
                    }
                }
            }
        }
    }
}
