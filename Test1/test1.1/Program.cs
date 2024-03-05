using System;

class Program
{
    static void Main()
    {
        Console.Write("input factorial: ");
        int number = Convert.ToInt32(Console.ReadLine());

        long factorial = CalculateFactorial(number);
        
        Console.WriteLine($"{number}! = {factorial}");
    }

    static long CalculateFactorial(int N)
    {
        if (N == 0)
            return 1;
        else
            return N * CalculateFactorial(N - 1);
    }
}
