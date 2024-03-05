using System;

class Program
{
    static void Main()
    {
        Console.Write("ป้อนจำนวนเต็มที่ต้องการหา factorial: ");
        int number = Convert.ToInt32(Console.ReadLine());

        long factorial = CalculateFactorial(number);
        
        Console.WriteLine($"Factorial ของ {number} คือ {factorial}");
    }

    static long CalculateFactorial(int n)
    {
        if (n == 0)
            return 1;
        else
            return n * CalculateFactorial(n - 1);
    }
}
