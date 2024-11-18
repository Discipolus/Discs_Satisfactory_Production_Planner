namespace DSPP.ProductCalculator.Models;

public class Recipe: GameEntity
{
    public SortedList<Product, double> Educts { get; set; } = new();

    public SortedList<Product, double> Products { get; set; } = new();

    public Buildings BuildingType { get; set; }

    public double Produktionszeit { get => Convert.ToDouble(mManufactoringDuration); set => mManufactoringDuration = value.ToString(); }

    public bool isDefault { get => !ClassName.Contains("Alternate"); }

    public Recipe(){}
    public Recipe(GameEntity gameEntity) : base(gameEntity){}

    public Recipe Clone()
	{
		return new Recipe((GameEntity) this);
	}

}