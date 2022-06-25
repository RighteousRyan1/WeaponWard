using Terraria.DataStructures;
using Terraria.ModLoader;
using WeaponWard.Content.Components.Items;

namespace WeaponWard.Core.Abstractions.Content
{
    /// <summary>
    ///     Base abstract class for Weapon Ward <see cref="ModItem"/>s.
    /// </summary>
    /// <remarks>
    ///     Implements: <br />
    ///     - <see cref="IWardContent"/> <br />
    ///     - <see cref="IWardItem"/> <br />
    ///     - <see cref="IHeldItemDrawDataCacheable"/> <br />
    /// </remarks>
    public abstract class WardItem : ModItem, IWardContent, IWardItem, IHeldItemDrawDataCacheable
    {
        #region IWardItem Impl

        public abstract WardItemType ItemType { get; }
        
        public abstract string ItemAsylumWikiLink { get; }

        #endregion

        #region IDrawableHeldItem Impl

        public virtual bool PreCacheDrawData(ref PlayerDrawSet drawInfo, ref DrawData drawData) {
            return true;
        }

        public virtual void PostCacheDrawData(PlayerDrawSet drawInfo, DrawData drawData) {
        }

        #endregion
    }
}