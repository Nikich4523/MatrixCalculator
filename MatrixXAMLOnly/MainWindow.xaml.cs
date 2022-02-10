using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Matrix;

namespace MatrixXAMLOnly
{
    // TODO: Перепроверить все поля на валидацию
    // TODO: Создание диаграмм

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            BtnAddition.Content = "Сложение матриц";
            BtnDifference.Content = "Вычитание матриц";
            BtnMultiplicationOnScalar.Content = "Умножение матрицы \nна скаляр";
            BtnTransposition.Content = "Транспонирование матрицы";
            BtnMultiplicationOnTransposed.Content = "Умножение матрицы на её \nтранспонированную матрицу";
            BtnRowsReplace.Content = "Перестановка строк";
            BtnRowsTranspositionReplace.Content = "Перестановка строк согласно \nвектору транспозиции";
            BtnInverse.Content = "Обратная матрицы";

            BtnAddition.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
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

        private Grid CreateMatrix(int a, int b, bool isOnlyRead)
        {
            Grid matrixGrid = new Grid();
            matrixGrid.Margin = new Thickness(25, 75, 25, 75);

            // Добавление колонок
            for (int i = 0; i < b; i++)
            {
                matrixGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            // Добавление строк
            for (int j = 0; j < a; j++)
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

                    if (isOnlyRead)
                    {
                        textBox.IsReadOnly = true;
                    }

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
                e.Key != Key.NumPad9 && e.Key != Key.D9 && e.Key != Key.OemMinus && e.Key != Key.Subtract)
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
                element += columns;
            }
            else if ((int)KeyArg == 38 || KeyArg == Key.W)
            {
                element -= columns;
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
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
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
            this.Title = "Калькулятор: матрицы - Сложение матриц";

            GridContentAddition.Visibility = Visibility.Visible;
            GridContentDifference.Visibility = Visibility.Collapsed;
            GridContentMultiplication.Visibility = Visibility.Collapsed;
            GridContentMultiplicationOnTransposed.Visibility = Visibility.Collapsed;
            GridContentRowsReplace.Visibility = Visibility.Collapsed;
            GridContentTranspose.Visibility = Visibility.Collapsed;
            GridContentRowTranspositionReplace.Visibility = Visibility.Collapsed;
            GridContentInverse.Visibility = Visibility.Collapsed;

            GridAdditionFrstTerm.Children.Clear();
            GridAdditionScndTerm.Children.Clear();
            GridAdditionResult.Children.Clear();

            GridAdditionFrstTerm.Children.Add(CreateMatrix(5, 5, false));
            GridAdditionScndTerm.Children.Add(CreateMatrix(5, 5, false));
            GridAdditionResult.Children.Add(CreateMatrix(5, 5, true));
        }

        private void BtnDifference_Click(object sender, RoutedEventArgs e)
        {
            this.Title = "Калькулятор: матрицы - Вычитание матриц";

            GridContentAddition.Visibility = Visibility.Collapsed;
            GridContentDifference.Visibility = Visibility.Visible;
            GridContentMultiplication.Visibility = Visibility.Collapsed;
            GridContentMultiplicationOnTransposed.Visibility = Visibility.Collapsed;
            GridContentRowsReplace.Visibility = Visibility.Collapsed;
            GridContentTranspose.Visibility = Visibility.Collapsed;
            GridContentRowTranspositionReplace.Visibility = Visibility.Collapsed;
            GridContentInverse.Visibility = Visibility.Collapsed;

            GridDifferenceFrstTerm.Children.Clear();
            GridDifferenceScndTerm.Children.Clear();
            GridDifferenceResult.Children.Clear();

            GridDifferenceFrstTerm.Children.Add(CreateMatrix(5, 5, false));
            GridDifferenceScndTerm.Children.Add(CreateMatrix(5, 5, false));
            GridDifferenceResult.Children.Add(CreateMatrix(5, 5, true));
        }

        private void BtnMultiplicationOnScalar_Click(object sender, RoutedEventArgs e)
        {
            this.Title = "Калькулятор: матрицы - Умножение матрицы на скаляр";

            GridContentAddition.Visibility = Visibility.Collapsed;
            GridContentDifference.Visibility = Visibility.Collapsed;
            GridContentMultiplication.Visibility = Visibility.Visible;
            GridContentMultiplicationOnTransposed.Visibility = Visibility.Collapsed;
            GridContentRowsReplace.Visibility = Visibility.Collapsed;
            GridContentTranspose.Visibility = Visibility.Collapsed;
            GridContentRowTranspositionReplace.Visibility = Visibility.Collapsed;
            GridContentInverse.Visibility = Visibility.Collapsed;

            GridMultiplicationOnScalarFrstTerm.Children.Clear();
            TextBoxMultiplicationOnScalarScndTerm.Text = "";
            GridMultiplicationOnScalarResult.Children.Clear();

            GridMultiplicationOnScalarFrstTerm.Children.Add(CreateMatrix(5, 5, false));
            GridMultiplicationOnScalarResult.Children.Add(CreateMatrix(5, 5, true));
        }

        private void BtnTransposition_Click(object sender, RoutedEventArgs e)
        {
            this.Title = "Калькулятор: матрицы - Транспонирование матрицы";

            GridContentAddition.Visibility = Visibility.Collapsed;
            GridContentDifference.Visibility = Visibility.Collapsed;
            GridContentMultiplication.Visibility = Visibility.Collapsed;
            GridContentMultiplicationOnTransposed.Visibility = Visibility.Collapsed;
            GridContentRowsReplace.Visibility = Visibility.Collapsed;
            GridContentTranspose.Visibility = Visibility.Visible;
            GridContentRowTranspositionReplace.Visibility = Visibility.Collapsed;
            GridContentInverse.Visibility = Visibility.Collapsed;

            GridTransposeFrom.Children.Clear();
            GridTransposed.Children.Clear();

            GridTransposeFrom.Children.Add(CreateMatrix(3, 5, false));
            GridTransposed.Children.Add(CreateMatrix(5, 3, true));
        }

        private void BtnMultiplicationOnTransposed_Click(object sender, RoutedEventArgs e)
        {
            this.Title = "Калькулятор: матрицы - Умножение матрицы на её транспонированную матрицу";

            GridContentAddition.Visibility = Visibility.Collapsed;
            GridContentDifference.Visibility = Visibility.Collapsed;
            GridContentMultiplication.Visibility = Visibility.Collapsed;
            GridContentMultiplicationOnTransposed.Visibility = Visibility.Visible;
            GridContentRowsReplace.Visibility = Visibility.Collapsed;
            GridContentTranspose.Visibility = Visibility.Collapsed;
            GridContentRowTranspositionReplace.Visibility = Visibility.Collapsed;
            GridContentInverse.Visibility = Visibility.Collapsed;

            GridMultiplicationOnTransposedTerm.Children.Clear();
            GridMultiplicationOnTransposedResult.Children.Clear();

            GridMultiplicationOnTransposedTerm.Children.Add(CreateMatrix(5, 5, false));
            GridMultiplicationOnTransposedResult.Children.Add(CreateMatrix(5, 5, true));
        }

        private void BtnRowsReplace_Click(object sender, RoutedEventArgs e)
        {
            this.Title = "Калькулятор: матрицы - Перестановка строк";

            GridContentAddition.Visibility = Visibility.Collapsed;
            GridContentDifference.Visibility = Visibility.Collapsed;
            GridContentMultiplication.Visibility = Visibility.Collapsed;
            GridContentMultiplicationOnTransposed.Visibility = Visibility.Collapsed;
            GridContentRowsReplace.Visibility = Visibility.Visible;
            GridContentTranspose.Visibility = Visibility.Collapsed;
            GridContentRowTranspositionReplace.Visibility = Visibility.Collapsed;
            GridContentInverse.Visibility = Visibility.Collapsed;

            GridRowsReplaceMatrix.Children.Clear();
            GridRowsReplaceResult.Children.Clear();

            GridRowsReplaceMatrix.Children.Add(CreateMatrix(5, 5, false));
            GridRowsReplaceResult.Children.Add(CreateMatrix(5, 5, true));
        }

        private void BtnInverse_Click(object sender, RoutedEventArgs e)
        {
            this.Title = "Калькулятор: матрицы - Обратная матрица";

            GridContentAddition.Visibility = Visibility.Collapsed;
            GridContentDifference.Visibility = Visibility.Collapsed;
            GridContentMultiplication.Visibility = Visibility.Collapsed;
            GridContentMultiplicationOnTransposed.Visibility = Visibility.Collapsed;
            GridContentRowsReplace.Visibility = Visibility.Collapsed;
            GridContentTranspose.Visibility = Visibility.Collapsed;
            GridContentRowTranspositionReplace.Visibility = Visibility.Collapsed;
            GridContentInverse.Visibility = Visibility.Visible;

            GridInverseMatrix.Children.Clear();
            GridInverseResult.Children.Clear();

            GridInverseMatrix.Children.Add(CreateMatrix(5, 5, false));
            GridInverseResult.Children.Add(CreateMatrix(5, 5, true));
        }

        private void BtnRowsTranspositionReplace_Click(object sender, RoutedEventArgs e)
        {
            this.Title = "Калькулятор: матрицы - Перестановка строк согласно вектору транспозиции";

            GridContentAddition.Visibility = Visibility.Collapsed;
            GridContentDifference.Visibility = Visibility.Collapsed;
            GridContentMultiplication.Visibility = Visibility.Collapsed;
            GridContentMultiplicationOnTransposed.Visibility = Visibility.Collapsed;
            GridContentRowsReplace.Visibility = Visibility.Collapsed;
            GridContentTranspose.Visibility = Visibility.Collapsed;
            GridContentRowTranspositionReplace.Visibility = Visibility.Visible;
            GridContentInverse.Visibility = Visibility.Collapsed;

            GridRowTranspositionReplaceMatrix.Children.Clear();
            GridRowTranspositionReplaceVector.Children.Clear();
            GridRowTranspositionReplaceResult.Children.Clear();

            GridRowTranspositionReplaceMatrix.Children.Add(CreateMatrix(5, 5, false));
            GridRowTranspositionReplaceVector.Children.Add(CreateMatrix(5, 1, false));
            GridRowTranspositionReplaceResult.Children.Add(CreateMatrix(5, 5, true));
        }
        #endregion

        #region Addition
        private void BtnAdditionCalculate_Click(object sender, RoutedEventArgs e)
        {
            int rowCount = ((Grid)GridAdditionResult.Children[0]).RowDefinitions.Count;
            int columnCount = ((Grid)GridAdditionResult.Children[0]).ColumnDefinitions.Count;

            double[,] frstTerm = new double[rowCount, columnCount];
            double[,] scndTerm = new double[rowCount, columnCount];

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    try
                    {
                        frstTerm[i, j] = Convert.ToDouble(((Grid)GridAdditionFrstTerm.Children[0]).Children.Cast<TextBox>().First(e => Grid.GetColumn(e) == j && Grid.GetRow(e) == i).Text);
                        scndTerm[i, j] = Convert.ToDouble(((Grid)GridAdditionScndTerm.Children[0]).Children.Cast<TextBox>().First(e => Grid.GetColumn(e) == j && Grid.GetRow(e) == i).Text);
                    }
                    catch
                    {
                        MessageBox.Show("Матрица указана некорректно!");
                        return;
                    }
                }
            }

            MyMatrix matrixFrstTerm = new MyMatrix(frstTerm);
            MyMatrix matrixScndTerm = new MyMatrix(scndTerm);

            MyMatrix result = matrixFrstTerm + matrixScndTerm;
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    ((TextBox)((Grid)GridAdditionResult.Children[0]).Children[(i * columnCount) + j]).Text = result.data[i, j].ToString();
                }
            }
        }

        private void BtnAddtitionCreateMatrix_Click(object sender, RoutedEventArgs e)
        {
            int rowCount = 0;
            int columnCount = 0;
            try
            {
                rowCount = Convert.ToInt32(TextBoxAdditionRowCount.Text);
                columnCount = Convert.ToInt32(TextBoxAdditionColumnCount.Text);
            }
            catch
            {
                MessageBox.Show("Количество строк/столбцов указано некорректно!");
                return;
            }

            GridAdditionFrstTerm.Children.Clear();
            GridAdditionScndTerm.Children.Clear();
            GridAdditionResult.Children.Clear();

            GridAdditionFrstTerm.Children.Add(CreateMatrix(rowCount, columnCount, false));
            GridAdditionScndTerm.Children.Add(CreateMatrix(rowCount, columnCount, false));
            GridAdditionResult.Children.Add(CreateMatrix(rowCount, columnCount, true));
        }

        private void BtnAdditionDiagramm_Click(object sender, RoutedEventArgs e)
        {
            int rowCount = ((Grid)GridAdditionResult.Children[0]).RowDefinitions.Count;
            int columnCount = ((Grid)GridAdditionResult.Children[0]).ColumnDefinitions.Count;

            double[,] result = new double[rowCount, columnCount];
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    result[i, j] = Convert.ToDouble(((Grid)GridAdditionResult.Children[0]).Children.Cast<TextBox>().First(e => Grid.GetColumn(e) == j && Grid.GetRow(e) == i).Text);
                }
            }

            DiagramWindow wnd = new DiagramWindow(new MyMatrix(result));
            wnd.Show();
        }
        #endregion

        #region Difference
        private void BtnDifferenceCreateMatrix_Click(object sender, RoutedEventArgs e)
        {
            int rowCount = 0;
            int columnCount = 0;
            try
            {
                rowCount = Convert.ToInt32(TextBoxDifferenceRowCount.Text);
                columnCount = Convert.ToInt32(TextBoxDifferenceColumnCount.Text);
            }
            catch
            {
                MessageBox.Show("Количество строк/столбцов указано некорректно!");
                return;
            }

            GridDifferenceFrstTerm.Children.Clear();
            GridDifferenceScndTerm.Children.Clear();
            GridDifferenceResult.Children.Clear();

            GridDifferenceFrstTerm.Children.Add(CreateMatrix(rowCount, columnCount, false));
            GridDifferenceScndTerm.Children.Add(CreateMatrix(rowCount, columnCount, false));
            GridDifferenceResult.Children.Add(CreateMatrix(rowCount, columnCount, true));
        }

        private void BtnDifferenceCalculate_Click(object sender, RoutedEventArgs e)
        {
            int rowCount = ((Grid)GridDifferenceResult.Children[0]).RowDefinitions.Count;
            int columnCount = ((Grid)GridDifferenceResult.Children[0]).ColumnDefinitions.Count;

            double[,] frstTerm = new double[rowCount, columnCount];
            double[,] scndTerm = new double[rowCount, columnCount];

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    try
                    {
                        frstTerm[i, j] = Convert.ToDouble(((Grid)GridDifferenceFrstTerm.Children[0]).Children.Cast<TextBox>().First(e => Grid.GetColumn(e) == j && Grid.GetRow(e) == i).Text);
                        scndTerm[i, j] = Convert.ToDouble(((Grid)GridDifferenceScndTerm.Children[0]).Children.Cast<TextBox>().First(e => Grid.GetColumn(e) == j && Grid.GetRow(e) == i).Text);
                    }
                    catch
                    {
                        MessageBox.Show("Матрица указана некорректно!");
                        return;
                    }
                }
            }

            MyMatrix matrixFrstTerm = new MyMatrix(frstTerm);
            MyMatrix matrixScndTerm = new MyMatrix(scndTerm);

            MyMatrix result = matrixFrstTerm - matrixScndTerm;
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    ((TextBox)((Grid)GridDifferenceResult.Children[0]).Children[(i * columnCount) + j]).Text = result.data[i, j].ToString();
                }
            }
        }

        private void BtnDifferenceDiagramm_Click(object sender, RoutedEventArgs e)
        {
            int rowCount = ((Grid)GridDifferenceResult.Children[0]).RowDefinitions.Count;
            int columnCount = ((Grid)GridDifferenceResult.Children[0]).ColumnDefinitions.Count;

            double[,] result = new double[rowCount, columnCount];
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    result[i, j] = Convert.ToDouble(((Grid)GridDifferenceResult.Children[0]).Children.Cast<TextBox>().First(e => Grid.GetColumn(e) == j && Grid.GetRow(e) == i).Text);
                }
            }

            DiagramWindow wnd = new DiagramWindow(new MyMatrix(result));
            wnd.Show();
        }
        #endregion

        #region MultiplicationOnScalar
        private void BtnMultiplicationOnScalarCreateMatrix_Click(object sender, RoutedEventArgs e)
        {
            int rowCount, columnCount;
            try
            {
                rowCount = Convert.ToInt32(TextBoxMultiplicationOnScalarRowCount.Text);
                columnCount = Convert.ToInt32(TextBoxMultiplicationOnScalarColumnCount.Text);
            }
            catch
            {
                MessageBox.Show("Количество строк/столбцов указано некорректно!");
                return;
            }

            GridMultiplicationOnScalarFrstTerm.Children.Clear();
            TextBoxMultiplicationOnScalarScndTerm.Text = "";
            GridMultiplicationOnScalarResult.Children.Clear();

            GridMultiplicationOnScalarFrstTerm.Children.Add(CreateMatrix(rowCount, columnCount, false));
            GridMultiplicationOnScalarResult.Children.Add(CreateMatrix(rowCount, columnCount, true));
        }

        private void BtnMultiplicationOnScalarCalculate_Click(object sender, RoutedEventArgs e)
        {
            int rowCount = ((Grid)GridMultiplicationOnScalarResult.Children[0]).RowDefinitions.Count;
            int columnCount = ((Grid)GridMultiplicationOnScalarResult.Children[0]).ColumnDefinitions.Count;

            double[,] frstTerm = new double[rowCount, columnCount];

            double scndTerm = 0;
            try
            {
                scndTerm = Convert.ToDouble(TextBoxMultiplicationOnScalarScndTerm.Text);
            }
            catch
            {
                MessageBox.Show("Скалярная величина указана некорректно!");
                return;
            }

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    try
                    {
                        frstTerm[i, j] = Convert.ToDouble(((Grid)GridMultiplicationOnScalarFrstTerm.Children[0]).Children.Cast<TextBox>().First(e => Grid.GetColumn(e) == j && Grid.GetRow(e) == i).Text);
                    }
                    catch
                    {
                        MessageBox.Show("Матрица указана некорректно!");
                        return;
                    }
                }
            }

            MyMatrix matrixFrstTerm = new MyMatrix(frstTerm);

            MyMatrix result = matrixFrstTerm * scndTerm;
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    ((TextBox)((Grid)GridMultiplicationOnScalarResult.Children[0]).Children[(i * columnCount) + j]).Text = result.data[i, j].ToString();
                }
            }
        }

        private void BtnMultiplicationOnScalarnDiagramm_Click(object sender, RoutedEventArgs e)
        {
            int rowCount = ((Grid)GridMultiplicationOnScalarResult.Children[0]).RowDefinitions.Count;
            int columnCount = ((Grid)GridMultiplicationOnScalarResult.Children[0]).ColumnDefinitions.Count;

            double[,] result = new double[rowCount, columnCount];
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    result[i, j] = Convert.ToDouble(((Grid)GridMultiplicationOnScalarResult.Children[0]).Children.Cast<TextBox>().First(e => Grid.GetColumn(e) == j && Grid.GetRow(e) == i).Text);
                }
            }

            DiagramWindow wnd = new DiagramWindow(new MyMatrix(result));
            wnd.Show();
        }
        #endregion

        #region Transposition
        private void BtnTranspositionCreateMatrix_Click(object sender, RoutedEventArgs e)
        {
            int rowCount = 0;
            int columnCount = 0;
            try
            {
                rowCount = Convert.ToInt32(TextBoxTranspositionRowCount.Text);
                columnCount = Convert.ToInt32(TextBoxTranspositionColumnCount.Text);
            }
            catch
            {
                MessageBox.Show("Количество строк/столбцов указано некорректно!");
                return;
            }

            GridTransposeFrom.Children.Clear();
            GridTransposed.Children.Clear();

            GridTransposeFrom.Children.Add(CreateMatrix(rowCount, columnCount, false));
            GridTransposed.Children.Add(CreateMatrix(columnCount, rowCount, true));
        }

        private void BtnTranspositionCalculate_Click(object sender, RoutedEventArgs e)
        {
            int rowCount = ((Grid)GridTransposeFrom.Children[0]).RowDefinitions.Count;
            int columnCount = ((Grid)GridTransposeFrom.Children[0]).ColumnDefinitions.Count;

            double[,] frstTerm = new double[rowCount, columnCount];

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    try
                    {
                        frstTerm[i, j] = Convert.ToDouble(((Grid)GridTransposeFrom.Children[0]).Children.Cast<TextBox>().First(e => Grid.GetColumn(e) == j && Grid.GetRow(e) == i).Text);
                    }
                    catch
                    {
                        MessageBox.Show("Матрица указана некорректно!");
                        return;
                    }
                }
            }

            MyMatrix result = MyMatrix.Transpose(new MyMatrix(frstTerm));
            for (int i = 0; i < result.Rows; i++)
            {
                for (int j = 0; j < result.Columns; j++)
                {
                    Grid matrixGrid = (Grid)GridTransposed.Children[0];
                    TextBox tb = (TextBox)matrixGrid.Children[(i * result.Columns) + j];
                    tb.Text = result.data[i, j].ToString();
                }
            }
        }

        private void BtnTranspositionDiagramm_Click(object sender, RoutedEventArgs e)
        {
            int rowCount = ((Grid)GridTransposed.Children[0]).RowDefinitions.Count;
            int columnCount = ((Grid)GridTransposed.Children[0]).ColumnDefinitions.Count;

            double[,] result = new double[rowCount, columnCount];
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    result[i, j] = Convert.ToDouble(((Grid)GridTransposed.Children[0]).Children.Cast<TextBox>().First(e => Grid.GetColumn(e) == j && Grid.GetRow(e) == i).Text);
                }
            }

            DiagramWindow wnd = new DiagramWindow(new MyMatrix(result));
            wnd.Show();
        }
        #endregion

        #region MultiplicationOnTransposed
        private void BtnMultiplicationOnTransposedCreateMatrix_Click(object sender, RoutedEventArgs e)
        {
            int rowCount = 0;
            int columnCount = 0;
            try
            {
                rowCount = Convert.ToInt32(TextBoxMultiplicationOnTransposedRowCount.Text);
                columnCount = Convert.ToInt32(TextBoxMultiplicationOnTransposedColumnCount.Text);
            }
            catch
            {
                MessageBox.Show("Количество строк/столбцов указано некорректно!");
                return;
            }

            GridMultiplicationOnTransposedTerm.Children.Clear();
            GridMultiplicationOnTransposedResult.Children.Clear();

            GridMultiplicationOnTransposedTerm.Children.Add(CreateMatrix(rowCount, columnCount, false));
            GridMultiplicationOnTransposedResult.Children.Add(CreateMatrix(rowCount, rowCount, true));
        }

        private void BtnMultiplicationOnTransposedCalculate_Click(object sender, RoutedEventArgs e)
        {
            int rowCount = ((Grid)GridMultiplicationOnTransposedTerm.Children[0]).RowDefinitions.Count;
            int columnCount = ((Grid)GridMultiplicationOnTransposedTerm.Children[0]).ColumnDefinitions.Count;

            double[,] frstTerm = new double[rowCount, columnCount];

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    try
                    {
                        frstTerm[i, j] = Convert.ToDouble(((Grid)GridMultiplicationOnTransposedTerm.Children[0]).Children.Cast<TextBox>().First(e => Grid.GetColumn(e) == j && Grid.GetRow(e) == i).Text);
                    }
                    catch
                    {
                        MessageBox.Show("Матрица указана некорректно!");
                        return;
                    }
                }
            }

            MyMatrix matrixFrst = new MyMatrix(frstTerm);
            MyMatrix matrixTransposed = MyMatrix.Transpose(matrixFrst);

            MyMatrix result = matrixFrst * matrixTransposed;
            for (int i = 0; i < result.Rows; i++)
            {
                for (int j = 0; j < result.Columns; j++)
                {
                    ((TextBox)((Grid)GridMultiplicationOnTransposedResult.Children[0]).Children[(i * result.Columns) + j]).Text = result.data[i, j].ToString();
                }
            }
        }

        private void BtnMultiplicationOnTransposedDiagramm_Click(object sender, RoutedEventArgs e)
        {
            int rowCount = ((Grid)GridMultiplicationOnTransposedResult.Children[0]).RowDefinitions.Count;
            int columnCount = ((Grid)GridMultiplicationOnTransposedResult.Children[0]).ColumnDefinitions.Count;

            double[,] result = new double[rowCount, columnCount];
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    result[i, j] = Convert.ToDouble(((Grid)GridMultiplicationOnTransposedResult.Children[0]).Children.Cast<TextBox>().First(e => Grid.GetColumn(e) == j && Grid.GetRow(e) == i).Text);
                }
            }

            DiagramWindow wnd = new DiagramWindow(new MyMatrix(result));
            wnd.Show();
        }
        #endregion

        #region RowReplace
        private void BtnRowReplaceCreateMatrix_Click(object sender, RoutedEventArgs e)
        {
            int rowCount = 0;
            int columnCount = 0;
            try
            {
                rowCount = Convert.ToInt32(TextBoxRowReplaceRowCount.Text);
                columnCount = Convert.ToInt32(TextBoxRowReplaceColumnCount.Text);
            }
            catch
            {
                MessageBox.Show("Количество строк/столбцов указано некорректно!");
                return;
            }

            GridRowsReplaceMatrix.Children.Clear();
            GridRowsReplaceResult.Children.Clear();

            GridRowsReplaceMatrix.Children.Add(CreateMatrix(rowCount, columnCount, false));
            GridRowsReplaceResult.Children.Add(CreateMatrix(rowCount, columnCount, true));
        }

        private void BtnRowsReplaceCalculate_Click(object sender, RoutedEventArgs e)
        {
            int rowCount = ((Grid)GridRowsReplaceMatrix.Children[0]).RowDefinitions.Count;
            int columnCount = ((Grid)GridRowsReplaceMatrix.Children[0]).ColumnDefinitions.Count;

            double[,] frstTerm = new double[rowCount, columnCount];

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    try
                    {
                        frstTerm[i, j] = Convert.ToDouble(((Grid)GridRowsReplaceMatrix.Children[0]).Children.Cast<TextBox>().First(e => Grid.GetColumn(e) == j && Grid.GetRow(e) == i).Text);
                    }
                    catch
                    {
                        MessageBox.Show("Матрица указана некорректно!");
                        return;
                    }
                }
            }

            MyMatrix matrixFrstTerm = new MyMatrix(frstTerm);

            MyMatrix result = MyMatrix.RowReplace(matrixFrstTerm, Convert.ToInt32(TextBoxRowReplaceFrstTerm.Text) - 1, Convert.ToInt32(TextBoxRowReplaceScndTerm.Text) - 1);
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    ((TextBox)((Grid)GridRowsReplaceResult.Children[0]).Children[(i * columnCount) + j]).Text = result.data[i, j].ToString();
                }
            }
        }

        private void BtnRowsReplaceDiagramm_Click(object sender, RoutedEventArgs e)
        {
            int rowCount = ((Grid)GridRowsReplaceResult.Children[0]).RowDefinitions.Count;
            int columnCount = ((Grid)GridRowsReplaceResult.Children[0]).ColumnDefinitions.Count;

            double[,] result = new double[rowCount, columnCount];
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    result[i, j] = Convert.ToDouble(((Grid)GridRowsReplaceResult.Children[0]).Children.Cast<TextBox>().First(e => Grid.GetColumn(e) == j && Grid.GetRow(e) == i).Text);
                }
            }

            DiagramWindow wnd = new DiagramWindow(new MyMatrix(result));
            wnd.Show();
        }
        #endregion

        #region RowTranspositionReplace
        private void BtnRowTranspositionReplaceCreateMatrix_Click(object sender, RoutedEventArgs e)
        {
            int rowCount, columnCount;
            try
            {
                rowCount = Convert.ToInt32(TextBoxRowTranspositionReplaceRowCount.Text);
                columnCount = Convert.ToInt32(TextBoxRowTranspositionReplaceColumnCount.Text);
            }
            catch
            {
                MessageBox.Show("Количество строк/столбцов указано некорректно!");
                return;
            }

            GridRowTranspositionReplaceMatrix.Children.Clear();
            GridRowTranspositionReplaceVector.Children.Clear();
            GridRowTranspositionReplaceResult.Children.Clear();

            GridRowTranspositionReplaceMatrix.Children.Add(CreateMatrix(rowCount, columnCount, false));
            GridRowTranspositionReplaceVector.Children.Add(CreateMatrix(rowCount, 1, false));
            GridRowTranspositionReplaceResult.Children.Add(CreateMatrix(rowCount, columnCount, true));
        }

        private void BtnRowTranspositionReplaceCalculate_Click(object sender, RoutedEventArgs e)
        {
            int rowCount = ((Grid)GridRowTranspositionReplaceMatrix.Children[0]).RowDefinitions.Count;
            int columnCount = ((Grid)GridRowTranspositionReplaceMatrix.Children[0]).ColumnDefinitions.Count;

            double[,] frstTerm = new double[rowCount, columnCount];
            int[] vector = new int[rowCount];

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    try
                    {
                        frstTerm[i, j] = Convert.ToDouble(((Grid)GridRowTranspositionReplaceMatrix.Children[0]).Children.Cast<TextBox>().First(e => Grid.GetColumn(e) == j && Grid.GetRow(e) == i).Text);
                    }
                    catch
                    {
                        MessageBox.Show("Матрица указана некорректно!");
                        return;
                    }
                }
            }

            for (int i = 0; i < rowCount; i++)
            {
                try
                {
                    vector[i] = Convert.ToInt32(((Grid)GridRowTranspositionReplaceVector.Children[0]).Children.Cast<TextBox>().First(e => Grid.GetColumn(e) == 0 && Grid.GetRow(e) == i).Text) - 1;
                }
                catch
                {
                    MessageBox.Show("Матрица указана некорректно!");
                    return;
                }
            }

            MyMatrix matrixFrstTerm = new MyMatrix(frstTerm);

            MyMatrix result = MyMatrix.RowReplaceByVector(matrixFrstTerm, vector);
            if (result != null)
            {
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < columnCount; j++)
                    {
                        ((TextBox)((Grid)GridRowTranspositionReplaceResult.Children[0]).Children[(i * columnCount) + j]).Text = result.data[i, j].ToString();
                    }
                }
            }
            else
            {
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < columnCount; j++)
                    {
                        ((TextBox)((Grid)GridRowTranspositionReplaceResult.Children[0]).Children[(i * columnCount) + j]).Text = matrixFrstTerm.data[i, j].ToString();
                    }
                }
            }
            
        }

        private void BtnRowTranspositionReplaceDiagramm_Click(object sender, RoutedEventArgs e)
        {
            int rowCount = ((Grid)GridRowTranspositionReplaceResult.Children[0]).RowDefinitions.Count;
            int columnCount = ((Grid)GridRowTranspositionReplaceResult.Children[0]).ColumnDefinitions.Count;

            double[,] result = new double[rowCount, columnCount];
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    result[i, j] = Convert.ToDouble(((Grid)GridRowTranspositionReplaceResult.Children[0]).Children.Cast<TextBox>().First(e => Grid.GetColumn(e) == j && Grid.GetRow(e) == i).Text);
                }
            }

            DiagramWindow wnd = new DiagramWindow(new MyMatrix(result));
            wnd.Show();
        }
        #endregion

        #region Inverse
        private void BtnInverseCreateMatrix_Click(object sender, RoutedEventArgs e)
        {
            int rowCount, columnCount;
            try
            {
                rowCount = Convert.ToInt32(TextBoxInverseRowCount.Text);
                columnCount = Convert.ToInt32(TextBoxInverseColumnCount.Text);
            }
            catch
            {
                MessageBox.Show("Количество строк/столбцов указано некорректно!");
                return;
            }

            GridInverseMatrix.Children.Clear();
            GridInverseResult.Children.Clear();

            GridInverseMatrix.Children.Add(CreateMatrix(rowCount, columnCount, false));
            GridInverseResult.Children.Add(CreateMatrix(rowCount, columnCount, true));
        }

        private void BtnInverseCalculate_Click(object sender, RoutedEventArgs e)
        {
            int rowCount = ((Grid)GridInverseMatrix.Children[0]).RowDefinitions.Count;
            int columnCount = ((Grid)GridInverseMatrix.Children[0]).ColumnDefinitions.Count;

            double[,] frstTerm = new double[rowCount, columnCount];

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    try
                    {
                        frstTerm[i, j] = Convert.ToDouble(((Grid)GridInverseMatrix.Children[0]).Children.Cast<TextBox>().First(e => Grid.GetColumn(e) == j && Grid.GetRow(e) == i).Text);
                    }
                    catch
                    {
                        MessageBox.Show("Матрица указана некорректно!");
                        return;
                    }
                }
            }

            MyMatrix matrixFrstTerm = new MyMatrix(frstTerm);

            MyMatrix result;
            try
            {
                result = MyMatrix.Inverse(matrixFrstTerm);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    ((TextBox)((Grid)GridInverseResult.Children[0]).Children[(i * columnCount) + j]).Text = result.data[i, j].ToString();
                }
            }
        }

        private void BtnInverseeDiagramm_Click(object sender, RoutedEventArgs e)
        {
            int rowCount = ((Grid)GridInverseResult.Children[0]).RowDefinitions.Count;
            int columnCount = ((Grid)GridInverseResult.Children[0]).ColumnDefinitions.Count;

            double[,] result = new double[rowCount, columnCount];
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    result[i, j] = Convert.ToDouble(((Grid)GridInverseResult.Children[0]).Children.Cast<TextBox>().First(e => Grid.GetColumn(e) == j && Grid.GetRow(e) == i).Text);
                }
            }

            DiagramWindow wnd = new DiagramWindow(new MyMatrix(result));
            wnd.Show();
        }
        #endregion
    }
}
