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
    [Fuel(10000)]
    public partial class MoonshineItem :
        FoodItem            
    {
        public override string FriendlyName                     { get { return "Moonshine"; } }
        public override string Description                      { get { return "Highly Flamable, wonder if it taiste good."; } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 10, Fat = 10, Protein = 10, Vitamins = 10};
        public override float Calories                          { get { return 200; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(DistillingSkill), 2)]    
    public partial class MoonshineRecipe : Recipe
    {
        public MoonshineRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<MoonshineItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SugarItem>(typeof(DistillingEfficiencySkill), 50,DistillingEfficiencySkill.MultiplicativeStrategy), 
                new CraftingElement<CornmealItem>(typeof(DistillingEfficiencySkill), 50,DistillingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<Waterbarrelitem>(typeof(DistillingEfficiencySkill), 2,DistillingEfficiencySkill.MultiplicativeStrategy), 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(MoonshineRecipe), Item.Get<MoonshineItem>().UILink(), 5, typeof(DistillingSpeedSkill)); 
            this.Initialize("Moonshine", typeof(MoonshineRecipe));
            CraftingComponent.AddRecipe(typeof(DistilleryObject), this);
        }
    }
}
