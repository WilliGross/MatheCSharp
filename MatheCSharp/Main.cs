using Ciloci.Flee;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MatheCSharp
{
    class Program
    {

        /**A utility array representing the alphabet */
        public static char[] alphabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

        /**A list where all functions are stored */
        private List<Function> functions = new List<Function>();

        /**
        * The main method - calls Menu()
        * @param args - program agrguments (not used)
        */
        public static void Main(string[] args)
        {
            new Program();
        }

        /**
        * The constructor that calls the menu
         */
        public Program()
        {
            Menu();
        }


        /**
         * Displays the menu and calls selected tasks
         */
        private void Menu()
        {
            Console.WriteLine("What would you like to do?"
                + "\n(CREATE a function, LOAD a previous function, CLOSE)");
            string programMode = Console.ReadLine();

            if (programMode != null)
            {

                programMode.ToLower();


                if (programMode.Contains("create") || programMode.Contains("1"))
                {
                    Console.WriteLine();
                    CreateFunctionsMenu();
                }

                if (programMode.Contains("load") || programMode.Contains("prev") || programMode.Contains("2"))
                {
                    Console.WriteLine();
                    ShowAndSelectPreviousFunctions();
                }

                if (programMode.Contains("close") || programMode.Contains("exit")
                        || programMode == "" || programMode.Contains("0") || programMode.Contains("3"))
                {
                }
                else
                {
                    Menu();
                }

            }
            else
            {
                Console.WriteLine("No operation entered! Exiting program!");
            }
        }


        /**
         * Displays a menu for creating functions
        */
        private void CreateFunctionsMenu()
        {
            Console.WriteLine("How would you like to create your function?"
                + "\n(TYPE, create LINEAR f. through 2 points, create EXPONENTIAL f. through 2 points, go BACK to main menu)");
            string functionType = Console.ReadLine();

            if (functionType != null)
            {

                functionType.ToLower();

                if (functionType.Contains("type") || functionType.Contains("1"))
                {
                    Console.WriteLine();
                    TypeFunction();
                }

                if (functionType.Contains("lin") || functionType.Contains("2"))
                {
                    Console.WriteLine();
                    CreateLinearFunction();
                }

                if (functionType.Contains("exp") || functionType.Contains("3"))
                {
                    Console.WriteLine();
                    CreateExponentialFunction();
                }



                //if nothing matches go back to main menu automatically due to recursive method call in menu()

            }
            else
            {
                Console.WriteLine("Nothing entered, going back to main menu!\n\n");
                //going back automatically due to recursive method call in menu()
            }

        }




        /**
         * Displays a menu for a specific function
         * @param function - the function to interact with
         */
        private void FunctionActionsMenu(Function function)
        {
            Console.WriteLine("Would you like to calculate a VALUE TABLE or check if a specified POINT lies on your function's graph?"
                    + "\nYou can also create a MIRRORED version of your function (type X, Y or origin).");
            string action = Console.ReadLine(); 

            if (action != null)
            {

                action.ToLower();

                if (action.Contains("val") || action.Contains("table") || action.Contains("1"))
                    Console.WriteLine();
                    ValueTable(function);

                if (action.Contains("check") || action.Contains("point") || action.Contains("2"))
                    Console.WriteLine();
                    CheckPointOnGraph(function);

                if (action.Contains("x") || action.Contains("3"))
                {
                    functions.Add(Function.MirrorX(function));
                    Console.WriteLine("Your function: f(x) = " + functions[functions.Count() - 1] + "\n");
                    FunctionActionsMenu(functions[functions.Count() - 1]);
                }

                if (action.Contains("y") || action.Contains("4"))
                {
                    functions.Add(Function.MirrorY(function));
                    Console.WriteLine( "Your function: f(x) = " + functions[functions.Count() - 1] + "\n");
                    FunctionActionsMenu(functions[functions.Count() - 1]);
                }

                if (action.Contains("ori") || action.Contains("5"))
                {
                    functions.Add(Function.MirrorOrigin(function));
                    Console.WriteLine( "Your function: f(x) = " + functions[functions.Count() - 1] + "\n");
                    FunctionActionsMenu(functions[functions.Count() - 1]);
                }

                if (action.Contains("mirr"))
                {
                    Console.WriteLine( "Please enter \"x\", \"y\" or \"origin\"!\n");
                    FunctionActionsMenu(function);
                }

            }

        }





        /**
        * Displays previous functions and lets the user select one; calls the function actions menu
        */
        private void ShowAndSelectPreviousFunctions()
        {

            if (functions.Count() > 0)
            {
                //show prev functions
                string prevFunctions = "The functions you've previously entered: \n\n";

                for (int i = 0; i < functions.Count(); i++)
                {
                    if ((i + 5) <= 25)
                    {
                        prevFunctions += alphabet[i + 5] + "(x) = " + functions[i] + "\n";
                    }
                    else
                    {
                        Console.WriteLine("You have to many functions! We can't display them all! Blame lazyness!\n");
                    }
                }

                Console.WriteLine(prevFunctions);

                //select prev functions

                string selection;

                if (functions.Count() == 1)
                {
                    selection = "f";
                }
                else
                {
                    Console.WriteLine("Which function would you like to select? (enter its letter)");
                    selection = Console.ReadLine();
                }

                if (selection == null || selection == "")
                {
                    Console.WriteLine("Nothing entered, going bach to main menu!\n\n");
                    return;
                }


                if (selection[0] - 97 - 5 > functions.Count() - 1) //test if that function exists
                { 
                    Console.WriteLine("The requested function is not available!\n");
                    ShowAndSelectPreviousFunctions();
                }
                else
                {
                    FunctionActionsMenu(functions[selection[0] - 97 - 5]); //ASCII value of a = 97 ; -5 as functions start with f
                }
            }
            else
            {
                Console.WriteLine("There are no previous functions saved! Create one first!\n\n");
            }

        }



        /**
        * Displays a value table for a function
        * @param function - the function the value table should be calculated for
        */
        private void ValueTable(Function function)
        {
            Console.WriteLine("Enter START and END value for x and STEP, seperated by spaces: ");
            string parameters = Console.ReadLine();  
            string[] parameterArray = parameters.Split(' ');

            if (parameterArray.Count() < 3 || parameterArray.Count() > 3) { //check if there are 3 arguments
                Console.WriteLine("Please enter 3 arguments!");
                Console.WriteLine();
                ValueTable(function);
            } 
            else 
            {
                Console.WriteLine();
                function.Table(Double.Parse(parameterArray[0]), Double.Parse(parameterArray[1]), Double.Parse(parameterArray[2]));
            }

        }





        /**
         * Checks if a specified point lies on the graph
         * @param function - the function whose graph should be checked
         */
        private void CheckPointOnGraph(Function function)
        {

            Point p = new Point(ReadDoubleFromstringInput("x coordinate of point P: "), ReadDoubleFromstringInput("y coordinate of point P: "));

            bool onGraph = function.TestPointOnGraph(p);

            if (onGraph)
                Console.WriteLine("The point lies on your function's graph");
            else
                Console.WriteLine("The point does not lie on your function's graph");
            Console.WriteLine();
        }




        /**
         * Manually enter a function
         */
        private void TypeFunction()
        {
            Console.WriteLine("Please enter your function: f(x) = ");
            string expression = Console.ReadLine();

            functions.Add(new Function());
            functions[functions.Count() - 1].SetExpression(expression);

            FunctionActionsMenu(functions[functions.Count() - 1]);
        }



        /**
         * Create an exponential function by specifying two points
         */
        private void CreateExponentialFunction()
        {

            functions.Add(new ExponentialFunction());

            Point p = new Point(ReadDoubleFromstringInput("x coordinate of point P: "), ReadDoubleFromstringInput("y coordinate of point P: "));
            Point q = new Point(ReadDoubleFromstringInput("x coordinate of point Q: "), ReadDoubleFromstringInput("y coordinate of point Q: "));

            Console.WriteLine();

            bool success = ((ExponentialFunction)functions[functions.Count() - 1]).CreateThroughPoints(p, q);

            if (success)
            {
                Console.WriteLine("Your function: f(x) = " + functions[functions.Count() - 1] + "\n");

                FunctionActionsMenu(functions[functions.Count() - 1]);
            }
        }




        /**
         * Create an linear function by specifying two points
         */
        private void CreateLinearFunction()
        {

            functions.Add(new LinearFunction());

            Point p = new Point(ReadDoubleFromstringInput("x coordinate of point P: "), ReadDoubleFromstringInput("y coordinate of point P: "));
            Point q = new Point(ReadDoubleFromstringInput("x coordinate of point Q: "), ReadDoubleFromstringInput("y coordinate of point Q: "));

            bool success = ((LinearFunction)functions[functions.Count() - 1]).CreateThroughPoints(p, q);


            if (success)
            {
                Console.WriteLine("Your function: f(x) = " + functions[functions.Count() - 1] + "\n");

                FunctionActionsMenu(functions[functions.Count() - 1]);
            }
        }


        /**
        * A utility method that converts a string expression into a double value
        * @param displayMessage - the message that should be displayed when the user needs to enter the expression
        * @return the calculated double value
        */
        private double ReadDoubleFromstringInput(string displayMessage)
        {

            Console.WriteLine(displayMessage);
            string expression = Console.ReadLine();

            ExpressionContext context = new ExpressionContext();
            context.Imports.AddType(typeof(Math));
            IGenericExpression<double> e = context.CompileGeneric<double>(expression);
            

            return e.Evaluate();
        }

    }
}