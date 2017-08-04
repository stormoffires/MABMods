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
    public partial class CornmealItem :
        FoodItem            
    {
        public override string FriendlyName                     { get { return "Cornmeal"; } }
        public override string Description                      { get { return "Dried and ground corn; it's like a courser flour."; } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 20, Fat = 5, Protein = 5, Vitamins = 0};
        public override float Calories                          { get { return 60; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(MillingSkill), 2)]    
    public partial class CornmealRecipe : Recipe
    {
        public CornmealRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CornmealItem>(),
               
               new CraftingElement<CerealGermItem>(2), 
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CornItem>(typeof(MillingEfficiencySkill), 10, MillingEfficiencySkill.MultiplicativeStrategy), 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(CornmealRecipe), Item.Get<CornmealItem>().UILink(), 5, typeof(MillingSpeedSkill)); 
            this.Initialize("Cornmeal", typeof(CornmealRecipe));
            CraftingComponent.AddRecipe(typeof(MillObject), this);
        }
    }
}
