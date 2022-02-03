using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MatrixXAMLOnly
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();

            GridContentAddition.Visibility = Visibility.Visible;
            GridAdditionFrstTerm.Children.Add(CreateMatrix(5, 5));
        }

        private void BtnHamburgerMenu_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation menuAnimation = new DoubleAnimation();
            menuAnimation.From = CurtainMenu.ActualWidth;
            menuAnimation.DecelerationRatio = 1.0;

            if (menuAnimation.From == 250)
            {
                menuAnimation.To = 0;
                menuAnimation.Duration = TimeSpan.FromSeconds(0.2);
            }
            else
            {
                menuAnimation.To = 250;
                menuAnimation.Duration = TimeSpan.FromSeconds(0.2);
            }

            CurtainMenu.BeginAnimation(WidthProperty, menuAnimation);
        }

        private Grid CreateMatrix(int a, int b)
        {
            Grid matrixGrid = new Grid();
            matrixGrid.Margin = new Thickness(25, 75, 25, 75);

            // Добавление колонок
            for (int i = 0; i < a; i++)
            {
                matrixGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            // Добавление строк
            for (int j = 0; j < b; j++)
            {
                matrixGrid.RowDefinitions.Add(new RowDefinition());
            }

            // Добавление TextBox'ов
            for (int i = 0; i < a; i++)
            {
                for (int j = 0; j < b; j++)
                {
                    TextBox textBox = new TextBox()
                    {
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center
                    };

                    textBox.KeyDown += TextBox_KeyDown;

                    Grid.SetColumn(textBox, i);
                    Grid.SetRow(textBox, j);
                    matrixGrid.Children.Add(textBox);
                }
            }

            return matrixGrid;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.NumPad0 && e.Key != Key.D0 &&
                e.Key != Key.NumPad1 && e.Key != Key.D1 &&
                e.Key != Key.NumPad2 && e.Key != Key.D2 &&
                e.Key != Key.NumPad3 && e.Key != Key.D3 &&
                e.Key != Key.NumPad4 && e.Key != Key.D4 &&
                e.Key != Key.NumPad5 && e.Key != Key.D5 &&
                e.Key != Key.NumPad6 && e.Key != Key.D6 &&
                e.Key != Key.NumPad7 && e.Key != Key.D7 &&
                e.Key != Key.NumPad8 && e.Key != Key.D8 &&
                e.Key != Key.NumPad9 && e.Key != Key.D9)
            {
                e.Handled = true;
            }
        }
    }

    enum Operation
    {
        Addition,
        Difference,
        MultiplicationOnScalar,
        Transposition,
        MultiplicationOnTransposed,
        RowReplace,
        RowReplaceOnTranspositionVector,
        Inverse
    }
}
