using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace DummyGUI_MLAdapter
{
    public class HelperFunctions
    {
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

        public static DataTable ConvertCSVToDataTable(string filepath)
        {
            DataTable dt = new DataTable();
            Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

            using (StreamReader sr = new StreamReader(filepath))
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
                        dr[i] = rows[i].Replace("\"", string.Empty);
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        public static void AssignColumnNamesAndTypes(ref DataTable dt)
        {
            string[] names = new string[] { "Sepal length", "Sepal width", "Petal length", "Petal width", "Label" };

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dt.Columns[i].ColumnName = names[i];;
            }
            
        }
    }
}
