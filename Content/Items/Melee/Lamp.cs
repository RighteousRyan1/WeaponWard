using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WeaponWard.Content.Components.Items;
using WeaponWard.Core.Abstractions.Content;

namespace WeaponWard.Content.Items.Melee
{
	public class Lamp : WardItem
	{
		public override WardItemType ItemType => WardItemType.Adapted;
		
		public override string ItemAsylumWikiLink => "https://itemasylum.fandom.com/wiki/Lamp";

        public override void SetDefaults() {
			Item.damage = 50;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}

		public override bool PreCacheDrawData(ref PlayerDrawSet drawInfo, ref DrawData drawData) {
			return false;
		}

		public override void PostCacheDrawData(PlayerDrawSet drawInfo, DrawData drawData) {
			base.PostCacheDrawData(drawInfo, drawData);

			drawData.color = Main.DiscoColor;
			drawData.texture = ModContent.Request<Texture2D>(Texture).Value;
			drawInfo.DrawDataCache.Add(drawData);
		}
	}
}