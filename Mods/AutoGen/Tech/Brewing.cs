namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Serialization;
    using Eco.Shared.Services;
    using Eco.Shared.Utils;
    using Gameplay.Systems.Tooltip;
    
    [Serialized]
    [RequiresSkill(typeof(DistillingSkill), 1)]    
    public partial class BrewingSkill : Skill
    {
        public override string FriendlyName { get { return "Brewing"; } }
        public override string Description { get { return "Consume with Moderation?"; } }

        public static int[] SkillPointCost = { 6, 8, 10, 12, 14 };
        public override int RequiredPoint { get { return this.Level < this.MaxLevel ? SkillPointCost[this.Level] : 0; } }
        public override int MaxLevel { get { return 5; } }
    }
    
    [Serialized]
    public partial class BrewingSkillBook : SkillBook<BrewingSkill>
    {
        public override string FriendlyName { get { return "Brewing Skill Book"; } }
        public override Type SkillScrollItem { get { return typeof(BrewingSkillScroll); } }
    }

    [Serialized]
    public partial class BrewSkillScroll : SkillScroll<BrewingSkill>
    {
        public override string FriendlyName { get { return "Brewing Skill Scroll"; } }
    }

    [RequiresSkill(typeof(DistillingSkill), 2)] 
    public partial class BrewingSkillBookRecipe : Recipe
    {
        public BrewingSkillBookRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<BrewingSkillBook>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<waterbarrelitem>(typeof(ResearchEfficiencySkill), 5, new PercentReductionStrategy(4, 0.2f)),
                new CraftingElement<hopsitem>(typeof(ResearchEfficiencySkill), 50, new PercentReductionStrategy(4, 0.2f)), 
                new CraftingElement<maltextractitem>(typeof(ResearchEfficiencySkill), 10, new PercentReductionStrategy(4, 0.2f)), 
            };
            this.CraftMinutes = new ConstantValue(40);

            this.Initialize("Brewing Skill Book", typeof(BrewingSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }
}
