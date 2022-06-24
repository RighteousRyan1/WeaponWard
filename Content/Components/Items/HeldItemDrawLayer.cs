using Terraria;
using Terraria.DataStructures;
using WeaponWard.Core.Abstractions.Content;

namespace WeaponWard.Content.Components.Items
{
    public class HeldItemDrawLayer : WardPlayerDrawLayer
    {
        public override Position GetDefaultPosition() {
            return new AfterParent(PlayerDrawLayers.HeldItem);
        }

        protected override void Draw(ref PlayerDrawSet drawInfo) {
            if (drawInfo.heldItem.ModItem is not IDrawableHeldItem item) return;
            item.DrawLayer(Main.spriteBatch, drawInfo);
        }
    }
}