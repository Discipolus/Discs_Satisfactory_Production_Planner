using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DSPP.ProductCalculator.Models;

public struct Edukt
{
    public string ProductName;
    public int Menge;
}

public class Rezept : GameEntity
{
    [JsonPropertyName("mFullName")]
    public string FullName { get; set; }

    [JsonPropertyName("mIngredients")]
    public List<Edukt> Edukte { get; set; }

    [JsonPropertyName("mProduct")]
    public List<Edukt> Produkte { get; set; }

    [JsonPropertyName("mProducedIn")]
    public Gebaeudetypen Gebaeudetyp { get; set; }

    [JsonPropertyName("mManufactoringDuration")]
    public double Produktionszeit { get; set; }

}
/*
 *				"ClassName": "Recipe_WorkBench_C",
				"FullName": "BlueprintGeneratedClass /Game/FactoryGame/Recipes/Buildings/Recipe_WorkBench.Recipe_WorkBench_C",
				"mDisplayName": "Werkbank",
				"mIngredients": "((ItemClass=\"/Script/Engine.BlueprintGeneratedClass'/Game/FactoryGame/Resource/Parts/IronPlate/Desc_IronPlate.Desc_IronPlate_C'\",Amount=3),(ItemClass=\"/Script/Engine.BlueprintGeneratedClass'/Game/FactoryGame/Resource/Parts/IronRod/Desc_IronRod.Desc_IronRod_C'\",Amount=3))",
				"mProduct": "((ItemClass=\"/Script/Engine.BlueprintGeneratedClass'/Game/FactoryGame/Buildable/Factory/WorkBench/Desc_WorkBench.Desc_WorkBench_C'\",Amount=1))",
				"mManufacturingMenuPriority": "0.000000",
				"mManufactoringDuration": "1.000000",
				"mManualManufacturingMultiplier": "1.000000",
				"mProducedIn": "(\"/Game/FactoryGame/Equipment/BuildGun/BP_BuildGun.BP_BuildGun_C\")",
				"mRelevantEvents": "",
				"mVariablePowerConsumptionConstant": "0.000000",
				"mVariablePowerConsumptionFactor": "1.000000"
			},
			{
				"ClassName": "Recipe_IronPlate_C",
				"FullName": "BlueprintGeneratedClass /Game/FactoryGame/Recipes/Constructor/Recipe_IronPlate.Recipe_IronPlate_C",
				"mDisplayName": "Eisenplatte",
				"mIngredients": "((ItemClass=\"/Script/Engine.BlueprintGeneratedClass'/Game/FactoryGame/Resource/Parts/IronIngot/Desc_IronIngot.Desc_IronIngot_C'\",Amount=3))",
				"mProduct": "((ItemClass=\"/Script/Engine.BlueprintGeneratedClass'/Game/FactoryGame/Resource/Parts/IronPlate/Desc_IronPlate.Desc_IronPlate_C'\",Amount=2))",
				"mManufacturingMenuPriority": "0.000000",
				"mManufactoringDuration": "6.000000",
				"mManualManufacturingMultiplier": "1.000000",
				"mProducedIn": "(\"/Game/FactoryGame/Buildable/Factory/ConstructorMk1/Build_ConstructorMk1.Build_ConstructorMk1_C\",\"/Game/FactoryGame/Buildable/-Shared/WorkBench/BP_WorkBenchComponent.BP_WorkBenchComponent_C\",\"/Script/FactoryGame.FGBuildableAutomatedWorkBench\")",
				"mRelevantEvents": "",
				"mVariablePowerConsumptionConstant": "0.000000",
				"mVariablePowerConsumptionFactor": "1.000000"
			}, 
 */
