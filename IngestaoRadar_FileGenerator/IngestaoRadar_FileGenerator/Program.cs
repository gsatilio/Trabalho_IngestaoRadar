using Controllers;
using Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Xml.Linq;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("File Generator: ");
        var path = @"C:\Radares\";
        var controller = new FileGeneratorControllers();
        
        Console.WriteLine($"\nCaminho de destino: {path}\n");

        bool result;
        var watch = System.Diagnostics.Stopwatch.StartNew();

        // SQL
        Console.WriteLine("Gerando JSON com registros do SQL...");
        result = controller.WriteJsonFromSql(path);
        Console.WriteLine($"{(result ? "SQLFile.json gerado com sucesso!" : "Erro ao gerar arquivo JSON")}\n");

        Console.WriteLine("Gerando XML com registros do SQL...");
        result = controller.WriteXmlFromSql(path);
        Console.WriteLine($"{(result ? "SQLFile.xml gerado com sucesso!" : "Erro ao gerar arquivo XML")}\n");

        Console.WriteLine("Gerando CSV com registros do SQL...");
        result = controller.WriteCsvFromSql(path);
        Console.WriteLine($"{(result ? "SQLFile.csv gerado com sucesso!" : "Erro ao gerar arquivo CSV")}\n");

        // MongoDB
        Console.WriteLine("Gerando JSON com registros do MongoDB...");
        result = controller.WriteJsonFromMongo(path);
        Console.WriteLine($"{(result ? "MongoFile.json gerado com sucesso!" : "Erro ao gerar arquivo JSON")}\n");

        Console.WriteLine("Gerando XML com registros do MongoDB...");
        result = controller.WriteXmlFromMongo(path);
        Console.WriteLine($"{(result ? "MongoFile.xml gerado com sucesso!" : "Erro ao gerar arquivo XML")}\n");

        Console.WriteLine("Gerando CSV com registros do MongoDB...");
        result = controller.WriteCsvFromMongo(path);
        Console.WriteLine($"{(result ? "MongoFile.csv gerado com sucesso!" : "Erro ao gerar arquivo CSV")}\n");

        watch.Stop();
        Console.WriteLine($"Escrita dos arquivos finalizada em: {watch.ElapsedMilliseconds} ms.");
    }
}