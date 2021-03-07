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
using System.Windows.Shapes;

namespace MasterMind
{
    /// <summary>
    /// Logika interakcji dla klasy create.xaml
    /// </summary>
    public partial class create : Window
    {

        MainWindow mainwindow;
        public create(MainWindow mainwindow)
        {
            this.mainwindow = mainwindow;
            InitializeComponent();
            mainwindow.player.Load();
            for (int i = 0; i < 8; i++)
            {
                Button x = (Button)MainGrid.Children[i + 1];

                x.Background = mainwindow.generalColors[i];
            }
        }

        private void ClearThick() {
            for (int i = 0; i < 8; i++)
            {
                Button x = (Button)MainGrid.Children[i + 1];

                x.BorderThickness = new Thickness(1,1,1,1);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClearThick();
            mainwindow.onColorClick(sender, e);
            
        }
        private void ColorMe(object sender, RoutedEventArgs e) {
            mainwindow.ColorMe(sender,e);
        }

        private bool checkCode() {
            for (int i = 0; i < 4; i++) {
                for(int j = 0; j < 4; j++)
                {
                    if (i == j) continue;
                    if (mainwindow.code[i] == mainwindow.code[j]) return false;
                }
            }
            return true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                Button x = (Button)CodeGrid.Children[i];

                mainwindow.code[i] = x.Background;

                Console.WriteLine(x.Background);
            }

            if (checkCode())
            {
                mainwindow.player.Play();
                mainwindow.main_menu(false);
                this.Close();
            }
            else
            {
                MessageBox.Show("Kolory nie mogą się powtarzać", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
