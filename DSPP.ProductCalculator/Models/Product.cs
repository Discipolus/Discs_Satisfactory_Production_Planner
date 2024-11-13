using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace DSPP.ProductCalculator.Models;

public class Product : GameEntity
{

    
    [JsonIgnore]
    List<Recipe> RezeptList { get; set; }

}
