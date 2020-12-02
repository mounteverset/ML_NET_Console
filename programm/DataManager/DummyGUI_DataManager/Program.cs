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
            string filename = "";
            int[] dataColumns = new int[dataManager.UserTable.Columns.Count];
            int resultColumn = 0;
            dataManager.SetColumnsType(dataColumns, resultColumn);
            List<int> mlResult = new List<int>();
            #region MethodCalls
            //dataManager.LoadCSV(filepath);
            dataManager.SaveCSV(filepath, filename);
            dataManager.SetColumnsType(dataColumns, resultColumn);
            dataManager.SetMLResult(mlResult);
            //dummyMLAdapter.LoadModel(filepath);
            //dummyMLAdapter.TrainModel(dataManager.UserTable);
            //dummyMLAdapter.TestModel(dataManager.UserTable);
            //dummyMLAdapter.PredictAndReturnResults(dataManager.UserTable);
     
            #endregion
        }
    }
}
