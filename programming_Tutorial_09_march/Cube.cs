using System;
using System.Collections.Generic;
using System.Text;

namespace programming_Tutorial_09_march
{
    class Cube : Rectangle
    {
        private double _depth;


        //depth property
        public double Depth { get { return _depth; } set { _depth = value; } }

        //Constructors 

        public Cube() : base()
        {

        }
        // my constructor - including retangle part
        public Cube(int lengthIn, int widthIn, string colourIn, double depthIn) : base(lengthIn, widthIn, colourIn)
        {
            Depth = depthIn;

        }

        //CalcAre - this method already exists in the rectangle class, but it must be modified. 
        //For that it is necessary to make it virtual in the other class and Override in this one (cube).

        public override double CalcArea()
        {
            return 4 * (Length * Width) + (2 * (Width * Depth));
        }


        //CalcVolume - unique to a cube 
        public double CalcVolume()
        {
            //Volume = (Decimal)Math.Pow((Double)Cube_Length, 3);

            return Math.Pow(Length, 3);
        }



        //To String 
        public override string ToString() // Give info about an abject
        {
            return base.ToString() + " Depth = " + Depth;
        }



    }
}
