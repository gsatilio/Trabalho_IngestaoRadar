using Controllers;

internal class Program
{
    private static void Main(string[] args)
    {
        var controller = new ReadAndPersistControllers();

        Console.WriteLine("Read and Persist");
        var succes = controller.MigrateData();

        Console.WriteLine($"{(succes ? "Operação realizada com sucesso!" : "Falha na operação...")}");
    }
}
