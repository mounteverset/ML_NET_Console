using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CommonInterfaces
{

    /// <summary>
    /// 
    /// </summary>
    public interface IMLAdapter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="trainingData"></param>
        void TrainModel(DataTable trainingData);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testData"></param>
        /// <returns></returns>
        List<int> TestModel(DataTable testData);
        void LoadModel(string filepath);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="filename"></param>
        void SaveModel(string filepath, string filename);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        List<int> PredictAndReturnResults(DataTable rawData);
    }


    /// <summary>
    /// 
    /// </summary>
    public interface IDataManager
    {
        //readonly when instantiated
        /// <summary>
        /// 
        /// </summary>
        DataTable UserTable { get; } 


        //readonly when instantiated
        /// <summary>
        /// 
        /// </summary>
        List<int> ML_Result { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        void LoadCSV(string filepath);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="filename"></param>
        void SaveCSV(string filepath, string filename);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int GetAmountOfColumns();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataColumns"></param>
        /// <param name="resultColumn"></param>
        void SetColumnsType(int[] dataColumns, int resultColumn);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mlResult"></param>
        void SetMLResult(List<int> mlResult);
        
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IStatistics
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MLResult"></param>
        /// <param name="realResult"></param>
        /// <returns></returns>
        double CalculateSensitivity(List<int> MLResult, List<int> realResult);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MLResult"></param>
        /// <param name="realResult"></param>
        /// <returns></returns>
        double CalculateSpecificity(List<int> MLResult, List<int> realResult);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MLResult"></param>
        /// <param name="realResult"></param>
        /// <returns></returns>
        double CalculatePrecision(List<int> MLResult, List<int> realResult);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MLResult"></param>
        /// <param name="realResult"></param>
        /// <returns></returns>
        double CalculateNegativePredictiveValue(List<int> MLResult, List<int> realResult);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MLResult"></param>
        /// <param name="realResult"></param>
        /// <returns></returns>
        double CalculatePositivePredictiveValue(List<int> MLResult, List<int> realResult);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MLResult"></param>
        /// <param name="realResut"></param>
        /// <returns></returns>
        Dictionary<string, int> CalculateConfusionMatrix(DataTable MLResult, DataTable realResut);


    }
}
