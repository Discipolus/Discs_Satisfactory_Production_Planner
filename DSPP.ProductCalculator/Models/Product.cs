namespace DSPP.ProductCalculator.Models;

public class Product : GameEntity
{
    public List<Recipe> Recipes { get; set; } = new();
    public Product() { }
    public Product(GameEntity gameEntity) : base(gameEntity){}
}