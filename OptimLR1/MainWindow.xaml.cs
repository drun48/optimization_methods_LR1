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
using OptimLR1.components;

namespace OptimLR1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MyMatrix A = new MyMatrix(3, 5);
            MyMatrix B = new MyMatrix(5, 6);

            for(int i = 0; i < 3; ++i)
            {
                for(int j = 0;  j <5; ++j)
                {
                    A[i, j] = j;
                }
            }

            for (int i = 0; i < 5; ++i)
            {
                for (int j = 0; j < 6; ++j)
                {
                    B[i, j] = j;
                }
            }

            MyMatrix C = A * B;

            Matrix<double> K = DenseMatrix.OfArray(new double[,] {
            {2, 5, 7, 10, 0, 0},
            {0, 9, 2, 2, 1, 3},
            {8, 1, 3, 0, 0, 6},
            { 0, 3, -6, 7, 5, -6} });
            var I = Matrix<double>.Build;
            var c = I.DenseDiagonal(K.ColumnCount, K.ColumnCount, 1);
            Grevil(K);
        }   
        
       public Matrix<double> Grevil(Matrix<double> A)
        {
            var matrix = Matrix<double>.Build;
            var I = matrix.DenseDiagonal(A.ColumnCount, A.ColumnCount, 1);
            var a = column(A, 0);
            var a_transp = a.Transpose();
            var A_obr = matrix.Dense(A.ColumnCount, A.RowCount);
            if (!any(a))
            {
                for(int i = 0; i < A_obr.ColumnCount; ++i)
                {
                    A_obr[0, i] = 0;
                }
            }
            else
            {
                var norm = a_transp*a;
                for (int i = 0; i < A_obr.ColumnCount; ++i)
                {
                    A_obr[0, i] = a_transp[0, i] / norm[0, 0];
                }
            }
            
            for(int k = 1; k < A.ColumnCount; ++k)
            {
                a = column(A, k);
                a_transp = a.Transpose();
                var  C = srez_col(A, 0, k) * srez_col(A_obr, 0, k, true);
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
                    var d_k = 1 + square(A_obr * a);
                    f = a_transp * A_obr.Transpose() * A_obr / d_k[0, 0];
                }
                for(int i = 0; i < A_obr.ColumnCount; ++i)
                {
                    A_obr[k, i] = f[0, i];
                }
            }
            return A_obr;
        }

        public bool  any(Matrix<double> C)
        {
            for(int i = 0; i < C.RowCount; ++i)
            {
                for(int j = 0; j < C.ColumnCount; ++j)
                {
                    if (C[i, j] != 0)
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
        public Matrix<double> srez_col(Matrix<double> A, int start, int end, bool flag =false)
        {
            var matrix = Matrix<double>.Build;
            int row = 0;
            if (flag)
                row = end;
            else
                row = A.RowCount;
            var srez = matrix.Dense(row, end-start, 0);
            for(int i = 0; i < end; ++i)
            {
                for(int j = 0; j < end-start; ++j)
                {
                    srez[i, j] = A[i, j];
                }
            }

            return srez;
        }
        public Matrix<double> square(Matrix<double> A)
        {
            for(int i = 0; i < A.RowCount; ++i)
            {
                for(int j = 0; j < A.ColumnCount; ++j)
                {
                    A[i, j] = A[i, j] * A[i, j];
                }
            }
            return A;
        }
    }
}
