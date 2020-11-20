using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonInterfaces;
using System.Data;

namespace DataManager
{
    public class DataManagerClass: IDataManager
    {
        #region Attributes
        public DataTable UserTable { get; }
        private DataTable _UserTable { get; set; }
        public int AmountOfColumns { get; set; }
        public int AmountOfRows { get; set; }
        public DataTable ML_Result { get; }
        private string CSV_FilePath { get; set; }

        #endregion

        #region Constructors
       public DataManagerClass() { }
       public DataManagerClass(string CSV_FilePath) { }
        #endregion

        #region Methods
        public void LoadCSV(string filepath)
        {
            throw new NotImplementedException();
            // Die CSV-Datei wir heruntergeladen
        }
        public void SaveCSV(string filepath, string filename)
        {
            throw new NotImplementedException();
            // Die CSV-Datei wir gespeichert
        }
        public int GetAmountOfColumns()
        {
            //Die Anzahl der Spalten wird ermittelt
            return 0;
        }
        public void SetColumnsType(int[] dataColumns, int resultColumn)
        {
            throw new NotImplementedException();
            // Spaltentyp wird festgelegt
        }
        public void SetMLResult(List<int> mlResult)
        {
            throw new NotImplementedException();
            // MLResult wird festgelegt
        }
        public DataTable ConvertCSVtoDataTable()
        {
            // Die heruntergeladene CSV-Datei wird in  Datatable konvertiert
            return new DataTable();
        }
        #endregion


    }
}
