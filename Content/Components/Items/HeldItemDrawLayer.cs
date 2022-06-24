using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.DataStructures;
using WeaponWard.Core.Abstractions.Content;
using Terraria.ID;

namespace WeaponWard.Content.Components.Items
{
    public class HeldItemDrawLayer : WardPlayerDrawLayer
    {
        public static bool DidEarlyReturn = false;

        /// <summary>
        ///     Conditions for rendering held items.
        /// </summary>
        /// <param name="DrawingDisabled">Various standard checks for disabling drawing.</param>
        /// <param name="DrawSet">The instance for manual checking.</param>
        public readonly record struct HeldItemContext(bool DrawingDisabled, PlayerDrawSet DrawSet);

        public override Position GetDefaultPosition() {
            return new AfterParent(PlayerDrawLayers.HeldItem);
        }

        protected override void Draw(ref PlayerDrawSet drawInfo) {
            if (drawInfo.heldItem.ModItem is not IDrawableHeldItem item) return;
            HeldItemContext context = new(DidEarlyReturn, drawInfo);
            item.DrawLayer(Main.spriteBatch, context);
        }

        public override void Load() {
            base.Load();
            
            IL.Terraria.DataStructures.PlayerDrawLayers.DrawPlayer_27_HeldItem += ReturnContext;
        }

        public override void Unload() {
            base.Unload();
            
            IL.Terraria.DataStructures.PlayerDrawLayers.DrawPlayer_27_HeldItem -= ReturnContext;
        }
        
        private static void ReturnContext(ILContext il) {
            ILCursor c = new(il);

            void InsertBeforeRet() {
                c.GotoNext(MoveType.Before, x => x.MatchRet());
                c.Emit(OpCodes.Ldc_I4_1);
                c.Emit<HeldItemDrawLayer>(OpCodes.Stsfld, nameof(DidEarlyReturn));
            }

            c.Emit(OpCodes.Ldc_I4_0);
            c.Emit<HeldItemDrawLayer>(OpCodes.Stsfld, nameof(DidEarlyReturn));

            // Honestly throw here since this is important.
            InsertBeforeRet();
            InsertBeforeRet();
        }
    }
}