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
            List<int> mlResult = new List<int>();
            #region MethodCalls
            dataManager.LoadCSV(filepath);
            dataManager.SaveCSV(filepath, filename);
            dataManager.AmountOfColumns= dataManager.GetAmountOfColumns();
            dataManager.SetColumnsType(dataColumns, resultColumn);
            dataManager.SetMLResult(mlResult);
            dataManager.ConvertCSVtoDataTable();
            #endregion
        }
    }
}
