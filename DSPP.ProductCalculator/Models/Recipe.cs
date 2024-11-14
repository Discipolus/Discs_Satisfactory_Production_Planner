namespace DSPP.ProductCalculator.Models;

public class Recipe: GameEntity
{
    [JsonPropertyName("mFullName")]
    public string FullName { get; set; }

    [JsonPropertyName("mIngredients")]
    public SortedList<Product, double> Educts { get; set; }

    [JsonPropertyName("mProduct")]
    public SortedList<Product, double> Products { get; set; }

    [JsonPropertyName("mProducedIn")]
    public Buildings BuildingType { get; set; }

    [JsonPropertyName("mManufactoringDuration")]
    public double Produktionszeit { get; set; }

	public Recipe Clone()
	{
		return new Recipe()
		{
			ClassName = this.ClassName,
			FullName = this.FullName,
			Educts = new SortedList<Product, double>(this.Educts),
			Products = new SortedList<Product, double>(this.Products),
			BuildingType = this.BuildingType,
			Produktionszeit = this.Produktionszeit
		};
	}

}