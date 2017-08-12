namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.Gameplay.Pipes;

    [RequiresSkill(typeof(BasicCraftingSkill), 1)]
    public partial class WaterBucketRecipe : Recipe
    {
    public WaterBucketRecipe()
    {
        this.Products = new CraftingElement[]
        {
            new CraftingElement<WaterBucketItem>(),          
        };
        this.Ingredients = new CraftingElement[]
        {
                new CraftingElement<BucketItem>(typeof(BasicCraftingEfficiencySkill), 1, BasicCraftingEfficiencySkill.MultiplicativeStrategy),
        };
            this.CraftMinutes = new MultiDynamicValue(MultiDynamicOps.Multiply
            , CreateCraftTimeValue(typeof(WaterBucketRecipe), Item.Get<WaterBucketItem>().UILink(), 5, typeof(BasicCraftingSpeedSkill))
            );
            this.Initialize("Water Bucket", typeof(WaterBucketRecipe));
            CraftingComponent.AddRecipe(typeof(WaterwheelObject), this);
        }
    }

    [Serialized]
    
    [Weight(6)]      
    public partial class WaterBucketItem :
    Item                                     
    {
        public override string FriendlyName { get { return "Water Bucket"; } }
        public override string Description  { get { return "MAB Special: Hope it doesnt leak"; } }
        
    }
}