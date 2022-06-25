using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using WeaponWard.Content.Components.Items;
using WeaponWard.Core.Abstractions.Content;
using WeaponWard.Core.Utilities;

namespace WeaponWard.Content.Items.Melee
{
	public readonly record struct LampManifest(int tileId, int tileStyle);

	public class Lamp : WardItem
	{
		public static List<LampManifest> Manifests = new();

		public override WardItemType ItemType => WardItemType.Adapted;

		public override string ItemAsylumWikiLink => "https://itemasylum.fandom.com/wiki/Lamp";
		
		protected int UseItemIndex;

		public override void SetDefaults() {
			Item.damage = 50;
			Item.DamageType = DamageClass.Melee;
			Item.width = 20;
			Item.height = 20;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}
		
		public override bool? UseItem(Player player) {
			UseItemIndex = Main.rand.Next(Manifests.Count);
			return base.UseItem(player);
		}

		public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox) {
			base.UseItemHitbox(player, ref hitbox, ref noHitbox);

			(int tileId, int tileStyle) = Manifests[UseItemIndex];
			Texture2D? texture = TileUtils.GetMultitileTexture(new TileUtils.TileDrawData(tileId, tileStyle));

			if (texture is null) return;
			
			// Square
			int diffW = hitbox.Width - texture.Height;
			int diffH = hitbox.Height - texture.Height;
			hitbox.X -= diffW;
			hitbox.Y -= diffH;
			hitbox.Width = texture.Height;
			hitbox.Height = texture.Height;
		}

		public override bool PreCacheDrawData(ref PlayerDrawSet drawInfo, ref DrawData drawData) {
			return false;
		}

		public override void PostCacheDrawData(PlayerDrawSet drawInfo, DrawData drawData) {
			base.PostCacheDrawData(drawInfo, drawData);

			(int tileId, int tileStyle) = Manifests[UseItemIndex];
			drawData.color = Main.DiscoColor;
			drawData.texture = TileUtils.GetMultitileTexture(new TileUtils.TileDrawData(tileId, tileStyle));
			drawInfo.DrawDataCache.Add(drawData);
		}
	}
}