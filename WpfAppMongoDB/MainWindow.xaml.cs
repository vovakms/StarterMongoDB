using System;
using System.Collections.Generic;
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

using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;

//using Microsoft.WindowsAPICodePack.Dialogs;
 
namespace WpfAppMongoDB
{
     
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
 
            string[] zipF = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.zip");
            fileZIP.Content = zipF[0] ;

        }
  
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string zipPath     = fileZIP.Content.ToString() ;
            string extractPath = comBox1.Text ;
            ZipFile.ExtractToDirectory(zipPath, extractPath);
            var nn = ZipFile.OpenRead(zipPath).Entries;
            string[] str1 = nn[0].FullName.Split('/');
            Directory.Move(comBox1.Text + str1[0], comBox1.Text + "mongodb");
            Directory.CreateDirectory(comBox1.Text + @"mongodb\" + "db");
            Directory.CreateDirectory(comBox1.Text + @"mongodb\" + "log");

            string text  =  "##store data\n";
                   text +=  "dbpath = "+ comBox1.Text +  "mongodb\\db\n";
                   text += "\n";
                   text += "##all output go here\n";
                   text += "logpath = " + comBox1.Text + "mongodb\\log\\mongo.log\n";
                   text += "\n";
                   text += "##log read and write operations\n";
                   text += "diaglog = 3";
             
            string writePath = comBox1.Text + @"mongodb\bin\mongo.config";

            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(text);
                }
                 
                 StatusBarItem1.Content = "Запись mongo.config выполнена";
            }
            catch 
            {
                System.Windows.MessageBox.Show("Требуется ввести имя", "Ошибка при вводе имени", MessageBoxButton.OK, MessageBoxImage.Error);
            }
 
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (rBut1.IsChecked == true)
            {
                ProcessStartInfo psi;
                psi = new ProcessStartInfo("cmd", @"/k C:\mongodb\bin\mongod.exe --config C:\mongodb\bin\mongo.config");
                Process.Start(psi);
            }
        }
    
    }
}

 

//string extractPath = txtBox1.Text.ToString() ;

// string[] nameDir = extractPath.Split('\\');

// string reNameZIP = nameDir[nameDir.Length - 1] + ".zip";

// File.Move(zipPath,  ".\\"+ reNameZIP);
// int x = nameDir[nameDir.Length - 1].Length   ;
//extractPath = extractPath.Substring(0, extractPath.Length - x) ;
