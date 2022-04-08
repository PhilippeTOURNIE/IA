using SolverSudoku;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Sudoku.UI
{
    /// <summary>
    /// Interaction logic for SudokuGrid.xaml
    /// </summary>
    public partial class SudokuGrid : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private ObservableCollection<int?> m_Cells = new ObservableCollection<int?>();

        public ObservableCollection<int?> Cells
        {
            get { return m_Cells; }
            set
            {
                m_Cells = value;
                OnPropertyChanged(nameof(Cells));
            }
        }
        public SudokuGrid()
        {
            InitializeComponent();
            m_Cells = new ObservableCollection<int?>();
            for (int i = 0; i < 81; i++)
            {
                m_Cells.Add(null);
            }

            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Resoudre();
        }

        void Resoudre()
        {
            List<Fact> faits = new List<Fact>();
            int i = 0;
            faits.Add(new Fact(0, new int?[] { Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++] })); //1
            faits.Add(new Fact(1, new int?[] { Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++] })); //1
            faits.Add(new Fact(2, new int?[] { Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++] })); //1
            faits.Add(new Fact(3, new int?[] { Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++] })); //1
            faits.Add(new Fact(4, new int?[] { Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++] })); //1
            faits.Add(new Fact(5, new int?[] { Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++] })); //1
            faits.Add(new Fact(6, new int?[] { Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++] })); //1
            faits.Add(new Fact(7, new int?[] { Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++] })); //1
            faits.Add(new Fact(8, new int?[] { Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++], Cells[i++] })); //1

            var expert = new ExpertSudoku(faits);

            int j = 0;
            for (int ii = 0; ii < 9; ii++)
               Cells[j++] = expert.Solution.Matrix[0, ii];
            for (int ii = 0; ii < 9; ii++)
                Cells[j++] = expert.Solution.Matrix[1, ii];
            for (int ii = 0; ii < 9; ii++)
                Cells[j++] = expert.Solution.Matrix[2, ii];
            for (int ii = 0; ii < 9; ii++)
                Cells[j++] = expert.Solution.Matrix[3, ii];
            for (int ii = 0; ii < 9; ii++)
                Cells[j++] = expert.Solution.Matrix[4, ii];
            for (int ii = 0; ii < 9; ii++)
                Cells[j++] = expert.Solution.Matrix[5, ii];
            for (int ii = 0; ii < 9; ii++)
                Cells[j++] = expert.Solution.Matrix[6, ii];
            for (int ii = 0; ii < 9; ii++)
                Cells[j++] = expert.Solution.Matrix[7, ii];
            for (int ii = 0; ii < 9; ii++)
                Cells[j++] = expert.Solution.Matrix[8, ii];

            OnPropertyChanged(nameof(Cells));


        }

        void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
