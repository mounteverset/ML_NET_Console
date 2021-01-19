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
        /// Durch UserTable können die anderen Klassen auf das Datatable _Usertable zugreifen
        /// </summary>
        public DataTable UserTable
        {
            get
            {
                return _UserTable;
            }
        } 
        /// <summary>
        ///  Tabelle, die die Testdaten und das MlResult enthält  
        /// </summary>
        private DataTable _UserTable { get; set; }
        /// <summary>
        /// Ergebnis des Machine Learnings
        /// </summary>
        public List<int> ML_Result { get; }
        private string CSV_FilePath { get; set; }  
        #endregion

 #region Constructors
       public DataManagerClass() { }
       public DataManagerClass(string CSV_FilePath) { }
        #endregion

        #region Methods
        /// <summary>
        /// Die CSV-Datei wird in ein Datatable konvertiert
        /// </summary>
        /// <param name="filepath"></param>
        public void LoadCSV(string filepath)
         {
            DataTable dt = new DataTable();    
            //Erleichtert das Analysieren der CSV-Datei
            Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            //Eine Instanz sr von StreamReader wird  zum Lesen aus einer Datei erzeugt.
            //Mit der using-Anweisung wird auch der StreamReader geschlossen.
            using (StreamReader sr = new StreamReader(filepath))
            {
                //Spalten aus der CSV-Datei lesen und in ein Array "headers" speichern, 
                //bis das Ende der Datei erreicht ist.
                string[] headers = sr.ReadLine().Split(',');
                //Die Spalten der CSV-Datei werden in ein DataTable gespeichert
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                //Wenn die aktuelle Streamposition sich nicht am Ende des Streams befindet dann...
                while (!sr.EndOfStream)
                {
                    //Zeilen aus der CSV-Datei lesen und in ein Array "rows" speichern, bis 
                    // das Ende der CSV-Datei erreicht ist.
                    string[] rows = CSVParser.Split(sr.ReadLine());
                    DataRow dr = dt.NewRow();
                    //Jede Zeile des DataTables wird vollständig ausgefüllt
                    for (int i = 0; i < headers.Length; i++)
                    {
                        //Jedes Vorkommen von "\"  soll mit einem leeren Zeichen ersetzt werden.
                        dr[i] = rows[i].Replace("\"", string.Empty);
                    }
                    //Ist eine Zeile voll (steht genauso wie in der CSV-Datei), dann 
                    //wird sie zum DataTable hinzufügt
                    dt.Rows.Add(dr);
                }
            }
            _UserTable = dt;
        }



        /// <summary>
        ///Die Datatable wird in einer CSV Datei gespeichert
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="filename"></param>
        public void SaveCSV(string filepath, string filename)
        {
            //filepath der neuen CSV-Datei
            filepath = $@"C:\Users\casa2\OneDrive\Dokumente\II_third_semester\programmierprojekt\neuronale_netzwerke\programm\DataManager\{filename}.CSV";
            //Eine leere CSV-Datei wird erstellt
            using (StreamWriter sw = new StreamWriter(filepath, false, System.Text.Encoding.Default))//JJJJ
            {
                int numberOfColumns = _UserTable.Columns.Count;
                //Die Kolonnen werden in der CSV-Datei gespeichert
                for (int i = 0; i < numberOfColumns; i++)
                {   
                    sw.Write(_UserTable.Columns[i]);
                    if (i < numberOfColumns - 1)
                        //Die Werte in der CSV-Datei werden mit einem Komma getrennt
                        sw.Write(',');
                }    
                //Die Zeilen werden gespeichert
                sw.Write(sw.NewLine);

                foreach (DataRow dr in _UserTable.Rows)
                {
                    //eine Zeile wird vollständig ausgefüllt
                    for (int i = 0; i < numberOfColumns; i++)
                    {  
                        sw.Write(dr[i].ToString());
                        //Die Werte in der CSV-Datei werden bis zu dem Wert der letzten Spalte 
                        //mit einem Komma getrennt
                        if (i < numberOfColumns - 1)
                            sw.Write(',');
                    }
                    //Nachdem eine Zeile vollständig ausgefüllt ist, wird eine neue erstellt
                    sw.Write(sw.NewLine);
                }
            }

        }



        /// <summary>
        /// Die Anzahl der Spalten  wird ermittelt
        /// </summary>
        /// <returns></returns>
        public int GetAmountOfColumns()
        {
           int  AmountOfColumns=_UserTable.Columns.Count;             
            return AmountOfColumns;
        }



        /// <summary>
        /// Die Spaltentypen werden festgelegt
        /// </summary>
        /// <param name="dataType"></param>
        public void SetColumnsDataType(Dictionary<int, String> dataType)//...fertig...
         {
            DataTable dt = new DataTable();
            // dt bekommt die gleichen Spalten und Linien wie _UserTable 
            dt = _UserTable.Clone();
            for (int i = 0; i < dataType.Count; i++)
            {
                //Der Datentyp der Spalten von dt wird geändert
                dt.Columns[i].DataType = System.Type.GetType(dataType[i]);
            }
            //_UserTable hat jetzt die selben Linien und Spalten aber mit veränderten Datentypen
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
            // Die Ergebnis-Spalte wird hinzufügt
            _UserTable.Columns.Add("ML_Result");
            //Die Werte des mlResults werden in die Ergebnis-Spalte hinzufügt 
            for (int i = 0; i < _UserTable.Rows.Count; i++)
            {
                DataRow dr = _UserTable.Rows[i];
                dr["ML_Result"]= mlResult[i];
            }
        }






        #endregion


    }
}
