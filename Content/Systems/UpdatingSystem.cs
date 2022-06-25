using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using WeaponWard.Content.Effects;
using WeaponWard.Core.Utilities;
using Microsoft.Xna.Framework.Graphics;

namespace WeaponWard.Content.Systems
{
    public class UpdatingSystem : ModSystem
    {
        public override void PostDrawTiles()
        {
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);
            foreach (var hb in HeartBreak.HeartBreaks)
                hb?.Render(Main.spriteBatch);
            Main.spriteBatch.End();
        }

        public override void PostUpdateEverything()
        {
            if (KeyUtils.KeyJustPressed(Microsoft.Xna.Framework.Input.Keys.OemSemicolon))
                new HeartBreak(Main.MouseWorld, 20, 80);
        }
    }
}
