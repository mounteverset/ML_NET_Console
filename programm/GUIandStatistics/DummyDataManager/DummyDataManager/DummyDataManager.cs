using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommonInterfaces;

namespace DummyDataManager
{
    public class DummyDataManager : IDataManager
    {
        public DataTable UserTable {
            get
            {
                string filepath = @"C:\..\..\..\..\Beispieldaten.iris.txt";
                return ConvertCSVToDataTable(filepath);
            }
                
                
                }
      
        public int AmountOfColumns { get; set; }  // set wird entfernt werden
        public int AmountOfRows { get; set; }     // set wird entfernt werden

        public DataTable ML_Result => throw new NotImplementedException();   // eigentlich nur List<Int>



        public int GetAmountOfColumns()
        {

            return 4;   // schön Wären hier Corner-Cases und in Abstimmung mit den tatsächlichen Spalten der Datatable
        }

        public void LoadCSV(string filepath)
        {
            // Exceptionhandling nötig im Dummy?
        }

        public void SaveCSV(string filepath, string filename)
        {
            // Exceptionhandling nötig im Dummy?
        }

        public void SetColumnsType(int[] dataColumns, int resultColumn)
        {
           // Exceptionhandling nötig im Dummy?
        }


        public void SetMLResult(List<int> mlResult)
        {
            // Exceptionhandling nötig im Dummy?
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
    }
}
