using MonoMod.Cil;
using Terraria.ModLoader;

namespace WeaponWard.Content.ComponentHandlers.Items
{
    public class HeldItemEdit : ILoadable
    {
        public void Load(Mod mod) {
            IL.Terraria.DataStructures.PlayerDrawLayers.DrawPlayer_27_HeldItem += ReturnContext;
        }

        public void Unload() {
            IL.Terraria.DataStructures.PlayerDrawLayers.DrawPlayer_27_HeldItem -= ReturnContext;
        }

        private static void ReturnContext(ILContext il) {
            ILCursor c = new(il);
        }
    }
}