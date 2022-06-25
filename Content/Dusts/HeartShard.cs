using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace WeaponWard.Content.Dusts
{
    public class HeartShard : ModDust
    {
        public override string Texture => $"{nameof(WeaponWard)}/Assets/Textures/undertale_heart_shard";

        public override bool Update(Dust gore)
        {
            // maybe something here?
            return base.Update(gore);
        }
    }
}
