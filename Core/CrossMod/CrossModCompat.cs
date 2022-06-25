using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;

namespace WeaponWard.Core.CrossMod
{
    public abstract class CrossModCompat : ILoadable
    {
        public virtual bool ModsLoaded => ModNames.All(x => ModLoader.TryGetMod(x, out _));

        public virtual Mod? Mod { get; set; }

        public abstract IEnumerable<string> ModNames { get; }

        public void Load(Mod mod) {
            Mod = mod;
            
            if (ModsLoaded) LoadCompat();
        }

        public void Unload() {
            if (ModsLoaded) UnloadCompat();
        }

        public virtual void LoadCompat() {
        }
        
        public virtual void UnloadCompat() {
        }
    }
}