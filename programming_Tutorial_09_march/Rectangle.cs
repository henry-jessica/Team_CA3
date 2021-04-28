using System;
using System.Collections.Generic;
using System.Text;

namespace programming_Tutorial_09_march
{
    class Rectangle
    {
        //Attributes of the Class - caracteristicas ou informações sobre o the rectangle called: Atributes of the Class 
        private int length;
        private int width;
        private string colour;

        // para eu usar os atributos em privado eu preciso encapsular primeiro 
        // para isso eu uso o get e set 

        public int Length  // Data encapsulated 
        {
            get // give read access to the private length attribute 
            {
                return length;
            }
            set // give write access to the private length attribute 
            {   // could have here to make sure we are happy with a value before we set it 
                length = value;
            }
        }

        public int Width // Data encapsulated 
        {
            get // give read access to the private length attribute 
            {
                return width;
            }
            set // give write access to the private length attribute 
            {   // could have here to make sure we are happy with a value before we set it 
                width = value;
            }
        }

        public string Colour //Data encapsulated 
        {
            get // give read access to the private length attribute 
            {
                return colour;
            }
            set // give write access to the private length attribute 
            {   // could have here to make sure we are happy with a value before we set it 
                colour = value;
            }
        }

        //methods (ação com o retangulo) 
        // Constructor method - must call this before we can use the class - sempre vai ter 

        public Rectangle() // crteating a object 
        {

        }
        //quando eu passo valor direto na instancia na class program
        public Rectangle(int l, int w)
        {
            length = l;
            width = w;

        }
        public Rectangle(int lengthIn, int widthIn, string colourIn)
        {
            length = lengthIn;
            width = widthIn;
            colour = colourIn;
        }

        public virtual double  CalcArea() //make virtual to another class use my class 
        {
            return length * width;

        }
        public int Perimeter()
        {
            return (length + width) * 2;

        }

        public double Diagonal()
        {
            double sqh = length * length + width * width;
            return Math.Sqrt(sqh);
        }


        public override string ToString()
        {
            return "Length = " + Length + " Width = " + Width + " Colour = " + Colour;
        }

    }
}
