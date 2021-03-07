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
    /// Logika interakcji dla klasy loseWindow.xaml
    /// </summary>
    public partial class loseWindow : Window
    {
        MainWindow mainwindow;
        public loseWindow(MainWindow mainwindow)
        {
            this.mainwindow = mainwindow;
            InitializeComponent();
            c1.Fill = mainwindow.code[0];
            c2.Fill = mainwindow.code[1];
            c3.Fill = mainwindow.code[2];
            c4.Fill = mainwindow.code[3];

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainwindow.new_game();
            this.Close();
        }
    }
}
