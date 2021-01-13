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
    public class MLAdapter //: IMLAdapter
    {
        #region Attributes
        private MLContext MLContext { get; set; }
        private ITransformer MLModel { get; set; }
        private string ModelFilePath { get; set; }
        private IDataView TrainingData { get; set; }
        private IDataView TestData { get; set; }
        private IDataView UserData { get; set; }
        private PredictionEngine<ObjectData,ObjectPrediction> PredictionEngine { get; set; }

        private SchemaDefinition _schemaDefinition = SchemaDefinition.Create(typeof(ObjectData));

        #endregion

        #region Public Methods
        public void TrainModel(DataTable trainingData, int[] inputColumns, int resultColumn)
        {
            List<ObjectData> data = CreateObjectDataList(trainingData, inputColumns, resultColumn);
            DefineSchemaDefinition(inputColumns);
            

            this.TrainingData = this.MLContext.Data.LoadFromEnumerable(data, this._schemaDefinition);

            //Regression warscheinlich nicht gut für Klassifizerung
            var trainer = this.MLContext.Regression.Trainers.FastTree(
                                                                labelColumnName: "Target",
                                                                featureColumnName: "FloatFeatures");

            var trainer2 = this.MLContext.BinaryClassification.Trainers.FastTree(
                                                                        labelColumnName: "Target",
                                                                        featureColumnName: "FloatFeatures");

            var trainer3 = this.MLContext.MulticlassClassification.Trainers.OneVersusAll(
                                                                                binaryEstimator: trainer2,
                                                                                labelColumnName: "Target");

            this.MLModel = trainer3.Fit(this.TrainingData);

            this.PredictionEngine = this.MLContext.Model.CreatePredictionEngine<ObjectData, ObjectPrediction>(
                this.MLModel,
                inputSchemaDefinition: this._schemaDefinition); 
                //outputSchemaDefinition:SchemaDefinition.Create(typeof(ObjectPrediction)));


            //var crossValidationResults = this.MLContext.MulticlassClassification.CrossValidate(this.TrainingData, trainer, 5, "Target");


            //var matrix = results[0].Metrics.TopKAccuracy;
            //IEnumerable<ITransformer> models = results.OrderByDescending(fold => fold.Metrics.TopKAccuracy).Select(fold => fold.Model);
            //IEnumerable<double> topKResults = results.Select(fold => fold.Metrics.TopKAccuracy);
            //Console.WriteLine(String.Join(", ", topKResults));
            //var topModel = models.ToArray()[0];
            //Console.WriteLine(models.ToArray()[0]);
        }
        public List<int> TestModel(DataTable testData, int[] inputColumns, int resultColumn)
        {
            return PredictAndReturnResults(testData, inputColumns);
        }
        public void LoadModel(string filepath)
        {
            throw new NotImplementedException();

        }
        public void SaveModel(string filepath, string filename)
        {
            throw new NotImplementedException();
        }

        public List<int> PredictAndReturnResults(DataTable rawData, int[] inputColumns)
        {
            //eine ObjectDataListe erstellen
            List<ObjectData> data = CreateObjectDataList(rawData, inputColumns);
            List<int> results = new List<int>();
            
            //for Schleife für alle Rows in rawData und dann das Ergebnis in eine Liste schreiben
            foreach (ObjectData row in data)
            {
                var singlePrediction = this.PredictionEngine.Predict(row);
                
                results.Add((int)singlePrediction.Label);
            }

            return results;
        }

        public List<int> PredictAndReturnResults(DataTable rawData)
        {
            var predictionEngine = this.MLContext.Model.CreatePredictionEngine<ObjectData, ObjectPrediction>(this.MLModel, this.TestData.Schema);
            float[] features = new float[4]{1, 3, 4, 3};
            ObjectData singleData = new ObjectData(features, 1);
            ObjectPrediction prediction = predictionEngine.Predict(singleData);
            
            return new List<int>();
        }

        #endregion

        #region Private Methods

        private static List<ObjectData> CreateObjectDataList(DataTable dt, int[] inputColumns)
        {
            List<ObjectData> objectDataList = new List<ObjectData>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                float[] floatFeatures = new float[inputColumns.Length];
                for (int j = 0; j < inputColumns.Length; j++)
                {
                    floatFeatures[j] = Convert.ToSingle(dt.Rows[i][inputColumns[j]]);
                }
                objectDataList.Add(new ObjectData(floatFeatures));
            }
            return objectDataList;
        }

        private static List<ObjectData> CreateObjectDataList(DataTable dt, int[] inputColumns, int resultColumn)
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
            this._schemaDefinition["Target"].ColumnType = new KeyDataViewType(typeof(uint), 3); //die 4 entspricht der Anzahl der versch. Iris- Klassen
            //this._schemaDefinition["Label"].ColumnType = NumberDataViewType.Single 
            //this._schemaDefinition["Target"].ColumnType = new KeyDataViewType(typeof(uint), 4);
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
