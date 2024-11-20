namespace DSPP.ProductCalculator.Models;

public class Product : GameEntity
{
    public double AmountPerMinute { get; set; }

    private Recipe? m_currentRecipe = null;
    public Recipe CurrentRecipe
    {
        get
        {
            if (m_currentRecipe != null)
            {
                return m_currentRecipe;
            }
            return Recipes.Where(Recipe => Recipe.isDefault).FirstOrDefault();
        }
        set
        {
            if (Recipes.Contains(value))
            {
                m_currentRecipe = value;
            }
        }
    }
    public List<Recipe> Recipes { get; set; } = new();
    public Product() { }
    public Product(GameEntity gameEntity) : base(gameEntity){ }

    public static List<Product>? GetEducts(Product product)
    {
        if (product == null || product.Recipes.Count == 0)
        {
            return null;
        }
        return product.CurrentRecipe.GetEductsWithTargetAmounts(product, product.AmountPerMinute);
    }

}