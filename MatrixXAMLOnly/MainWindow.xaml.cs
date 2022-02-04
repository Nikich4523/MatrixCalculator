using System;
using System.Linq;
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
        Operation operation;

        public MainWindow()
        {
            InitializeComponent();
            operation = Operation.NoChosen;

            BtnAddition.Content = "Сложение матрицы";
            BtnDifference.Content = "Вычитание матрицы";
            BtnMultiplicationOnScalar.Content = "Умножение матрицы \nна скаляр";
            BtnTransposition.Content = "Транспонирование матрицы";
            BtnMultiplicationOnTransposed.Content = "Умножение матрицы на её \nтранспонированную матрицу";
            BtnRowsReplace.Content = "Перестановка строк";
            BtnRowsReplaceOnTranspositionVector.Content = "Перестановка строк согласно \nвектору транспозиции";
            BtnInverse.Content = "Обратная матрицы";


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
            int tabCounter = 1;
            for (int i = 0; i < a; i++)
            {
                for (int j = 0; j < b; j++)
                {
                    TextBox textBox = new TextBox()
                    {
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        TabIndex = tabCounter
                    };

                    tabCounter++;

                    textBox.KeyDown += TextBox_KeyDown;

                    Grid.SetColumn(textBox, j);
                    Grid.SetRow(textBox, i);
                    matrixGrid.Children.Add(textBox);
                }
            }

            return matrixGrid;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W ||
                e.Key == Key.A ||
                e.Key == Key.S ||
                e.Key == Key.D ||
                e.Key == Key.Tab)
            {
                e.Handled = true;
                FocusOnNext((TextBox)sender, e.Key);
                return;
            }

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

        private void FocusOnNext(TextBox tb, Key KeyArg)
        {
            Grid matrixGrid = (Grid)tb.Parent;
            int columns = matrixGrid.ColumnDefinitions.Count;
            int rows = matrixGrid.RowDefinitions.Count;
            int element = tb.TabIndex;

            if (((int)KeyArg) == 40 || KeyArg == Key.S)
            {
                element += rows;
            }
            else if ((int)KeyArg == 38 || KeyArg == Key.W)
            {
                element -= rows;
            }
            else if ((int)KeyArg == 37 || KeyArg == Key.A)
            {
                element -= 1;
            }
            else if ((int)KeyArg == 39 || KeyArg == Key.D || KeyArg == Key.Tab)
            {
                element += 1;
            }
            else
            {
                return;
            }


            int counter = 0;
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    counter++;
                    if (element == counter)
                    {
                        matrixGrid.Children.Cast<TextBox>().First(e => Grid.GetColumn(e) == j && Grid.GetRow(e) == i).Focus();
                    }
                }
            }
            
        }


        #region HumburgerMenuItems
        private void BtnAddition_Click(object sender, RoutedEventArgs e)
        {
            GridContentAddition.Visibility = Visibility.Visible;
            GridContentDifference.Visibility = Visibility.Collapsed;
            GridContentMultiplication.Visibility = Visibility.Collapsed;
            GridContentMultiplicationOnTransposed.Visibility = Visibility.Collapsed;
            GridContentRowsReplace.Visibility = Visibility.Collapsed;
            GridContentTranspose.Visibility = Visibility.Collapsed;
            // GridContentRowReplaceOnTranspositionVector;
            // GridContentInverse;

            GridAdditionFrstTerm.Children.Add(CreateMatrix(5, 5));
            GridAdditionScndTerm.Children.Add(CreateMatrix(5, 5));
            GridAdditionResult.Children.Add(CreateMatrix(5, 5));
        }

        private void BtnDifference_Click(object sender, RoutedEventArgs e)
        {
            GridContentAddition.Visibility = Visibility.Collapsed;
            GridContentDifference.Visibility = Visibility.Visible;
            GridContentMultiplication.Visibility = Visibility.Collapsed;
            GridContentMultiplicationOnTransposed.Visibility = Visibility.Collapsed;
            GridContentRowsReplace.Visibility = Visibility.Collapsed;
            GridContentTranspose.Visibility = Visibility.Collapsed;
            // GridContentRowReplaceOnTranspositionVector;
            // GridContentInverse;

            GridDifferenceFrstTerm.Children.Add(CreateMatrix(5, 5));
            GridDifferenceScndTerm.Children.Add(CreateMatrix(5, 5));
            GridDifferenceResult.Children.Add(CreateMatrix(5, 5));
        }

        private void BtnMultiplicationOnScalar_Click(object sender, RoutedEventArgs e)
        {
            GridContentAddition.Visibility = Visibility.Collapsed;
            GridContentDifference.Visibility = Visibility.Collapsed;
            GridContentMultiplication.Visibility = Visibility.Visible;
            GridContentMultiplicationOnTransposed.Visibility = Visibility.Collapsed;
            GridContentRowsReplace.Visibility = Visibility.Collapsed;
            GridContentTranspose.Visibility = Visibility.Collapsed;
            // GridContentRowReplaceOnTranspositionVector;
            // GridContentInverse;

            GridMultiplicationOnScalarFrstTerm.Children.Add(CreateMatrix(5, 5));
            GridMultiplicationOnScalarResult.Children.Add(CreateMatrix(5, 5));
        }

        private void BtnTransposition_Click(object sender, RoutedEventArgs e)
        {
            GridContentAddition.Visibility = Visibility.Collapsed;
            GridContentDifference.Visibility = Visibility.Collapsed;
            GridContentMultiplication.Visibility = Visibility.Collapsed;
            GridContentMultiplicationOnTransposed.Visibility = Visibility.Collapsed;
            GridContentRowsReplace.Visibility = Visibility.Collapsed;
            GridContentTranspose.Visibility = Visibility.Visible;
            // GridContentRowReplaceOnTranspositionVector;
            // GridContentInverse;

            GridTransposeFrom.Children.Add(CreateMatrix(3, 5));
            GridTransposed.Children.Add(CreateMatrix(5, 3));
        }

        private void BtnMultiplicationOnTransposed_Click(object sender, RoutedEventArgs e)
        {
            GridContentAddition.Visibility = Visibility.Collapsed;
            GridContentDifference.Visibility = Visibility.Collapsed;
            GridContentMultiplication.Visibility = Visibility.Collapsed;
            GridContentMultiplicationOnTransposed.Visibility = Visibility.Visible;
            GridContentRowsReplace.Visibility = Visibility.Collapsed;
            GridContentTranspose.Visibility = Visibility.Collapsed;
            // GridContentRowReplaceOnTranspositionVector;
            // GridContentInverse;
        }

        private void BtnRowsReplace_Click(object sender, RoutedEventArgs e)
        {
            GridContentAddition.Visibility = Visibility.Collapsed;
            GridContentDifference.Visibility = Visibility.Collapsed;
            GridContentMultiplication.Visibility = Visibility.Collapsed;
            GridContentMultiplicationOnTransposed.Visibility = Visibility.Collapsed;
            GridContentRowsReplace.Visibility = Visibility.Visible;
            GridContentTranspose.Visibility = Visibility.Collapsed;
            // GridContentRowReplaceOnTranspositionVector;
            // GridContentInverse;
        }

        private void BtnInverse_Click(object sender, RoutedEventArgs e)
        {
            GridContentAddition.Visibility = Visibility.Collapsed;
            GridContentDifference.Visibility = Visibility.Collapsed;
            GridContentMultiplication.Visibility = Visibility.Collapsed;
            GridContentMultiplicationOnTransposed.Visibility = Visibility.Collapsed;
            GridContentRowsReplace.Visibility = Visibility.Collapsed;
            GridContentTranspose.Visibility = Visibility.Collapsed;
            // GridContentRowReplaceOnTranspositionVector;
            // GridContentInverse;
        }

        private void BtnRowsReplaceOnTranspositionVector_Click(object sender, RoutedEventArgs e)
        {
            GridContentAddition.Visibility = Visibility.Collapsed;
            GridContentDifference.Visibility = Visibility.Collapsed;
            GridContentMultiplication.Visibility = Visibility.Collapsed;
            GridContentMultiplicationOnTransposed.Visibility = Visibility.Collapsed;
            GridContentRowsReplace.Visibility = Visibility.Collapsed;
            GridContentTranspose.Visibility = Visibility.Collapsed;
            // GridContentRowReplaceOnTranspositionVector;
            // GridContentInverse;
        }
        #endregion
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
        Inverse,
        NoChosen
    }
}
