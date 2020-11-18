using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonInterfaces;
using System.Data;
using Microsoft.ML;


namespace MLAdapter
{
    public class MLAdadpter : IMLAdapter
    {
        #region Attributes
        private MLContext MLContext { get; set; }
        private ITransformer MLModel { get; set; }
        private string ModelFilePath { get; set; }
        private IDataView TrainingData { get; set; }
        private IDataView TestData { get; set; }
        private IDataView UserData { get; set; }
        private PredictionEngine<object,object> PredictionEngine { get; set; }


        #endregion

        #region Methods
        public void trainModel(DataTable trainingData)
        {
            throw new NotImplementedException();
        }
        public void testModel(DataTable testData)
        {
            throw new NotImplementedException();

        }
        public void loadModel(string filepath)
        {
            throw new NotImplementedException();

        }
        public void saveModel(string filepath)
        {
            throw new NotImplementedException();
        }
        public DataTable predictAndReturnResults(DataTable rawData)
        {

            return new DataTable();

        }
        #endregion

        #region Constructors
        public MLAdadpter() { }

        #endregion
    }
}
