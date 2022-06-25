using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using WeaponWard.Content.Components.Items;
using WeaponWard.Core.Abstractions.Content;
using WeaponWard.Core.Utilities;

namespace WeaponWard.Content.ComponentHandlers.Items
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

            TooltipLine line1 = MakeOriginalityLine(Mod, wardItem.ItemType);
            int index1 = tooltips.FindIndex(x => x.Name == "JourneyResearch" && x.Mod == "Terraria");
            
            if (index1 == -1)
                tooltips.Add(line1);
            else
                tooltips.Insert(index1, line1);

            if (wardItem.ItemAsylumWikiLink is not null)
            {
                TooltipLine line2 = ProvideLinkLine(Mod);
                int index2 = tooltips.FindIndex(x => x.Name == "WardOriginalityType" && x.Mod == "WeaponWard");

                if (index1 == -1)
                    tooltips.Add(line2);
                else
                    tooltips.Insert(index2, line2);
            }
        }
        public override void UpdateInventory(Item item, Player player)
        {
            if (item.ModItem is not IWardItem wardItem)
                return;

            if (wardItem.ItemAsylumWikiLink is not null)
            {
                if (Main.HoverItem.ModItem is not null)
                {
                    if (Main.HoverItem.ModItem is IWardItem)
                    {
                        if (KeyUtils.KeyJustPressed(Keys.RightAlt))
                            Process.Start(new ProcessStartInfo(wardItem.ItemAsylumWikiLink)
                            {
                                UseShellExecute = true,
                            });
                    }
                }
            }
        }

        private static TooltipLine MakeOriginalityLine(Mod mod, WardItemType itemType)
        {
            return new TooltipLine(mod, "WardOriginalityType", Language.GetTextValue($"Mods.WeaponWard.OriginalityTooltip.{itemType}"))
            {
                OverrideColor = Color.LightGoldenrodYellow
            };
        }
        private static TooltipLine ProvideLinkLine(Mod mod)
        {
            return new TooltipLine(mod, "ItemAsylumLink", Language.GetTextValue($"Mods.WeaponWard.ItemLink.LinkText"))
            {
                OverrideColor = Color.DarkGray
            };
        }
    }
}