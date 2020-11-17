using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CommonInterfaces
{
    public interface IMLAdapter
    {
        void trainModel(DataTable trainingData);
        void testModel(DataTable testData);
        void loadModel(string filepath);
        void saveModel(string filepath);
        DataTable predictAndReturnResults(DataTable rawData);
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
        void setMLResult(DataTable mlResult);
        DataTable convertCSVtoDataTable();
    }

    public interface IStatistics
    {
        DataTable confusionMatrix(DataTable MLResult, DataTable realResut);
        double CalculatecertainErrorDimensions(DataTable MLResult, DataTable realResut);

    }
}
