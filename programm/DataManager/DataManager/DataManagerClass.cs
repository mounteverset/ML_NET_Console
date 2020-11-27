using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonInterfaces;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace DataManager
{
    public class DataManagerClass: IDataManager
    {
        #region Attributes
        public DataTable UserTable
        {
            get
            {
                string filepath = @"C:\..\..\..\Beispieldaten.iris.txt";
                return ConvertCSVtoDataTable(filepath);
            }
        }
        private DataTable _UserTable { get; set; }
        public List<int> ML_Result { get; }
        private string CSV_FilePath { get; set; }

        #endregion

        #region Constructors
       public DataManagerClass() { }
       public DataManagerClass(string CSV_FilePath) { }
        #endregion

        #region Methods
        public void LoadCSV(string filepath)
        { // Die CSV-Datei wird heruntergeladen
            var reader = new StreamReader(File.OpenRead(filepath));
            List<string> listA = new List<string>();
            List<string> listB = new List<string>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');

                listA.Add(values[0]);
                listB.Add(values[1]);
                foreach (var coloumn1 in listA)
                {
                    Console.WriteLine(coloumn1);
                }
                foreach (var coloumn2 in listA)
                {
                    Console.WriteLine(coloumn2);
                }
            }         
        }
        public void SaveCSV(string filepath, string filename)
        {
            //Die CSV-Datei muss mit einer zusätzlichen Spalte gespeichert werden. Diese Spalte Entspricht
            // Die Ergebnisse des Machine learnings (MLResult)
            throw new NotImplementedException();
            // Die CSV-Datei wird gespeichert
        }
        public int GetAmountOfColumns()
        {
           int  AmountOfColumns=UserTable.Columns.Count;
            //Die Anzahl der Spalten wird ermittelt
            return AmountOfColumns;
        }
        public void SetColumnsType(int[] dataColumns, int resultColumn)
        {
            // Der Spaltentyp wird festgelegt(Katogorische Sachen): Spalte1: string, Spalte2: double, etc...
            throw new NotImplementedException();         
        }
        public void SetMLResult(List<int> mlResult)
        {
            throw new NotImplementedException();
            // MLResult wird festgelegt
        }
        public  DataTable ConvertCSVtoDataTable( string filepath)
        {
            // Die heruntergeladene CSV-Datei wird in  Datatable konvertiert

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
        #endregion


    }
}
