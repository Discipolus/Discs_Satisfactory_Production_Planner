using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DSPP.ProductCalculator.Models
{
    public abstract class GameEntity
    {
        [JsonPropertyName("ClassName")]
        public string ClassName { get; set; }

        [JsonPropertyName("mDisplayName")]
        public string DisplayName { get; set; }
    }
}
