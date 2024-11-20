namespace DSPP.ProductCalculator.Models;

public class Recipe: GameEntity
{
    public SortedList<Product, double> Educts { get; set; } = new();

    public SortedList<Product, double> Products { get; set; } = new();

    public Buildings BuildingType { get; set; }

    public double Produktionszeit { get => Convert.ToDouble(mManufactoringDuration); set => mManufactoringDuration = value.ToString(); }

    public bool isDefault { get => !(ClassName.Contains("Alternate") || (mDisplayName.Contains("Alternativ"))); }

    public Recipe(){}
    public Recipe(GameEntity gameEntity) : base(gameEntity){}

    public Recipe Clone()
	{
		return new Recipe((GameEntity) this);
	}

    public List<Product> GetEductsWithTargetAmounts(Product product, double targetAmount)
    {
        if (!Products.ContainsKey(product))
        {
            return null;
        }
        List<Product> educts = new();
        
        foreach (Product educt in Educts.Keys)
        {
            educts.Add(
                new Product(educt)
                {
                    AmountPerMinute = targetAmount * Educts[educt],                    
                });
        }
        return educts;
    }

}