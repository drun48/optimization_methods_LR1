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
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Data;

namespace OptimLR1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int N, M;
        Matrix<double> Table_A, Table_B, Table_psevdo, Table_X, Table_BB;
        double Nevyazka;
        MainViewModel DataX;
        MainViewModel mainviewmodel = new MainViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataX = new MainViewModel();
            DataContext = mainviewmodel;

            Matrix<double> K = DenseMatrix.OfArray(new double[,] {
            {2, 5, 7, 10, 0, 0},
            {0, 9, 2, 2, 1, 3},
            {8, 1, 3, 0, 0, 6},
            { 0, 3, -6, 7, 5, -6} });
            Matrix<double> L = DenseMatrix.OfArray(new double[,] {
            { 1, -5, -2, 9, 2, -7 },{-2, 0, 1, 4, -6, 3 },{4, 5, -1, -1, 0, 1 },{5, -3, -6, -3, -2, 3 } });
            var I = Matrix<double>.Build;
            var c = I.DenseDiagonal(K.ColumnCount, K.ColumnCount, 1);
            var k = Grevil(K);
            //if ((K * k).Transpose() == K * k)
            //{
            //    Console.WriteLine(1);
            //}
            //if ((k * K).Transpose() == k * K)
            //{
            //    Console.WriteLine(2);
            //}
            //if (K * k * K == K)
            //{
            //    Console.WriteLine(3);
            //}
            //if (k * K * k == k)
            //{
            //    Console.WriteLine(4);
            //}
            Data_A.Visibility = Visibility.Hidden;
            Data_B.Visibility = Visibility.Hidden;
            //Data_Psvd.Visibility = Visibility.Hidden;
            txtbl1.Visibility = Visibility.Hidden;
            txtbl2.Visibility = Visibility.Hidden;
            txtbl3.Visibility = Visibility.Hidden;
            ButtonCalculate.Visibility = Visibility.Hidden;
            Table_A = K;
            Table_B = DenseMatrix.OfArray(new double[,] { { 1 }, { -5 }, { -2 }, { 9 } });
        }

        public Matrix<double> Grevil(Matrix<double> A)
        {
            var matrix = Matrix<double>.Build;
            var I = matrix.DenseDiagonal(A.RowCount, A.RowCount, 1);
            var a = column(A, 0);
            var a_transp = a.Transpose();
            var A_obr = matrix.Dense(1, A.RowCount);
            if (!any(a))
            {
                for (int i = 0; i < A_obr.ColumnCount; ++i)
                {
                    A_obr[0, i] = 0;
                }
            }
            else
            {
                var norm = a_transp * a;
                for (int i = 0; i < A_obr.ColumnCount; ++i)
                {
                    A_obr[0, i] = a_transp[0, i] / norm[0, 0];
                }
            }

            for (int k = 1; k < A.ColumnCount; ++k)
            {
                a = column(A, k);
                a_transp = a.Transpose();
                var C = a - srez_col(A, 0, k) * A_obr * a;
                var f = matrix.Dense(1, A_obr.ColumnCount);
                if (any(C))
                {
                    var norm_C = C.Transpose() * C;
                    for (int i = 0; i < A_obr.ColumnCount; ++i)
                    {
                        f[0, i] = C.Transpose()[0, i] / norm_C[0, 0];
                    }
                }
                else
                {
                    //double d_k = 1 + ((A_obr * a).Transpose() * (A_obr * a))[0,0];
                    double d_k = 1 + sum(square(A_obr * a));
                    //double d_k_c = 1 + sum(A_obr.Transpose()*a);
                    f = a_transp * A_obr.Transpose() * A_obr / d_k;
                }
                A_obr = A_obr * (I - a * f);
                Vector<double> f_row = f.Row(0);
                A_obr = A_obr.InsertRow(k, f_row);

            }
            return A_obr;
        }

        public bool any(Matrix<double> C)// проверка на не 0
        {
            for (int i = 0; i < C.RowCount; ++i)
            {
                for (int j = 0; j < C.ColumnCount; ++j)
                {
                    if (Math.Abs(C[i, j]) >= 1e-9)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public Matrix<double> column(Matrix<double> A, int index_column)
        {
            var matrix = Matrix<double>.Build;
            var column = matrix.Dense(A.RowCount, 1, 0);
            for (int i = 0; i < A.RowCount; ++i)
            {
                column[i, 0] = A[i, index_column];
            }
            return column;
        }

        private void ButtonExecute_Click(object sender, RoutedEventArgs e)
        {
            Data_A.Visibility = Visibility.Visible;
            Data_B.Visibility = Visibility.Visible;
            txtbl1.Visibility = Visibility.Visible;
            ButtonCalculate.Visibility = Visibility.Visible;
            N = Convert.ToInt32(txtN.Text);
            M = Convert.ToInt32(txtM.Text);
        }

        public Matrix<double> srez_col(Matrix<double> A, int start, int end)
        {
            var matrix = Matrix<double>.Build;
            int row = A.RowCount;
            var srez = matrix.Dense(row, end - start, 0);
            for (int i = 0; i < row; ++i)
            {
                for (int j = 0; j < end - start; ++j)
                {
                    srez[i, j] = A[i, j];
                }
            }

            return srez;
        }
        public Matrix<double> square(Matrix<double> A)
        {
            for (int i = 0; i < A.RowCount; ++i)
            {
                for (int j = 0; j < A.ColumnCount; ++j)
                {
                    A[i, j] = A[i, j] * A[i, j];
                }
            }
            return A;
        }
        public double sum(Matrix<double> A)
        {
            double sum = 0;
            for (int i = 0; i < A.RowCount; ++i)
            {
                for (int j = 0; j < A.ColumnCount; ++j)
                {
                    sum += A[i, j];
                }
            }
            return sum;
        }
        
        private double[,] DataToDouble(DataGrid _DG)
        {
            DataView Tbl = Data_A.ItemsSource as DataView;
            double[,] Result = new double[Tbl.Table.Rows.Count, Tbl.Table.Columns.Count];
            for (int i = 0; i < Tbl.Table.Rows.Count; i++)
            {
                for (int j = 0; j < Tbl.Table.Columns.Count; j++)
                {
                    Result[i,j] = Convert.ToDouble(Tbl[i].Row.ItemArray[j]);
                }
            }
            return Result;
        }

        private void AddDataToLW (Matrix<double> _matr)
        {
            double[,] _Dmatr = _matr.ToArray();
            List<double[]> coll_res = new List<double[]>();
            GridView gridView = new GridView();
            gridView.Columns.Add(new GridViewColumn { Header = "n\\m", DisplayMemberBinding = new Binding(".[" + _Dmatr.GetLength(1).ToString() + "]") });
            for (int i = 0; i < M; i++)
                gridView.Columns.Add(new GridViewColumn { Header = i.ToString(), DisplayMemberBinding = new Binding(".[" + i.ToString() + "]") });

            for (int i = 0; i < _Dmatr.GetLength(0); i++)
            {
                double[] str = new double[_Dmatr.GetLength(1)];
                //var rectResult = _Dmatr.Cast<object>().ToArray();
                for (int j = 0; j < _Dmatr.GetLength(1); j++)
                {
                    str[j] = _Dmatr[i, j];
                }
                coll_res.Add(str);
            }
            for (int i = 0; i < N; i++)//для отображения левых индексов
               coll_res[i].Append(i);

            mainviewmodel.ItemSource_Psvd = coll_res;
            //Data_Psvd.ItemsSource = coll_res;
            //Data_Psvd.ItemsSource = coll_res;
            //mainviewmodel.gridView = gridView;
            //GridWiew1 = gridView;
        }

        private void ButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            txtbl2.Visibility = Visibility.Visible;
            txtbl3.Visibility = Visibility.Visible;

            //Table_A = DenseMatrix.OfArray(DataToDouble(Data_A));
            //Table_B = DenseMatrix.OfArray(DataToDouble(Data_B));

            Table_psevdo = Grevil(Table_A);
            Table_X = Table_psevdo * Table_B;
            Table_BB = Table_A * Table_X;
            Nevyazka = sum(square(Table_B - Table_BB));

            for (int i = 0; i < Table_X.RowCount; ++i)
            {
                StackPanel stack = new StackPanel();
                stack.Orientation = Orientation.Horizontal;
                for(int j = 0; j < Table_X.ColumnCount; ++j)
                {
                    TextBlock text = new TextBlock();
                    text.Text = Table_X[i, j].ToString();
                    stack.Children.Add(text);
                }
                this.Table.Children.Add(stack);
            }

            AddDataToLW(Table_X);
            txtbl3.Text += " " + Nevyazka.ToString();
        }

        public void Get_NM(int _N, int _M)
        {
            N = _N;
            M = _M;
        }
    }
}
