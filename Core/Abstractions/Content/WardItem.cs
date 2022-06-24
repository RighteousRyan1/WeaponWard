using System;
using Terraria.ModLoader;
using WeaponWard.Content.Components.Items;

namespace WeaponWard.Core.Abstractions.Content
{
    /// <summary>
    ///     Base abstract class for Weapon Ward <see cref="ModItem"/>s, implements <see cref="IWardContent"/>.
    /// </summary>
    public abstract class WardItem : ModItem, IWardContent, IWardItem, IDrawableHeldItem
    {
        #region IWardItem Impl

        public abstract WardItemType ItemType { get; }
        
        public abstract string ItemAsylumWikiLink { get; }

        #endregion

        #region IDrawableHeldItem Impl

        public virtual bool? PreRegisterHeldItem() {
            return null;
        }

        public virtual void RegisterHeldItem() {
        }

        public virtual void PostRegisterHeldItem() {
        }

        #endregion
    }
}