using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;
using Terraria.ModLoader;

namespace WeaponWard.Core.Utilities
{
    public static class FolderLoadingUtils
    {
        public static Dictionary<string, SoundStyle> LoadSoundStylesFromFolder(Mod mod, params string[] pattern)
        {
            var dict = new Dictionary<string, SoundStyle>();

            var files = mod.GetFileNames();
            var matchedFiles = files.Where(f => pattern.All(s => f.Contains(s)));

            foreach (var path in matchedFiles)
            {
                var noExtension = Path.GetFileNameWithoutExtension(path);
                var filePathNoExtension = path.Replace(Path.GetExtension(path), string.Empty);
                dict.Add($"{noExtension}", new($"{mod.Name}/{filePathNoExtension}"));
            }

            return dict;
        }
    }
}
