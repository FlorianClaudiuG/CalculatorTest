using CalculatorStaffSolution;
using CalculatorTest.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorTestFinal
{
    class FinalCalculatorTester
    {
        Random random;

        CalculatorFinal staffCalculator;
        Calculator studentCalculator;

        /// <summary>
        /// Standard constructor.
        /// Prepares the two calculator instances.
        /// </summary>
        public FinalCalculatorTester()
        {
            staffCalculator = new CalculatorFinal();
            studentCalculator = new Calculator();
            random = new Random();
        }

        public double RunBasicTests()
        {
            double total = 0;

            double addTest = RunBasicTest('+');
            Console.WriteLine("Addition Test: "+addTest*100+ "%");

            double subtractTest = RunBasicTest('-');
            Console.WriteLine("Subtraction Test: "+subtractTest*100+ "%");

            double multiplicationTest = RunBasicTest('*');
            Console.WriteLine("Multiplication Test: "+ multiplicationTest*100 + "%");

            double divisionTest = RunBasicTest('/');
            Console.WriteLine("Division Test: "+ divisionTest*100 + "%");

            total = (addTest + subtractTest + multiplicationTest + divisionTest);
            total /= 4;
            return total;
        }

        public double RunAdvancedTests()
        {
            double total = 0;
            double facTest = RunAdvancedTest('!');
            Console.WriteLine("Factorial Test: " + facTest * 100 + "%");

            double powTest = RunAdvancedTest('^');
            Console.WriteLine("Power Test: " + powTest * 100 + "%");

            double rootTest = RunAdvancedTest('\\');
            Console.WriteLine("Root Test: " + rootTest * 100 + "%");

            double modTest = RunAdvancedTest('%');
            Console.WriteLine("Modulus Test: " + modTest * 100 + "%");

            total = (facTest + powTest + rootTest + modTest);
            total /= 4;
            return total;
        }

        public double RunComboTests()
        {
            double successRate = 0;

            char[] symbols = {'+','-','*','/'};

            //COMBO TESTS
            for (int i = 0; i < CalculatorFinal.ComboTestCount1; i++)
            {
                //Generates two random numbers for this operation.
                int number1 = random.Next(int.MaxValue);
                int number2 = random.Next(int.MaxValue);
                int number3 = random.Next(int.MaxValue);

                char operator1 = symbols[random.Next(symbols.Length)];
                char operator2 = symbols[random.Next(symbols.Length)];

                //Run the first test against the student solution.
                string result1 = string.Empty;
                result1 = StudentCalc(operator1, operator2, number1, number2,number3);

                //Run the second test against a staff solution.
                string result2 = string.Empty;
                staffCalculator.Calculate(number1, operator1);
                staffCalculator.Calculate(number2, operator2);
                result2 = staffCalculator.Calculate(number3, '=');

                //Naturally, if your answer is correct, this will increase.
                if (result1.Equals(result2))
                {
                    successRate++;
                }
            }
            
            return ((successRate / CalculatorFinal.ComboTestCount1));
        }

        public double RunOverflowTests()
        {
            double successRate = 0.0;
            double tempTotal = 0;

            int testTypes = 5;

            //PLUS TESTS
            for (int i = 0; i < (CalculatorFinal.OverflowTestCount1/testTypes); i++)
            {
                //Generates two random numbers for this operation.
                double number1 = random.NextDouble()*double.MaxValue;
                double number2 = double.MaxValue;
                
                //Run the first test against the student solution.
                string result1 = string.Empty;
                result1 = StudentCalc('+', number1, number2);

                //Run the second test against a staff solution.
                string result2 = string.Empty;
                staffCalculator.Calculate(number1, '+');
                result2 = checked(staffCalculator.Calculate(number2, '='));

                //Naturally, if your answer is correct, this will increase.
                if (result1.Equals(result2))
                {
                    successRate++;
                    tempTotal++;
                }
            }

            Console.WriteLine("Addition Overflow: " + (tempTotal/ (CalculatorFinal.OverflowTestCount1 / testTypes)) * 100 + "%");
            tempTotal = 0;

            //MINUS TESTS
            for (int i = 0; i < (CalculatorFinal.OverflowTestCount1 / testTypes); i++)
            {
                //Generates two random numbers for this operation.
                double number1 = double.MinValue;
                double number2 = random.NextDouble() * double.MaxValue;

                //Run the first test against the student solution.
                string result1 = string.Empty;
                result1 = StudentCalc('-', number1, number2);

                //Run the second test against a staff solution.
                string result2 = string.Empty;
                staffCalculator.Calculate(number1, '-');
                result2 = staffCalculator.Calculate(number2, '=');

                //Naturally, if your answer is correct, this will increase.
                if (result1.Equals(result2))
                {
                    successRate++;
                    tempTotal++;
                }
            }

            Console.WriteLine("Subtraction Overflow: " + (tempTotal / (CalculatorFinal.OverflowTestCount1 / testTypes)) * 100 + "%");
            tempTotal = 0;

            //DIVISION TESTS
            for (int i = 0; i < (CalculatorFinal.OverflowTestCount1 / testTypes); i++)
            {
                //Generates two random numbers for this operation.
                int number1 = random.Next(int.MaxValue);
                int number2 = 0;

                //Run the first test against the student solution.
                string result1 = string.Empty;
                result1 = StudentCalc('/', number1, number2);

                //Run the second test against a staff solution.
                string result2 = string.Empty;
                staffCalculator.Calculate(number1, '/');
                result2 = staffCalculator.Calculate(number2, '=');

                //Naturally, if your answer is correct, this will increase.
                if (result1.Equals(result2))
                {
                    successRate++;
                    tempTotal++;
                }
            }

            Console.WriteLine("Division Overflow: " + (tempTotal / (CalculatorFinal.OverflowTestCount1 / testTypes)) * 100 + "%");
            tempTotal = 0;

            //FACTORIAL TESTS
            for (int i = 0; i < (CalculatorFinal.OverflowTestCount1 / testTypes); i++)
            {
                //Generates two random numbers for this operation.
                int number1 = random.Next(13, int.MaxValue);

                //Run the first test against the student solution.
                string result1 = string.Empty;
                result1 = StudentCalc('!', number1);

                //Run the second test against a staff solution.
                string result2 = string.Empty;
                result2 = staffCalculator.Calculate(number1, '!');
                
                //Naturally, if your answer is correct, this will increase.
                if (result1.Equals(result2))
                {
                    successRate++;
                    tempTotal++;
                }
            }

            Console.WriteLine("Factorial Overflow: " + (tempTotal / (CalculatorFinal.OverflowTestCount1 / testTypes)) * 100 + "%");
            tempTotal = 0;

            //POW TESTS
            for (int i = 0; i < (CalculatorFinal.OverflowTestCount1 / testTypes); i++)
            {
                //Generates two random numbers for this operation.
                double number1 = random.NextDouble() * double.MaxValue;
                double number2 = double.MaxValue;

                //Run the first test against the student solution.
                string result1 = string.Empty;
                result1 = StudentCalc('^', number1, number2);

                //Run the second test against a staff solution.
                string result2 = string.Empty;
                staffCalculator.Calculate(number1, '^');
                result2 = staffCalculator.Calculate(number2, '=');

                //Naturally, if your answer is correct, this will increase.
                if (result1.Equals(result2))
                {
                    successRate++;
                    tempTotal++;
                }
            }

            Console.WriteLine("Power Overflow: " + (tempTotal / (CalculatorFinal.OverflowTestCount1 / testTypes)) * 100 + "%");
            tempTotal = 0;

            return ((successRate / CalculatorFinal.OverflowTestCount1));
        }

        /// <summary>
        /// Given a particular operation, this will run a simple
        /// calculation using the students own calculator and compare
        /// the result against a pre-written solution.
        /// </summary>
        /// <param name="operation">The operation you wish to run.</param>
        /// <returns>The success rate for this type of operation as a percentage.</returns>
        public double RunBasicTest(char operation)
        {
            double successRate = 0.0;

            for (int i = 0; i < CalculatorFinal.BasicTestCount1; i++)
            {
                //Generates two random numbers for this operation.
                int number1 = random.Next(int.MaxValue);
                int number2 = random.Next(int.MaxValue);

                //Run the first test against the student solution.
                string result1 = string.Empty;
                result1 = StudentCalc(operation, number1, number2);

                //Run the second test against a staff solution.
                string result2 = string.Empty;
                staffCalculator.Calculate(number1, operation);
                result2 = staffCalculator.Calculate(number2, '=');

                //Naturally, if your answer is correct, this will increase.
                if (result1.Equals(result2))
                {
                    successRate++;
                }
            }

            return ((successRate/CalculatorFinal.BasicTestCount1));
        }

        private double RunAdvancedTest(char operation)
        {
            double successRate = 0.0;

            int number1, number2;
            string result1, result2;

            for (int i = 0; i < CalculatorFinal.AdvancedTestCount1; i++)
            {
                result1 = string.Empty;
                result2 = string.Empty;

                if (operation == '!')
                {
                    //We only need to generate the one number.
                    number1 = random.Next(0, 13);
                    result1 = StudentCalc(operation, number1);

                    //Run the second test against a staff solution.
                    result2 = staffCalculator.Calculate(number1, operation);
                }
                else if(operation == '\\')
                {
                    //We only need to generate the one number.
                    number1 = random.Next(int.MaxValue);

                    //Run the first test against the student solution.
                    result1 = StudentCalc(operation, number1);

                    //Run the second test against a staff solution.
                    result2 = staffCalculator.Calculate(number1, operation);
                }
                else
                {
                    //We need two numbers for these operations.
                    number1 = random.Next(250);
                    number2 = random.Next(1,250);

                    //Run the first test against the student solution.
                    result1 = StudentCalc(operation, number1, number2);

                    //Run the second test against a staff solution.
                    staffCalculator.Calculate(number1, operation);
                    result2 = staffCalculator.Calculate(number2, '=');
                }

                //Naturally, if your answer is correct, this will increase.
                if (result1.Equals(result2))
                {
                    successRate++;
                }
            }

            return ((successRate / CalculatorFinal.AdvancedTestCount1));
        }

        private string StudentCalc(char operation, double number1, double number2)
        {
            try
            {
                string result1;
                studentCalculator.Calculate(number1, operation);
                result1 = studentCalculator.Calculate(number2, '=');
                return result1;
            }
            catch (Exception)
            {
                return "CRASH";
            }
        }

        private string StudentCalc(char operator1, char operator2, double number1, double number2, double number3)
        {
            try
            {
                string result = string.Empty;
                studentCalculator.Calculate(number1, operator1);
                studentCalculator.Calculate(number2, operator2);
                result = studentCalculator.Calculate(number3, '=');
                return result;
            }
            catch(Exception)
            {
                return "CRASH";
            }
        }

        private string StudentCalc(char operation, int number1)
        {
            try
            {
                //Run the first test against the student solution.
                return studentCalculator.Calculate(number1, operation);
            }
            catch (Exception)
            {
                return "CRASH";
            }
        }
    }
}
