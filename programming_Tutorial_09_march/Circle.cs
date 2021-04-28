using System;
using System.Collections.Generic;
using System.Text;

namespace programming_Tutorial_09_march
{
    class Circle
    {
        private double radius;
        private string colour;

        public Circle()
        {


        }

        //mesma ideia usar o get e o set para encapsular os valores privados e tornalos publicos

        public double Radius
        {
            get
            {
                return radius;
            }
            set
            {
                radius = value;
            }
        }
        //mesma ideia usar o get e o set para encapsular os valores privados e tornalos publicos
        public string Colour
        {
            get
            {
                return colour;
            }
            set
            {
                colour = value;
            }
        }



        public Circle(double r, string c)
        {
            radius = r;
            colour = c;
        }

        public double CalcCircle()
        {
            return Math.PI * (radius * radius);
        }

    }
}
