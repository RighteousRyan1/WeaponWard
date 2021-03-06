using System.Collections.Generic;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using WeaponWard.Content.Components.Items;
using WeaponWard.Core.Utilities;

namespace WeaponWard.Content.ComponentHandlers.Items
{
    public class HeldItemEdit : ILoadable
    {
        private class HeldItemMixin : Mixin
        {
            public HeldItemMixin(ILCursor cursor) : base(cursor) { }

            protected override void ReplaceCall<TDelegate>(TDelegate @delegate) {
                // Remove original call(virt) from stack.
                Cursor.Remove();

                // Currently on stack:
                //  List<DrawData> - DrawDataCache
                //  DrawData - DrawDataCache item
                // To add:
                //  PlayerDrawSet&

                Cursor.Emit(OpCodes.Ldarg_0); // PlayerDrawSet&

                // Push new delegate.
                Cursor.EmitDelegate(@delegate);
            }
        }

        private delegate void DataCacheDelegate(List<DrawData> drawDataCache, DrawData drawData, ref PlayerDrawSet drawInfo);

        public void Load(Mod mod) {
            // TODO: Determine if delegation here is necessary. I was having debugging issues earlier but I do not know if this is the thing that fixed them.
            Main.QueueMainThreadAction(() =>
            {
                IL.Terraria.DataStructures.PlayerDrawLayers.DrawPlayer_27_HeldItem += CacheManipulator;
            });
        }

        public void Unload() {
            Main.QueueMainThreadAction(() =>
            {
                IL.Terraria.DataStructures.PlayerDrawLayers.DrawPlayer_27_HeldItem -= CacheManipulator;
            });
        }

        private static void CacheManipulator(ILContext il) {
            ILCursor c = new(il);
            Mixin m = new HeldItemMixin(c);

            m.ReplaceCallvirts<DataCacheDelegate>("System.Collections.Generic.List`1<Terraria.DataStructures.DrawData>", "Add", HandleDataCache);
        }

        private static void HandleDataCache(List<DrawData> drawDataCache, DrawData drawData, ref PlayerDrawSet drawInfo) {
            if (drawInfo.heldItem.ModItem is not IHeldItemDrawDataCacheable cacheableItem) {
                drawDataCache.Add(drawData);
                return;
            }

            if (cacheableItem.PreCacheDrawData(ref drawInfo, ref drawData)) drawDataCache.Add(drawData);
            cacheableItem.PostCacheDrawData(drawInfo, drawData);
        }
    }
}