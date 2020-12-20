using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonInterfaces;
using System.Data;
using Microsoft.ML;
using Microsoft.ML.Data;


namespace MLAdapter
{
    public class MLAdapter : IMLAdapter
    {
        #region Attributes
        private MLContext MLContext { get; set; }
        private ITransformer MLModel { get; set; }
        private string ModelFilePath { get; set; }
        private IDataView TrainingData { get; set; }
        private IDataView TestData { get; set; }
        private IDataView UserData { get; set; }
        private PredictionEngine<object,object> PredictionEngine { get; set; }
        private SchemaDefinition _schemaDefinition = SchemaDefinition.Create(typeof(ObjectData));
        



        #endregion

        #region Public Methods
        public void TrainModel(DataTable trainingData, int[] inputColumns, int resultColumn)
        {
            List<ObjectData> data = CreateObjectDataList(trainingData, inputColumns, resultColumn);
            DefineSchemaDefinition(inputColumns);

            IDataView dataView = this.MLContext.Data.LoadFromEnumerable(data, this._schemaDefinition);
            var trainer = this.MLContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy(
                                                                            labelColumnName: "Target",
                                                                            featureColumnName: "FloatFeatures");
            //this.MLContext.Transforms.Categorical.
            //this.MLContext.
            var results = this.MLContext.MulticlassClassification.CrossValidate(dataView, trainer, 5, "Target");

            var matrix = results[0].Metrics.TopKAccuracy;
            IEnumerable<ITransformer> models =
                results
                    .OrderByDescending(fold => fold.Metrics.TopKAccuracy)
                    .Select(fold => fold.Model);
                    

            IEnumerable<double> topKResults = results.Select(fold => fold.Metrics.TopKAccuracy);
            Console.WriteLine(String.Join(", ", topKResults));
            var topModel = models.ToArray()[0];
            Console.WriteLine(models.ToArray()[0]);







        }
        public List<int> TestModel(DataTable testData, int[] inputColumns, int resultColumn)
        {
            


            throw new NotImplementedException();

        }
        public void LoadModel(string filepath)
        {
            throw new NotImplementedException();

        }
        public void SaveModel(string filepath, string filename)
        {
            throw new NotImplementedException();
        }


        public List<int> PredictAndReturnResults(DataTable rawData)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Methods

        public static List<ObjectData> CreateObjectDataList(DataTable dt, int[] inputColumns, int resultColumn)
        {
            List<ObjectData> objectDataList = new List<ObjectData>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                float[] floatFeatures = new float[inputColumns.Length];
                for (int j = 0; j < inputColumns.Length; j++)
                {
                    floatFeatures[j] = Convert.ToSingle(dt.Rows[i][inputColumns[j]]);
                }

                int result = Convert.ToInt32(dt.Rows[i][resultColumn]);
                objectDataList.Add(new ObjectData(floatFeatures, result));
            }

            return objectDataList;
        }

        private void CreateIDataViewFromDataTable(DataTable dataTable)
        {
            // magic happens here

            //var ts = dataTable.AsEnumerable().Select(row => new ObjectData()); //ObjectData als generische Klasse für die Inputs zum ML-Learning
            //IDataView dataView = this.MLContext.Data.LoadFromEnumerable(ts);

            //this.TrainingData = dataView;
            
            
        }

        private void DefineSchemaDefinition(int[] inputColumns)
        {
            //var uint_type = Type.GetType("UInt32");
            this._schemaDefinition["FloatFeatures"].ColumnType = new VectorDataViewType(NumberDataViewType.Single, inputColumns.Length);
            this._schemaDefinition["Target"].ColumnType = new KeyDataViewType(typeof(uint), 4);
        }
        #endregion

        #region Constructors
        public MLAdapter() 
        {
            this.MLContext = new MLContext();
        }

        public MLAdapter(int seed)
        {
            this.MLContext = new MLContext(seed);
        }

        #endregion
    }
}
