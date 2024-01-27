using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
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

namespace felveteli
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>+
    public partial class MainWindow : Window
    {
        ObservableCollection<IFelvetelizo> Datas = new ObservableCollection<IFelvetelizo>();
        public MainWindow()
        {
            InitializeComponent();
            ReadData();
            Dg_Students.ItemsSource = Datas;
        }
        public void ReadData()
        {
            string Data_file = "felvetelizok.csv";
            StreamReader sr = new StreamReader(Data_file);

            sr.ReadLine();

            while (!sr.EndOfStream)
            {
                string[] fields = sr.ReadLine().Split(';');


                Kuldo userData = new Kuldo(
                    fields[0],
                    fields[1],
                    fields[2],
                    Convert.ToDateTime(fields[3]),
                    fields[4],
                    int.Parse(fields[5]),
                    int.Parse(fields[6])
                );

                Datas.Add(userData);
            }
            sr.Close();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Kuldo ujdiak = new Kuldo();


            UjDiak w = new UjDiak(ujdiak);
            w.ShowDialog();

            if (ujdiak.Neve != null && ujdiak.ErtesitesiCime != null && ujdiak.Magyar.ToString().Length > 0 && ujdiak.Matematika.ToString().Length > 0 && ujdiak.Email != null && ujdiak.OM_Azonosito != null)
            {
                Datas.Add(ujdiak);
            }




        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                StreamReader sr = new StreamReader(fileDialog.FileName);

                sr.ReadLine();
                if (System.IO.Path.GetExtension(fileDialog.FileName).Trim() == ".csv")
                {
                    var Result = MessageBox.Show("Akarod, hogy felülírd az előző adatokat?", "Felülírás", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (Result == MessageBoxResult.Yes)
                    {


                        Datas.Clear();
                        while (!sr.EndOfStream)
                        {
                            string[] fields = sr.ReadLine().Split(';');


                            Kuldo userData = new Kuldo(
                            fields[0],
                            fields[1],
                            fields[2],
                            Convert.ToDateTime(fields[3]),
                            fields[4],
                            int.Parse(fields[5]),
                            int.Parse(fields[6])
                            );

                            Datas.Add(userData);
                        }
                        sr.Close();
                    }
                    else
                    {

                        while (!sr.EndOfStream)
                        {
                            string[] fields = sr.ReadLine().Split(';');


                            Kuldo userData = new Kuldo(
                            fields[0],
                            fields[1],
                            fields[2],
                            Convert.ToDateTime(fields[3]),
                            fields[4],
                            int.Parse(fields[5]),
                            int.Parse(fields[6])
                            );

                            Datas.Add(userData);
                        }
                        sr.Close();
                    }
                }
                else if (System.IO.Path.GetExtension(fileDialog.FileName).Trim() == ".json")
                {
                    var Result = MessageBox.Show("Akarod, hogy felülírd az előző adatokat?", "Felülírás", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (Result == MessageBoxResult.Yes)
                    {
                        Datas.Clear();
                        string jsonString = File.ReadAllText(fileDialog.FileName);
                        var serializeOpt = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        List<Kuldo> importData = JsonSerializer.Deserialize<List<Kuldo>>(jsonString, serializeOpt);

                        if (importData != null)
                        {
                            foreach (var adat in importData)
                            {
                                Datas.Add(adat);
                            }
                        }
                    }
                    else
                    {
                        string jsonString = File.ReadAllText(fileDialog.FileName);
                        var serializeOpt = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        List<Kuldo> importData = JsonSerializer.Deserialize<List<Kuldo>>(jsonString, serializeOpt);

                        if (importData != null)
                        {
                            foreach (var adat in importData)
                            {
                                Datas.Add(adat);
                            }
                        }
                    }


                }





            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Csv file|*.csv|JSON file|*.json";
            fileDialog.ShowDialog();
            string filePath = "";
            if (fileDialog.FileName != "")
            {
                switch (fileDialog.FilterIndex)
                {
                    case 1:
                        filePath = $"{fileDialog.SafeFileName}";
                        break;
                    case 2:
                        filePath = $"{fileDialog.SafeFileName}";
                        break;
                }
            }
            StreamWriter sw = new StreamWriter(filePath);
            foreach (var item in Datas)
            {
                sw.WriteLine($"{item.OM_Azonosito};{item.Neve};{item.ErtesitesiCime};{item.Email};{item.SzuletesiDatum};{item.Matematika};{item.Magyar}");
            }
            sw.Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (Dg_Students.SelectedItems != null && Dg_Students.SelectedItems.Count > 0)
            {
                var toRemove = Dg_Students.SelectedItems.Cast<IFelvetelizo>().ToList();
                var items = Dg_Students.ItemsSource as ObservableCollection<IFelvetelizo>;
                if (items != null)
                {
                    foreach (var order in toRemove)
                    {
                        items.Remove(order);
                    }
                }
            }
        }
        private void btnModify_Click(object sender, RoutedEventArgs e)
        {


            Kuldo selectedItem = Dg_Students.SelectedItem as Kuldo;
            Datas.Remove(selectedItem);
            if (selectedItem != null)
            {

                UjDiak w = new UjDiak(selectedItem);
                w.ShowDialog();
            }

            Datas.Add(selectedItem);

        }
    }
}

