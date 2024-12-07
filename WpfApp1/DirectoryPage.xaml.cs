using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using Ookii.Dialogs.Wpf;
using Models;



namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for DirectoryPage.xaml
    /// </summary>
    public partial class DirectoryPage : UserControl
    {
        public ObservableCollection<FolderItem> Folders = new ObservableCollection<FolderItem>();
        public DirectoryPage()
        {
            InitializeComponent();
            Directories_ListView.ItemsSource = Folders;
        }

        private void AddDirectory_Click(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog dialog = new OpenFileDialog();
            //dialog.Title = "Select File";
            //dialog.InitialDirectory = @"C:\";
            //dialog.Filter = "All files (*.*)|*.*|Text File (*.mp3)|*.mp3";
            //dialog.FilterIndex = 2;
            //dialog.ShowDialog(); 

            var dialog = new VistaFolderBrowserDialog
            {
                Description = "Select a folder",
                UseDescriptionForTitle = true,
                RootFolder = Environment.SpecialFolder.MyDocuments
            };
            if(dialog.ShowDialog() == true)
            {
                string selectedPath = dialog.SelectedPath;
                Folders.Add(new FolderItem { FolderPath = selectedPath });
            }

        }

        private void RemoveDirectory_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void LoadDirectory() { 
        
        }
    }

}
