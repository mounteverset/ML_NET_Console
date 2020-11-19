using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonInterfaces;

namespace GUIandStatistics
{

    class Statistics : IStatistics

    {
        // zusätzliche Statistik: Sensitivität, Spezifität, positiver Vorhersagewert , negativer Vorhersagewert,
        // positiver Vorhersagewert, Korrekt/Falschklassifikationsrate
        // oder kombinierte Werte wie F-Maß und Funktionsgraphen
        // 4 Felder Test zur Beurteilung der Klassifikation
        // public double sensitivity(DataTable MLResult, DataTable realResult)
        // public double specificity(DataTable MLResult, DataTable realResult)
        // public double precision(DataTable MLResult, DataTable realResult)
        // public double negativepredictivevalue(DataTable MLResult, DataTable realResult)
        // public double positivepredictivevalue(DataTable MLResult, DataTable realResult)

        //confusion Matrix als Dictionary...


         public double CalculateSensitivity(List<int> MLResult, List<int> realResult)
         {
               double sensitiv = 0.87;
               return sensitiv;
         }   
               
         public double CalculateSpecificity(List<int> MLResult, List<int> realResult)
         {
               double specifi = 0.76;
              return specifi;
          }    
              
          public double CalculatePrecision(List<int> MLResult, List<int> realResult)
          {
               double precisio = 0.91;
            return precisio;
               }
          public double CalculateNegativePredictiveValue(List<int> MLResult, List<int> realResult)
          {
               double predictive =0.77;
               return predictive;
               }



        public double CalculatePositivePredictiveValue(List<int> MLResult, List<int> realResult)
        {
            return 0.76;
        }





        public Dictionary<string, int> CalculateConfusionMatrix(DataTable MLResult, DataTable realResut)  //2x2 Tabelle 
        {
            Dictionary<string, int> confusion_Matrix =  new Dictionary<string, int>();

            confusion_Matrix.Add("True positive", 15);
            confusion_Matrix.Add("False positive", 4);
            confusion_Matrix.Add("False negative", 3);
            confusion_Matrix.Add("True negative", 17);

            return confusion_Matrix;

            /*
            // new DataTable
            DataTable confusionMatrix = new DataTable("confusionMatrix");
            // declare variables for Datacolum and Datarow
            DataColumn column;
            DataRow row;

            // Create new DataColumn, set DataType,
            // ColumnName and add to DataTable.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "positive";
            column.ReadOnly = true;
            

            // Add the Column to the DataColumnCollection.
            confusionMatrix.Columns.Add(column);

            // second column
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "negative";
            column.ReadOnly = true;

            // Add the Column to the DataColumnCollection
            */
        }

    }
}
