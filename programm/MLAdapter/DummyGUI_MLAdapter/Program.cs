using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MLAdapter;


namespace DummyGUI_MLAdapter
{
    class Program
    {
        static void Main(string[] args)
        {
            
            MLAdapter.MLAdadpter mLAdapter = new MLAdadpter();
            mLAdapter.TrainModel(new DataTable());
            mLAdapter.TestModel(new DataTable());
            DataTable data = new DataTable();
            List<int> results = mLAdapter.PredictAndReturnResults(data);
            
            
            mLAdapter.SaveModel("hier");
            mLAdapter.LoadModel("dort");

        }
    }
}
