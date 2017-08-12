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
    [RequiresSkill(typeof(AgricultureSkill), 0)]
    public partial class DistillingSkill : Skill
    {
        public override string FriendlyName { get { return "Distilling"; } }
        public override string Description { get { return "Process of Distilling Alcohol"; } }

        public static ModificationStrategy MultiplicativeStrategy =
            new MultiplicativeStrategy(new float[] { 1, 1 - 0.1f, 1 - 0.2f, 1 - 0.3f, 1 - 0.4f, 1 - 0.5f });
        public static ModificationStrategy AdditiveStrategy =
            new AdditiveStrategy(new float[] { 0, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f });
        public static int[] SkillPointCost = { 1, 2, 5, 10, 15 };
        public override int RequiredPoint { get { return this.Level < this.MaxLevel ? SkillPointCost[this.Level] : 0; } }
        public override int MaxLevel { get { return 5; } }
    }

    [Serialized]
    public partial class DistillingSkillBook : SkillBook<DistillingSkill>
    {
        public override string FriendlyName { get { return "Distilling Book"; } }
        public override Type SkillScrollItem { get { return typeof(DistillingSkillScroll); } }
    }

    [Serialized]
    public partial class DistillingSkillScroll : SkillScroll<DistillingSkill>
    {
        public override string FriendlyName { get { return "Distilling Scroll"; } }
    }

    [RequiresSkill(typeof(AgricultureSkill), 0)]
    public partial class DistillingBookRecipe : Recipe
    {
        public DistillingBookRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<DistillingSkillBook>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BucketItem>(typeof(ResearchEfficiencySkill), 10, new PercentReductionStrategy(4, 0.2f)),
				new CraftingElement<CornmealItem>(typeof(ResearchEfficiencySkill), 40, new PercentReductionStrategy(4, 0.2f)),
				new CraftingElement<SugarItem>(typeof(ResearchEfficiencySkill), 40, new PercentReductionStrategy(4, 0.2f)),
            };
            this.CraftMinutes = new ConstantValue(60);

            this.Initialize("Distilling Book", typeof(DistillingSkillBook));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }
}