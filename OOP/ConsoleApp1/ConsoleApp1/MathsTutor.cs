using System;
using System.IO;

namespace MathsTutor
{
    public class MathsTutor
    {
        public static void Main(string[] args)
        {
            int problemsSolved = 0;
            int correctAnswers = 0;
            int incorrectAnswers = 0;

            Pack pack = new Pack();
            pack.Shuffle();

            Console.WriteLine("Welcome to MathsTutor");
            Console.WriteLine("Press Enter to go to the MathsTutor Menu...");
            Console.ReadLine();

            string choice = "";

            while (choice != "4")
            {
                Console.Clear();
                Console.WriteLine("MathsTutor Menu");
                Console.WriteLine("1. Instructions");
                Console.WriteLine("2. Deal 3 cards");
                Console.WriteLine("3. Deal 5 cards");
                Console.WriteLine("4. Quit");
                Console.Write("Enter your choice: ");
                choice = Console.ReadLine();

                if (choice == "1")
                {
                    DisplayInstructions();
                }
                else if (choice == "2")
                {
                    (problemsSolved, correctAnswers, incorrectAnswers) = DealThreeCards(pack, problemsSolved, correctAnswers, incorrectAnswers);
                }
                else if (choice == "3")
                {
                    (problemsSolved, correctAnswers, incorrectAnswers) = DealFiveCards(pack, problemsSolved, correctAnswers, incorrectAnswers);
                }
                else if (choice == "4")
                {
                    // Exit the loop and quit the application
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }

                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }

            SaveStatistics(problemsSolved, correctAnswers, incorrectAnswers);
            Console.WriteLine("Goodbye!");
        }

        private static void DisplayInstructions()
        {
            Console.Clear();
            Console.WriteLine("Instructions:");
            Console.WriteLine("1. Choose either 'Deal 3 cards' or 'Deal 5 cards' from the menu.");
            Console.WriteLine("2. The program will generate a math problem based on the cards dealt.");
            Console.WriteLine("3. Enter your answer for the problem.");
            Console.WriteLine("4. The program will tell you if your answer is correct or not.");
            Console.WriteLine("5. Choose another option or quit the application.");
        }

        private static (int, int, int) DealThreeCards(Pack pack, int problemsSolved, int correctAnswers, int incorrectAnswers)
        {
            Card card1 = pack.Deal();
            Card card2 = pack.Deal();
            Card card3 = pack.Deal();

            int num1 = card1.Value;
            int num2 = card3.Value;
            char operation = card2.Operator;
            int result = 0;

            switch (operation)
            {
                case '+':
                    result = num1 + num2;
                    break;
                case '-':
                    result = num1 - num2;
                    break;
                case '*':
                    result = num1 * num2;
                    break;
                case '/':
                    if (num2 == 0)
                    {
                        Console.WriteLine("Cannot divide by zero. Dealing new cards.");
                        return (problemsSolved, correctAnswers, incorrectAnswers);
                    }
                    result = num1 / num2;
                    break;
            }

            Console.Clear();
            Console.WriteLine($"{num1} {operation} {num2} = ?");
            Console.Write("Enter your answer: ");
            int userAnswer = Convert.ToInt32(Console.ReadLine());

            problemsSolved++;

            if (userAnswer == result)
            {
                Console.WriteLine("Correct!");
                correctAnswers++;
            }
            else
            {
                Console.WriteLine($"Not correct. The correct answer is {result}.");
                incorrectAnswers++;
            }

            return (problemsSolved, correctAnswers, incorrectAnswers);

        }
        private static (int, int, int) DealFiveCards(Pack pack, int problemsSolved, int correctAnswers, int incorrectAnswers)
        {
            Card card1 = pack.Deal();
            Card card2 = pack.Deal();
            Card card3 = pack.Deal();
            Card card4 = pack.Deal();
            Card card5 = pack.Deal();

            int num1 = card1.Value;
            int num2 = card3.Value;
            int num3 = card5.Value;
            char operation1 = card2.Operator;
            char operation2 = card4.Operator;

            int intermediateResult = 0;
            int finalResult = 0;

            if (operation1 == '*' || operation1 == '/')
            {
                if (operation1 == '*')
                {
                    intermediateResult = num1 * num2;
                }
                else
                {
                    if (num2 == 0)
                    {
                        Console.WriteLine("Cannot divide by zero. Dealing new cards.");
                        return (problemsSolved, correctAnswers, incorrectAnswers);
                    }
                    intermediateResult = num1 / num2;
                }

                if (operation2 == '+')
                {
                    finalResult = intermediateResult + num3;
                }
                else
                {
                    finalResult = intermediateResult - num3;
                }
            }
            else
            {
                if (operation2 == '*' || operation2 == '/')
                {
                    if (operation2 == '*')
                    {
                        intermediateResult = num2 * num3;
                    }
                    else
                    {
                        if (num3 == 0)
                        {
                            Console.WriteLine("Cannot divide by zero. Dealing new cards.");
                            return (problemsSolved, correctAnswers, incorrectAnswers);
                        }
                        intermediateResult = num2 / num3;
                    }

                    if (operation1 == '+')
                    {
                        finalResult = num1 + intermediateResult;
                    }
                    else
                    {
                        finalResult = num1 - intermediateResult;
                    }
                }
                else
                {
                    if (operation1 == '+')
                    {
                        intermediateResult = num1 + num2;
                    }
                    else
                    {
                        intermediateResult = num1 - num2;
                    }

                    if (operation2 == '+')
                    {
                        finalResult = intermediateResult + num3;
                    }
                    else
                    {
                        finalResult = intermediateResult - num3;
                    }
                }
            }

            Console.Clear();
            Console.WriteLine($"{num1} {operation1} {num2} {operation2} {num3} = ?");
            Console.Write("Enter your answer: ");
            int userAnswer = Convert.ToInt32(Console.ReadLine());

            problemsSolved++;

            if (userAnswer == finalResult)
            {
                Console.WriteLine("Correct!");
                correctAnswers++;
            }
            else
            {
                Console.WriteLine($"Not correct. The correct answer is {finalResult}.");
                incorrectAnswers++;
            }

            return (problemsSolved, correctAnswers, incorrectAnswers);
        }

        private static void SaveStatistics(int problemsSolved, int correctAnswers, int incorrectAnswers)
        {
            string path = "MathsTutorStatistics.txt";
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine($"Problems solved: {problemsSolved}");
                sw.WriteLine($"Correct answers: {correctAnswers}");
                sw.WriteLine($"Incorrect answers: {incorrectAnswers}");
                sw.WriteLine("----------------------------");
            }
        }
    }
}

