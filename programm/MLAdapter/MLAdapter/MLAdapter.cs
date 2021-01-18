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
using System.IO;


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
        private PredictionEngine<ObjectData,ObjectPrediction> PredictionEngine { get; set; }

        private SchemaDefinition _schemaDefinition = SchemaDefinition.Create(typeof(ObjectData));

        private int[] InputColumns { get; set; }
        private string[] StringInputColumns { get; set; }

        private DataViewSchema InputSchema { get; set; }

        #endregion

        #region Public Methods
        /// <summary>
        /// Training des Machine Learning Modells mit einem Testdatensatz
        /// </summary>
        /// <param name="trainingData"></param>
        /// <param name="inputColumns">Die für das Modell relevanten Spaltennummern als Null-indexiertes Array</param>
        /// <param name="resultColumn">Die Spaltennummer mit den Labels der Daten</param>
        public void TrainModel(DataTable trainingData, int[] inputColumns, int resultColumn)
        {
            // if abfrage für den fall dass die label column ein string ist
            // dann mit der methode map value to key arbeiten

            //die anzahl der verschiedenen Values der Labels durch Kreiren eines Hashsets herausfinden und dann Set.Count
            this.InputColumns = inputColumns;

            List<ObjectData> data = CreateObjectDataList(trainingData, inputColumns, resultColumn);
            int uniqueValues = GetNumberOfUniqueValues(trainingData, resultColumn);
            DefineSchemaDefinition(inputColumns, uniqueValues);

            this.TrainingData = this.MLContext.Data.LoadFromEnumerable(data, this._schemaDefinition);

            //Regression warscheinlich nicht gut für Klassifizerung
            //var trainer = this.MLContext.Regression.Trainers.FastTree(
            //                                                    labelColumnName: "Target",
            //                                                    featureColumnName: "FloatFeatures");

            var trainer2 = this.MLContext.BinaryClassification.Trainers.FastTree(
                                                                        labelColumnName: "Target",
                                                                        featureColumnName: "FloatFeatures");

            var trainer3 = this.MLContext.MulticlassClassification.Trainers.OneVersusAll(
                                                                        binaryEstimator: trainer2,
                                                                        labelColumnName: "Target");

            this.MLModel = trainer3.Fit(this.TrainingData);

            this.PredictionEngine = this.MLContext.Model.
                                        CreatePredictionEngine<ObjectData, ObjectPrediction>(
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

        /// <summary>
        /// Trainieren des MachineLearningModels mit den Spaltennamen anstatt den Spaltennummern
        /// </summary>
        /// <param name="trainingData"></param>
        /// <param name="inputColumns"></param>
        /// <param name="resultColumn"></param>
        public void TrainModel(DataTable trainingData, string[] inputColumns, string resultColumn)
        {
            // tbd
        }
        /// <summary>
        /// MLAdapter gibt die vorhergesagten Werte für die Testdaten zurück, damit sie nachgelagert zur Analyse des Modells verwendet werden können.
        /// </summary>
        /// <param name="testData"></param>
        /// <param name="inputColumns"></param>
        /// <param name="resultColumn"></param>
        /// <returns></returns>
        public List<int> TestModel(DataTable testData, int[] inputColumns, int resultColumn)
        {
            //Check ob beim Trainieren inputColumns belegt wurden und ob diese die gleichen sind für das Trainieren
            if (this.InputColumns != null && this.InputColumns.SequenceEqual(inputColumns))
            {
                return PredictAndReturnResults(testData, inputColumns);
            }
            return new List<int>();
        }
        public void LoadModel(string filepath)
        {            
            this.MLContext.Model.Load(filePath: filepath, out var inputSchema);
            this.InputSchema = inputSchema;
        }
        public void SaveModel(string filepath, string filename)
        {

            this.MLContext.Model.Save(this.MLModel, this.TrainingData.Schema, filePath: filepath);
            using (StreamWriter sw = new StreamWriter(filepath+filename))
            {
                for (int i = 0; i < this.InputColumns.Length; i++)
                {
                    sw.Write(this.InputColumns[i] + ",");
                }  
            }
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
                
                results.Add((int)singlePrediction.Label-1);
            }

            return results;
        }

        public List<int> PredictAndReturnResults(DataTable rawData)
        {
            if (this.InputColumns != null)
            {
                return PredictAndReturnResults(rawData, this.InputColumns);
            }
            return new List<int>(); // nicht sauber, throwException besser hier mit try catch Block
        }

        #endregion

        #region Private Methods
        private static int GetNumberOfUniqueValues(DataTable dt, int resultColumn)
        {
            HashSet<int> set = new HashSet<int>();

            for (int i = 0; i < dt.Rows.Count-1; i++)
            {
                set.Add(Convert.ToInt32(dt.Rows[i][resultColumn]));
            }

            return set.Count;
        }
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

        private static List<ObjectData> CreateObjectDataList(DataTable dt, string[] inputColumns, string resultColumn)
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

        private void DefineSchemaDefinition(int[] inputColumns, int numberOfUniqueValues)
        {
            //var uint_type = Type.GetType("UInt32");
            this._schemaDefinition["FloatFeatures"].ColumnType = new VectorDataViewType(NumberDataViewType.Single, inputColumns.Length);
            this._schemaDefinition["Target"].ColumnType = new KeyDataViewType(typeof(uint), numberOfUniqueValues);
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
