using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using WeaponWard.Content.Components.Items;
using WeaponWard.Core.Abstractions.Content;
using WeaponWard.Core.Utilities;

namespace WeaponWard.Content.Items.Melee
{
    
    public class YoMama : WardItem
    {
        public override WardItemType ItemType => WardItemType.Original;
        public override string ItemAsylumWikiLink => "https://the-roblox-item-asylum.fandom.com/wiki/Yo_mama";

        public SoundStyle MusicStyle = new($"{nameof(WeaponWard)}/Assets/Music/yo_mama") {
            MaxInstances = 0
        };

        private SlotId _soundSlot;
        private ActiveSound _activeSound;

        private float _cdFloat = 0.7f; // use delay?

        private int _timer;
        private int _timerJoke;

        public static Dictionary<string, SoundStyle> YoMamaJokes = new(); // <name, (text to show in chat, joke sfx)>
        private static Dictionary<string, string> _jokeTexts = new();
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yo mama"); // localization later, lazy.
            Tooltip.SetDefault("brody foxx says this funny line"); // again.

            YoMamaJokes = FolderLoadingUtils.LoadSoundStylesFromFolder(Mod, "Assets", "Sounds", "YoMama");

            foreach (var joke in YoMamaJokes)
            {
                if (joke.Key.ToLower().Contains("drpepper"))
                    _jokeTexts[joke.Key] = "Yo mama so STUPID she made an appointment with DR PEPPER!";
                if (joke.Key.ToLower().Contains("onion"))
                    _jokeTexts[joke.Key] = "Yo mama so UGLY she made an ONION CRY!";
                if (joke.Key.ToLower().Contains("worldwide"))
                    _jokeTexts[joke.Key] = "Yo mama so FAT she doesn't need internet, she's already WORLDWIDE!";
            }
        }

        private static void SayJoke(Player player, int element)
        {
            SoundEngine.PlaySound(YoMamaJokes.ElementAt(element).Value, player.Center);
            Main.NewText($"<{player.name}> {_jokeTexts.ElementAt(element).Value}");
        }
        public override void SetDefaults()
        {
            Item.useTime = 100;
            Item.useAnimation = 100;
            Item.autoReuse = false;
            Item.damage = 50;
            Item.knockBack = 2f;
            Item.useStyle = 100;
            Item.noUseGraphic = true;
            Item.width = 20;
            Item.height = 20;
        }
        public override void UpdateInventory(Player player)
        {
            if (player.itemAnimation == player.itemAnimationMax)
                _timerJoke = 0;
            if (player.HeldItem.ModItem != this && _soundSlot.IsValid)
                _timer = 0;
            else
                _timer++;

            if (_timer == 1)
                _soundSlot = SoundEngine.PlaySound(MusicStyle, player.Center);
            else if (_timer == 0)
                DoStop();

            if (_timer > 0)
                if (SoundEngine.TryGetActiveSound(_soundSlot, out var result))
                    result.Position = player.Center;
        }
        public void DoStop()
        {
            var soundRecieved = SoundEngine.TryGetActiveSound(_soundSlot, out var sound);

            if (soundRecieved)
                sound?.Stop();
        }
        public override bool? UseItem(Player player)
        {
            if (_timerJoke == 1)
                SayJoke(player, Main.rand.Next(0, YoMamaJokes.Count));
            _timerJoke++;
            DoStop();
            return base.UseItem(player);
        }
        public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            var pUseTime = player.itemAnimation;

            var cdNormal = (int)(Item.useTime * _cdFloat);
            var cdNormalNoDamage = (int)(Item.useTime * 0.6f);

            var extensionX = 40;

            if (pUseTime >= cdNormal) {
                hitbox = new(0, 0, 0, 0);
                noHitbox = true;
            }
            else {
                if (pUseTime >= cdNormalNoDamage)
                    hitbox = new((int)player.position.X, (int)player.position.Y, player.width + extensionX, player.height);
                noHitbox = false;
            }
        }
        public override void UseItemFrame(Player player)
        {
            var pUseTime = player.itemAnimation;

            var cdNormal = (int)(Item.useTime * _cdFloat);

            if (pUseTime <= cdNormal)
                player.bodyFrame.Y = player.bodyFrame.Height * 3; // arm up
            else
                player.bodyFrame.Y = player.bodyFrame.Height * 1; // arm forward
        }
    }
}
