namespace DSPP.ProductCalculator.Models.Json;

public class Mschematicdependency
{
    public string Class { get; set; }
    public string mSchematics { get; set; }
    public string mRequireAllSchematicsToBePurchased { get; set; }
    public string mGamePhase { get; set; }
    public string mOnlyAllowInSelectedPhase { get; set; }
}