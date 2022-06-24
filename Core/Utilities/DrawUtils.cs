using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using WeaponWard.Content.Components.Items;

namespace WeaponWard.Core.Utilities
{
    public static class DrawUtils
    {
        public static SpriteEffects SpriteEffectsFromDirection(int direction) {
            return direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        }

        public static SpriteEffects SpriteEffectsFromDirection(this Player player) {
            return SpriteEffectsFromDirection(player.direction);
        }
        
        public static Rectangle MakeSourceRect(this Texture2D texture) {
            return new Rectangle(0, 0, texture.Width, texture.Height);
        }

        public static Rectangle MakeSourceRect(this Texture2D texture, int x, int y) {
            return new Rectangle(x, y, texture.Width, texture.Height);
        }

        public static void DrawAtItem(this IDrawableHeldItem item, SpriteBatch spriteBatch, Texture2D texture, Color color, SpriteEffects spriteEffects) {
            Rectangle sourceRect = item.GetSourceRect(texture);
            Vector2 origin = item.GetOrigin(sourceRect, item.DrawData.Direction);

            spriteBatch.Draw(
                texture,
                item.DrawData.Position - Main.screenPosition + new Vector2(0, -item.DrawData.YOffset),
                null,
                color,
                item.DrawData.Rotation,
                origin,
                1f,
                spriteEffects,
                0f
            );
        }
    }
}