using System;

namespace MatheCSharp
{
    internal class ExponentialFunction : Function
    {

        /**
        * Create a function whose graph runs through two given points
        * @param p - point 1
        * @param q - point 2
        */
        public bool CreateThroughPoints(Point p, Point q)
        {

            double a, b;

            if (p.Equals(q))
            {
                a = Math.Pow(p.y, 1 / p.x);

                if (a - (int)a == 0)
                    expression += (int)a + "^x";
                else
                    expression += Function.RoundDouble(a, 3) + "^x";

                return true;
            }


            if (p.x == q.x || p.y == q.y)
            {
                Console.WriteLine("Invalid points, exponential functions' graphs are never perfectly horizontal or vertical!");
                expression = "Invalid points!";
                return false;
            }


            a = Math.Pow(q.y / p.y, 1 / (q.x - p.x));
            b = p.y / Math.Pow(a, p.x);


            if (a == 0 || b == 0)
                expression = "0";
            else {

                if (b - (int)b == 0)
                    expression += (b != 1.0) ? (int)b + " * " : "";
                else
                    expression += (b != 1.0) ? Function.RoundDouble(b, 3) + " * " : "";


                if (a - (int)a == 0)
                    expression += (int)a + "^x";
                else
                    expression += Function.RoundDouble(a, 3) + "^x";


            }
            return true;

        }

    }
}