using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace WeaponWard.Content.Players
{
    public static class CustomDeathSounds
    {
        public static Dictionary<string, SoundStyle> DeathStyles;
        public static void Initialize(Mod mod)
        {
            DeathStyles = new();
            // grab all files inside of the directory
            var files = mod.GetFileNames();
            var deathSounds = files.Where(f => f.Contains("Assets") && f.Contains("Sounds") && f.Contains("DeathSounds"));

            foreach (var path in deathSounds)
            {
                var noExtension = Path.GetFileNameWithoutExtension(path);
                var filePathNoExtension = path.Replace(Path.GetExtension(path), string.Empty);
                DeathStyles.Add($"{noExtension}", new($"{mod.Name}/{filePathNoExtension}"));
            }
        }
    }
    public class DeathSoundPlayer : ModPlayer
    {
        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            // if (Config.AreDeathSoundsEnabled)
            if (pvp)
            {
                // play tf2 hitsound
            }

            SoundEngine.PlaySound(CustomDeathSounds.DeathStyles.ElementAt(Main.rand.Next(0, CustomDeathSounds.DeathStyles.Count)).Value, Player.position);
        }
    }
}
