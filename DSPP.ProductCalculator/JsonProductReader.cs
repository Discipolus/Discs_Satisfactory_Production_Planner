namespace DSPP.ProductCalculator;
public class JsonProductReader
{
    public List<Product> DeserializeJson(string jsonPath)
    {
        List<GameEntityGroup> gameEntityGroups = ReadGameEntitieGroups(jsonPath);
        List<Product> products = new List<Product>();
        List<Recipe> recipes = new List<Recipe>();

        foreach (GameEntityGroup gameEntityGroup in gameEntityGroups)
        {
            if (gameEntityGroup.NativeClass.Contains("FGItemDescriptor") || gameEntityGroup.NativeClass.Contains("FGResourceDescriptor"))
            {
                ReadAllProducts(gameEntityGroup, products);                
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
        AddRecipesToProducts(products, recipes);
        
        products.Sort();

        return products;
    }

    private void AddRecipesToProducts(List<Product> products, List<Recipe> recipes)
    {
        foreach (Product product in products)
        {
            product.Recipes = recipes.FindAll(x => x.Products.ContainsKey(product));
        }
    }

    private List<GameEntityGroup> ReadGameEntitieGroups(string json)
    {
        StreamReader sr = new StreamReader(json);
        return JsonSerializer.Deserialize<List<GameEntityGroup>>(sr.ReadToEnd());
    }

    private List<Product> ReadAllProducts(GameEntityGroup gameEntityGroup, List<Product> products)
    {
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

    private List<Recipe> ReadAllRecipes(GameEntityGroup gameEntityGroup, List<Product> products)
    {
        List<Recipe> recipes = new List<Recipe>();

        foreach (GameEntity gameEntity in gameEntityGroup.Classes)
        {
            Recipe recipe = new Recipe()
            {
                ClassName = gameEntity.ClassName,
                FullName = gameEntity.FullName,
                DisplayName = gameEntity.mDisplayName,
                Produktionszeit = Convert.ToDouble(gameEntity.mManufactoringDuration)
            };
            SortedList<Product, double> recipeProducts = ParseProductsFromString(gameEntity.mProduct, products);
            if (recipeProducts.Count == 0)
            {
                continue;
            }
            recipe.Products = recipeProducts;

            SortedList<Product, double> recipeEducts = ParseProductsFromString(gameEntity.mIngredients, products);
            if (recipeEducts.Count == 0)
            {
                continue;
            }
            recipe.Educts = recipeEducts;

            Buildings buildingType = GetGebaeudetyp(gameEntity.mProducedIn);
            if (buildingType == Buildings.None)
            {
                continue;
            }
            recipe.BuildingType = buildingType;

            recipes.Add(recipe);
        }
        return recipes;
    }
    private SortedList<Product, double> ParseProductsFromString(string jsonProductArray, List<Product> products)
    {
        SortedList<Product, double> sortedList = new();
        string[] productsAndAmounts = jsonProductArray.Split('(', ')').Where(x=> !string.IsNullOrWhiteSpace(x)).ToArray();

        foreach (string productAndAmount in productsAndAmounts)
        {
            Product product = products.FirstOrDefault(x => productAndAmount.Contains(x.ClassName));
            if (product == null)
            {
                continue;
            }
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
