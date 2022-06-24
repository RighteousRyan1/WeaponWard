using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;

namespace WeaponWard.Content.Components.Items
{
    // Some parts adapted from Ariame's code he sent me in Empyrea.
    public interface IDrawableHeldItem
    {
        public struct HeldItemDrawData
        {
            public readonly Vector2 Position;
            public readonly float Rotation;
            public readonly float YOffset;
            public readonly int Direction;
            
            public HeldItemDrawData(Vector2 position, float rotation, float yOffset, int direction)
            {
                Position = position;
                Rotation = rotation;
                YOffset = yOffset;
                Direction = direction;
            }
        }

        HeldItemDrawData DrawData { get; set; }

        HeldItemDrawData GetDrawData(Player player) {
            return new HeldItemDrawData(player.itemLocation, player.itemRotation, player.gfxOffY, player.direction);
        }

        Vector2 GetOrigin(Rectangle sourceRect, int direction) {
            return new Vector2(sourceRect.Width * 0.5f - sourceRect.Width * 0.5f * direction, sourceRect.Height);
        }

        Rectangle GetSourceRect(Texture2D texture);

        void DrawHeldItem(SpriteBatch spriteBatch, Texture2D texture, Color color, SpriteEffects spriteEffects);

        void DrawLayer(SpriteBatch spriteBatch, PlayerDrawSet playerDrawSet);
    }
}