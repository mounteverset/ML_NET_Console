using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CommonInterfaces;

namespace DummyMLAdapter
{
    public class DummyMLAdapterClass: IMLAdapter
    {
        #region Methods
        public void trainModel(DataTable trainingData)
        {
            throw new NotImplementedException();
            // Das ML.Model wird trainiert 
        }
        public void testModel(DataTable testData)
        {
            throw new NotImplementedException();
            // Das ML.Model wird getestet
        }
        public void loadModel(string filepath)
        {
            throw new NotImplementedException();
            // Das ML.Model wird heruntergeladen
        }
        public void saveModel(string filepath)
        {
            throw new NotImplementedException();
            // Das ML.Model wird gespeichert
        }
        public DataTable predictAndReturnResults(DataTable rawData)
        {
            //output
            return new DataTable();
        }
        #endregion
    }
}
