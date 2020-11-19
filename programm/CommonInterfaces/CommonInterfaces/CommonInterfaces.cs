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
        void trainModel(DataTable trainingData);
        List<int> testModel(DataTable testData);
        void loadModel(string filepath);
        void saveModel(string filepath);
        List<int> predictAndReturnResults(DataTable rawData);
    }



    public interface IDataManager
    {
        //readonly when instantiated
        DataTable UserTable { get; } 

        int AmountOfColumns { get; set; }
        int AmountOfRows { get; set; }

        //readonly when instantiated
        DataTable ML_Result { get; }

        void loadCSV(string filepath);
        void saveCSV(string filepath, string filename);
        int getAmountOfColumns();
        void setColumnsType(int[] dataColumns, int resultColumn);
        void setMLResult(List<int> mlResult);
        
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
