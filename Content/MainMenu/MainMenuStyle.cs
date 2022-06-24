using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace WeaponWard.Content.MainMenu
{
    public struct MainMenuStyle
    {
        // tiles across the entire screen
        public bool IsTiled { get; init; }
        // the name... of the main menu style
        public string Name { get; init; } = "My Creator did not name me.";
        // the path to the appropriate music to play on the menu
        public string MusicPath { get; init; }
        // override the texture if necessary, if null, don't replace it
        public string LogoTexturePath { get; init; }
        public string TextureUsed { get; init; }
        public Action<ModMenu> Act { get; init; } = null;

        public MainMenuStyle(string name, bool tiled, string textureUsed, string musicPath, string logoTexturePath)
        {
            Name = name;
            MusicPath = musicPath;
            LogoTexturePath = logoTexturePath;
            IsTiled = tiled;
            TextureUsed = textureUsed;
        }
    }
}
