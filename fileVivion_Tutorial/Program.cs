using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;  // must include this reference when implementing file handling


namespace fileVivion_Tutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            //WriteToFile();

            //ReadAllLines();

            ReadCSVFile();
        }

        static void GetFileInformation()
        {
            // playlist2017 is in the bin folder of solution, hence no need to specify location
            string myPath = @"playlist2017.txt";
            FileInfo f = new FileInfo(myPath);
            Console.WriteLine(f.Length);
            Console.WriteLine(f.Name);
            Console.WriteLine(f.FullName);
            Console.WriteLine(f.DirectoryName);


        }
        static void ReadCSVFile()
        {
            Console.WriteLine("\n Reading all lines from CSV file and printing each artist to the console \n");
            Console.WriteLine("\n Accumulating the length of all songs printing totalto the console \n");


            // playlist2017 is in the bin folder of solution, hence no need to specify location
            FileStream fs = new FileStream("playlist2017.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);

            // i have placed the file in the debug folder, so no path needed


            string lineIn; // will hold data that we read in

            string[] fieldArray = new string[3]; // array to store chopped up line

            int totalTime = 0; // variable to accumulate time of all songs

            lineIn = sr.ReadLine(); // read in first line from file

            while (lineIn != null)
            {
                // split lineIn where there is a ','

                fieldArray = lineIn.Split(',');

                int songLength = int.Parse(fieldArray[2]); // store length of song in songLength


                totalTime += songLength; // accumulating songlength

                Console.WriteLine(fieldArray[0]); // print artist name

                lineIn = sr.ReadLine(); // read in the next line


            }

            Console.WriteLine("Total playlist playing time = {0}", totalTime);

            Console.WriteLine("\n all records in file processed");

            sr.Close();
        }

        static void WriteToFile()
        {

            Console.WriteLine("\n Writing to file \n");

            // create a streamwriter object called sw (you can call it what you like), which allows us to write characters to a file
            // you can think of it as a pipe connecting our program to the file, which facilitates different operations

            FileStream fs = new FileStream(@"h:\myfile.txt", FileMode.Open, FileAccess.Write);

            StreamWriter sw = new StreamWriter(fs);

            // note the @, this means the string is read verbatim (as is), escape chars are ignored
            // note also, that generally it is a bad idea to hard code the path of a file 
            // if you dont specify a path, the file will be placed in the Debug folder of your project


            // write three lines out to the file
            sw.WriteLine("Hello");
            sw.WriteLine("today is monday");
            sw.WriteLine("it is sunny");

            // close the connection to the file
            sw.Close();


        }

        static void ReadAllLines()
        {

            Console.WriteLine("\n Reading all lines from the file and printing each line to the console \n");


            FileStream fs = new FileStream(@"h:\myfile.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);


            string lineIn; // will hold data that we read in

            lineIn = sr.ReadLine(); // read in first line from file


            while (lineIn != null) // null signals we are end of the file
            {
                Console.WriteLine(lineIn); // print to screen

                lineIn = sr.ReadLine(); // read in next line from file
            }

            sr.Close();

        }
    }
}