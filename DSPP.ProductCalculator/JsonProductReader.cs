namespace DSPP.ProductCalculator;
public class JsonProductReader
{
    private List<GameEntityGroup> gameEntityGroups = new ();
    private List<Product> allProducts = new ();
    private List<Recipe> allRecipes = new ();
    private List<GameEntity> allBuildings = new ();
    public List<Product> DeserializeJson(string jsonPath)
    {
        gameEntityGroups = ReadGameEntitieGroups(jsonPath);        

        foreach (GameEntityGroup gameEntityGroup in gameEntityGroups)
        {
            if (gameEntityGroup.NativeClass.Contains("FGItemDescriptor") || gameEntityGroup.NativeClass.Contains("FGResourceDescriptor"))
            {
                allProducts.AddRange(ConvertGameEntityListToProductList(gameEntityGroup));
            }
            if (gameEntityGroup.NativeClass.Contains("FGBuildableManufacturer"))
            {
                allBuildings.AddRange(gameEntityGroup.Classes);
            }
        }
        foreach (GameEntityGroup gameEntityGroup in gameEntityGroups)
        {
            if (gameEntityGroup.NativeClass.Contains("FGRecipe"))
            {
                allRecipes = ReadAllRecipes(gameEntityGroup, allProducts);
                break;
            }
        }
        AddRecipesToProducts(allProducts, allRecipes);

        allProducts.Sort();

        return allProducts;
    }

    private void AddRecipesToProducts(List<Product> products, List<Recipe> recipes)
    {
        foreach (Product product in products)
        {
            product.Recipes = recipes.FindAll(x => x.Products.ContainsKey(product));
        }
    }

    private List<GameEntityGroup>? ReadGameEntitieGroups(string json)
    {
        StreamReader sr = new StreamReader(json);
        return JsonSerializer.Deserialize<List<GameEntityGroup>>(sr.ReadToEnd());
    }

    private List<Product> ConvertGameEntityListToProductList(GameEntityGroup gameEntityGroup)
    {
        List<Product> products = new();
        foreach (GameEntity gameEntity in gameEntityGroup.Classes)
        {
            products.Add(new Product(gameEntity));
        }
        return products;
    }

    private List<Recipe> ReadAllRecipes(GameEntityGroup gameEntityGroup, List<Product> products)
    {
        List<Recipe> recipes = new();

        foreach (GameEntity gameEntity in gameEntityGroup.Classes)
        {
            Recipe recipe = new Recipe()
            {
                ClassName = gameEntity.ClassName,
                FullName = gameEntity.FullName,
                mDisplayName = gameEntity.mDisplayName
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
        string[] productsAndAmounts = jsonProductArray.Split('(', ')').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

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
    private Buildings GetGebaeudetyp(string mProducedIn)
    {
        string[] productionEntities = mProducedIn.Split('(', ',', ')').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
        GameEntity? building = new();
        foreach (string entity in productionEntities)
        {
            building = allBuildings.FirstOrDefault(x => entity.Contains(x.ClassName));
            if (building != null)
            {
                break;
            }
        }
        if (building == null)
        {
            return Buildings.None;
        }

        if (Enum.TryParse(building.mDisplayName, out Buildings buildingType))
        {
            return buildingType;
        }
        else
        {
            return Buildings.None;
        }
    }
}
