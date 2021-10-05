using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Data;

namespace OptimLR1
{
    public class MainViewModel : NotifyPropertyChanged
    {
        private DataTable _data;
        private DataTable _datab;
        private ICommand _addRowCommand;
        private ICommand _addColumnCommand;
        private ICommand _removeRowCommand;
        private ICommand _removeColumnCommand;
        private ICommand _setSizeCommand;
        private ICommand _cleanCommand;
        private ICommand _calcSumCommand;
        private int _rowsCount;
        private int _columnsCount;
        private long _sum;

        public DataTable Data // контейнер для матрицы, DataGrid привязана сюда
        {
            get => _data;
            set
            {
                _data = value;
                OnPropertyChanged();
            }
        }
        public DataTable Datab // контейнер для матрицы, DataGrid привязана сюда
        {
            get => _datab;
            set
            {
                _datab = value;
                OnPropertyChanged();
            }
        }
        public int RowsCount // тексбокс Строки привязан сюда
        {
            get => _rowsCount;
            set
            {
                _rowsCount = value;
                OnPropertyChanged();
            }
        }
        public int ColumnsCount // тексбокс Колонки привязан сюда
        {
            get => _columnsCount;
            set
            {
                _columnsCount = value;
                OnPropertyChanged();
            }
        }
        public long Sum // число суммы ячеек привязано сюда
        {
            get => _sum;
            set
            {
                _sum = value;
                OnPropertyChanged();
            }
        }

        private void AddRows(int count) // добавить строки
        {
            for (int i = 1; i <= count; i++)
                Data.Rows.Add();
            RowsCount = Data.Rows.Count;
        }
        private void AddColumns(int count) // добавить колонки
        {
            for (int i = 1; i <= count; i++)
            {
                Data.Columns.Add(new DataColumn(Data.Columns.Count.ToString(), typeof(int))
                {
                    AllowDBNull = false,
                    DefaultValue = 0
                });
            }
            Data = Data.Copy(); // здесь и далее - хак, способ полностью перерисовать таблицу DataGrid, так как она не поддерживает динамическое изменение колонок.
            ColumnsCount = Data.Columns.Count;
        }
        private void AddRowsb(int count) // добавить строки
        {
            for (int i = 1; i <= count; i++)
                Datab.Rows.Add();
        }
        
        private void RemoveRows(int count) // удалить строки (с конца)
        {
            for (int i = 1; i <= count && Data.Rows.Count > 1; i++)
                Data.Rows.RemoveAt(Data.Rows.Count - 1);
            RowsCount = Data.Rows.Count;
        }
        private void RemoveColumns(int count) // удалить колонки (с конца)
        {
            for (int i = 1; i <= count && Data.Columns.Count > 1; i++)
                Data.Columns.RemoveAt(Data.Columns.Count - 1);
            Data = Data.Copy();
            ColumnsCount = Data.Columns.Count;
        }
        private void RemoveRowsb(int count) // удалить строки (с конца)
        {
            for (int i = 1; i <= count && Datab.Rows.Count > 1; i++)
                Datab.Rows.RemoveAt(Datab.Rows.Count - 1);
        }

        // что делают команды, можно будет увидеть по биндингам на них у кнопок в xaml
        public ICommand AddRowCommand => _addRowCommand ?? (_addRowCommand = new RelayCommand(parameter =>
        {
            AddRows(1);
        }));
        public ICommand AddColumnCommand => _addColumnCommand ?? (_addColumnCommand = new RelayCommand(parameter =>
        {
            AddColumns(1);
        }));
        public ICommand RemoveRowCommand => _removeRowCommand ?? (_removeRowCommand = new RelayCommand(parameter =>
        {
            RemoveRows(1);
        }, parameter => Data.Rows.Count > 1));
        public ICommand RemoveColumnCommand => _removeColumnCommand ?? (_removeColumnCommand = new RelayCommand(parameter =>
        {
            RemoveColumns(1);
        }, parameter => Data.Columns.Count > 1));
        public ICommand SetSizeCommand => _setSizeCommand ?? (_setSizeCommand = new RelayCommand(parameter =>
        {
            if (RowsCount > Data.Rows.Count)
                AddRows(RowsCount - Data.Rows.Count);
            if (RowsCount < Data.Rows.Count)
                RemoveRows(Data.Rows.Count - RowsCount);
            if (ColumnsCount > Data.Columns.Count)
                AddColumns(ColumnsCount - Data.Columns.Count);
            if (ColumnsCount < Data.Columns.Count)
                RemoveColumns(Data.Columns.Count - ColumnsCount);

            if (RowsCount > Datab.Rows.Count)
                AddRowsb(RowsCount - Datab.Rows.Count);
            if (RowsCount < Datab.Rows.Count)
                RemoveRowsb(Datab.Rows.Count - RowsCount);
        }));
        public ICommand CleanCommand => _cleanCommand ?? (_cleanCommand = new RelayCommand(parameter =>
        {
            for (int i = 0; i < Data.Rows.Count; i++)
                for (int j = 0; j < Data.Columns.Count; j++)
                    Data.Rows[i][j] = 0;
            Data = Data.Copy();
        }));
        public ICommand CalcSumCommand => _calcSumCommand ?? (_calcSumCommand = new RelayCommand(parameter =>
        {
            long sum = 0;
            for (int i = 0; i < Data.Rows.Count; i++)
                for (int j = 0; j < Data.Columns.Count; j++)
                    sum += (int)Data.Rows[i][j];
            Sum = sum;
        }));

        public MainViewModel() // конструктор, выполняется 1 раз при запуске программы
        {
            Data = new DataTable();
            AddColumns(1);
            AddRows(1);
            Datab = new DataTable();
            AddRowsb(1);
            Datab.Columns.Add(new DataColumn(Datab.Columns.Count.ToString(), typeof(int))
            {
                AllowDBNull = false,
                DefaultValue = 0
            });
            Datab = Datab.Copy(); 
        }
    }
}
