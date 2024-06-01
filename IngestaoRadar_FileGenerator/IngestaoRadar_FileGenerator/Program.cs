using Models;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("File Generator");
        GenerateJsonFile();
    }
    async public static void GenerateJsonFile()
    {
        string outputFileName = "C:\\5by5\\Estagio\\Semana 7\\Trabalho_Radar\\teste.json"; // initialize to the output file
        var client = new MongoDatabase().Connection;
        var db = client.GetDatabase("Radar");
        var collection = db.GetCollection<BsonDocument>("Data");

        try
        {
            using (var streamWriter = new StreamWriter(outputFileName))
            {
                await collection.Find(new BsonDocument())
                    .ForEachAsync(async (document) =>
                    {
                        using (var stringWriter = new StringWriter())
                        using (var jsonWriter = new JsonWriter(stringWriter))
                        {
                            var context = BsonSerializationContext.CreateRoot(jsonWriter);
                            collection.DocumentSerializer.Serialize(context, document);
                            var line = stringWriter.ToString();
                            await streamWriter.WriteLineAsync(line);
                        }
                    });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}