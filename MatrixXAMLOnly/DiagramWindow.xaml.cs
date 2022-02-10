using System;
using System.Windows;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Matrix;

namespace MatrixXAMLOnly
{
    /// <summary>
    /// Логика взаимодействия для DiagramWindow.xaml
    /// </summary>
    public partial class DiagramWindow : Window
    {
        MyMatrix matrix;

        public SeriesCollection SeriesCollection { get; set; }

        public Func<ChartPoint, string> PointLabel { get; set; }

        public DiagramWindow(MyMatrix matrix)
        {
            InitializeComponent();

            this.matrix = matrix;
            SeriesCollection = new SeriesCollection();

            ComboBoxOrientation.SelectedIndex = 0;

            PointLabel = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            DataContext = this;
        }

        private void BtnCreateDiagram_Click(object sender, RoutedEventArgs e)
        {
            SeriesCollection.Clear();

            int num;
            try
            {
                num = Convert.ToInt32(TextBoxNum.Text) - 1;
            }
            catch
            {
                MessageBox.Show("Проверьте правильность введенных данных!");
                return;
            }


            if (ComboBoxOrientation.SelectedIndex == 0)
            {

                if (num + 1 < 1 || num + 1 > matrix.Rows)
                {
                    MessageBox.Show("Указанное число превышает размерность матрицы!");
                    return;
                }

                for (int i = 0; i < matrix.Columns; i++)
                {
                    SeriesCollection.Add(new PieSeries
                    {
                        Title = $"№{i + 1}",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(matrix.data[num, i]) },
                        DataLabels = true
                    });
                }
            }
            else
            {
                if (num + 1 < 1 || num + 1 > matrix.Columns)
                {
                    MessageBox.Show("Указанное число превышает размерность матрицы!");
                    return;
                }

                for (int i = 0; i < matrix.Rows; i++)
                {
                    SeriesCollection.Add(new PieSeries
                    {
                        Title = $"№{i + 1}",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(matrix.data[i, num]) },
                        DataLabels = true
                    });
                }
            }
        }
    }
}
