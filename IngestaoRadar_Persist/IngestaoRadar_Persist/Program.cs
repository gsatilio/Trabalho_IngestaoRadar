using System.Text.Json;
using Models;
using Newtonsoft.Json;
using Models;
using System.Net;
using Controllers;

internal class Program
{
    private static void Main(string[] args)
    {
        var controller = new RadarController();
        var url = "https://dados.antt.gov.br/dataset/79d287f4-f5ca-4385-a17c-f61f53831f17/resource/fa861690-70de-4a27-a82f-0eee74abdbc0/download/dados_dos_radares.json";

        var response = controller.SaveRadarDataFromApi(url);

        if (response)
        {
            Console.WriteLine("Operação realizada com sucesso!");
        }
    }
}