namespace DSPP.ProductCalculator.Models;

public class Product : GameEntity
{    
    [JsonIgnore]
    public List<Recipe> Recipes { get; set; }

}