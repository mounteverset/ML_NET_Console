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
                
                string filepath = @"C:\Users\casa2\OneDrive\Dokumente\II_third_semester\programmierprojekt\machinelearning-samples-master\machinelearning-samples-master\datasets\taxi-fare-test1.CSV";
                return LoadCSVTest(filepath);
            }
        } 
        private DataTable _UserTable { get; set; }
        public List<int> ML_Result
        {
            get 
            {
                return new List<int> { 1, 3, 5, 7, 8 };
            }
        }
        private string CSV_FilePath { get; set; }

        #endregion

        #region Constructors
       public DataManagerClass() { }
       public DataManagerClass(string CSV_FilePath) { }
        #endregion

        #region Methods
        public void LoadCSV(string filepath)
        { // Die CSV-Datei wird heruntergeladen
            //var reader = new StreamReader(File.OpenRead(filepath));
            //List<string>  InhaltCSVDatei = new List<string>();
            //while (!reader.EndOfStream)
            //{
            //    string line = reader.ReadLine();
            //    string[] values = line.Split(',');

            //    InhaltCSVDatei.Add(values[0]);        //:::::  
            //}
            //foreach (string coloumn1 in InhaltCSVDatei)
            //{
            //    Console.WriteLine(coloumn1);
            //}
        }

        public DataTable LoadCSVTest(string filepath)
        {//TODO: Alle LinienCode verstehen
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
        public void SaveCSV(string filepath, string filename)
        {        
            




            //string newColumnHeader = "ML_Result";
            //int lineNumber = 0;
            //using (StreamWriter sw = File.CreateText(filepath))
            //    foreach (var line in File.ReadAllLines(@"C:\Users\casa2\OneDrive\Dokumente\II_third_semester\programmierprojekt\machinelearning-samples-master\machinelearning-samples-master\datasets\taxi-fare-test2.CSV"))
            //    {
            //        sw.WriteLine(
            //            (lineNumber++ == 0 ?
            //                newColumnHeader :
            //               ML_Result[lineNumber].ToString()) +
            //            "," + line);
            //    }

            //Die CSV-Datei muss mit einer zusätzlichen Spalte gespeichert werden. Diese Spalte Entspricht
            // Die Ergebnisse des Machine learnings (MLResult)

            // Die CSV-Datei wird gespeichert
        }
        public int GetAmountOfColumns()//...fertig...
        {
           int  AmountOfColumns=UserTable.Columns.Count;    
            //Die Anzahl der Spalten wird ermittelt
            return AmountOfColumns;
        }
        public void SetColumnsType(int[] dataColumns, int resultColumn)//...fertig...
        {//Frage: Wozu die Übergabeparameter?
            // Der Spaltentyp wird festgelegt(Katogorische Sachen): Spalte1: string, Spalte2: double, etc... 
            DataRow dr = UserTable.Rows[0];
          
            for (int SpalteNummer = 0; SpalteNummer < UserTable.Columns.Count; SpalteNummer++)
            {                          
                string s = dr[SpalteNummer].ToString();
                if (int.TryParse(s, out int s1))
                {
                    Console.WriteLine($"Spalte {SpalteNummer}: {dr[SpalteNummer]}:   {s1.GetType()}");
                }
                else if (double.TryParse(s, out double s2))
                {
                    Console.WriteLine($"Spalte {SpalteNummer}: {dr[SpalteNummer]}:   {s2.GetType()}");
                }
                else if (float.TryParse(s, out float s3))
                {
                    Console.WriteLine($"Spalte {SpalteNummer}: {dr[SpalteNummer]}:   {s3.GetType()}");
                }
                else if (bool.TryParse(s, out bool s4))
                {
                    Console.WriteLine($"Spalte {SpalteNummer}: {dr[SpalteNummer]}:   {s4.GetType()}");
                }
                else
                {
                    Console.WriteLine($"Spalte {SpalteNummer}: {dr[SpalteNummer]}:   {s.GetType()}");
                }

            }       
        }
        public void SetMLResult(List<int> mlResult)
        {
            throw new NotImplementedException();
            // MLResult wird festgelegt
        }
        //public  DataTable ConvertCSVtoDataTable( string filepath)
        //{
        //    // Die heruntergeladene CSV-Datei wird in  Datatable konvertiert

        //    DataTable dt = new DataTable();
        //    Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

        //    using (StreamReader sr = new StreamReader(filepath))
        //    {
        //        string[] headers = sr.ReadLine().Split(',');
        //        foreach (string header in headers)
        //        {
        //            dt.Columns.Add(header);
        //        }
        //        while (!sr.EndOfStream)
        //        {
        //            string[] rows = CSVParser.Split(sr.ReadLine());
        //            DataRow dr = dt.NewRow();

        //            for (int i = 0; i < headers.Length; i++)
        //            {
        //                dr[i] = rows[i].Replace("\"", string.Empty);
        //            }
        //            dt.Rows.Add(dr);
        //        }
        //    }

        //    return dt;
           
        //}
        #endregion


    }
}
