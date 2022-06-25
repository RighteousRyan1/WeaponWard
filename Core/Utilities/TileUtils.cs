using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ObjectData;
using WeaponWard.Core.Utilities.Graphics;

namespace WeaponWard.Core.Utilities
{
    /// <summary>
    ///     Drawing utilities copied and adapted from https://github.com/Jadams505/Twaila/blob/1.4/Util/ImageUtil.cs
    /// </summary>
    public class TileUtils : ILoadable
    {
        public readonly record struct TileDrawData(int Id, int Style);

        private static Dictionary<TileDrawData, Texture2D?> DrawCache = new();
        
        public static Texture2D? GetMultitileTexture(TileDrawData drawData) {
            // Caching is VITAL because this consumes a lot of resources.
            if (DrawCache.ContainsKey(drawData)) return DrawCache[drawData];
            
            Texture2D? SetReturn(Texture2D? texture) {
                return DrawCache[drawData] = texture;
            }
            
            Texture2D? spritesheet = GetTileTexture(drawData.Id);
            TileObjectData? tileData = TileObjectData.GetTileData(drawData.Id, drawData.Style);
            
            if (spritesheet is null || tileData is null) return SetReturn(null);

            Texture2D? texture = GetTextureFromTileObjectData(spritesheet, Main.graphics.GraphicsDevice, tileData);

            return SetReturn(texture ?? null);
        }

        public static Texture2D? GetTextureFromTileObjectData(Texture2D spritesheet, GraphicsDevice gd, TileObjectData data) {
            TextureBuilder builder = new();
            int height = 0;
            for (int y = 0; y < data.Height; y++) {
                for (int x = 0; x < data.Width; x++) {
                    int width = data.CoordinateWidth;
                    Rectangle scissor = new(
                        (width + data.CoordinateWidth) * x,
                        height + data.CoordinatePadding * y,
                        width,
                        data.CoordinateHeights[y]
                    );
                    builder.AddComponent(scissor, spritesheet, new Point(width * x, height));
                }

                height += data.CoordinateHeights[y];
            }

            return builder.Build(gd);
        }

        public static Texture2D? GetTileTexture(int id) {
            if (id < 0 || id > TileLoader.TileCount) 
                return null;
            
            Main.instance.LoadTiles(id);
            return TextureAssets.Tile[id].Value;
        }

        void ILoadable.Load(Mod mod) {
        }

        void ILoadable.Unload() {
            // DrawCache = null!;
        }
    }
}