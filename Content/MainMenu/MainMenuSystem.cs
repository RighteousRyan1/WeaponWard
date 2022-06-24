using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace WeaponWard.Content.MainMenu
{
    public class MainMenuSystem : ModMenu
    {
        public static List<MainMenuStyle> MenuStyles = new()
        {
            new(name: "cirno", tiled: true, textureUsed: "Assets/Textures/propaganda", musicPath: /*"Music/chirumiru"*/null, logoTexturePath: null) // change logotexturepath to blank texture.
        };

        public MainMenuStyle UsedStyle = MenuStyles[0];
        public override string DisplayName => UsedStyle.Name ?? "No Name";
        public override Asset<Texture2D> Logo => UsedStyle.LogoTexturePath is not null ? Mod.Assets.Request<Texture2D>(UsedStyle.LogoTexturePath, AssetRequestMode.ImmediateLoad) : base.Logo;
        public override string Name => UsedStyle.Name.Replace(" ", string.Empty) ?? "NoName";
        public override int Music => UsedStyle.MusicPath is not null ? MusicLoader.GetMusicSlot(Path.Combine(UsedStyle.MusicPath)) : base.Music;
        public override void Update(bool isOnTitleScreen)
        {
            UsedStyle.Act?.Invoke(this);
        }
        public override void OnSelected()
        {
            UsedStyle = MenuStyles.ElementAt(Main.rand.Next(0, MenuStyles.Count));
        }

        public override bool PreDrawLogo(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
        {
            if (UsedStyle.TextureUsed != null)
            {
                var tex = Mod.Assets.Request<Texture2D>(UsedStyle.TextureUsed, AssetRequestMode.ImmediateLoad).Value;
                if (UsedStyle.IsTiled)
                {
                    int padding = 2;
                    float scale = 0.5f;

                    Vector2 dimensions = tex.Size() * scale;

                    // draw small tank graphics using GameResources.GetGameResource
                    for (int i = -padding; i < Main.screenWidth / dimensions.X + padding; i++)
                    {
                        for (int j = -padding; j < Main.screenHeight / dimensions.Y + padding; j++)
                        {
                            spriteBatch.Draw(tex, new Vector2(i, j) * dimensions, null, Color.White, 0f, Vector2.Zero, scale, default, default);
                        }
                    }
                }
                else
                {
                    spriteBatch.Draw(Mod.Assets.Request<Texture2D>(UsedStyle.TextureUsed, AssetRequestMode.ImmediateLoad).Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White);
                }
            }
            return base.PreDrawLogo(spriteBatch, ref logoDrawCenter, ref logoRotation, ref logoScale, ref drawColor);
        }
    }
}
