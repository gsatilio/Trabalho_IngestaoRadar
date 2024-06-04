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
            Console.WriteLine("Persist:\n");
            Console.WriteLine("Selecione uma opção:");
            Console.WriteLine("[ 1 ] Carregar arquivo via API");
            Console.WriteLine("[ 2 ] Carregar arquivo local");
            Console.WriteLine("[ 3 ] Limpar console");
            Console.WriteLine("[ 0 ] Sair");
            option = int.Parse(Console.ReadLine());

            int mt = -1;
            int bt = -1;
            if (option == 1 || option == 2)
            {
                do
                {
                    Console.WriteLine("Deseja inserir por batch de 100?\n0 - Nao, quero um batch só\n1 - Sim, quero por batch de 100");
                    bt = int.Parse(Console.ReadLine());
                } while (bt != 0 && bt != 1);
                // Se nao insere por batch, automaticamente MT = 0 pois é um insert só em uma thread
                if (bt == 0)
                {
                    mt = 0;
                }
                else // Senao, dá a opção de escolher se quer MT
                {
                    do
                    {
                        Console.WriteLine("Deseja usar Multi Thread?\n0 - Nao\n1 - Sim");
                        mt = int.Parse(Console.ReadLine());
                    } while (mt != 0 && mt != 1);
                }
            }

            switch (option)
            {
                case 1:
                    watch.Start();
                    var apiResponse = controller.SaveRadarDataFromApi(url, mt, bt);
                    watch.Stop();
                    Console.WriteLine($"{(apiResponse ? "Operação realizada com sucesso! " + watch.ElapsedMilliseconds + "ms" : "Erro no processamento...")}");
                    watch.Reset();
                    break;
                case 2:
                    watch.Start();
                    var fileResponse = controller.SaveRadarDataFromFile(path, mt, bt);
                    watch.Stop();
                    Console.WriteLine($"{(fileResponse ? "Operação realizada com sucesso! " + watch.ElapsedMilliseconds + "ms" : "Erro no processamento...")}");
                    watch.Reset();
                    break;
                case 3:
                    Console.Clear();
                    break;
                case 0:
                    Console.WriteLine("Saindo...");
                    break;
                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }
            Console.WriteLine("Continuar...");
            Console.ReadKey();
        } while (option > 0);
    }
}