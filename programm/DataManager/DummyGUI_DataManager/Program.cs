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
            string filepath = @"C:\Users\casa2\OneDrive\Dokumente\II_third_semester\programmierprojekt\neuronale_netzwerke\programm\DataManager\testFile.CSV";
            string filename = "finalFile";
            List<int> mlResult = new List<int> { 1, 3, 5, 7, 8 };
            Dictionary<int, string> dataType = new Dictionary<int, string>();
            dataType.Add(0, "System.Int32");
            dataType.Add(1, "System.Double");
            dataType.Add(2, "System.Boolean");
            dataType.Add(3, "System.Int32");
            #region MethodCalls
            dataManager.LoadCSV(filepath);
            dataManager.SetColumnsDataType(dataType);
            dataManager.SetMLResult(mlResult);
            dataManager.SaveCSV(filepath, filename);
            #endregion

        }
       
    }
}
