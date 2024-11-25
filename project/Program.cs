using System;
using System.Collections.Generic;
using System.IO;

static class AdvancedCalculator
{
    public static void Run()
    {
        Dictionary<int, string> operationHistory = new Dictionary<int, string>();
        int operationCount = 0;

        Console.WriteLine("\n== Advanced Calculator ==");

        while (true)
        {
            Console.Write("Enter the first number: ");
            if (!double.TryParse(Console.ReadLine(), out double operandA))
            {
                Console.WriteLine("Invalid input.");
                continue;
            }

            Console.Write("Enter the second number: ");
            if (!double.TryParse(Console.ReadLine(), out double operandB))
            {
                Console.WriteLine("Invalid input.");
                continue;
            }

            Console.WriteLine("Choose an operation (+, -, *, /, %, ^): ");
            string operation = Console.ReadLine();

            double result = 0;
            bool validOperation = true;

            switch (operation)
            {
                case "+":
                    result = operandA + operandB;
                    break;
                case "-":
                    result = operandA - operandB;
                    break;
                case "*":
                    result = operandA * operandB;
                    break;
                case "/":
                    if (operandB == 0)
                    {
                        Console.WriteLine("Division by zero is not allowed.");
                        validOperation = false;
                    }
                    else
                    {
                        result = operandA / operandB;
                    }
                    break;
                case "%":
                    result = operandA % operandB;
                    break;
                case "^":
                    result = Math.Pow(operandA, operandB);
                    break;
                default:
                    Console.WriteLine("Invalid operation.");
                    validOperation = false;
                    break;
            }

            if (validOperation)
            {
                Console.WriteLine($"Result: {result}");
                operationCount++;
                operationHistory[operationCount] = $"{operandA} {operation} {operandB} = {result}";
                LogOperation(operationCount, operandA, operandB, operation, result);
            }

            Console.WriteLine("Do you want to perform another operation? (yes/no): ");
            if (Console.ReadLine()?.ToLower() != "yes")
                break;
        }

        Console.WriteLine("\nOperation History:");
        foreach (var entry in operationHistory)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}");
        }
    }

    private static void LogOperation(int operationId, double a, double b, string op, double res)
    {
        string logEntry = $"{operationId}. {a} {op} {b} = {res}";
        File.AppendAllText("advanced_calculator_log.txt", logEntry + Environment.NewLine);
    }
}

static class EnhancedGuessTheNumber
{
    public static void Run()
    {
        Console.WriteLine("\n== Play 'Guess the Number' ==");

        Console.WriteLine("Do you want to set the range? (yes/no): ");
        string response = Console.ReadLine()?.ToLower();

        int minRange = 1, maxRange = 100;
        if (response == "yes")
        {
            Console.Write("Enter the minimum value of the range: ");
            minRange = int.Parse(Console.ReadLine());
            Console.Write("Enter the maximum value of the range: ");
            maxRange = int.Parse(Console.ReadLine());
        }

        while (true)
        {
            PlayGame(minRange, maxRange);
            Console.WriteLine("Do you want to play again? (yes/no): ");
            if (Console.ReadLine()?.ToLower() != "yes")
                break;
        }
    }

    private static void PlayGame(int min, int max)
    {
        Random rnd = new Random();
        int randomNumber = rnd.Next(min, max + 1);
        int guessCount = 0;

        Console.WriteLine($"Enter a number between {min} and {max}.");

        while (true)
        {
            guessCount++;
            Console.Write("Enter your guess: ");
            int playerGuess = int.Parse(Console.ReadLine());

            if (playerGuess > randomNumber)
                Console.WriteLine("The number is lower.");
            else if (playerGuess < randomNumber)
                Console.WriteLine("The number is higher.");
            else
            {
                Console.WriteLine($"Congratulations! You guessed it in {guessCount} attempts.");
                break;
            }
        }
    }
}

static class CategoryHangman
{
    public static void Run()
    {
        var categories = new Dictionary<string, string[]>
        {
            { "instrument", new[] { "Guitar", "Piano", "Violin", "Drum" } },
            { "car", new[] { "Toyota", "Nissan", "Bmw", "Audi" } }
        };

        Console.WriteLine("\n== Play Hangman ==");
        Console.WriteLine("Choose a category: instrument or car");
        string category = Console.ReadLine()?.ToLower();

        // Validate category
        if (!categories.ContainsKey(category))
        {
            Console.WriteLine("Invalid category.");
            return;
        }

        var wordList = categories[category];
        Random rnd = new Random();
        string secretWord = wordList[rnd.Next(wordList.Length)];

        char[] hiddenWord = new string('_', secretWord.Length).ToCharArray();
        List<char> missedLetters = new List<char>();
        int attemptsLeft = 6;

        Console.WriteLine($"The word has {secretWord.Length} letters.");

        while (attemptsLeft > 0)
        {
            Console.WriteLine($"\n[{new string(hiddenWord)}]");
            Console.WriteLine($"Missed letters: {string.Join(", ", missedLetters)}");
            Console.WriteLine($"Attempts left: {attemptsLeft}");
            Console.Write("Enter a letter: ");
            char guess = Console.ReadLine()[0];

            if (secretWord.Contains(guess))
            {
                for (int i = 0; i < secretWord.Length; i++)
                    if (secretWord[i] == guess)
                        hiddenWord[i] = guess;
            }
            else
            {
                if (!missedLetters.Contains(guess))
                    missedLetters.Add(guess);
                attemptsLeft--;
            }

            if (new string(hiddenWord) == secretWord)
            {
                Console.WriteLine($"Congratulations! You guessed the word: {secretWord}");
                return;
            }
        }

        Console.WriteLine($"You lost. The word was: {secretWord}");
    }
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\n== Main Menu ==");
            Console.WriteLine("1. Advanced Calculator");
            Console.WriteLine("2. Play 'Guess the Number'");
            Console.WriteLine("3. Play Hangman");
            Console.WriteLine("0. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AdvancedCalculator.Run();
                    break;
                case "2":
                    EnhancedGuessTheNumber.Run();
                    break;
                case "3":
                    CategoryHangman.Run();
                    break;
                case "0":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        }
    }
}
