﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MLAdapter;
using System.IO;
using System.Text.RegularExpressions;


namespace DummyGUI_MLAdapter
{
    class Program
    {
        static void Main(string[] args)
        {
            string filepath = @"../../../../../optdigits-train.csv";
            string testdata = @"../../../../../optdigits-test.csv";

            //Prepare the data for the MNIST dataset
            DataTable dt = HelperFunctions.ConvertCsvToDataTable(filepath, false);
            DataTable testdaten = HelperFunctions.ConvertCsvToDataTable(testdata, false);
            HelperFunctions.PrintDataTableToConsole(dt);           
            Console.WriteLine("********************************************************************************");
            int[] inputColumns = new int[64];
            for (int i = 0; i < 64; i++)
            {
                inputColumns[i] = i;
            }
            int labelColumn = 64;
            // ML Adapter - function calls
            MLAdapter.MLAdapter mLAdapter = new MLAdapter.MLAdapter();
            mLAdapter.TrainModel(dt, inputColumns, labelColumn);
            List<int> results = mLAdapter.TestModel(testdaten, inputColumns, labelColumn);
            double accuracy = HelperFunctions.GetModelAccuracy(testdaten, results, labelColumn);
            Console.WriteLine(accuracy);
            mLAdapter.SaveModel(@"../", "mnist_model.zip");
            
            // Test of load and save functionality
            MLAdapter.MLAdapter mLAdapter_Loaded = new MLAdapter.MLAdapter();
            string loadFilePath = "../";
            string fileName = "mnist_model.zip";
            mLAdapter_Loaded.LoadModel(Path.Combine(loadFilePath, fileName));
            //TestModel is the same as PredictAndReturnResults because the statistical analysis used to be outsourced
            var results2 = mLAdapter_Loaded.TestModel(testdaten, inputColumns, 4);
            var results3 = mLAdapter_Loaded.PredictAndReturnResults(testdaten, inputColumns);
        }

       
    }
}
