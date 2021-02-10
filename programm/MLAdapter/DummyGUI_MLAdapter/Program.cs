using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MLAdapter;
using System.IO;
using System.Text.RegularExpressions;
using DataManager;
using CommonInterfaces;
using static DummyGUI_MLAdapter.ConsoleHelperFunctions;


namespace DummyGUI_MLAdapter
{
    class Program
    {
        static void Main(string[] args)
        {            
            bool repeat = false;
            DataManager.DataManagerClass dataManager = new DataManagerClass();
            MLAdapter.MLAdapter mLAdapter = new MLAdapter.MLAdapter();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            bool modelLoaded = false;
            string folderpath = @"../../../../../Datasets";
            string modelpath = @"../../../../../ML_Models";
            

            do
            {
                string[] folderContent = HelperFunctions.GetFolderContent(folderpath);
                string[] modelFolderContent = HelperFunctions.GetModelFolderContent(modelpath);
                if (modelLoaded == false)
                {
                    DisplayDefaultHomescreen();
                    ConsoleKeyInfo consoleKey = Console.ReadKey(true);
                    // hier noch check auf a und b einbauen
                    // Neue Tabelle laden und Modell trainieren
                    if (consoleKey.Key == ConsoleKey.A)
                    {

                        int resultCSV = DisplayCSVChoice(folderContent);
                        bool hasHeader = GetHeaderResponse(consoleKey);
                        try
                        {
                            //DataTable dt = HelperFunctions.ConvertCsvToDataTable(folderContent[resultCSV - 1], false);
                            dataManager.LoadCSV(folderContent[resultCSV - 1], hasHeader);

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.ReadKey(true);
                            repeat = GetRestartResponse(consoleKey);
                            continue;
                        }

                        if (Console.BufferWidth < dataManager.UserTable.Columns.Count * 5)
                            Console.BufferWidth = dataManager.UserTable.Columns.Count * 5 + 2;

                        PrintDataTableToConsole(dataManager.UserTable, 5);

                        // Input Spalten auswählen

                        dataManager.SetInputColumns(GetInputColumnsResponse(dataManager.UserTable.Columns.Count, consoleKey));

                        // Label Spalte auswählen

                        dataManager.SetLabelColumn(GetLabelColumnResponse(dataManager.UserTable.Columns.Count, dataManager.InputColumns, consoleKey));

                        // aktuelle Auswahl der Columns nochmal schön darstellen

                        PrintDataTableToConsole(dataManager.UserTable, dataManager.InputColumns, dataManager.LabelColumn, 5);

                        bool wantToProceed = GetProceedResponse(consoleKey);

                        if (wantToProceed == false)
                        {
                            repeat = true;
                            continue;
                        }
                        try
                        {
                            mLAdapter.TrainModel(dataManager);
                            Console.WriteLine("\nDas Trainieren des Modells war erfolgreich.");
                            Console.WriteLine("\nDrücken Sie eine beliebige Taste um mit dem Testen des Modells fortzufahren...");
                            Console.ReadKey(true);

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        int testCSV = DisplayCSVChoice(folderContent);

                        try
                        {
                            dataManager.LoadCSV(folderContent[testCSV - 1], hasHeader);

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.ReadKey(true);
                            repeat = GetRestartResponse(consoleKey);
                            continue;
                        }

                        List<int> testResults = mLAdapter.TestModel(dataManager);

                        DisplayModelAccuracy(testResults, dataManager);

                        modelLoaded = true;

                        repeat = true;
                    }

                    // ML Modell laden
                    else if (consoleKey.Key == ConsoleKey.B)
                    {
                        //string filepath = GetFilepathResponse();

                        int resultModel = GetLoadModelResponse(modelFolderContent);

                        try
                        {
                            mLAdapter.LoadModel(modelFolderContent[resultModel - 1]);
                            Console.WriteLine("\nDas Laden des Modells war erfolgreich.");
                            Console.WriteLine("Drücken Sie eine beliebige Taste um fortzufahren...");
                            modelLoaded = true;
                            repeat = true;
                            Console.ReadKey(true);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.ReadKey(true);
                            repeat = GetRestartResponse(consoleKey); 
                            continue;
                        }
                    }
                    //else if (consoleKey.Key == ConsoleKey.B)
                    //{
                    //    string filepath = GetFilepathResponse();

                    //    try
                    //    {
                    //        mLAdapter.LoadModel(filepath);
                    //        Console.WriteLine("\nDas Laden des Modells war erfolgreich.");
                    //        Console.WriteLine("Drücken Sie eine beliebige Taste um fortzufahren...");
                    //        modelLoaded = true;
                    //        repeat = true;
                    //        Console.ReadKey(true);
                    //    }
                    //    catch (Exception e)
                    //    {
                    //        Console.WriteLine(e.Message);
                    //        Console.ReadKey(true);
                    //        repeat = GetRestartResponse(consoleKey);
                    //        continue;
                    //    }
                    //}
                    else if (consoleKey.Key == ConsoleKey.X)
                    {
                        TriggerShutdown();
                    }
                    else
                    {
                        repeat = DisplayCharNotRecognized(consoleKey);
                        continue;
                    }
                }
                else
                {
                    DisplayFullHomescreen();

                    ConsoleKeyInfo consoleKey = Console.ReadKey(true);

                    //Neue Tabelle laden und Modell trainieren
                    if (consoleKey.Key == ConsoleKey.A)
                    {

                        int resultCSV = DisplayCSVChoice(folderContent);
                        bool hasHeader = GetHeaderResponse(consoleKey);
                        try
                        {
                            //DataTable dt = HelperFunctions.ConvertCsvToDataTable(folderContent[resultCSV - 1], false);
                            dataManager.LoadCSV(folderContent[resultCSV - 1], hasHeader);

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.ReadKey(true);
                            repeat = GetRestartResponse(consoleKey);
                            continue;
                        }

                        if (Console.BufferWidth < dataManager.UserTable.Columns.Count * 5)
                            Console.BufferWidth = dataManager.UserTable.Columns.Count * 5 + 2;

                        PrintDataTableToConsole(dataManager.UserTable, 5);

                        // Input Spalten auswählen

                        dataManager.SetInputColumns(GetInputColumnsResponse(dataManager.UserTable.Columns.Count, consoleKey));

                        // Label Spalte auswählen

                        dataManager.SetLabelColumn(GetLabelColumnResponse(dataManager.UserTable.Columns.Count, dataManager.InputColumns, consoleKey));

                        // aktuelle Auswahl der Columns nochmal schön darstellen

                        PrintDataTableToConsole(dataManager.UserTable, dataManager.InputColumns, dataManager.LabelColumn, 5);

                        bool wantToProceed = GetProceedResponse(consoleKey);

                        if (wantToProceed == false)
                        {
                            repeat = true;
                            continue;
                        }
                        try
                        {
                            mLAdapter.TrainModel(dataManager);
                            Console.WriteLine("\nDas Trainieren des Modells war erfolgreich.");
                            Console.WriteLine("\nDrücken Sie eine beliebige Taste um mit dem Testen des Modells fortzufahren...");
                            Console.ReadKey(true);

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        int testCSV = DisplayCSVChoice(folderContent);

                        try
                        {
                            dataManager.LoadCSV(folderContent[testCSV - 1], hasHeader);

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.ReadKey(true);
                            repeat = GetRestartResponse(consoleKey);
                            continue;
                        }

                        List<int> testResults = mLAdapter.TestModel(dataManager);

                        DisplayModelAccuracy(testResults, dataManager);

                        modelLoaded = true;

                        repeat = true;
                    }

                    //ML Modell laden
                    else if (consoleKey.Key == ConsoleKey.B)
                    {
                        //string filepath = GetFilepathResponse();

                        int resultModel = GetLoadModelResponse(modelFolderContent);

                        try
                        {
                            mLAdapter.LoadModel(modelFolderContent[resultModel - 1]);
                            Console.WriteLine("\nDas Laden des Modells war erfolgreich.");
                            Console.WriteLine("Drücken Sie eine beliebige Taste um fortzufahren...");
                            modelLoaded = true;
                            repeat = true;
                            Console.ReadKey(true);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.ReadKey(true);
                            repeat = GetRestartResponse(consoleKey);
                            continue;
                        }
                    }

                    //Modell anwenden
                    else if (consoleKey.Key == ConsoleKey.C)
                    {
                        int resultCSV = DisplayCSVChoice(folderContent);
                        bool hasHeader = GetHeaderResponse(consoleKey);
                        try
                        {
                            //DataTable dt = HelperFunctions.ConvertCsvToDataTable(folderContent[resultCSV - 1], false);
                            dataManager.LoadCSV(folderContent[resultCSV - 1], hasHeader);

                            // noch check ob die input spalten schon belegt sind einbauen und ggf. nochmal abfragen 
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.ReadKey(true);
                            repeat = GetRestartResponse(consoleKey);
                            continue;
                        }

                        if (Console.BufferWidth < dataManager.UserTable.Columns.Count * 5)
                            Console.BufferWidth = dataManager.UserTable.Columns.Count * 5 + 2;

                        if (dataManager.InputColumns == null)
                        {
                            PrintDataTableToConsole(dataManager.UserTable, 5);
                            dataManager.SetInputColumns(GetInputColumnsResponse(dataManager.UserTable.Columns.Count, consoleKey));
                        }



                        dataManager.SetMLResult(mLAdapter.PredictAndReturnResults(dataManager));

                        PrintDataTableToConsole(dataManager.UserTable, dataManager.InputColumns, dataManager.UserTable.Columns.IndexOf("ML_Result"));

                        string mlPredictionFileName = GetCSVSaveResponse();
                        try
                        {
                            if (!String.IsNullOrEmpty(mlPredictionFileName))
                            {
                                dataManager.SaveCSV(folderpath, mlPredictionFileName);
                                Console.WriteLine("Das Speichern der Datei war erfolgreich. Die Datei liegt im Ordner programm/Datasets");
                            }

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine( e.Message);
                            repeat = GetRestartResponse(consoleKey);
                        }

                        Console.WriteLine("Drücken Sie eine beliebige Taste, um zum Hauptmenü zurückzukehren...");
                        Console.ReadKey(true);

                    }

                    //Modell speichern
                    else if (consoleKey.Key == ConsoleKey.D)
                    {
                        string filename = GetSaveFilenameResponse();
                        //string filepath = GetSaveFilepathResponse();

                        try
                        {
                            mLAdapter.SaveModel(filename);
                            Console.WriteLine("\n\nDas Speichern des Modells war erfolgreich!");
                            Console.WriteLine("\nDrücken Sie eine beliebige Taste, um zum Hauptmenü zurückzukehren...");
                            Console.ReadKey(true);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("\nEtwas ist falsch gelaufen:");
                            Console.WriteLine("\n" + e.Message);
                            repeat = GetRestartResponse(consoleKey);
                            continue;
                        }

                    }

                    else if (consoleKey.Key == ConsoleKey.X)
                    {
                        TriggerShutdown();
                    }
                    else
                    {
                        repeat = DisplayCharNotRecognized(consoleKey);
                        continue;
                    }
                }
            }
            while (repeat == true);

            TriggerShutdown();
        }
    }
}




//string filepath = @"../../../../../optdigits-train.csv";
//string testdata = @"../../../../../optdigits-test.csv";

//Prepare the data for the MNIST dataset
//DataTable dt = HelperFunctions.ConvertCsvToDataTable(filepath, false);
//DataTable testdaten = HelperFunctions.ConvertCsvToDataTable(testdata, false);
//HelperFunctions.PrintDataTableToConsole(dt, 10); 

//Console.WriteLine("********************************************************************************");

//int[] inputColumns = new int[64];
//for (int i = 0; i < 64; i++)
//{
//    inputColumns[i] = i;
//}
//int labelColumn = 64;
//// ML Adapter - function calls
//MLAdapter.MLAdapter mLAdapter = new MLAdapter.MLAdapter();
//DataManager.DataManagerClass dm = new DataManagerClass(filepath);


//mLAdapter.TrainModel(dm);
//List<int> results = mLAdapter.TestModel(dm);
//double accuracy = HelperFunctions.GetModelAccuracy(dm.UserTable, dm.ML_Result, dm.LabelColumn);
//Console.WriteLine(accuracy);
//mLAdapter.SaveModel(@"../", "mnist_model.zip");

//// Test of load and save functionality
//MLAdapter.MLAdapter mLAdapter_Loaded = new MLAdapter.MLAdapter();
//string loadFilePath = "../";
//string fileName = "mnist_model.zip";
//mLAdapter_Loaded.LoadModel(Path.Combine(loadFilePath, fileName));
////TestModel is the same as PredictAndReturnResults because the statistical analysis used to be outsourced
//var results2 = mLAdapter_Loaded.TestModel(dm);
//var results3 = mLAdapter_Loaded.PredictAndReturnResults(dm);