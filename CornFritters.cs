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
    public partial class CornFrittersItem :
        FoodItem            
    {
        public override string FriendlyName                     { get { return "Corn Fritters"; } }
        public override string Description                      { get { return ""; } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 30, Fat = 40, Protein = 5, Vitamins = 5};
        public override float Calories                          { get { return 500; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(CulinaryArtsSkill), 1)]    
    public partial class CornFrittersRecipe : Recipe
    {
        public CornFrittersRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CornFrittersItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CornmealItem>(typeof(CulinaryCreationsEfficiencySkill), 15, CulinaryCreationsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<CornItem>(typeof(CulinaryCreationsEfficiencySkill), 10, CulinaryCreationsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<OilItem>(typeof(CulinaryCreationsEfficiencySkill), 10, CulinaryCreationsEfficiencySkill.MultiplicativeStrategy), 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(CornFrittersRecipe), Item.Get<CornFrittersItem>().UILink(), 5, typeof(CulinaryCreationsSpeedSkill)); 
            this.Initialize("Corn Fritters", typeof(CornFrittersRecipe));
            CraftingComponent.AddRecipe(typeof(StoveObject), this);
        }
    }
}