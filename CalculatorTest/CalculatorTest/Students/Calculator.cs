using CalculatorStaffSolution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorTest.Students
{
    /// <summary>
    /// This is the calculator class that you will use to complete your assignment.  Note that
    /// certain methods have already been crafted.  This provides some input on how to 
    /// proceed with this assessed component.
    /// 
    /// Good luck!
    ///  
    /// Tommy and Dave.
    /// </summary>
    public class Calculator
    {
        /**
         * Here are some basic variables to help us get running.
         */
        double accumulator;
        double previousOperand;
        char previousOperation;
        const string Error = "ERROR";

        /**
         * We have a collection of other useful boolean variables here.  You can try to
         * use these as part of your work to dicate the structure of execution.
         */
        bool errorFound;
        bool awaitingOperation;
        bool calculationComplete;

        //Is the program on its first pass through? Important to know what input we're using.
        bool firstPass = true;
        
        /// <summary>
        /// Moves through the next step of the calculator's process.
        /// </summary>
        /// <param name="operand">The number that a particular operation is committed to.</param>
        /// <param name="operation">The next operation we wish to run.</param>
        /// <returns>The current value of the calculation.</returns>
        public string Calculate(double operand, char operation)
        {
            //Assume no failure until we find one!
            errorFound = false;
            calculationComplete = false;

            //Run any pending calculations
            if (awaitingOperation)
            {
                //Run the operation we still need to run.
                RunOperation(operand, previousOperation);

                //Checking if the accumulator is overflowing.
                if (Double.MaxValue < accumulator || Double.MinValue > accumulator)
                {
                    errorFound = true;
                } 
            }

            //Store the operand and operation. We need them here in case we have a terminal
            //operation as the first one (e.g. '\' or '!').
            previousOperand = operand;
            previousOperation = operation;

            //Run any terminal operations (e.g. =) if needed.
            if (IsTerminalOperation(operation))
            {
                RunTerminalOperation(operation);

                //We won't be awaiting further computation.
                awaitingOperation = false;

                //And this calculation is done!
                calculationComplete = true;
            }
            else
            {
                //We need one more operand for this operation.
                awaitingOperation = true;
            }

            //Run either an error or the actual output.
            if (errorFound)
            {
                ResetCalculator();
                return Error;
            }
            else
            {
                if (calculationComplete)
                {
                    string result = accumulator.ToString();
                    ResetCalculator();
                    return result;
                }
                else
                {
                    return accumulator.ToString();
                }
            }
        }

        private void ResetCalculator()
        {
            //Reset all of the variables that you need for this calculator.
            accumulator = 0;
            firstPass = true;
        }

        /// <summary>
        /// Determines whether a calculator operation is terminal.
        /// </summary>
        /// <param name="operation">The operator in question.</param>
        /// <returns>True if terminal, false otherwise.</returns>
        private bool IsTerminalOperation(char operation)
        {
            if (operation == '=' || operation == '\\' || operation == '!')
            {
                return true;
            }

            return false;
        }

        private void RunTerminalOperation(char operation)
        {
            double result = 0;
            double input = SelectInput();
            int intValue;

            switch (operation)
            {
                case '=':
                    break;

                case '\\':
                    result = Root(input);
                    accumulator = result;
                    break;

                case '!':
                    if (int.TryParse(Convert.ToString(input), out intValue))
                    {
                        result = Factorial(intValue);
                        accumulator = result;
                    }
                    else
                    {
                        errorFound = true;
                    }
                    break;

                default: break;
            }
        }


        /// <summary>
        /// Figures out which is the calculation that we need to
        /// complete and then runs it.
        /// </summary>
        /// <param name="operand">The operand from the last statement.</param>
        /// <param name="operation">The operation we wish to run.</param>
        private void RunOperation(double operand, char operation)
        {
            double result = 0;

            double input = SelectInput();

            switch (operation)
            {
                case '+':
                    result = Add(input, operand);
                    accumulator = result;
                    break;

                case '-':
                    result = Subtract(input, operand);
                    accumulator = result;
                    break;

                case '*':
                    result = Multiply(input, operand);
                    accumulator = result;
                    break;

                case '/':
                    result = Divide(input, operand);
                    accumulator = result;
                    break;

                case '^':
                    result = Power(input, operand);
                    accumulator = result;
                    break;

                case '%':
                    result = Modulus(input, operand);
                    accumulator = result;
                    break;

                default: break;
            }
        }

        /// <summary>
        /// Decides which input is the one needed for this 
        /// calculation?
        /// </summary>
        /// <returns>The input we need for this calculation.</returns>
        private double SelectInput()
        {
            double input;

            //In order to do calculations with more
            //than 3 operands we need the accumulator as input.
            if (firstPass)
            {
                input = previousOperand;
                firstPass = false;
            }
            else
            {
                input = accumulator;
            }

            return input;
        }

        /// <summary>
        /// Calculates the addition of two numbers.
        /// </summary>
        /// <param name="number1">The first number in the operation.</param>
        /// <param name="number2">The second number in the operation.</param>
        /// <returns>The result of the addition, rounded to two decimal places.</returns>
        private double Add(double number1, double number2)
        {
            double result = number1 + number2;
            result = Math.Round(result, 2);
            return result;
        }

        private double Subtract(double number1, double number2)
        {
            double result = number1 - number2;
            result = Math.Round(result, 2);
            return result;
        }

        private double Multiply(double number1, double number2)
        {
            double result = number1 * number2;
            result = Math.Round(result, 2);
            return result;
        } 

        private double Divide(double number1, double number2)
        {
            if (number2 == 0)
            {
                errorFound = true;
                return 0;
            }

            double result = number1 / number2;
            result = Math.Round(result, 2);
            return result;
        }

        private double Power(double number1, double number2)
        {
            double result = Math.Pow(number1, number2);
            result = Math.Round(result, 2);
            return result;
        }

        private double Modulus(double number1, double number2)
        {
            //Handling the case in which NaN is returned by '%'.
            if (number2 == 0)
            {
                errorFound = true;
                return 0;
            } 

            double result = number1 % number2;
            result = Math.Round(result, 2);
            return result;
        }
        
        private double Root(double number)
        {
            if (number < 0)
            {
                errorFound = true;
                return 0;
            }

            double result = Math.Sqrt(number);
            result = Math.Round(result, 2);
            return result;
        }
        
        private int Factorial(int number)
        {
            int result = 1;

            //12! is the highest value the int type can handle.
            //Anything over 12! won't be correctly represented, so it returns an error.
            //This statement also handles negative factorials.
            if (number < 0 || number > 12)
            {
                errorFound = true;
                return 0;
            }
            
            //Calculating the factorial itself here.
            for (int i = 2; i <= number; i++)
            {
                result *= i;
            }

            return result;
        }
    }
}
