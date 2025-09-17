using System;

class Program
{
    static void Main()
    {
        bool running = true;
        double acc = 0.0;
        double mem = 0.0;

        Console.WriteLine("Калькулятор: +, -, *, /, %, 1/x, x^2, sqrt(x), M+, M-, MR, exit");

        while (running)
        {
            Console.Write("Операция: ");
            string? opToken = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(opToken))
            {
                Console.WriteLine("Ошибка: команда не распознана.");
                continue;
            }

            opToken = opToken.Trim();

            if (opToken.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                running = false;
                break;
            }

            if (opToken.Equals("MR", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Память = {mem}");
                acc = mem;
                continue;
            }
            if (opToken.Equals("M+", StringComparison.OrdinalIgnoreCase))
            {
                mem += acc;
                Console.WriteLine($"M+: память теперь {mem}");
                continue;
            }
            if (opToken.Equals("M-", StringComparison.OrdinalIgnoreCase))
            {
                mem -= acc;
                Console.WriteLine($"M-: память теперь {mem}");
                continue;
            }

            try
            {
                double a = ReadNumber("Введите число: ");

                // Унарные операции
                if (opToken.Equals("1/x", StringComparison.OrdinalIgnoreCase))
                {
                    if (a == 0.0) throw new DivideByZeroException("Деление на ноль невозможно.");
                    acc = 1.0 / a;
                    Console.WriteLine($"Результат: {acc}");
                    continue;
                }
                if (opToken.Equals("x^2", StringComparison.OrdinalIgnoreCase))
                {
                    acc = a * a;
                    Console.WriteLine($"Результат: {acc}");
                    continue;
                }
                if (opToken.Equals("sqrt(x)", StringComparison.OrdinalIgnoreCase) ||
                    opToken.Equals("sqrt", StringComparison.OrdinalIgnoreCase))
                {
                    if (a < 0.0) throw new ArgumentOutOfRangeException(nameof(a), "Квадратный корень определён для x >= 0.");
                    acc = Math.Sqrt(a);
                    Console.WriteLine($"Результат: {acc}");
                    continue;
                }

                if (opToken is "+" or "-" or "*" or "/" or "%")
                {
                    double b = ReadNumber("Введите второе число: ");

                    acc = opToken switch
                    {
                        "+" => a + b,
                        "-" => a - b,
                        "*" => a * b,
                        "/" => b == 0.0 ? throw new DivideByZeroException("Деление на ноль невозможно.") : a / b,
                        "%" => b == 0.0 ? throw new DivideByZeroException("Остаток от деления на ноль невозможен.") : a % b,
                        _ => throw new InvalidOperationException("Неизвестная операция.")
                    };

                    Console.WriteLine($"Результат: {acc}");
                    continue;
                }

                Console.WriteLine("Неизвестная операция. Доступно: + - * / % 1/x x^2 sqrt(x) M+ M- MR exit");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }

    static double ReadNumber(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? raw = Console.ReadLine();

            if (raw == null)
            {
                Console.WriteLine("Ошибка: пустой ввод.");
                continue;
            }

            raw = raw.Trim().Replace('.', ',');

            if (double.TryParse(raw, out double value))
                return value;

            Console.WriteLine("Ошибка: введите корректное число.");
        }
    }
}