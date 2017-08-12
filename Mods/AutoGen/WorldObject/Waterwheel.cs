namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Minimap;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Pipes.LiquidComponents;
    using Eco.Gameplay.Pipes.Gases;
    using Eco.Shared;
    using Eco.Shared.Math;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    using Eco.Shared.Items;
    using Eco.Gameplay.Pipes;
    using Eco.World.Blocks;
    
    [Serialized]    
    [RequireComponent(typeof(AttachmentComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(CraftingComponent))]
    [RequireComponent(typeof(LinkComponent))]
	[RequireComponent(typeof(PowerGridComponent))]              
    [RequireComponent(typeof(PowerGeneratorComponent))] 
    public partial class WaterwheelObject : WorldObject
    {
        public override string FriendlyName { get { return "Waterwheel"; } }

        protected override void Initialize()
        {
            base.Initialize();
            this.GetComponent<PowerGridComponent>().Initialize(10, typeof(MechanicalPower));      
            this.GetComponent<PowerGeneratorComponent>().Initialize(200);                       
        }
       
    }

    [Serialized]
    public partial class WaterwheelItem : WorldObjectItem<WaterwheelObject>
    {
        public override string FriendlyName { get { return "Waterwheel"; } }
        public override string Description { get { return "MAB Special: Generates mechanical Power and waterbuckets"; } }

        static WaterwheelItem()
        {
            WorldObject.AddOccupancyFromString(typeof(WaterwheelObject), "1,1,1", new Vector3i(0,0,0));
            
        }
    }


    [RequiresSkill(typeof(PrimitiveMechanicsSkill), 1)]
    public partial class WaterwheelRecipe : Recipe
    {
        public WaterwheelRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<WaterwheelItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(typeof(PrimitiveMechanicsEfficiencySkill), 20, PrimitiveMechanicsEfficiencySkill.MultiplicativeStrategy),   
            };
            SkillModifiedValue value = new SkillModifiedValue(30, PrimitiveMechanicsSpeedSkill.MultiplicativeStrategy, typeof(PrimitiveMechanicsSpeedSkill), "craft time");
            SkillModifiedValueManager.AddBenefitForObject(typeof(WaterwheelRecipe), Item.Get<WaterwheelItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<WaterwheelItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("Waterwheel", typeof(WaterwheelRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}