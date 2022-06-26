using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;
using WeaponWard.Content.Dusts;

namespace WeaponWard.Content.Effects
{
    public class HeartBreak
    {
        public static List<HeartBreak> HeartBreaks = new();

        public static SoundStyle CrackSound = new($"{nameof(WeaponWard)}/Assets/Sounds/undertale_heart_crack");
        public static SoundStyle BreakSound = new($"{nameof(WeaponWard)}/Assets/Sounds/undertale_heart_break");

        public int TicksExisted { get; private set; }
        public int TimeCracked { get; private set; }
        public bool Broken { get; private set; }

        public readonly int TicksBeforeCrack;
        public readonly int TicksBeforeBreak;

        public float HeartScale = 1f;
        public float ShardScale = 1f;

        public Vector2 Position;

        public Color Color = Color.Red;

        private Texture2D _textureToUse;

        public readonly int Id;

        public HeartBreak(Vector2 position, int ticksBeforeCrack, int ticksBeforeBreak, Color colorOverride = default)
        {
            TicksBeforeBreak = ticksBeforeBreak;
            TicksBeforeCrack = ticksBeforeCrack;

            Position = position;

            if (colorOverride != default)
                Color = colorOverride;
            _textureToUse = ModContent.GetInstance<WeaponWard>().Assets.Request<Texture2D>($"Assets/Textures/undertale_heart_full", AssetRequestMode.ImmediateLoad).Value;

            HeartBreaks.Add(this);

            Id = HeartBreaks.FindIndex(x => x == this);
        }

        private void Crack()
        {
            _textureToUse = ModContent.GetInstance<WeaponWard>().Assets.Request<Texture2D>($"Assets/Textures/undertale_heart_cracked", AssetRequestMode.ImmediateLoad).Value;
            SoundEngine.PlaySound(CrackSound, Position);
        }
        private void Break()
        {
            Broken = true;
            for (int i = 0; i < 6; i++)
            {
                Vector2 velocity = Vector2.UnitY.RotatedBy(Main.rand.NextFloat(MathHelper.TwoPi));
                float magnitude = 3.5f;
                var dust = Dust.NewDustDirect(Position, 1, 1, ModContent.DustType<HeartShard>(), velocity.X * magnitude, velocity.Y * magnitude, 0, Color, ShardScale);
            }
            SoundEngine.PlaySound(BreakSound, Position);
        }

        public void Render(SpriteBatch spriteBatch)
        {
            // handle some logic here too
            TicksExisted++;
            if (TicksExisted == TicksBeforeCrack)
                Crack();
            if (TicksExisted == TicksBeforeBreak)
                Break();


            if (!Broken)
                spriteBatch.Draw(_textureToUse, Position - Main.screenPosition, null, Color, 0f, _textureToUse.Size() / 2, HeartScale, default, 0f);
        }
    }
    public class HeartSource : IEntitySource
    {
        public string? Context { get; set; }
    }
}
