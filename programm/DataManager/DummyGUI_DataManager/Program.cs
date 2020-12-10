using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataManager;
using System.Data;
using DummyMLAdapter;

namespace DummyGUI_DataManager
{
    class Program
    {
        static void Main(string[] args)
        {

            DataManagerClass dataManager = new DataManagerClass();
            DummyMLAdapterClass dummyMLAdapter = new DummyMLAdapterClass();
            string filepath = @"C:\Users\casa2\OneDrive\Dokumente\II_third_semester\programmierprojekt\machinelearning-samples-master\machinelearning-samples-master\datasets\taxi-fare-test1.CSV";
            string filepath2 = @"C:\Users\casa2\OneDrive\Dokumente\II_third_semester\programmierprojekt\machinelearning-samples-master\machinelearning-samples-master\datasets\FinalCSV.CSV";
            string filename = "";
            int[] dataColumns = new int[dataManager.UserTable.Columns.Count];
            int resultColumn = 0;
            List<int> mlResult = new List<int>();
            Dictionary<int, string> dataType = new Dictionary<int, string>();
            #region MethodCalls
            dataManager.LoadCSVTest(filepath);
            dataManager.SaveCSV(filepath2, filename);
            dataManager.SetColumnsDataType(dataType);
            dataManager.SetColumnsIOType( dataColumns,  resultColumn);
            dataManager.SetMLResult(mlResult);         
            #endregion
        }
    }
}
