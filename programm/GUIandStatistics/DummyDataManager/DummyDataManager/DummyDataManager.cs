using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonInterfaces;

namespace DummyDataManager
{
    public class DummyDataManager : IDataManager
    {
        public DataTable UserTable => throw new NotImplementedException();

        public int AmountOfColumns { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int AmountOfRows { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public DataTable ML_Result => throw new NotImplementedException();

        public DataTable convertCSVtoDataTable()    // muss nicht Teil des Interfaces sein
        {
            throw new NotImplementedException();
        }

        public int getAmountOfColumns()
        {
            return 4;   // schön Wären hier Coner-Cases und in Abstimmung mit den tatsächlichen Spalten der Datatable
        }

        public void loadCSV(string filepath)
        {
            // Exceptionhandling nötig im Dummy?
        }

        public void saveCSV(string filepath, string filename)
        {
            // Exceptionhandling nötig im Dummy?
        }

        public void setColumnsType(int[] dataColumns, int resultColumn)
        {
           // Exceptionhandling nötig im Dummy?
        }

        public void setMLResult(DataTable mlResult)
        {
            // Exceptionhandling nötig im Dummy?
        }
    }
}
