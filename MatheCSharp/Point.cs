namespace MatheCSharp
{
    internal class Point
    {
        //coordinates of the point
        public double x;
        public double y;

        //constructor
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }



        /**
        * A string to represent the point
        */
        public override string ToString()
        {
            return "(" + x + "," + y + ")";
        }



        /**
        * Overwriting of .equals() with object parameter
        */
        public override bool Equals(System.Object obj)
        {
            if (obj == null)
                return false;

            Point p = obj as Point;
            if ((System.Object)p == null)
            {
                return false;
            }

            return (x == p.x) && (y == p.y);
        }

        /**
        * Overwriting of .equals() with point parameter
        */
        public bool Equals(Point p)
        {
            if ((object)p == null)
                return false;

            return (x == p.x) && (y == p.y);
        }




        /**
         * Overwriting of .hashCode()
         */
        public override int GetHashCode()
        {
            return (x + y).GetHashCode();
        }

    }
}