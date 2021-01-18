using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MLAdapter;
using System.IO;
using System.Text.RegularExpressions;


namespace DummyGUI_MLAdapter
{
    class Program
    {
        static void Main(string[] args)
        {
            string filepath = @"../../../../../Beispieldaten_iris.txt";
            string testdata = @"../../../../../Testdaten_iris.txt";
            DataTable dt = HelperFunctions.ConvertCsvToDataTable(filepath);
            DataTable testdaten = HelperFunctions.ConvertCsvToDataTable(testdata);
            HelperFunctions.PrintDataTableToConsole(dt);
            // HelperFunctions.AssignColumnNamesAndTypes(ref dt);
            //string container = dt.Columns[0].ColumnName;
            //var container_2 = dt.Columns[0].DataType;
            //var value = dt.Rows[0][2].GetType();
            Console.WriteLine("********************************************************************************");
            int[] inputColumns = new int[] {0, 1, 2, 3};

            MLAdapter.MLAdapter mLAdapter = new MLAdapter.MLAdapter();
            mLAdapter.TrainModel(dt, inputColumns, 4);
            var results = mLAdapter.TestModel(testdaten, inputColumns, 4);

            
            /*mLAdapter.TrainModel(dt);
            mLAdapter.TestModel(dt);
            DataTable data = new DataTable();
            List<int> results = mLAdapter.PredictAndReturnResults(data);
            
            
            mLAdapter.SaveModel("hier", "name");
            mLAdapter.LoadModel("dort");
            */
        }

       
    }
}
