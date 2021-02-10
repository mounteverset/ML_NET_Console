using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataManager;

namespace DummyGUI_MLAdapter
{
    class ConsoleHelperFunctions
    {
        public static void DisplayDefaultHomescreen()
        {
            Console.Clear();

            Console.WriteLine(String.Concat(Enumerable.Repeat('*', 40)));
            Console.WriteLine("Machine Learning mit ML.NET");
            Console.WriteLine(String.Concat(Enumerable.Repeat('*', 40)));
            Console.Write("\n\n\n");
            Console.WriteLine("Folgende Optionen stehen zur Verfügung:\n");
            Console.WriteLine("(A) - Eine neue CSV Tabelle mit Daten öffnen und einlesen\n");
            Console.WriteLine("(B) - Ein vorhandenes Modell öffnen\n");
            Console.WriteLine("(X) - Das Programm beenden\n");
            Console.WriteLine("Drücken Sie A oder B um fortzufahren...\n");
            Console.WriteLine("");
        }

        public static void DisplayFullHomescreen()
        {
            Console.Clear();

            Console.WriteLine(String.Concat(Enumerable.Repeat('*', 40)));
            Console.WriteLine("Machine Learning mit ML.NET");
            Console.WriteLine(String.Concat(Enumerable.Repeat('*', 40)));
            Console.Write("\n\n\n");
            Console.WriteLine("Folgende Optionen stehen zur Verfügung:\n");
            Console.WriteLine("(A) - Eine neue CSV Tabelle mit Daten öffnen und einlesen\n");
            Console.WriteLine("(B) - Ein vorhandenes Modell öffnen\n");
            Console.WriteLine("(C) - Machine Learning Modell anwenden\n");
            Console.WriteLine("(D) - Machine Learning Modell speichern\n");
            Console.WriteLine("(X) - Das Programm beenden");
            Console.WriteLine("Drücken Sie A, B, C oder D um fortzufahren...\n");

        }

        public static int GetLabelColumnResponse(int columnCount, int[] inputColumns, ConsoleKeyInfo consoleKey)
        {
            bool answerInCorrectFormat = false;
            bool wantToRepeat = false;
            int labelColumn = 0;
            List<int> inputCols = new List<int>(inputColumns);

            Console.WriteLine("\n\nBitte geben Sie Nummer der Spalte ein, die als Ergebniswert für das Machine Learning Modell dienen sollen.");
            Console.WriteLine("Die Nummern starten bei 0 in der linken Spalte und werden nach rechts durchgezählt.");
            Console.WriteLine("Bitte geben Sie die Spaltennummer ein und bestätigen mit Enter");       

            do
            {
                try
                {                    
                    string stringInput = Console.ReadLine();
                    labelColumn = int.Parse(stringInput);
                    if (labelColumn > columnCount && labelColumn < 0)
                    {
                        Console.WriteLine("Die Label Spalte liegt außerhalb des Wertebereichs.");
                        wantToRepeat = AskForRepeat();
                    }
                    else if (inputCols.Contains(labelColumn))
                    {
                        Console.WriteLine("Die Label Spalte ist schon als Eingabespalte deklariert worden.");
                        wantToRepeat = AskForRepeat();
                    }
                    else  
                        answerInCorrectFormat = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(String.Format("Etwas ist falsch gelaufen: {0}", e));
                    wantToRepeat = AskForRepeat();
                }
            }
            while (answerInCorrectFormat == false && wantToRepeat == true);

            return labelColumn;
        }
    
        public static int[] GetInputColumnsResponse(int columnCount, ConsoleKeyInfo consoleKey)
        {
            bool answerInCorrectFormat = false;
            bool wantToRepeat = false;
            List<int> inputColumns = new List<int>();
            Console.WriteLine("\n\nBitte geben Sie Nummer der Spalten ein, die als Eingabewerte für das Machine Learning Modell dienen sollen.");
            Console.WriteLine("Die Nummern starten bei 0 in der linken Spalte und werden nach rechts durchgezählt.");
            Console.WriteLine("Bitte trennen Sie die Spaltennummern mit einem Komma");
            Console.WriteLine("Beispiel: 0,1,3,4,5,7");
            Console.WriteLine("\nHinweis: Drücken Sie A, um alle Spalten außer die letzte Spalte als Eingabespalten festzulegen.");
            do
            {

                string stringInput = Console.ReadLine();
                try
                {                   
                    if (stringInput.Contains('A') || stringInput.Contains('a'))
                    {
                        for (int i = 0; i < columnCount-1; i++)
                        {
                            inputColumns.Add(i);
                        }
                    }
                    else
                    {
                        string[] stringInputSplit = stringInput.Split(',');
                        foreach (string s in stringInputSplit)
                            inputColumns.Add(int.Parse(s));
                        answerInCorrectFormat = true;
                    }                                       
                }
                catch (Exception e)
                {
                    Console.WriteLine(String.Format("Etwas ist falsch gelaufen: {0}", e));
                    wantToRepeat = AskForRepeat();
                }
            }
            while (answerInCorrectFormat == false && wantToRepeat == true);

            return inputColumns.ToArray();
        }

        public static bool GetHeaderResponse(ConsoleKeyInfo consoleKey)
        {

            bool answerInCorrectFormat = false;
            bool wantToRepeat = false;
            do
            {
                Console.WriteLine("\n\nBesitzt diese Tabelle in der ersten Zeile eine Beschriftung mit Spaltennamen?");
                Console.WriteLine("Drücken Sie Y für JA, N für Nein");
                consoleKey = Console.ReadKey(true);

                if (consoleKey.Key != ConsoleKey.Y && consoleKey.Key != ConsoleKey.N)
                {                    
                    do
                    {
                        wantToRepeat = DisplayCharNotRecognized(consoleKey);
                    }
                    while (wantToRepeat);

                }
            }
            while (answerInCorrectFormat == false && wantToRepeat == true);
            
            if (consoleKey.Key == ConsoleKey.Y)
                return true;
            else
                return false;
        }

        public static bool GetRestartResponse(ConsoleKeyInfo consoleKey)
        {
            Console.Clear();
            Console.WriteLine("Es wurde nicht die richtige Datei gefunden. Möchten Sie das Programm noch einmal starten?");
            Console.WriteLine("Drücken sie Y um fortzufahren, N um das Programm zu beenden");
            consoleKey = Console.ReadKey(true);
            if (consoleKey.Key != ConsoleKey.Y && consoleKey.Key != ConsoleKey.N)
            {
                DisplayCharNotRecognized(consoleKey);
            }
            if (consoleKey.Key == ConsoleKey.Y)
                return true;
            else if (consoleKey.Key == ConsoleKey.N)
                return false;
            else
                return false;
        }

        public static bool AskForRepeat ()
        {
            Console.WriteLine("Es wurde keine passende Eingabe erkannt. Möchten Sie die Eingabe wiederholen?");
            Console.WriteLine("Drücken Sie Y um zu wiederholen, N um das Programm zu beenden.");
            ConsoleKeyInfo consoleKey = Console.ReadKey(true);
            
            if (consoleKey.Key == ConsoleKey.Y || consoleKey.Key == ConsoleKey.N)
            {
                if (consoleKey.Key == ConsoleKey.Y)
                    return true;
                else
                {
                    TriggerShutdown();
                    return false;
                }               
            }
            return AskForRepeat();
        }

        public static void TriggerShutdown()
        {
            Console.WriteLine("Programm wird beendet...");
            System.Threading.Thread.Sleep(3000);
            System.Environment.Exit(0);
        }

        public static bool DisplayCharNotRecognized(ConsoleKeyInfo consoleKey)
        {

            //Console.Clear();
            Console.WriteLine("Es wurde keine passende Eingabe erkannt. Möchten Sie noch einmal starten?");
            Console.WriteLine("Drücken Sie Y um fortzufahren, N um das Programm zu beenden.");
            consoleKey = Console.ReadKey(true);
            if (consoleKey.Key != ConsoleKey.Y && consoleKey.Key != ConsoleKey.N)
            {
                DisplayCharNotRecognized(consoleKey);
            }

            if (consoleKey.Key == ConsoleKey.Y)
                return true;
            else if (consoleKey.Key == ConsoleKey.N)
                return false;
            else
                return false;

        }

        public static int DisplayCSVChoice(string [] folderContent)
        {           

            Console.Clear();
            Console.WriteLine("************************************************");
            Console.WriteLine("Auswahl einer CSV Tabelle");
            Console.WriteLine("************************************************\n\n");
            
            for (int i = 0; i < folderContent.Length; i++)
            {
                Console.WriteLine(String.Format("({0}): {1}", i+1 , folderContent[i]));
            }

            Console.WriteLine("\nTragen Sie die Nummer der csv - Datei ein und bestätigen Sie die Eingabe mit Enter:");

            int parsed_input = -1;
            bool wantToRepeat = false;
            do
            {
                string input = Console.ReadLine();
                
                try
                {
                    parsed_input = int.Parse(input);
                    if (parsed_input > 0 && parsed_input <= folderContent.Length)
                        return parsed_input;
                    else
                    {
                        Console.WriteLine("\nDie eingegebene Zahl entspricht keiner Auswahlmöglichkeit.");

                        parsed_input = -1;

                        wantToRepeat = AskForRepeat();                      
                    }
                        
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nEin Fehler ist aufgetreten: " + e.Message);
                    parsed_input = -1;
                    wantToRepeat = AskForRepeat();
                }
            }
            while (parsed_input == -1 && wantToRepeat == true);


            return 0;
        }

        public static void PrintDataTableToConsole(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++) //looping through all rows including the column. change `i=1` if need to exclude the columns display
            {
                for (int j = 0; j < dt.Columns.Count; j++) //looping through all columns
                {
                    Console.CursorLeft = j * 4;
                    Console.Write(String.Format("|{0}", dt.Rows[i][j])); //display of the data
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Outputs all values from the DataTable that is passed in
        /// </summary>
        /// <param name="dt">The table which will be printed into the Console</param>
        /// <param name="cutOff">Defines how many rows of the DataTable will be printed</param>
        public static void PrintDataTableToConsole(DataTable dt, int cutOff)
        {
            Console.WriteLine("\n\nDas ist ein Teil der aktuell geladenen Tabelle:\n\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                Console.CursorLeft = i * 4;
                Console.Write(String.Format("|{0}", dt.Columns[i].ColumnName));
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(String.Concat(Enumerable.Repeat('_', dt.Columns.Count * 4)));
            Console.WriteLine();

            if (cutOff < dt.Rows.Count)
            {
                for (int i = 0; i < cutOff; i++) //looping through all rows including the column up until the cutoff. change `i=1` if need to exclude the columns display
                {
                    for (int j = 0; j < dt.Columns.Count; j++) //looping through all columns
                    {
                        Console.CursorLeft = j * 4;
                        Console.Write(String.Format("|{0}", dt.Rows[i][j])); //display of the data
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                PrintDataTableToConsole(dt);
            }
        }

        public static void PrintDataTableToConsole(DataTable dt, int[] inputColumns, int labelColumn, int cutOff)
        {
            
            List<int> inputCols = new List<int>(inputColumns);
            Console.WriteLine("\n\nDas ist die geladene Tabelle mit den ausgewählten Spalten:");
            Console.WriteLine("Grün = Input Spalten, Rot =  Label Spalte)");
            
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                Console.CursorLeft = i * 4;
                if (inputCols.Contains(i))
                    Console.ForegroundColor = ConsoleColor.Green;
                else if (i == labelColumn)
                    Console.ForegroundColor = ConsoleColor.Red;
                else
                    Console.ForegroundColor = ConsoleColor.White;
                Console.Write(String.Format("|{0}", dt.Columns[i].ColumnName));
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(String.Concat(Enumerable.Repeat('_', dt.Columns.Count * 4)));
            Console.WriteLine();

            if (cutOff < dt.Rows.Count)
            {
                for (int i = 0; i < cutOff; i++) //looping through all rows including the column up until the cutoff. change `i=1` if need to exclude the columns display
                {
                    for (int j = 0; j < dt.Columns.Count; j++) //looping through all columns
                    {
                        Console.CursorLeft = j * 4;
                        if (inputCols.Contains(j))
                            Console.ForegroundColor = ConsoleColor.Green;
                        else if (j == labelColumn)
                            Console.ForegroundColor = ConsoleColor.Red;
                        else
                            Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(String.Format("|{0}", dt.Rows[i][j]));
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                PrintDataTableToConsole(dt);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void PrintDataTableToConsole(DataTable dt, int[] inputColumns, int labelColumn)
        {

            List<int> inputCols = new List<int>(inputColumns);
            Console.WriteLine("\n\nDas ist die geladene Tabelle mit den ausgewählten Spalten:");
            Console.WriteLine("Grün = Input Spalten, Rot =  Label Spalte)");

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                Console.CursorLeft = i * 4;
                if (inputCols.Contains(i))
                    Console.ForegroundColor = ConsoleColor.Green;
                else if (i == labelColumn)
                    Console.ForegroundColor = ConsoleColor.Red;
                else
                    Console.ForegroundColor = ConsoleColor.White;
                Console.Write(String.Format("|{0}", dt.Columns[i].ColumnName));
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(String.Concat(Enumerable.Repeat('_', dt.Columns.Count * 4)));
            Console.WriteLine();

            for (int i = 0; i < dt.Rows.Count; i++) //looping through all rows including the column up until the cutoff. change `i=1` if need to exclude the columns display
            {
                for (int j = 0; j < dt.Columns.Count; j++) //looping through all columns
                {
                    Console.CursorLeft = j * 4;
                    if (inputCols.Contains(j))
                        Console.ForegroundColor = ConsoleColor.Green;
                    else if (j == labelColumn)
                        Console.ForegroundColor = ConsoleColor.Red;
                    else
                        Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(String.Format("|{0}", dt.Rows[i][j]));
                }
                Console.WriteLine();
            }
            
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static bool GetProceedResponse(ConsoleKeyInfo consoleKey)
        {
            
            bool answerInCorrectFormat = false;
            bool wantToRepeat = false;            
            do
            {
                Console.WriteLine("\n\nMöchten Sie mit den ausgewählten Daten ein Machine Learning Modell trainieren oder die Auswahl der Spalten korrigieren?");
                Console.WriteLine("Drücken Sie Y um fortzufahren, N um das Programm noch einmal zu starten.");
                consoleKey = Console.ReadKey(true);

                if (consoleKey.Key == ConsoleKey.Y || consoleKey.Key == ConsoleKey.N)
                {
                    answerInCorrectFormat = true;
                    if (consoleKey.Key == ConsoleKey.Y)
                        return true;
                    else
                        return false;
                }
                else
                {
                    wantToRepeat = AskForRepeat();
                }
                                
            }
            while (answerInCorrectFormat == false && wantToRepeat == true);
            return false;
        }

        public static void DisplayModelAccuracy(List<int> testResults, DataManagerClass dm)
        {

            Console.WriteLine("Das Trainieren des Machine Learning Modells ist abgeschlossen.");
            Console.WriteLine(String.Format("Das Model hat mit den ausgewählten Testdaten mit einer Genauigkeit von {0}%", HelperFunctions.GetModelAccuracy(dm.UserTable, testResults, dm.LabelColumn) * 100 ));
            Console.WriteLine("Drücken Sie eine beliebige Taste um fortzufahren...");
            Console.ReadKey(true);
        }

        public static string GetFilepathResponse()
        {
            Console.Clear();
            Console.WriteLine(String.Concat(Enumerable.Repeat('*', 40)));
            Console.WriteLine("Laden eines Machine Learning Modells");
            Console.WriteLine(String.Concat(Enumerable.Repeat('*', 40)));
            Console.WriteLine("\nBitte geben Sie den Dateipfad inkl. Dateiname und Endung vom gespeicherten Machine Learning Modell an.");
            string filepath = Console.ReadLine();
            return filepath;
        }

        public static string GetSaveFilenameResponse()
        {
            Console.Clear();
            Console.WriteLine(String.Concat(Enumerable.Repeat('*', 40)));
            Console.WriteLine("Speichern des Machine Learning Modells");
            Console.WriteLine(String.Concat(Enumerable.Repeat('*', 40)));
            Console.WriteLine("\nBitte geben Sie den Dateinamen an.");

            string filename = Console.ReadLine();

            return String.Format("{0}.zip", filename);
        }

        public static string GetSaveFilepathResponse()
        {
            Console.WriteLine("\nBitte geben Sie den Dateipfad an, an dem das Modell gespeichert werden soll.");

            string filepath = Console.ReadLine();

            return filepath;
        }

        public static int GetLoadModelResponse(string[] modelFolderContent)
        {
            Console.Clear();
            Console.WriteLine(String.Concat(Enumerable.Repeat('*', 40)));
            Console.WriteLine("Laden eines Machine Learning Modells");
            Console.WriteLine(String.Concat(Enumerable.Repeat('*', 40)));

            for (int i = 0; i < modelFolderContent.Length; i++)
            {
                Console.WriteLine(String.Format("({0}): {1}", i + 1, modelFolderContent[i]));
            }

            Console.WriteLine("\nTragen Sie die Nummer des zu ladenden Machine Learning Models ein und bestätigen Sie die Eingabe mit Enter:");

            int parsed_input = -1;
            bool wantToRepeat = false;
            do
            {
                string input = Console.ReadLine();

                try
                {
                    parsed_input = int.Parse(input);
                    if (parsed_input > 0 && parsed_input <= modelFolderContent.Length)
                        return parsed_input;
                    else
                    {
                        Console.WriteLine("\nDie eingegebene Zahl entspricht keiner Auswahlmöglichkeit.");

                        parsed_input = -1;

                        wantToRepeat = AskForRepeat();
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("\nEin Fehler ist aufgetreten: " + e.Message);
                    parsed_input = -1;
                    wantToRepeat = AskForRepeat();
                }
            }
            while (parsed_input == -1 && wantToRepeat == true);


            return 0;
        }

        public static string GetCSVSaveResponse()
        {
            Console.WriteLine("\n\nMöchten Sie die Machine Learning Resultate in einer Datei speichern?");
            Console.WriteLine("Drücken Sie Y um die Ergebnisse zu speichern, N um zum Hauptmenü zurückzukehren.");

            bool answerInCorrectFormat = false;
            bool wantToRepeat = false;
            string response = "";
            do
            {
                ConsoleKeyInfo consoleKey = Console.ReadKey(true);

                if (consoleKey.Key == ConsoleKey.Y || consoleKey.Key == ConsoleKey.N)
                {
                    answerInCorrectFormat = true;
                    if (consoleKey.Key == ConsoleKey.Y)
                    {
                        Console.WriteLine("\nBitte geben Sie einen Dateinamen an:");
                        response = Console.ReadLine();
                        return response;
                    }
                    else
                        return response;
                }
                else
                {
                    answerInCorrectFormat = false;
                    wantToRepeat = AskForRepeat();
                }
                
            }
            while (answerInCorrectFormat == false && wantToRepeat == true);

            return "";
            
        }
    }
}
