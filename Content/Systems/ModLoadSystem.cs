using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using WeaponWard.Content.Players;

namespace WeaponWard.Content.Systems
{
    public class ModLoadSystem : ModSystem
    {
        public List<Action> ModLoadEvents = new();

        public void SetupDefaultLoadEvents()
        {
            ModLoadEvents.Add(() => {
                CustomDeathSounds.Initialize(Mod);
            });
        }
        public void DoLoadEvents()
        {
            ModLoadEvents.ForEach(e => e?.Invoke());
        }
        public override void OnModLoad()
        {
            SetupDefaultLoadEvents();
            DoLoadEvents();
        }
    }
}
