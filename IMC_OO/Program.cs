using System;
using System.Threading;

namespace IMC_OO
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine();
            Thread.Sleep(1000);
            Console.Clear();

            Console.Read();

            Person person1 = new Person(); //instance of the class 

            Console.Write("Enter Height ");
            person1.Height = double.Parse(Console.ReadLine());
            Console.Write("Enter Width ");
            person1.Weight = double.Parse(Console.ReadLine());


        }
    }
}
