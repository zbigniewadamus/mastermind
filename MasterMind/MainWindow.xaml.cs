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
using System.IO;
using System.Media;
using System.Threading;
using System.Reflection;
using System.Runtime;


namespace MasterMind
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //public SoundPlayer player = new SoundPlayer("../../sounds/click_sound.wav");
        public SoundPlayer player = new SoundPlayer("sounds/click_sound.wav");
        //public SoundPlayer bg_player = new SoundPlayer("../../sounds/bg_music.wav");
        public MediaPlayer bg_music = new MediaPlayer();

        BrushConverter conv = new BrushConverter();
        public Brush[] generalColors = new Brush[8];
        
        public Brush[] code = new Brush[4];
        //{So 0xFFB71612, 0xFF917BE7, #FF25098E, #FFC68642}; 
        Brush selectedColor;
        string[,] attempts = new string[6, 4];
        int level = 0;
        


        public MainWindow()
        {
            InitializeComponent();
            main_menu(true);
            player.Load();
            

            bg_music.Open(new Uri(@"sounds\bg_music.wav",UriKind.RelativeOrAbsolute));
            bg_music.Volume = 0.15f;
            bg_music.Play();
           
            Console.WriteLine(c1.Background);
            Console.WriteLine(c2.Background);
            Console.WriteLine(c3.Background);
            Console.WriteLine(c4.Background);
            Console.WriteLine(c5.Background);
            Console.WriteLine(c6.Background);
            Console.WriteLine(c7.Background);
            Console.WriteLine(c8.Background);


            ToggleAttempts();


            generalColors[0] = (Brush)conv.ConvertFromString("#FF71EB18");
            generalColors[1] = (Brush)conv.ConvertFromString("#FFB71612");
            generalColors[2] = (Brush)conv.ConvertFromString("#FFDDC103");
            generalColors[3] = (Brush)conv.ConvertFromString("#FF917BE7");
            generalColors[4] = (Brush)conv.ConvertFromString("#FF20A0BE");
            generalColors[5] = (Brush)conv.ConvertFromString("#FF25098E");
            generalColors[6] = (Brush)conv.ConvertFromString("#FFDD365A");
            generalColors[7] = (Brush)conv.ConvertFromString("#FFC68642");
        }


        private void WinScreen() {
            for (int i = 1; i < GlobalGrid.Children.Count; i++)
            {
                GlobalGrid.Children[i].Visibility = System.Windows.Visibility.Hidden;
            }
            bg_music.Pause();
            
            winLabel.Visibility = System.Windows.Visibility.Visible;
            tryagainButton.Visibility = System.Windows.Visibility.Visible;
            winGrid.Visibility = System.Windows.Visibility.Visible;
            for (int i = 0; i < winGrid.Children.Count; i++)
            {
                winGrid.Children[i].Visibility = System.Windows.Visibility.Visible;
            }

            cW1.Fill = code[0];
            cW2.Fill = code[1];
            cW3.Fill = code[2];
            cW4.Fill = code[3];
            SoundPlayer win_player = new SoundPlayer("sounds/win_sound.wav");
            win_player.Load();
            win_player.Play();
            
        }

        public void letColor() {
            for (int i = 0;i<8;i++) {
                Button x = (Button)GlobalGrid.Children[i + 1];  
                x.Background = generalColors[i];
            }
        }
        public void new_game()
        {
            main_menu(true);
            level = 0;
            selectedColor = null;
            ToggleAttempts();
            ClearThickness();
            for (int i = 0; i < 6; i++)
            {
                Grid clearGrid = (Grid)GlobalGrid.Children[i + 9];
                Button[] clearButton = new Button[] { (Button)clearGrid.Children[1], (Button)clearGrid.Children[2], (Button)clearGrid.Children[3], (Button)clearGrid.Children[4] };
                Ellipse[] tips = new Ellipse[] { (Ellipse)clearGrid.Children[5], (Ellipse)clearGrid.Children[6], (Ellipse)clearGrid.Children[7], (Ellipse)clearGrid.Children[8] };
                clearButton[0].Background = Brushes.White;
                clearButton[1].Background = Brushes.White;
                clearButton[2].Background = Brushes.White;
                clearButton[3].Background = Brushes.White;
                tips[0].Fill = Brushes.White;
                tips[1].Fill = Brushes.White;
                tips[2].Fill = Brushes.White;
                tips[3].Fill = Brushes.White;
            }
        }
        public void main_menu(bool vis) {
            switch (vis) {
                case true:
                    for (int i = 1; i < 16; i++)
                    {
                        GlobalGrid.Children[i].Visibility = System.Windows.Visibility.Hidden;
                    }
                    for (int i = 16; i < GlobalGrid.Children.Count; i++) {
                        GlobalGrid.Children[i].Visibility = System.Windows.Visibility.Visible;
                    }
                    mainmenuButton.Visibility = System.Windows.Visibility.Hidden;
                    winLabel.Visibility = System.Windows.Visibility.Hidden;
                    tryagainButton.Visibility = System.Windows.Visibility.Hidden;
                    winGrid.Visibility = System.Windows.Visibility.Hidden;
                    questionButton.Visibility = System.Windows.Visibility.Visible;
                    break;
                case false:
                   
                    for (int i = 1; i < 16; i++)
                    {
                        GlobalGrid.Children[i].Visibility = System.Windows.Visibility.Visible;
                    }
                    for (int i = 16; i < GlobalGrid.Children.Count; i++)
                    {
                        GlobalGrid.Children[i].Visibility = System.Windows.Visibility.Hidden;
                    }
                    questionButton.Visibility = System.Windows.Visibility.Visible;
                    mainmenuButton.Visibility = System.Windows.Visibility.Visible;
                    break;

        }
        }

        private bool checkTab(Brush check) {
            for (int i = 0; i < 4; i++)
            {
                if (code[i] == check) {
                    return false;
                }
            }
            
            return true;
        }
        
        


        private void RandomCode(object sender, EventArgs e) {
            player.Play();

            Random generator = new Random();       
            for (int i = 0; i < 4; i++)
            {
                int randIndex;
                do
                {
                    randIndex = generator.Next(0, 7);

                } while (!checkTab(generalColors[randIndex]));
                code[i] = generalColors[randIndex];
                Console.WriteLine("###" + code[i] + "..." + randIndex);
            }
            main_menu(false);
        }

        private void ToggleAttempts()
        {
            for (int i = 0; i < 6; i++)
            {
                Grid toggledGrid = (Grid)GlobalGrid.Children[i + 9];
                Button[] toggleButton = new Button[] { (Button)toggledGrid.Children[1], (Button)toggledGrid.Children[2], (Button)toggledGrid.Children[3], (Button)toggledGrid.Children[4] };
                if (level != i)
                {
                    toggleButton[0].IsHitTestVisible = false;
                    toggleButton[0].Cursor = Cursors.Arrow;
                    toggleButton[1].Cursor = Cursors.Arrow;
                    toggleButton[2].Cursor = Cursors.Arrow;
                    toggleButton[3].Cursor = Cursors.Arrow;
                    toggleButton[1].IsHitTestVisible = false;
                    toggleButton[2].IsHitTestVisible = false;
                    toggleButton[3].IsHitTestVisible = false;
                }
                else
                {
                    toggleButton[0].IsHitTestVisible = true;
                    toggleButton[0].Cursor = Cursors.Hand;
                    toggleButton[1].Cursor = Cursors.Hand;
                    toggleButton[2].Cursor = Cursors.Hand;
                    toggleButton[3].Cursor = Cursors.Hand;
                    toggleButton[1].IsHitTestVisible = true;
                    toggleButton[2].IsHitTestVisible = true;
                    toggleButton[3].IsHitTestVisible = true;
                }
            } 
        }          

        public void ClearThickness() {
            c1.BorderThickness = new Thickness(1, 1, 1, 1);
            c2.BorderThickness = new Thickness(1, 1, 1, 1);
            c3.BorderThickness = new Thickness(1, 1, 1, 1);
            c4.BorderThickness = new Thickness(1, 1, 1, 1);
            c5.BorderThickness = new Thickness(1, 1, 1, 1);
            c6.BorderThickness = new Thickness(1, 1, 1, 1);
            c7.BorderThickness = new Thickness(1, 1, 1, 1);
            c8.BorderThickness = new Thickness(1, 1, 1, 1);
        }
        public void onColorClick(object sender, EventArgs e)
        {
            Button clicked = (Button)sender;
            
            player.Play();
            ClearThickness();
            if (clicked.Background != selectedColor)
            {
                clicked.BorderThickness = new Thickness(5, 5, 5, 5);
                selectedColor = clicked.Background;
            }
            else {
                selectedColor = null;
            }
           
        }

        public void ColorMe(object sender, EventArgs e) {
            Button clicked = (Button)sender;
            Console.WriteLine("Kupso");
            player.Play();
            if (clicked.Background == Brushes.White || clicked.Background != selectedColor)
                clicked.Background =  selectedColor;   
                
            
        }


        private void Check(object sender, RoutedEventArgs e)
        {
            int tips_index = 0;
            Grid checking_level = (Grid)GlobalGrid.Children[9 + level];
            Button[] colors = new Button[] { (Button)checking_level.Children[1], (Button)checking_level.Children[2], (Button)checking_level.Children[3], (Button)checking_level.Children[4] };
            Ellipse[] tips = new Ellipse[] { (Ellipse)checking_level.Children[5], (Ellipse)checking_level.Children[6], (Ellipse)checking_level.Children[7], (Ellipse)checking_level.Children[8] };
            player.Play();
            int checksum = 0;
            for (int i = 0; i < 4; i++)
            {
                if (colors[i].Background == Brushes.White)
                {
                    MessageBox.Show("Wypełnij wszystkie 4 kratki kolorami.");
                    break;
                }
                checksum++;
            }

            if (checksum == 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    Console.WriteLine(colors[i].Background + " " + code[i]);
                    if (colors[i].Background.ToString() == code[i].ToString())
                    {
                        if (tips_index >= 3)
                        {
                            WinScreen();
                            break;
                        }
                        Console.WriteLine("Probuje kolorowac");
                        tips[tips_index].Fill = Brushes.Black;
                        tips_index++;
                    }
                }

                for (int i = 0; i < 4; i++) {
                    if (colors[i].Background.ToString() != code[i].ToString())
                    for (int k = 0; k < 4; k++) {
                        if (colors[i].Background.ToString() == code[k].ToString()) {
                            tips[tips_index].Fill = Brushes.LightGray;
                            tips_index++;
                        }
                    }
                }

                level++;
                if (level > 5) {
                    loseWindow losewindow = new loseWindow(this);
                    losewindow.Owner = this;
                    losewindow.ShowDialog();
                    main_menu(true);
                }
                ToggleAttempts();

            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno?", "Wyjście", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK) {
                System.Windows.Application.Current.Shutdown();   
            }
            player.Play();
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            player.Play();
            create createwindow = new create(this);
            createwindow.Owner = this;
            createwindow.ShowDialog();

        }

        private void tryagainButton_Click(object sender, RoutedEventArgs e)
        {
            bg_music.Play();
            player.Play();
            new_game();
        }

        private void questionButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            instruction createwindow = new instruction();
            createwindow.Owner = this;
            createwindow.ShowDialog();
        }

        private void mainmenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno?", "Wróć do menu głównego", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                new_game();
                main_menu(true);
            }
        }
    }
}
