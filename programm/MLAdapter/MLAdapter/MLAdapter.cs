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
        private IDataView TrainingData { get; set; }
        private IDataView TestData { get; set; }
        private IDataView UserData { get; set; }
        private PredictionEngine<ObjectData,ObjectPrediction> PredictionEngine { get; set; }
        private SchemaDefinition _schemaDefinition = SchemaDefinition.Create(typeof(ObjectData));
        //private string ModelFilePath { get; set; } not required nor used in this class

        private int[] InputColumns { get; set; }
        private string[] StringInputColumns { get; set; }

        private DataViewSchema InputSchema { get; set; }

        #endregion

        #region Public Methods
        /// <summary>
        /// Method to train a ML model for multiclass classification problems
        /// </summary>
        /// <param name="trainingData">Input data used to train the model, values should be floating point numbers</param>
        /// <param name="inputColumns">Contains the column numbers of the columns used as an input for the ML model training</param>
        /// <param name="resultColumn">Contains the labels of the corresponding DataRows in the table</param>
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
        /*
        public void TrainModel(DataTable trainingData, string[] inputColumns, string resultColumn)
        {
            // tbd
        }
        */

        /// <summary>
        /// Returns the predicted values for the input values, in order to be analyzed in another function
        /// </summary>
        /// <param name="testData">Input data to test the previously trained model</param>
        /// <param name="inputColumns">Contains the column numbers of the columns used as an input for the ML model prediction</param>
        /// <param name="resultColumn"></param>
        /// <returns></returns>
        public List<int> TestModel(DataTable testData, int[] inputColumns, int resultColumn)
        {
            List<ObjectData> data = CreateObjectDataList(testData, inputColumns, resultColumn);
            this.TestData = this.MLContext.Data.LoadFromEnumerable(data, this._schemaDefinition);

            return PredictAndReturnResults(testData, inputColumns);            
        }

        /// <summary>
        /// Loads a previously trained model from a .zip file
        /// </summary>
        /// <param name="filepath">Contains the full path to the file including the filename</param>
        public void LoadModel(string filepath)
        {            
            this.MLModel = this.MLContext.Model.Load(filePath: filepath, out var inputSchema);
            
            DefineSchemaDefinition(inputSchema);
            this.PredictionEngine = this.MLContext.Model.
                                        CreatePredictionEngine<ObjectData, ObjectPrediction>(
                                        this.MLModel,
                                        inputSchema);
        }

        /// <summary>
        /// Saves the model as a .zip file
        /// </summary>
        /// <param name="filepath">Contains the full path to the specified folder without double backslashes</param>
        /// <param name="filename">Contains the specified filename including .zip</param>
        public void SaveModel(string filepath, string filename)
        {
            this.MLContext.Model.Save(this.MLModel, this.TrainingData.Schema, filePath: Path.Combine(filepath, filename));
        }

        /// <summary>
        /// After successfully training the model, this function can be used to receive a prediction from the model given a set of input
        /// </summary>
        /// <param name="rawData">Contains all of the required input data for making a prediction using the trained ML Model, the features should match the structure of the training data</param>
        /// <param name="inputColumns">Specifies which column numbers are used as input data for the ML Model, these have to match the input columns from the training data</param>
        /// <returns>Returns a list of predictions for each DataRow in the DataTable</returns>
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

        #endregion

        /// <summary>
        /// Used to determine the number of different label classes
        /// </summary>
        /// <param name="dt">The input DataTable</param>
        /// <param name="resultColumn">The column number for the label column</param>
        /// <returns></returns>
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
        /// <summary>
        /// Helper Function to create lists of the custom ObjectData Type from the passed-in DataTable, used when testing and predicting values, as the label column is left out
        /// </summary>
        /// <param name="dt">The input DataTable</param>
        /// <param name="inputColumns">Contains the column numbers of the columns used as an input for the ML model prediction</param>
        /// <returns></returns>
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

        /// <summary>
        /// Helper Function to create lists of the custom ObjectData Type from the passed-in DataTable, used when training the model
        /// </summary>
        /// <param name="dt">The input DataTable</param>
        /// <param name="inputColumns">Contains the column numbers of the columns used as an input for the ML model training</param>
        /// <param name="resultColumn">The column number for the label column</param>
        /// <returns></returns>
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

        /// <summary>
        /// Helper Function to create lists of the custom ObjectData Type from the passed-in DataTable, used when training the model
        /// </summary>
        /// <param name="dt">The input DataTable</param>
        /// <param name="inputColumns">Contains the column names of the columns used as an input for the ML model training</param>
        /// <param name="resultColumn">The column name for the label column</param>
        /// <returns></returns>
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

        /*
        private void CreateIDataViewFromDataTable(DataTable dataTable)
        {
            
            //var ts = dataTable.AsEnumerable().Select(row => new ObjectData()); //ObjectData als generische Klasse für die Inputs zum ML-Learning
            //IDataView dataView = this.MLContext.Data.LoadFromEnumerable(ts);

            //this.TrainingData = dataView;
        }
        */

        private void DefineSchemaDefinition(DataViewSchema inputSchema)
        {
            this._schemaDefinition["FloatFeatures"].ColumnType = inputSchema[0].Type;
            this._schemaDefinition["Target"].ColumnType = inputSchema[1].Type;
        }

        /// <summary>
        /// Helper Function to create a valid Schema Defintion which is required to create an IDataView Object based on the ObjectData class
        /// </summary>
        /// <param name="inputColumns">Contains the column names of the columns used as an input for the ML model training</param>
        /// <param name="numberOfUniqueValues">Number of different label classes</param>
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
        /// <summary>
        /// Creates a new MLAdapter with a random seed
        /// </summary>
        public MLAdapter() 
        {
            this.MLContext = new MLContext();
        }
        /// <summary>
        /// Creates a new MLAdapter with a seed number, if a fixed seed
        ///  is provided, MLContext environment becomes deterministic, meaning that
        ///  the results are repeatable and will remain the same across multiple runs
        /// </summary>
        /// <param name="seed">Fixed seed number to create a deterministic MLContext</param>
        public MLAdapter(int seed)
        {
            this.MLContext = new MLContext(seed);
        }

        #endregion
    }
}
