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
        {
           
        }//Gibt kein Datatable zurück... Why?

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
        /// <summary>
        /// Die CSV-Datei muss mit einer zusätzlichen Spalte gespeichert werden. Diese Spalte Entspricht
        ///Die Ergebnisse des Machine learnings(MLResult)
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="filename"></param>
        public void SaveCSV(string filepath, string filename)//...fertig...JJJJJ
        {
            DataTable dt = new DataTable();
            dt = UserTable;
            dt.Columns.Add("ML_Result");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
               DataRow dr= dt.Rows[i];
                dr[dt.Columns.Count-1] = ML_Result[i];//JJJ
            }
            ConvertDatatableToCSV(filepath, dt);

        }
        /// <summary>
        /// Die Datatable wird in eine CSV Datei gespeichert
        /// </summary>
        /// <param name="filepath"></param>
        public void ConvertDatatableToCSV (string filepath, DataTable dt)
        {
            using (StreamWriter sw = new StreamWriter(filepath, false, System.Text.Encoding.Default))//JJJJ
            {
                int numberOfColumns = dt.Columns.Count;
                //Die Kolonnen werden in der CSV-Datei gespeichert
                for (int i = 0; i < numberOfColumns; i++)
                {
                    sw.Write(dt.Columns[i]);
                    if (i < numberOfColumns - 1)
                        sw.Write(',');
                }
                //Die Linien werden gespeichert
                sw.Write(sw.NewLine);

                foreach (DataRow dr in dt.Rows)
                {
                    for (int i = 0; i < numberOfColumns; i++)
                    {
                        sw.Write(dr[i].ToString());

                        if (i < numberOfColumns - 1)
                            sw.Write(',');
                    }
                    sw.Write(sw.NewLine);
                }
            }
        }
        public int GetAmountOfColumns()//...fertig...
        {
           int  AmountOfColumns=UserTable.Columns.Count;    
            //Die Anzahl der Spalten wird ermittelt
            return AmountOfColumns;
        }
        /// <summary>
        /// Der Spaltentypen werden festgelegt
        /// </summary>
        /// <param name="dataType"></param>
        public void SetColumnsDataType(Dictionary<int, String> dataType)//...fertig...
        {
            DataRow dr = UserTable.Rows[0];

            for (int SpalteNummer = 0; SpalteNummer < UserTable.Columns.Count; SpalteNummer++)
            {                          
                string s = dr[SpalteNummer].ToString(); 
                if (int.TryParse(s, out int s1))
                {
                    dataType.Add(SpalteNummer, s1.GetType().ToString());
                }
                else if (double.TryParse(s, out double s2))
                {
                    dataType.Add(SpalteNummer, s2.GetType().ToString());
                }
                else if (float.TryParse(s, out float s3))
                {
                    dataType.Add(SpalteNummer, s3.GetType().ToString());
                }
                else if (bool.TryParse(s, out bool s4))
                {
                    dataType.Add(SpalteNummer, s4.GetType().ToString());
                }
                else
                {
                    dataType.Add(SpalteNummer, s.GetType().ToString());
                }

            }       
        }
        public void SetColumnsIOType(int[] dataColumns, int resultColumn)
        {
            dataColumns = new int[UserTable.Columns.Count - 1];
            int j = 0;
            for (int i = 0; i < UserTable.Columns.Count; i++)
            {
               string a= UserTable.Columns[i].ColumnName;
                if (a== "ML_Result")
                {
                    resultColumn = i + 1;
                }
                else
                {
                    if (j<dataColumns.Length)
                    {
                        dataColumns[j] = i + 1;
                        j++;
                    }
                    
                }
            }
        }
        public void SetMLResult(List<int> mlResult)
        {
            throw new NotImplementedException();
            // MLResult wird festgelegt
        }










        //public   DataTable convertcsvtodatatable( string filepath)
        //{
        //    //    // die heruntergeladene csv-datei wird in  datatable konvertiert

        //    DataTable dt = new DataTable();
        //   Regex csvparser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

        //   using (StreamReader sr = new StreamReader(filepath))
        //    {
        //       string[] headers = sr.ReadLine().Split(',');
        //       foreach (string header in headers)
        //      {
        //            dt.Columns.Add(header);
        //       }
        //       while (!sr.EndOfStream)
        //       {
        //            string[] rows = csvparser.Split(sr.ReadLine());
        //            DataRow dr = dt.NewRow();

        //           for (int i = 0; i < headers.Length; i++)
        //           {
        //                dr[i] = rows[i].Replace("\"", string.Empty);
        //           }
        //           dt.Rows.Add(dr);
        //       }
        //   }

        //    return dt;
           
        //}
        #endregion


    }
}
