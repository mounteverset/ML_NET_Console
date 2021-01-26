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
        public void TrainModel(DataTable trainingData, int[] inputColumns, int resultColumn)
        {
            throw new NotImplementedException();
            // Das ML.Model wird trainiert 
        }
        public List<int> TestModel(DataTable testData, int[] inputColumns, int resultColumn)
        {           
            // Das ML.Model wird getestet
            return new List<int>();
        }
        public void LoadModel(string filepath)
        {
            throw new NotImplementedException();
            // Das ML.Model wird heruntergeladen
        }
        public void SaveModel(string filepath, string filename)
        {
            throw new NotImplementedException();
            // Das ML.Model wird gespeichert
        }
        public List<int> PredictAndReturnResults(DataTable rawData, int[] inputColumns)
        {
            //output
            return new List<int>();
        }
        #endregion
    }
}
