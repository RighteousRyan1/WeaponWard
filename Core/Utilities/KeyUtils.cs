using Microsoft.Xna.Framework.Input;
using Terraria;

namespace WeaponWard.Core.Utilities
{
    public static class KeyUtils
    {
        public static bool KeyJustPressed(Keys key) => Main.keyState.IsKeyDown(key) && !Main.oldKeyState.IsKeyDown(key);
    }
}
