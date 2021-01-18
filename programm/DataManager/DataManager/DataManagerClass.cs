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
        /// <summary>
        /// Durch UserTable können die anderen Klassen auf 
        /// </summary>
        public DataTable UserTable
        {
            get
            {
                return _UserTable;
            }
        } 
        private DataTable _UserTable { get; set; }
        public List<int> ML_Result { get; }
        private string CSV_FilePath { get; set; } = @"C:\Users\casa2\OneDrive\Dokumente\II_third_semester\programmierprojekt\machinelearning-samples-master\machinelearning-samples-master\datasets\FinalCSV.CSV";

        #endregion

 #region Constructors
       public DataManagerClass() { }
       public DataManagerClass(string CSV_FilePath) { }
        #endregion

        #region Methods
        public void LoadCSV(string filepath)
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
            _UserTable = dt;
        }



        /// <summary>
        /// Die CSV-Datei muss mit einer zusätzlichen Spalte gespeichert werden. Diese Spalte Entspricht
        ///Die Ergebnisse des Machine learnings(MLResult)
        ///Die Datatable wird in einer CSV Datei gespeichert
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="filename"></param>
        public void SaveCSV(string filepath, string filename)//...fertig...
        {
            filepath = $@"C:\Users\casa2\OneDrive\Dokumente\II_third_semester\programmierprojekt\machinelearning-samples-master\machinelearning-samples-master\datasets\{filename}.CSV";
            using (StreamWriter sw = new StreamWriter(filepath, false, System.Text.Encoding.Default))//JJJJ
            {
                //TextWriter writer = new StreamWriter(filename);
                int numberOfColumns = _UserTable.Columns.Count;
                //Die Kolonnen werden in der CSV-Datei gespeichert
                for (int i = 0; i < numberOfColumns; i++)
                {   
                    sw.Write(_UserTable.Columns[i]);
                    if (i < numberOfColumns - 1)
                        sw.Write(',');
                }    
                //Die Linien werden gespeichert
                sw.Write(sw.NewLine);

                foreach (DataRow dr in _UserTable.Rows)
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
           int  AmountOfColumns=_UserTable.Columns.Count;    
            //Die Anzahl der Spalten wird ermittelt
            return AmountOfColumns;
        }



        /// <summary>
        /// Die Spaltentypen werden festgelegt
        /// </summary>
        /// <param name="dataType"></param>
        public void SetColumnsDataType(Dictionary<int, String> dataType)//...fertig...
         {
            DataTable dt = new DataTable();
            dt = _UserTable.Clone();
            for (int i = 0; i < dataType.Count; i++)
            {
                //dt.Columns.Add();
                dt.Columns[i].DataType = System.Type.GetType(dataType[i]);
                //Console.WriteLine(dt.Columns[i].DataType);
            }
            _UserTable = dt;
      

        }



        public void SetColumnsIOType(int[] dataColumns, int resultColumn)
        {
            dataColumns = new int[_UserTable.Columns.Count - 1];
            int j = 0;
            for (int i = 0; i < _UserTable.Columns.Count; i++)
            {
               string a= _UserTable.Columns[i].ColumnName;
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
        }//Weg???


        /// <summary>
        /// MLResult wird festgelegt  
        /// </summary>
        /// <param name="mlResult"></param>
        public void SetMLResult(List<int> mlResult)
        {
            _UserTable.Columns.Add("ML_Result");
            for (int i = 0; i < _UserTable.Rows.Count; i++)
            {
                DataRow dr = _UserTable.Rows[i];
                dr["ML_Result"]= mlResult[i];
            }
        }






        #endregion


    }
}
