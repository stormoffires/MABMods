namespace Eco.Mods.TechTree
{
    using System.Collections.Generic;
    using System.Linq;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Mods.TechTree;
    using Eco.Shared.Items;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    
    [Serialized]
    [Weight(0.1f)]                                          
    public partial class Maltextractitem :
        FoodItem            
    {
        public override string FriendlyName                     { get { return "Malt Extract"; } }
        public override string Description                      { get { return "Used for brewing beer"; } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 2, Fat = 2, Protein = 2, Vitamins = 2};
        public override float Calories                          { get { return 10; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(BrewingSkill), 2)]    
    public partial class MaltextractRecipe : Recipe
    {
        public MaltextractRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<Maltextractitem>(),             
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CerealGermItem>(typeof(BrewingEfficiencySkill), 5,BrewingEfficiencySkill.MultiplicativeStrategy), 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(MaltextractRecipe), Item.Get<Maltextractitem>().UILink(), 5, typeof(BrewingSpeedSkill)); 
            this.Initialize("Maltextract", typeof(MaltextractRecipe));
            CraftingComponent.AddRecipe(typeof(MillObject), this);
        }
    }
}
