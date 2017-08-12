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
    [RequireComponent(typeof(RoomRequirementsComponent))]                 
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(CraftingComponent))]               
    [RequireRoomContainment]
    [RequireRoomMaterial(typeof(BrickItem), 10)] 
    [RequireRoomVolume(10)]                              
    public partial class DistilleryObject : WorldObject
    {
        public override string FriendlyName { get { return "Distillery"; } }

        protected override void Initialize()
        {
            base.Initialize();
            this.GetComponent<MinimapComponent>().Initialize("Cooking");                                           
        }
       
    }

    [Serialized]
    public partial class DistilleryItem : WorldObjectItem<DistilleryObject>
    {
        public override string FriendlyName { get { return "Distillery"; } }
        public override string Description { get { return "Makes a clear highly flamable liquid that can consumed? what a world!"; } }

        static DistilleryItem()
        {
            WorldObject.AddOccupancyFromString(typeof(DistilleryObject), "2,2,2", new Vector3i(0,0,0));
            
        }
    }


    [RequiresSkill(typeof(DistillingSkill), 0)]
    public partial class DistilleryRecipe : Recipe
    {
        public DistilleryRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<DistilleryItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(WoodworkingEfficiencySkill), 10, WoodworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<logItem>(typeof(WoodworkingEfficiencySkill), 10, WoodworkingEfficiencySkill.MultiplicativeStrategy),   
            };
            SkillModifiedValue value = new SkillModifiedValue(120, WoodworkingSpeedSkill.MultiplicativeStrategy, typeof(WoodworkingSpeedSkill), "craft time");
            SkillModifiedValueManager.AddBenefitForObject(typeof(DistilleryRecipe), Item.Get<DistilleryItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<DistilleryItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("Distillery", typeof(DistilleryRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}