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
            mLAdapter.trainModel(new DataTable());
            mLAdapter.testModel(new DataTable());
            DataTable results = new DataTable();
            mLAdapter.predictAndReturnResults(results);
            var resultView = results.DefaultView;
            
            mLAdapter.saveModel("hier");
            mLAdapter.loadModel("dort");

        }
    }
}
