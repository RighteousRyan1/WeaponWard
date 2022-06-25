using System.Collections.Generic;
using System.Linq;
using Terraria.ID;
using WeaponWard.Content.Items.Melee;

namespace WeaponWard.Core.CrossMod
{
    public class VanillaCompat : CrossModCompat
    {
        public override IEnumerable<string> ModNames {
            get { yield return ""; }
        }

        public override bool ModsLoaded => true;

        public override void LoadCompat() {
            base.LoadCompat();
            
            static LampManifest W(int id, int style) {
                return new LampManifest(id, style);
            }

            List<int> items = new()
            {
                ItemID.NebulaLamp, ItemID.SolarLamp, ItemID.StardustLamp, ItemID.VortexLamp, ItemID.BoneLamp, ItemID.LesionLamp,
                ItemID.FleshLamp, ItemID.GlassLamp, ItemID.HoneyLamp, ItemID.FrozenLamp, ItemID.LihzahrdLamp, ItemID.LivingWoodLamp,
                ItemID.SkywareLamp, ItemID.SlimeLamp, ItemID.SteampunkLamp, ItemID.BambooLamp, ItemID.BorealWoodLamp, ItemID.CactusLamp,
                ItemID.CrystalLamp, ItemID.DynastyLamp, ItemID.EbonwoodLamp, ItemID.GraniteLamp, ItemID.MarbleLamp, ItemID.MartianLamppost,
                ItemID.MeteoriteLamp, ItemID.MushroomLamp, ItemID.PalmWoodLamp, ItemID.PearlwoodLamp, ItemID.PumpkinLamp,
                ItemID.RichMahoganyLamp, ItemID.SandstoneLamp, ItemID.ShadewoodLamp, ItemID.SpiderLamp, ItemID.SpookyLamp,
                ItemID.BlueDungeonLamp, ItemID.BlueDungeonLamp, ItemID.GreenDungeonLamp, ItemID.PinkDungeonLamp, ItemID.ObsidianLamp,
                ItemID.GoldenLamp,
            };

            List<LampManifest> weaponManifests = items.Select(x => ContentSamples.ItemsByType[x])
                                                      .Select(item => W(item.createTile, item.placeStyle))
                                                      .ToList();

            Lamp.Manifests.AddRange(weaponManifests);
        }
    }
}