using System;

namespace programming_Tutorial_09_march
{
    class Program
    {
        static void Main(string[] args)
        {
            Rectangle s1 = new Rectangle(10, 20, "blue");
            Console.WriteLine(s1.ToString());
            Console.WriteLine("Area = " + s1.CalcArea());

            Cube s2 = new Cube(10, 20, "blue", 30);
            Console.WriteLine(s2.ToString());
            Console.WriteLine("Area = " + s2.CalcArea());
            Console.WriteLine("Volumn = " + s2.CalcVolume());

            Rectangle[] Shapes = new Rectangle[4];

            Shapes[0] = new Rectangle(5, 4, "green");
            Shapes[1] = new Rectangle(6, 4, "red");
            Shapes[2] = new Cube(10, 5, "brown",20);
            Shapes[3] = new Cube(5, 4, "orange", 30);

            // podemos usar o polimorfismo 

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(Shapes[i].ToString());
                Console.WriteLine("Area =  " + Shapes[i].CalcArea());
            }
            Console.ReadKey();
        }
    }
}
