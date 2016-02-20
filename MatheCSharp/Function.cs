using System;
using Ciloci.Flee;
using System.Text;

namespace MatheCSharp
{
    internal class Function
    {

        /**
 * The actual function
 */
        internal string expression = "";

        /**
         * Directly enter the expression
         * @param expression - the expression to save as the function
         */
        public void SetExpression(string expression)
        {
            this.expression = expression;
        }




        /**
         * This checks whether a point is on the graph
         * @param p - the point that is checked
         * @return true, if point is on graph; false, if not
         */
        public bool TestPointOnGraph(Point p)
        {
            ExpressionContext context = new ExpressionContext();
            context.Imports.AddType(typeof(Math));
            context.Variables.Add("x", p.x);
            IGenericExpression<double> e = context.CompileGeneric<double>(expression);

            return e.Evaluate() == p.y;
        }




        /**
         * Displays a value table for the expression
         * @param start - start value for x
         * @param end - end value for x
         * @param step - the step between x values
         */
        public void Table(double start, double end, double step)
        {
            StringBuilder sb = new StringBuilder("f(x) = " + expression + "\n\n");

            double xValue = start;

            ExpressionContext context = new ExpressionContext();
            context.Imports.AddType(typeof(Math));
            context.Variables.Add("x", 0.0);
            IGenericExpression<double> e = context.CompileGeneric<double>(expression);

            if (start <= end) //increasing x
            {               
                while (xValue <= end)
                {
                    context.Variables["x"] = xValue;
                    sb.Append("f(" + xValue + ") = " + RoundDouble(e.Evaluate(), 3) + "\n");
                    xValue += step;
                }
            }
            else //decreasing x
            {
                while (xValue >= end)
                {
                    context.Variables["x"] = xValue;
                    sb.Append("f(" + xValue + ") = " + RoundDouble(e.Evaluate(), 3) + "\n");
                    xValue -= step;
                }
            }

            Console.WriteLine(sb.ToString() + "\n");           

        }



        /**
         * A string to represent the function
         */
        public override string ToString()
        {
            return expression;
        }


        /**
         * Round a double value to a specified number of decimals
         * @param doubleValue - the object to be rounded
         * @param decimals - the number of decimals
         * @return the rounded double
         */
        public static double RoundDouble(double doubleValue, int decimals)
        {
            return Math.Round(Math.Pow(10, decimals) * doubleValue) / Math.Pow(10, decimals);
        }




        public static Function MirrorX(Function function)
        {

            Function f;

            //avoid double -
            if (function.expression.StartsWith("-"))
            {

                if (function is ExponentialFunction) {
                    f = new ExponentialFunction();
                } else if (function is LinearFunction) {
                    f = new LinearFunction();
                } else {
                    f = new Function();
                }

                f.SetExpression(function.expression.Substring(1));

                //add - otherwise	
            }
            else {

                if (function is ExponentialFunction) {
                    f = new ExponentialFunction();
                    f.SetExpression("-" + function);
                    return f;

                } else if (function is LinearFunction){
                    f = new LinearFunction();
                    f.SetExpression("-(" + function + ")");
                    return f;

                } else {
                    f = new Function();
                    f.SetExpression("-(" + function + ")");
                    return f;
                }
            }

            return f;

        }




        public static Function MirrorY(Function function)
        {

            String[] splitX = function.expression.Split('x');
            Function f = new Function();


            f.SetExpression(splitX[0] + "(-x)");
            if (splitX.Length >= 2)
                f.SetExpression(f.expression + splitX[1]);


            if (function is ExponentialFunction)
			return (ExponentialFunction)f;
		else if (function is LinearFunction)
			return (LinearFunction)f;
		else 
			return f;
        }




        public static Function MirrorOrigin(Function function)
        {

            Function f = MirrorX(function);
            Function g = MirrorY(f);

            if (function is ExponentialFunction)
			return (ExponentialFunction)g;
		else if (function is LinearFunction)
			return (LinearFunction)g;
		else 
			return g;
        }


    }
}