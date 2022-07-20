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

        private int _timer;

        public static Dictionary<string, (string, SoundStyle)> YoMamaJokes = new() // <name, (text to show in chat, joke sfx)>
        {

        };
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yo mama"); // localization later, lazy.
            Tooltip.SetDefault("brody foxx says this funny line"); // again.
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
        }
        public override void HoldItem(Player player)
        {
        }
        public override void UpdateInventory(Player player)
        {
            if (player.HeldItem.ModItem != this && _soundSlot.IsValid)
                _timer = 0;
            else
                _timer++;

            if (_timer == 1)
                _soundSlot = SoundEngine.PlaySound(MusicStyle, player.Center);
            else if (_timer == 0)
                DoStop();

            if (_timer > 0)
                if (_activeSound != null)
                    _activeSound.Position = player.Center;
        }
        public void DoStop()
        {
            var soundRecieved = SoundEngine.TryGetActiveSound(_soundSlot, out var sound);

            if (soundRecieved)
                sound?.Stop();
        }
        public override bool? UseItem(Player player)
        {
            DoStop();
            return base.UseItem(player);
        }
        public override void UseItemFrame(Player player)
        {
            var pUseTime = player.itemAnimation;

            var cdFloat = 0.7f;
            var cdNormal = (int)(Item.useTime * cdFloat);

            if (pUseTime <= cdNormal)
                player.bodyFrame.Y = player.bodyFrame.Height * 3; // arm up
            else
                player.bodyFrame.Y = player.bodyFrame.Height * 1; // arm forward
        }
    }
}
