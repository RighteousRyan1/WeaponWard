using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using WeaponWard.Content.Components.Items;
using WeaponWard.Core.Abstractions.Content;

namespace WeaponWard.Content.Globals.Items
{
    /// <summary>
    ///     Handles items implementing the <see cref="IWardItem"/> component.
    /// </summary>
    public class WardItemComponentHandler : WardGlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(item, tooltips);
            
            if (item.ModItem is not IWardItem wardItem)
                return;

            TooltipLine line = MakeOriginalityLine(Mod, wardItem.ItemType);
            int index = tooltips.FindIndex(x => x.Name == "JourneyResearch" && x.Mod == "Terraria");
            
            if (index == -1)
                tooltips.Add(line);
            else
                tooltips.Insert(index, line);
        }

        private static TooltipLine MakeOriginalityLine(Mod mod, IWardItem.WardItemType itemType)
        {
            return new TooltipLine(mod, "WardOriginalityType", Language.GetTextValue($"Mods.WeaponWard.OriginalityTooltip.{itemType}"))
            {
                OverrideColor = Color.LightGoldenrodYellow
            };
        }
    }
}