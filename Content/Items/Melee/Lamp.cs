using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WeaponWard.Content.Components.Items;
using WeaponWard.Core.Abstractions.Content;

namespace WeaponWard.Content.Items.Melee
{
	public class Lamp : WardItem
	{
		public override IWardItem.WardItemType ItemType => IWardItem.WardItemType.Adapted;
		
		public override void SetDefaults()
		{
			Item.damage = 50;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = 1;
			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = 2;
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
	}
}