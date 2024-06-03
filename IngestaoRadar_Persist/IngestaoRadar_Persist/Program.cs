using System.Text.Json;
using Models;
using Newtonsoft.Json;
using Models;
using System.Net;
using Controllers;
using System.Reflection;

internal class Program
{
    private static void Main(string[] args)
    {
        var controller = new RadarController();
        var url = "https://dados.antt.gov.br/dataset/79d287f4-f5ca-4385-a17c-f61f53831f17/resource/fa861690-70de-4a27-a82f-0eee74abdbc0/download/dados_dos_radares.json";

        string path = @"C:\Radares\dados_dos_radares.json";
        var watch = new System.Diagnostics.Stopwatch();

        int option = 0;
        do
        {
            Console.Clear();
            Console.WriteLine("Persist:\n");
            Console.WriteLine("Selecione uma opção:");
            Console.WriteLine("[ 1 ] Carregar arquivo via API");
            Console.WriteLine("[ 2 ] Carregar arquivo local");
            Console.WriteLine("[ 0 ] Sair");
            option = int.Parse(Console.ReadLine());

            int mt = -1;
            do
            {
                Console.WriteLine("Deseja usar Multi Thread? 0 - Nao e 1 - Sim");
                mt = int.Parse(Console.ReadLine());
            } while (mt != 0 && mt != 1);


            switch (option)
            {
                case 1:
                    watch.Start();
                    var apiResponse = controller.SaveRadarDataFromApi(url, mt);
                    watch.Stop();
                    Console.WriteLine($"{(apiResponse ? "Operação realizada com sucesso! " + watch.ElapsedMilliseconds + "ms" : "Erro no processamento...")}");
                    watch.Reset();
                    break;
                case 2:
                    watch.Start();
                    var fileResponse = controller.SaveRadarDataFromFile(path, mt);
                    watch.Stop();
                    Console.WriteLine($"{(fileResponse ? "Operação realizada com sucesso! " + watch.ElapsedMilliseconds + "ms" : "Erro no processamento...")}");
                    watch.Reset();
                    break;
                case 0:
                    Console.WriteLine("Saindo...");
                    break;
                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }
            Console.ReadKey();
        } while (option > 0);
    }
}