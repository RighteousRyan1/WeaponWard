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
using WeaponWard.Core.Utilities;

namespace WeaponWard.Content.Players
{
    public static class CustomDeathSounds
    {
        public static Dictionary<string, SoundStyle> DeathStyles;
        public static void Initialize(Mod mod)
        {
            // that was easy.
            DeathStyles = FolderLoadingUtils.LoadSoundStylesFromFolder(mod, "Assets", "Sounds", "DeathSounds");
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
