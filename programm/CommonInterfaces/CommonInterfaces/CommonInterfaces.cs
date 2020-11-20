using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CommonInterfaces
{
    public interface IMLAdapter<T>
    {
        void TrainModel(DataTable trainingData);
        List<int> TestModel(DataTable testData);
        void LoadModel(string filepath);
        void SaveModel(string filepath, string filename);
        List<int> PredictAndReturnResults(DataTable rawData);
    }



    public interface IDataManager
    {
        //readonly when instantiated
        DataTable UserTable { get; } 


        //readonly when instantiated
        List<int> ML_Result { get; }

        void LoadCSV(string filepath);
        void SaveCSV(string filepath, string filename);
        int GetAmountOfColumns();
        void SetColumnsType(int[] dataColumns, int resultColumn);
        void SetMLResult(List<int> mlResult);
        
    }

    public interface IStatistics
    {
        double calculateSensitivity(List<int> MLResult, List<int> realResult);

        double calculateSpecificity(List<int> MLResult, List<int> realResult);

        double calculatePrecision(List<int> MLResult, List<int> realResult);

        double calculateNegativePredictiveValue(List<int> MLResult, List<int> realResult);

        double calculatePositivePredictiveValue(List<int> MLResult, List<int> realResult);

        Dictionary<string, int> CalculateConfusionMatrix(DataTable MLResult, DataTable realResut);


    }
}
