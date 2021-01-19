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
            string filepath = @"C:\Users\casa2\OneDrive\Dokumente\II_third_semester\programmierprojekt\neuronale_netzwerke\programm\DataManager\taxi-fare-test1.CSV";
            string filename = "FinalCSV";
            string filepath2 = "";
            int[] dataColumns = new int[2];
            int resultColumn = 0;
            List<int> mlResult = new List<int> { 1, 3, 5, 7, 8 };
            Dictionary<int, string> dataType = new Dictionary<int, string>();
            dataType.Add(0, "System.Int32");
            dataType.Add(1, "System.Double");
            dataType.Add(2, "System.Boolean");
            #region MethodCalls
            dataManager.LoadCSV(filepath);
            dataManager.SetColumnsDataType(dataType);
            dataManager.SetColumnsIOType( dataColumns,  resultColumn);
            dataManager.SetMLResult(mlResult);
            PrintDataTableToConsole(dataManager.UserTable);
            dataManager.SaveCSV(filepath2, filename);
            #endregion

        }

        public static void PrintDataTableToConsole(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++) //looping through all rows including the column. change i=1 if need to exclude the columns display
            {
                for (int j = 0; j < dt.Columns.Count; j++) //looping through all columns
                {
                    Console.Write(dt.Rows[i][j] + " "); //display of the data
                }
                Console.WriteLine();
            }
        }
    }
}
