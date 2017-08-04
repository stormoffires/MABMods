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
    [RequiresSkill(typeof(MetalworkingSkill), 1)]    
    public partial class CastingSkill : Skill
    {
        public override string FriendlyName { get { return "Casting"; } }
        public override string Description { get { return ""; } }

        public static int[] SkillPointCost = { 6, 8, 10, 12, 14 };
        public override int RequiredPoint { get { return this.Level < this.MaxLevel ? SkillPointCost[this.Level] : 0; } }
        public override int MaxLevel { get { return 4; } }
    }
    
    [Serialized]
    public partial class CastingSkillBook : SkillBook<CastingSkill>
    {
        public override string FriendlyName { get { return "Casting Skill Book"; } }
        public override Type SkillScrollItem { get { return typeof(CastingSkillScroll); } }
    }

    [Serialized]
    public partial class CastingSkillScroll : SkillScroll<CastingSkill>
    {
        public override string FriendlyName { get { return "Casting Skill Scroll"; } }
    }

    [RequiresSkill(typeof(MetalworkingSkill), 2)] 
    public partial class CastingSkillBookRecipe : Recipe
    {
        public CastingSkillBookRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CastingSkillBook>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(ResearchEfficiencySkill), 15, new PercentReductionStrategy(4, 0.2f)),
                new CraftingElement<BrickItem>(typeof(ResearchEfficiencySkill), 10, new PercentReductionStrategy(4, 0.2f)), 
            };
            this.CraftMinutes = new ConstantValue(40);

            this.Initialize("Casting Skill Book", typeof(CastingSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }
}
