using System.Diagnostics;
using System.Runtime.InteropServices;
string archivePath = "secret_file.7z";
string outputPath = "выходные файлы";
string letters = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm";
Directory.CreateDirectory(outputPath);
string sevenZipPath = @"C:\Program Files (x86)\7-Zip\7z.exe";
for(int password = 0; password <= 9999; password++)
{
    string pass = password.ToString("D4");
    ProcessStartInfo psi = new ProcessStartInfo
    {
        FileName = sevenZipPath,
        Arguments = $"x \"{archivePath}\" -o\"{outputPath}\" -p{pass} -y",
        RedirectStandardError = true,
        RedirectStandardOutput = true,
    };
    using(Process? process = Process.Start(psi))
    {
        process.WaitForExit();
        string output = process.StandardOutput.ReadToEnd();
        if(output.Contains("Everything is Ok"))
        {
            Console.WriteLine($"Подходит пароль: {pass}, файл извлечён в папку '{outputPath}'");
            return;
        }
        else
        {
            Console.WriteLine($"Пароль {pass} не подошёл");
        }
    }
}
Console.WriteLine("Не удалось подобрать пароль");