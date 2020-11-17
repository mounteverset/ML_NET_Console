using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }

    public interface IStatistics
    {

    }
}
