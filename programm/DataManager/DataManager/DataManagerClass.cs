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
        public void loadCSV(string filepath)
        {
            throw new NotImplementedException();
            // Die CSV-Datei wir heruntergeladen
        }
        public void saveCSV(string filepath, string filename)
        {
            throw new NotImplementedException();
            // Die CSV-Datei wir gespeichert
        }
        public int getAmountOfColumns()
        {
            //Die Anzahl der Spalten wird ermittelt
            return 0;
        }
        public void setColumnsType(int[] dataColumns, int resultColumn)
        {
            throw new NotImplementedException();
            // Spaltentyp wird festgelegt
        }
        public void setMLResult(DataTable mlResult)
        {
            throw new NotImplementedException();
            // MLResult wird festgelegt
        }
        public DataTable convertCSVtoDataTable()
        {
            // Die heruntergeladene CSV-Datei wird in  Datatable konvertiert
            return new DataTable();
        }
        #endregion


    }
}
