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
            string filepath = @"../../../../Beispieldaten.iris.txt";
            DataTable dt = HelperFunctions.ConvertCSVToDataTable(filepath);
            HelperFunctions.PrintDataTableToConsole(dt);
          
            MLAdadpter mLAdapter = new MLAdadpter();           
            mLAdapter.TrainModel(dt);
            mLAdapter.TestModel(dt);
            DataTable data = new DataTable();
            List<int> results = mLAdapter.PredictAndReturnResults(data);
            
            
            mLAdapter.SaveModel("hier");
            mLAdapter.LoadModel("dort");
            
        }
       
    }
}
