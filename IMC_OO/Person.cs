using System;
using System.Collections.Generic;
using System.Text;

namespace IMC_OO
{
    class Person
    {
        // private fields/ attributes
        private string name;
        private double height;
        private double weight;

        public Person() // crteating a object 
        {

        }

        public string Name  // Data encapsulated 
        {
            get { return name; } //gives read access to private attribute
            set { name = value; } //gives writes to private attribute
        }

        public double Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }
        public double Weight
        {
            get
            {
                return weight;
            }
            set
            {
                weight = value;
            }
        }


        public Person(string n)
        {
            name = n;

        }
        public Person(string n, double h, double w)
        {
            name = n;
            height = h;
            weight = w;

        }

        public double CalcBMI()
        {
            return weight / (height * height);
        }

        public string CheckRange()
        {
            string range = " ";

            double personBMI = CalcBMI();

            if (personBMI < 18.5)
            {
                range = "Underweight";
            }
            else if (personBMI >= 18.5 && personBMI <= 24.9)
            {
                range = "Normal";
            }
            else if (personBMI >= 25 && personBMI <= 29.9)
            {
                range = "Overweight";
            }
            else
            {
                range = "Obese";
            }
            return range;
        }
    }
}
