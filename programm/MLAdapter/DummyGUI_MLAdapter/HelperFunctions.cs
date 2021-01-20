using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using MLAdapter;



namespace DummyGUI_MLAdapter
{
    public class HelperFunctions
    {
        /// <summary>
        /// Outputs all values from the DataTable that is passed in
        /// </summary>
        /// <param name="dt">The table which will be printed into the Console</param>
        public static void PrintDataTableToConsole(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++) //looping through all rows including the column. change `i=1` if need to exclude the columns display
            {
                for (int j = 0; j < dt.Columns.Count; j++) //looping through all columns
                {
                    Console.Write(dt.Rows[i][j] + " "); //display of the data
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Reads in a CSV
        /// </summary>
        /// <param name="filepath">Filepath to the CSV file including the filename and file type</param>
        /// <param name="hasHeader">If the CSV has got a header row they will be used to name the corresponding column in the DataTable</param>
        /// <returns>Returns a DataTable in which the values from the CSV are stored, the values are of type string</returns>
        public static DataTable ConvertCsvToDataTable(string filepath, bool hasHeader)
        {
            DataTable dt = new DataTable();
            Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

            using (StreamReader sr = new StreamReader(filepath))
            {
                if (hasHeader == true)
                {
                    string[] headers = sr.ReadLine().Split(',');
                    foreach (string header in headers)
                    {
                        dt.Columns.Add(header);
                    }
                    while (!sr.EndOfStream)
                    {
                        string[] rows = CSVParser.Split(sr.ReadLine());
                        DataRow dr = dt.NewRow();

                        for (int i = 0; i < headers.Length; i++)
                        {
                            dr[i] = float.Parse(rows[i].Replace("\"", string.Empty), CultureInfo.InvariantCulture);
                        }
                        dt.Rows.Add(dr);
                    }
                }
                else
                {
                    string[] firstLine = sr.ReadLine().Split(',');
                    
                    for (int i = 0; i < firstLine.Length; i++)
                    {
                        dt.Columns.Add(i.ToString());
                    }
                    DataRow firstRow = dt.NewRow();
                    for (int i = 0; i < firstLine.Length; i++)
                    {
                        firstRow[i] = float.Parse(firstLine[i].Replace("\"", string.Empty), CultureInfo.InvariantCulture);
                    }
                    dt.Rows.Add(firstRow);
                    while (!sr.EndOfStream)
                    {
                        string[] rows = CSVParser.Split(sr.ReadLine());
                        DataRow dr = dt.NewRow();

                        for (int i = 0; i < firstLine.Length; i++)
                        {
                            dr[i] = float.Parse(rows[i].Replace("\"", string.Empty), CultureInfo.InvariantCulture);
                        }
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }

        //obsolete
        public static void AssignColumnNamesAndTypes(ref DataTable dt)
        {
            string[] names = new string[] { "Sepal length", "Sepal width", "Petal length", "Petal width", "Label" };

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dt.Columns[i].ColumnName = names[i];;
            }

            //dt.Columns[0].DataType =  
            
        }

        //obsolete
        public static List<List<float>> ConvertToDataList (DataTable dt)
        {
            List<List<float>> dataList = new List<List<float>>();
            DataRowCollection dataRows = dt.Rows;

            //outer loop for traversing every row and adding it to DataList at the end
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                List<float> row = new List<float>();
                //inner loop to create a List of floats from the current DataRow and add it to the
                for (int j = 0; j < dataRows[i].ItemArray.Length; j++)
                {
                    row.Add(Convert.ToSingle(dataRows[i][j]));
                }
                dataList.Add(row);
            }


            return dataList;
        }

        //obsolete
        public static void PrintDataListToConsole(List<List<float>> dataList)
        {
            for (int i = 0; i < dataList.Count; i++)
            {
                for (int j = 0; j < dataList[i].Count; j++)
                {
                    Console.Write(dataList[i][j] + " ");
                }
                Console.WriteLine("");
            }
        }

        /// <summary>
        /// For every row in the DataTable object a new DataObject gets created and added to the list which is returned after processing each line
        /// </summary>
        /// <param name="dt">DataTable object which stores all the values</param>
        /// <param name="inputColumns">Zero-based number of all the columns which are inputs for the machine learning model</param>
        /// <param name="resultColumn">Zero-based column number of the result column</param>
        /// <returns></returns>
        public static List<ObjectData> CreateObjectData(DataTable dt, int[] inputColumns, int resultColumn)
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
        /// Statistical Analysis for a machine learning model
        /// </summary>
        /// <param name="testData">Input DataTable, has to be the same one which was used to get the predictions from the model</param>
        /// <param name="mlResults">The predicted values produced my be the machine learning model</param>
        /// <param name="resultColumn">Specifies the number of the label column in the testdata DataTable (0-indexed)</param>
        /// <returns>Percentage of how many times the model predicted the correct class</returns>
        public static double GetModelAccuracy (DataTable testData, List<int> mlResults, int resultColumn)
        {
            //Extract the label from the testdata into a new list
            List<int> actualLabels = new List<int>();
            foreach (DataRow row in testData.Rows)
            {
                actualLabels.Add(Convert.ToInt32(row[resultColumn]));
            }

            // Get the number of times the results matches the truth
            int counter = 0;
            for (int i = 0; i < actualLabels.Count; i++)
            {
                if (actualLabels[i] == mlResults[i])
                {
                    counter++;
                }
            }
            
            return (float)counter / (float)actualLabels.Count;
        }
        
    }
}
