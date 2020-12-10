using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MLAdapter;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace DummyGUI_MLAdapter
{
    class Program
    {
        static void Main(string[] args)
        {
            string filepath = @"../../../../../Beispieldaten.iris.txt";
            DataTable dt = HelperFunctions.ConvertCsvToDataTable(filepath);
            HelperFunctions.PrintDataTableToConsole(dt);
            HelperFunctions.AssignColumnNamesAndTypes(ref dt);
            string container = dt.Columns[0].ColumnName;
            var container_2 = dt.Columns[0].DataType;
            var value = dt.Rows[0][2].GetType();
            Console.WriteLine("********************************************************************************");
            List<List<float>> list = HelperFunctions.ConvertToDataList(dt);
            HelperFunctions.PrintDataListToConsole(list);
            var val = list[0][2].GetType();
            int[] inputColumns = new int[] {0, 1, 2, 3};
            List<ObjectData> objectDataList = HelperFunctions.CreateObjectData(dt, inputColumns , 4);

            SchemaDefinition schema = SchemaDefinition.Create(typeof(ObjectData));
            schema["FloatFeatures"].ColumnType = new VectorDataViewType(NumberDataViewType.Single, inputColumns.Length );



            MLAdapter.MLAdapter mLAdapter = new MLAdapter.MLAdapter();

            
            /*mLAdapter.TrainModel(dt);
            mLAdapter.TestModel(dt);
            DataTable data = new DataTable();
            List<int> results = mLAdapter.PredictAndReturnResults(data);
            
            
            mLAdapter.SaveModel("hier", "name");
            mLAdapter.LoadModel("dort");
            */
        }

       
    }
}
