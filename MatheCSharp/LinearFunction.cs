using System;

namespace MatheCSharp
{
    internal class LinearFunction : Function
    {

        /**
        * Create a function whose graph runs through two given points
        * @param p - point 1
        * @param q - point 2
        */
        public bool CreateThroughPoints(Point p, Point q)
        {

            if (p.x == q.x && !p.Equals(q))
            {
                Console.WriteLine("There is no function f(x) for a vertical straight line!");
                return false;
            }


            double m = (q.y - p.y) / (q.x - p.x);

            if (System.Double.IsNaN(m))
                m = 0;

            double t = p.y - m * p.x;


            if (m != 0)
            {
                if (m - (int)m == 0)
                    expression += (m != 1.0) ? (int)m + " * x" : "x";
                else
                    expression += (m != 1.0) ? Function.RoundDouble(m, 3) + " * x" : "x";
            }

            if (expression != "" && t != 0)
                expression += " + ";

            if (t - (int)t == 0)
                expression += (t != 0.0) ? ((int)t).ToString() : "";
            else
                expression += (t != 0.0) ? Function.RoundDouble(t, 3).ToString() : "";
            return true;

        }

    }
}