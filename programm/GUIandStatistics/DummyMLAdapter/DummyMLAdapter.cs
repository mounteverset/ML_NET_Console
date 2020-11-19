using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonInterfaces;

namespace DummyMLAdapter
{
    public class MLAdapter : IMLAdapter
    {
        public void LoadModel(string filepath)
        {
            // ExceptionHandling nötig im Dummy?
        }


        public void SaveModel(string filepath, string filename)
        {
            // ExceptionHandling nötig im Dummy?
        }

        public List<int> TestModel(DataTable testData)   // müsste hier Datatable zurückgegeben werden mit den Ergebnissen?
                                                    // da das Ergebnis angezeigt und bewerten werden muss
        {
            List<int> testresult = new List<int>
            {
                2,
                1,
                0,
                1,
                1,
                2
            };
            return testresult;
            // ExceptionHandling nötig im Dummy?
        }

        public void TrainModel(DataTable trainingData)
        {
            // ExceptionHandling nötig im Dummy?
        }



        List<int> IMLAdapter.PredictAndReturnResults(DataTable rawData)
        {
            List<int> result = new List<int>
            {
                0,
                2,
                1,
                2,
                2,
                0
            };
            return result;
        }
    }
}
