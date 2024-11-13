using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using DSPP.ProductCalculator.Models;


namespace DSPP.ProductCalculator;
public class JsonProductReader
{
    public void DeserializeJson(string jsonPath)
    {
        List<GameEntityGroup> gameEntityGroups = ReadGameEntitieGroups(jsonPath);
        List<Product> products = new List<Product>();
        List<Recipe> recipes = new List<Recipe>();

        foreach (GameEntityGroup gameEntityGroup in gameEntityGroups)
        {
            if (gameEntityGroup.NativeClass.Contains("FGItemDescriptor"))
            {
                products = ReadAllProducts(gameEntityGroup);
                break;
            }
        }
        foreach (GameEntityGroup gameEntityGroup in gameEntityGroups)
        {
            if (gameEntityGroup.NativeClass.Contains("FGRecipe"))
            {
                recipes = ReadAllRecipes(gameEntityGroup, products);
                break;
            }
        }
    }

    public List<GameEntityGroup> ReadGameEntitieGroups(string json)
    {
        StreamReader sr = new StreamReader(json);
        return JsonSerializer.Deserialize<List<GameEntityGroup>>(sr.ReadToEnd());
    }

    public List<Product> ReadAllProducts(GameEntityGroup gameEntityGroup)
    {
        List<Product> products = new List<Product>();

        foreach (GameEntity gameEntity in gameEntityGroup.Classes)
        {
            products.Add(new Product()
            {
                ClassName = gameEntity.ClassName,
                DisplayName = gameEntity.mDisplayName
            });

        }

        return products;
    }

    public List<Recipe> ReadAllRecipes(GameEntityGroup gameEntityGroup, List<Product> products)
    {
        List<Recipe> rezepte = new List<Recipe>();

        foreach (GameEntity gameEntity in gameEntityGroup.Classes)
        {
            rezepte.Add(new Recipe()
            {
                ClassName = gameEntity.ClassName,
                FullName = gameEntity.FullName,
                DisplayName = gameEntity.mDisplayName,
                Educts = ParseProductsFromString(gameEntity.mIngredients, products),
                Products = ParseProductsFromString(gameEntity.mProduct, products),
                BuildingType = GetGebaeudetyp(gameEntity.mProducedIn),
                Produktionszeit = Convert.ToDouble(gameEntity.mManufactoringDuration)
            });
        }
        return new List<Recipe>();
    }
    public SortedList<Product, double> ParseProductsFromString(string jsonProductArray, List<Product> products)
    {
        SortedList<Product, double> sortedList = new SortedList<Product, double>();
        string[] productsAndAmounts = jsonProductArray.Split('(', ')');

        foreach (string productAndAmount in productsAndAmounts)
        {
            if (productAndAmount.Length == 0)
            {
                continue;
            }            
            Product product = products.FirstOrDefault(x => productAndAmount.Contains(x.ClassName));
            double amount = double.Parse(productAndAmount.Split("Amount=")[1]);
            sortedList.Add(product, amount);
        }
        
        return sortedList;        
    }
    private Buildings GetGebaeudetyp(string jsonGebaeudetyp)
    {
        return Buildings.Schmelzofen;
    }
}
