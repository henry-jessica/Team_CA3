using System;

namespace RevisaoCA2
{
    class Program
    {
        static string[] contestants = new string[3];
        static int[] votes = new int[3];
        static string TABLEINPUT = "{0,-35}{1,-5}{2,2}";
        static int total = 0, max = 0, position = 0;
        static double averenge = 0;


        static void Main(string[] args)
        {
            for (int i = 0; i < contestants.Length; i++)
            {
                Console.Write(TABLEINPUT, "\nEnter Name of the Contestestant", 1 + i, ": ");
                contestants[i] = Console.ReadLine();

                Console.Write(TABLEINPUT, "\nEnter Vote of Contestestant ", 1 + i, ": ");
                votes[i] = int.Parse(Console.ReadLine());
            }

            total = CalculeTotal(votes);
            averenge = total / contestants.Length;
            PrintReport();
        }
        /// <summary>
        /// To Print Report of Vote 
        /// </summary>
        static private void PrintReport()
        {
            Console.WriteLine("\n\n{0,-15}{1,-15}{2,-1}", "contestants", "Votes", " ");

            for (int i = 0; i < contestants.Length; i++)
            {
                Console.WriteLine("\n\n{0,-15}{1,-15}", contestants[i], votes[i]);
            }

            Console.WriteLine("\n{0,-20}{1,-15}", "Total votes", total);
            Console.WriteLine("\n{0,-20}{1,-15}", "Averenge", averenge);
            Console.WriteLine("\n{0,-20}{1,-15}", "Highest number of votes", GetLarde(votes));
            Console.WriteLine("\n{0,-20}{1,-15}", "The contestants most voted", contestants[position]);

        }
       /// <summary>
       /// Calcule total of votes
       /// </summary>
       /// <param name="vote"></param>
       /// <returns></returns>
        static private int CalculeTotal(int[] vote)
        {
            for (int i = 0; i < vote.Length; i++)
            {
                total = votes[i] + total;
            }

            return total;
        }
        static private int GetLarde(int[] vote)
        {
            int max = 0;
            for (int i = 0; i < vote.Length; i++)
            {
                if (vote[i] > max)
                {
                    max = vote[i];

                    position = i;
                }
            }
            return max;
        }

    }
}

