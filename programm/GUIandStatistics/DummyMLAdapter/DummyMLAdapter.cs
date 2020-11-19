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
        public void loadModel(string filepath)
        {
            // ExceptionHandling nötig im Dummy?
        }

        public DataTable predictAndReturnResults(DataTable rawData)
        {
            throw new NotImplementedException();
        }

        public void saveModel(string filepath)
        {
            // ExceptionHandling nötig im Dummy?
        }

        public void testModel(DataTable testData)   // müsste hier Datatable zurückgegeben werden mit den Ergebnissen?
                                                    // da das Ergebnis angezeigt und bewerten werden muss
        {
            // ExceptionHandling nötig im Dummy?
        }

        public void trainModel(DataTable trainingData)
        {
            // ExceptionHandling nötig im Dummy?
        }
    }
}
