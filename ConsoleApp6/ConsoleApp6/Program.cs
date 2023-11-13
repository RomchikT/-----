using System;
using System.Diagnostics;
using System.Threading;

class TypingTest
{
    static void Main(string[] args)
    {
        string text = "привет";
        Console.WriteLine("Введите следующий текст: ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(text);
        Console.ResetColor();
        Console.SetCursorPosition(0, 1);

        int errors = 0;
        Stopwatch stopwatch = new Stopwatch();
        bool таймер_завершен = false;

        Thread таймер_поток = new Thread(() =>
        {
            stopwatch.Start();
            Thread.Sleep(5000); // Задержка в 5 секунд
            таймер_завершен = true;
        });

        таймер_поток.Start();

        for (int i = 0; i < text.Length && !таймер_завершен; i++)
        {
            char userInput = Console.ReadKey(true).KeyChar;

            if (text[i] != userInput)
            {
                i--;
                errors++;
            }
            else
            {
                Console.Write(text[i]);
                Console.ResetColor();
            }
        }

        stopwatch.Stop();
        double accuracy = ((double)(text.Length - errors) / text.Length) * 100;
        Console.WriteLine($"Точность: {accuracy}%");
        Console.WriteLine($"Количество ошибок: {errors}");
        Console.WriteLine($"Время: {stopwatch.Elapsed.TotalSeconds} сек.");

        if (таймер_завершен)
        {
            Console.WriteLine("Время скоропечатания истекло!");
        }
    }
}