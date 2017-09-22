using System;
using System.Collections.Generic;

namespace PizzaDelivery
{
   public class PizzaDeliveryProgram
   {
      //static string[] lines;
      static List<string> lines;
      public static void ReadTestCases()
      {
         int numberOfTestCases = 0;
         int nextInputLine = 0;
         int currentTestXSize = 0;
         int currentTestYSize = 0;

         if(lines.Count < 1)
         {
            Console.WriteLine("File was empty.");
            return;
         }

         bool success = int.TryParse(lines[nextInputLine], out numberOfTestCases);

         if (!success)
         {
            Console.WriteLine("Failed to get test cases.");
            return;
         }

         nextInputLine++;

         for (int i = 1; i <= numberOfTestCases; i++)
         {
            if (lines.Count < (1 + nextInputLine))
            {
               Console.WriteLine("File missing test case " + i + " axis values.");
               return;
            }

            string[] axisValues = lines[nextInputLine].Split(' ');

            if (axisValues.Length != 2)
            {
               Console.WriteLine("Failed to get test case " + i + " axis values.");
               return;
            }

            success &= int.TryParse(axisValues[0], out currentTestYSize);
            success &= int.TryParse(axisValues[1], out currentTestXSize);

            if (!success)
            {
               Console.WriteLine("Failed to parse test case " + i + " axis values.");
               return;
            }

            nextInputLine++;

            //int[,] testCaseArray = new int[currentTestXSize,currentTestYSize]; //this method works but is slow

            int[] rowCost = new int[currentTestXSize];
            int[] columnCost = new int[currentTestYSize];

            for (int j = 1; j <= currentTestXSize; j++)
            {
               if (lines.Count < (nextInputLine + 1))
               {
                  Console.WriteLine("File is missing test case " + i + " row " + j + " values.");
                  return;
               }

               string[] rowValues = lines[nextInputLine].Split(' ');

               if (rowValues.Length < currentTestYSize)
               {
                  Console.WriteLine("Failed to get test case " + i + " row " + j + " values.");
                  return;
               }

               int thisRowsCost = 0;
               int thisSquaresCost = 0;

               for (int k = 0; k < currentTestYSize; k++)
               {
                  //success &= int.TryParse(rowValues[k], out testCaseArray[j-1,k]);
                  success &= int.TryParse(rowValues[k], out thisSquaresCost);
                  columnCost[k] += thisSquaresCost;
                  thisRowsCost += thisSquaresCost;
               }

               if (!success)
               {
                  Console.WriteLine("Failed to parse test case " + i + " row " + j + " values.");
                  return;
               }

               rowCost[j-1] = thisRowsCost;
               nextInputLine++;
            }

            //int result = GetMinimumTravelDistance(testCaseArray, currentTestXSize, currentTestYSize);
            int result = GetMinimumTravelDistance(rowCost, columnCost);
            //Console.WriteLine("Test case " + i + " minimum distance is " + result + " blocks."); // for debugging use
            Console.WriteLine(result + " blocks.");
         }
      }

      public static int GetMinimumTravelDistance(int[] rowCost, int[] columnCost)
      {
         int result = 0;
         result += GetMinimumTravelDistanceFromLine(rowCost);
         result += GetMinimumTravelDistanceFromLine(columnCost);
         return result;
      }

      public static int GetMinimumTravelDistanceFromLine(int[] lineCost)
      {
         int result = int.MaxValue;
         int size = lineCost.Length;

         for(int i = 0; i < size; i++)
         {
            int currentLineCost = 0;

            for(int j = 0; j < size; j++)
            {
               currentLineCost += (Math.Abs(i - j) * lineCost[j]);
            }

            if(currentLineCost < result)
            {
               result = currentLineCost;
            }
         }
         return result;
      }

      /* public static int GetMinimumTravelDistance(int[,] array, int x, int y)  //This method works but it has terrible time complexity
      {
         int result = int.MaxValue;
         int idealX = 0;
         int idealY = 0;

         for(int i = 0; i < x; i++)
         {
            for(int j = 0; j < y; j++)
            {
               int currentLocationsDistanceToAllPoints = 0;

               for(int k = 0; k < x; k++)
               {
                  for(int l = 0; l < y; l++)
                  {
                     if((l==j) && (k==i))
                     {
                        currentLocationsDistanceToAllPoints += 0;
                     }
                     else
                     {
                        currentLocationsDistanceToAllPoints += ((Math.Abs(i - k) + Math.Abs(j - l)) * array[k, l]);
                     }
                  }
               }

               if(currentLocationsDistanceToAllPoints < result)
               {
                  result = currentLocationsDistanceToAllPoints;
                  idealX = i;
                  idealY = j;
               }
            }
         }
         // Console.WriteLine("Ideal location is " + idealX +" " + idealY);  //for debugging use 
         return result;
      } */

      /* public static void GetInputFile()
      {
         string path = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\testinput.txt");
         lines = System.IO.File.ReadAllLines(path);
      } */
      static void Main(string[] args)
      {
         // My solution to https://paradox.kattis.com/problems/pizza
         // GetInputFile(); //For reading inputs from a file
         string line;
         lines = new List<string>();
         while ((line = Console.ReadLine()) != null)
         {
            lines.Add(line);
         }
         ReadTestCases();
         Console.WriteLine("Press Enter to close.");  //for manual use 
         Console.Read();  //for manual use 
      } 
   }
}
