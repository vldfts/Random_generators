using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace As_Cript_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AddFilesToCombox();
        }

        private void AddFilesToCombox()
        {
            string path = Directory.GetCurrentDirectory() + "\\data\\";
            if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path);
                foreach (string item in files)
                {
                    string item2 = item.Remove(0, Directory.GetCurrentDirectory().Length);
                    ComboBoxItem it = new ComboBoxItem();
                    it.Content = item2;
                    combox_files.Items.Add(it);
                }
            }
        }


        private void gen_click(object sender, RoutedEventArgs e)
        {
            int n = int.Parse(kilk.Text);
            result.BorderBrush = new SolidColorBrush(Colors.Gray);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            switch (generator.SelectedIndex)
            {
                case 0:
                    textBuffer.Text += Model.Vstr(n);
                    break;
                case 1:
                    textBuffer.Text += Model.Lehmer(true, n);
                    break;
                case 2:
                    textBuffer.Text += Model.Lehmer(false, n);
                    break;
                case 3:
                    textBuffer.Text += Model.L20(n);
                    break;
                case 4:
                    textBuffer.Text += Model.L89(n);
                    break;
                case 5:
                    textBuffer.Text += Model.Geffe(n);
                    break;
                case 6:
                    textBuffer.Text += Model.Biblio();
                    break;
                case 7:
                    textBuffer.Text += Model.Wolfram(n);
                    break;
                case 8:
                    textBuffer.Text += Model.BM(true, n);
                    break;
                case 9:
                    textBuffer.Text += Model.BM(false, n);
                    break;
                case 10:
                    textBuffer.Text += Model.BBS(true, n);
                    break;
                case 11:
                    textBuffer.Text += Model.BBS(false, n);
                    break;
                default: break;
            }
            sw.Stop();
            timer.Content = (double)sw.ElapsedMilliseconds/1000 +" сек";
        }

        private void start_button_Click(object sender, RoutedEventArgs e)
        {
            double[] sho_tam = new double[3];
            string bytePoslidovn = "";
            double kvatl_norm_rozpodil = 0;
            try
            {
                if (textBuffer.Text.Substring(0, 5).Contains(' '))
                {
                    bytePoslidovn = textBuffer.Text;
                }
                else
                {

                    bytePoslidovn = Model.Peretvor_Bit_To_Byte(new StringBuilder(textBuffer.Text));

                }
            }
            catch (Exception ex)
            {
                textBuffer.Text = ex.Message;
            }
            switch (kvantil.SelectedIndex)
            {
                case 0:
                    kvatl_norm_rozpodil = 2.326;
                    break;
                case 1:
                    kvatl_norm_rozpodil = 1.6449;
                    break;
                case 2:
                    kvatl_norm_rozpodil = 1.2816;
                    break;
                default:
                    break;
            }
            switch (test.SelectedIndex)
            {
                case 0:
                    sho_tam = Model.Rivnomirnist(new StringBuilder(bytePoslidovn), kvatl_norm_rozpodil);
                    break;
                case 1:
                    sho_tam = Model.Nezalezhnist(new StringBuilder(bytePoslidovn), kvatl_norm_rozpodil);
                    break;
                case 2:
                    sho_tam = Model.Odnoridnist(new StringBuilder(bytePoslidovn), kvatl_norm_rozpodil);
                    break;
                default:
                    break;
            }
            result.Content = "Пр " +Math.Round(sho_tam[0],3)+" -- "+ Math.Round(sho_tam[2], 3)+ " Теор";
            if (sho_tam[1] == 1)
            {
                result.BorderBrush = new SolidColorBrush(Colors.Green);
            }
            else
            {
                result.BorderBrush = new SolidColorBrush(Colors.Red);
            }
        }

        private void del_button_Click(object sender, RoutedEventArgs e)
        {
            textBuffer.Text = "";
            timer.Content = "";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                label_open_file.Foreground = new SolidColorBrush(Colors.Black);
                ComboBoxItem o = (ComboBoxItem)combox_files.SelectedItem;
                textBuffer.Text += File.ReadAllText(Directory.GetCurrentDirectory() + o.Content.ToString());
            }
            catch
            {
                label_open_file.Foreground = new SolidColorBrush(Colors.Red);
                textBuffer.Text = "Спочатку виберіть файл!!!";
            }

        }

        private void downl_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem o = (ComboBoxItem)combox_files.SelectedItem;
            using (StreamWriter sw = new StreamWriter(Directory.GetCurrentDirectory() + o.Content.ToString(), false))
            {
                sw.Write(textBuffer.Text);
            }
        }
    }
}
