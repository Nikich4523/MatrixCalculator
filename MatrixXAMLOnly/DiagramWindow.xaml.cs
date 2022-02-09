using System.Windows;
using Matrix;

namespace MatrixXAMLOnly
{
    /// <summary>
    /// Логика взаимодействия для DiagramWindow.xaml
    /// </summary>
    public partial class DiagramWindow : Window
    {
        public DiagramWindow()
        {
            InitializeComponent();
        }

        public DiagramWindow(MyMatrix matrix)
        {
            InitializeComponent();
        }
    }
}
