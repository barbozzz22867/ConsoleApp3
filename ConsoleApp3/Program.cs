using System;
using System.Diagnostics;
using System.ComponentModel;

public class Program
{
    public static async Task Main()
    {
        ProcessStartInfo startInfo = new()
        {
            FileName = "dotnet",
            UseShellExecute = false,
            CreateNoWindow = true,

            // TODO 1. Перенаправление stdout
            RedirectStandardOutput = true,
            // TODO 2. Перенаправление stderr
            RedirectStandardError = true
        };

        startInfo.ArgumentList.Add("--info");

        using Process process = new()
        {
            StartInfo = startInfo
        };

        Stopwatch stopwatch = Stopwatch.StartNew();

        try
        {
            // TODO 3. Запуск процесса
            process.Start();

            Console.WriteLine($"PID: {process.Id}");

            // TODO 4. Асинхронное чтение stdout
            Task<string> stdoutTask = process.StandardOutput.ReadToEndAsync();

            // TODO 5. Асинхронное чтение stderr
            // Важно: оба чтения стартуют ДО ожидания завершения процесса
            Task<string> stderrTask = process.StandardError.ReadToEndAsync();

            // TODO 6. Ожидание завершения процесса
            await process.WaitForExitAsync();

            // TODO 7. Получение результатов
            string stdout = await stdoutTask;
            string stderr = await stderrTask;

            // TODO 8. Остановка секундомера
            stopwatch.Stop();

            // TODO 9. Длительность и ExitCode
            Console.WriteLine($"Длительность: {stopwatch.Elapsed}");
            Console.WriteLine($"Exit code: {process.ExitCode}");

            // TODO 10. Вывод stdout и stderr отдельно
            Console.WriteLine("=== STDOUT ===");
            Console.WriteLine(stdout);

            Console.WriteLine("=== STDERR ===");
            Console.WriteLine(stderr);
        }
        catch (Win32Exception ex)
        {
            stopwatch.Stop();
            Console.WriteLine($"Ошибка запуска: {ex.Message}");
        }
    }
}