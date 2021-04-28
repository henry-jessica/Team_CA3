using System;
using System.IO;
namespace FileHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            //WriteToFile();
            // WriteToFile_Range();
            WriteToFile_2();
            Console.ReadKey();
        }
        private static void WriteToFile()
        {

            string[] Range = { "0", "0-9", "10-19", "19-20", "+30" };

            FileStream fs = new FileStream(@"C:\Users\jessi\Downloads\temp.txt", FileMode.Open, FileAccess.Read);
            StreamReader inputTream = new StreamReader(fs);

            string lineIn;
            int count = 0;
            int total = 0;

            lineIn = inputTream.ReadLine();
            string[] myFields = new string[2]; //there are two fields in each line, date and temperature
            Console.WriteLine("{0,-20}{1,-10}", "Date", "Temperature");
            while (lineIn != null)
            {
                myFields = lineIn.Split(',');
                lineIn = inputTream.ReadLine();
                total += int.Parse(myFields[1]);
                count++;
                Console.WriteLine("{0,-20}{1,-10}", myFields[0], myFields[1]);
            }

            Console.WriteLine("Averange temperature = {0}", total / count);
        }
        private static void WriteToFile_Range()
        {

            string[] Range = { "0", "0-9", "10-19", "19-20", "+30" };
            int[] Range_In = new int[5];

            FileStream fs = new FileStream(@"C:\Users\jessi\Downloads\temp.txt", FileMode.Open, FileAccess.Read);
            StreamReader inputTream = new StreamReader(fs);

            string lineIn;
           // int count = 0;
            int date;
            int temperature; 
            

            lineIn = inputTream.ReadLine();

            string[] myFields = new string[2]; 
            Console.WriteLine("{0,-20}{1,-10}", "Range", "Number of Days");
          
            while (lineIn != null)
            {
                myFields = lineIn.Split(',');
                lineIn = inputTream.ReadLine();
                date = int.Parse(myFields[1]);
                temperature = int.Parse(myFields[1]); //convert to int
                temperature = temperature / 10;

                switch (temperature)
                {
                    case 0:
                        Range_In[1] += +1;
                        break;
                    case 1:
                        Range_In[2] += +1;
                        break;
                    case 2:
                        Range_In[3] += +1;
                        break;
                    default:
                        Range_In[4] += +1;
                        break;
                }
            }

            for (int i = 0; i < Range_In.Length; i++)
            {
                Console.WriteLine("{0,-20}{1,-10}", Range[i], Range_In[i]);
            }


        }
        static void WriteToFile_2()
        {
            Console.WriteLine("\n Writing to file \n");
            // create a streamwriter object called sw (you can call it what you like), which allows us to write characters to a file
            // you can think of it as a pipe connecting our program to the file, which facilitates different operations
            FileStream fs = new FileStream(@"C:\Users\jessi\Downloads\temp.txt", FileMode.Open, FileAccess.Write);
            StreamWriter StreamWriter = new StreamWriter(fs);
            // note the @, this means the string is read verbatim (as is), escape chars are ignored
            // note also, that generally it is a bad idea to hard code the path of a file 
            // if you dont specify a path, the file will be placed in the Debug folder of your project
            string[] myFields = new string[2]; // 2 elements 
            string choice = " ";
            string exit = "5";
            Console.Write("\nClub Payments");
            while(choice != exit)
            {
                Console.WriteLine("Name:");
                myFields[0] = Console.ReadLine();
                Console.WriteLine("Pagou? Sim ou Nao");
                myFields[1] = Console.ReadLine();
                StreamWriter.WriteLine(myFields[0]+","+myFields[1]);
                Console.WriteLine("again?");
                choice = Console.ReadLine();
            }
            // close the connection to the file
            StreamWriter.Close();
        }
    }
}
