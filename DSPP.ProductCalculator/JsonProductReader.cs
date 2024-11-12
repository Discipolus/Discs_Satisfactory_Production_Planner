using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using DSPP.ProductCalculator.Models;


namespace DSPP.ProductCalculator;
public class JsonProductReader
{
    public List<Product> ReadProducts(string json)
    {
        StreamReader sr = new StreamReader(json);        
        JsonNode node = JsonObject.Parse(sr.ReadToEnd());
        var rezepte = JsonSerializer.Deserialize<List<Rezept>>(json);

        return new List<Product>();
    }
}
