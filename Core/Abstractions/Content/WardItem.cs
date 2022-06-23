using Terraria.ModLoader;

namespace WeaponWard.Core.Abstractions.Content
{
    /// <summary>
    ///     Base abstract class for Weapon Ward <see cref="ModItem"/>s, implements <see cref="IWardContent"/>.
    /// </summary>
    public abstract class WardItem : ModItem, IWardContent
    {
    }
}