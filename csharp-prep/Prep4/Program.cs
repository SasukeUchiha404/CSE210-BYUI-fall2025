using System;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        // Collect numbers until 0 is entered
        int number;
        do
        {
            Console.Write("Enter number: ");
            number = Convert.ToInt32(Console.ReadLine());
            if (number != 0)
            {
                numbers.Add(number);
            }
        } while (number != 0);

        // Core Requirements
        int sum = 0;
        foreach (int n in numbers)
        {
            sum += n;
        }

        double average = (numbers.Count > 0) ? (double)sum / numbers.Count : 0;

        int max = int.MinValue;
        foreach (int n in numbers)
        {
            if (n > max) max = n;
        }

        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {max}");

        // Stretch Challenge: Smallest positive number
        int minPositive = int.MaxValue;
        foreach (int n in numbers)
        {
            if (n > 0 && n < minPositive)
            {
                minPositive = n;
            }
        }
        if (minPositive != int.MaxValue)
            Console.WriteLine($"The smallest positive number is: {minPositive}");

        // Stretch Challenge: Sorted list
        numbers.Sort();
        Console.WriteLine("The sorted list is:");
        foreach (int n in numbers)
        {
            Console.WriteLine(n);
        }
    }
}
