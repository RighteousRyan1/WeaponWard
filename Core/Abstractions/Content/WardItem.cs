using Terraria.ModLoader;
using WeaponWard.Content.Components.Items;

namespace WeaponWard.Core.Abstractions.Content
{
    /// <summary>
    ///     Base abstract class for Weapon Ward <see cref="ModItem"/>s, implements <see cref="IWardContent"/>.
    /// </summary>
    public abstract class WardItem : ModItem, IWardContent, IWardItem
    {
        public abstract IWardItem.WardItemType ItemType { get; }
    }
}