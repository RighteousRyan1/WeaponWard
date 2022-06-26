using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using WeaponWard.Content.Effects;

namespace WeaponWard.Content.Globals
{
    public class RealKnifeNPC : GlobalNPC
    {
        public override void OnHitByItem(NPC npc, Player player, Item item, int damage, float knockback, bool crit)
        {
            /*if (npc.life <= 0)
            {
                // TODO: for when the "real knife" item is implemented.
                if (item.ModItem is RealKnife)
                {
                    new HeartBreak(npc.Center, 20, 80)
                    {
                        HeartScale = 2f
                    };
                }
            }*/
        }
    }
}
