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
                string filepath = @"C:\Users\Paul\source\repos\Datenanalyse mit neuronalem Netzwerk\programm\Beispieldaten.iris.txt";
                return ConvertCSVToDataTable(filepath);
                /*
                List<double> ExampleData = new List<double>
                {
                    5.1,3.5,1.4,0.2,0,4.9,3.0,1.4,0.2,0,4.7,3.2,1.3,0.2,0,4.6,3.1,1.5,0.2,0,5.0,3.6,1.4,0.2,0,5.4,3.9,1.7,0.4,0,4.6,3.4,1.4,0.3,0,5.0,3.4,1.5,0.2,0,4.4,2.9,1.4,0.2,0,4.9,3.1,1.5,0.1,0,5.4,3.7,1.5,0.2,0,4.8,3.4,1.6,0.2,0,4.8,3.0,1.4,0.1,0,4.3,3.0,1.1,0.1,0,5.8,4.0,1.2,0.2,0,5.7,4.4,1.5,0.4,0,5.4,3.9,1.3,0.4,0,5.1,3.5,1.4,0.3,0,5.7,3.8,1.7,0.3,0,5.1,3.8,1.5,0.3,0,5.4,3.4,1.7,0.2,0,5.1,3.7,1.5,0.4,0,4.6,3.6,1.0,0.2,0,5.1,3.3,1.7,0.5,0,4.8,3.4,1.9,0.2,0,5.0,3.0,1.6,0.2,0,5.0,3.4,1.6,0.4,0,5.2,3.5,1.5,0.2,0,5.2,3.4,1.4,0.2,0,4.7,3.2,1.6,0.2,0,4.8,3.1,1.6,0.2,0,5.4,3.4,1.5,0.4,0,5.2,4.1,1.5,0.1,0,5.5,4.2,1.4,0.2,0,4.9,3.1,1.5,0.1,0,5.0,3.2,1.2,0.2,0,5.5,3.5,1.3,0.2,0,4.9,3.1,1.5,0.1,0,4.4,3.0,1.3,0.2,0,5.1,3.4,1.5,0.2,0,5.0,3.5,1.3,0.3,0,4.5,2.3,1.3,0.3,0,4.4,3.2,1.3,0.2,0,5.0,3.5,1.6,0.6,0,5.1,3.8,1.9,0.4,0,4.8,3.0,1.4,0.3,0,5.1,3.8,1.6,0.2,0,4.6,3.2,1.4,0.2,0,5.3,3.7,1.5,0.2,0,5.0,3.3,1.4,0.2,0,7.0,3.2,4.7,1.4,1,6.4,3.2,4.5,1.5,1,6.9,3.1,4.9,1.5,1,5.5,2.3,4.0,1.3,1,6.5,2.8,4.6,1.5,1,5.7,2.8,4.5,1.3,1,6.3,3.3,4.7,1.6,1,4.9,2.4,3.3,1.0,1,6.6,2.9,4.6,1.3,1,5.2,2.7,3.9,1.4,1,5.0,2.0,3.5,1.0,1,5.9,3.0,4.2,1.5,1,6.0,2.2,4.0,1.0,1,6.1,2.9,4.7,1.4,1,5.6,2.9,3.6,1.3,1,6.7,3.1,4.4,1.4,1,5.6,3.0,4.5,1.5,1,5.8,2.7,4.1,1.0,1,6.2,2.2,4.5,1.5,1,5.6,2.5,3.9,1.1,1,5.9,3.2,4.8,1.8,1,6.1,2.8,4.0,1.3,1,6.3,2.5,4.9,1.5,1,6.1,2.8,4.7,1.2,1,6.4,2.9,4.3,1.3,1,6.6,3.0,4.4,1.4,1,6.8,2.8,4.8,1.4,1,6.7,3.0,5.0,1.7,1,6.0,2.9,4.5,1.5,1,5.7,2.6,3.5,1.0,1,5.5,2.4,3.8,1.1,1,5.5,2.4,3.7,1.0,1,5.8,2.7,3.9,1.2,1,6.0,2.7,5.1,1.6,1,5.4,3.0,4.5,1.5,1,6.0,3.4,4.5,1.6,1,6.7,3.1,4.7,1.5,1,6.3,2.3,4.4,1.3,1,5.6,3.0,4.1,1.3,1,5.5,2.5,4.0,1.3,1,5.5,2.6,4.4,1.2,1,6.1,3.0,4.6,1.4,1,5.8,2.6,4.0,1.2,1,5.0,2.3,3.3,1.0,1,5.6,2.7,4.2,1.3,1,5.7,3.0,4.2,1.2,1,5.7,2.9,4.2,1.3,1,6.2,2.9,4.3,1.3,1,5.1,2.5,3.0,1.1,1,5.7,2.8,4.1,1.3,1,6.3,3.3,6.0,2.5,2,5.8,2.7,5.1,1.9,2,7.1,3.0,5.9,2.1,2,6.3,2.9,5.6,1.8,2,6.5,3.0,5.8,2.2,2,7.6,3.0,6.6,2.1,2,4.9,2.5,4.5,1.7,2,7.3,2.9,6.3,1.8,2,6.7,2.5,5.8,1.8,2,7.2,3.6,6.1,2.5,2,6.5,3.2,5.1,2.0,2,6.4,2.7,5.3,1.9,2,6.8,3.0,5.5,2.1,2,5.7,2.5,5.0,2.0,2,5.8,2.8,5.1,2.4,2,6.4,3.2,5.3,2.3,2,6.5,3.0,5.5,1.8,2,7.7,3.8,6.7,2.2,2,7.7,2.6,6.9,2.3,2,6.0,2.2,5.0,1.5,2,6.9,3.2,5.7,2.3,2,5.6,2.8,4.9,2.0,2,7.7,2.8,6.7,2.0,2,6.3,2.7,4.9,1.8,2,6.7,3.3,5.7,2.1,2,7.2,3.2,6.0,1.8,2,6.2,2.8,4.8,1.8,2,6.1,3.0,4.9,1.8,2,6.4,2.8,5.6,2.1,2,7.2,3.0,5.8,1.6,2,7.4,2.8,6.1,1.9,2,7.9,3.8,6.4,2.0,2,6.4,2.8,5.6,2.2,2,6.3,2.8,5.1,1.5,2,6.1,2.6,5.6,1.4,2,7.7,3.0,6.1,2.3,2,6.3,3.4,5.6,2.4,2,6.4,3.1,5.5,1.8,2,6.0,3.0,4.8,1.8,2,6.9,3.1,5.4,2.1,2,6.7,3.1,5.6,2.4,2,6.9,3.1,5.1,2.3,2,5.8,2.7,5.1,1.9,2,6.8,3.2,5.9,2.3,2,6.7,3.3,5.7,2.5,2,6.7,3.0,5.2,2.3,2,6.3,2.5,5.0,1.9,2,6.5,3.0,5.2,2.0,2,6.2,3.4,5.4,2.3,2,5.9,3.0,5.1,1.8,2
                };
                // Create a new DataTable.
                DataTable table = new DataTable("Irisdata");
                // Declare variables for DataColumn and DataRow objects.
                DataColumn column;
                DataRow row;
                
                // Create new DataColumn, set DataType,
                // ColumnName and add to DataTable.
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Double");
                column.ColumnName = "Attribute1";
                column.ReadOnly = true;

                // Add the Column to the DataColumnCollection.
                table.Columns.Add(column);

                // Create second column.
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Double");
                column.ColumnName = "Attribute2";
                
                // Add the column to the table.
                table.Columns.Add(column);
                // Create second column.
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Double");
                column.ColumnName = "Attribute3";

                // Add the column to the table.
                table.Columns.Add(column);

                // Add the column to the table.
                table.Columns.Add(column);
                // Create second column.
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Double");
                column.ColumnName = "Attribute4";

                // Add the column to the table.
                table.Columns.Add(column);

                // Add the column to the table.
                table.Columns.Add(column);
                // Create second column.
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Int32");
                column.ColumnName = "Result";

                // Add the column to the table.
                table.Columns.Add(column);  */



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
