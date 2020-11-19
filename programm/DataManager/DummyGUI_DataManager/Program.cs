using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataManager;
using System.Data;

namespace DummyGUI_DataManager
{
    class Program
    {
        static void Main(string[] args)
        {

            DataManagerClass dataManager = new DataManagerClass();
            string filepath = "";
            string filename = "";
            int[] dataColumns = new int[dataManager.AmountOfColumns];
            int resultColumn = 0;
            DataTable MLResult = new DataTable();
            #region MethodCalls
            dataManager.loadCSV(filepath);
            dataManager.saveCSV(filepath, filename);
            dataManager.AmountOfColumns= dataManager.getAmountOfColumns();
            dataManager.setColumnsType(dataColumns, resultColumn);
            dataManager.setMLResult(MLResult);
            dataManager.convertCSVtoDataTable();
            #endregion
        }
    }
}
