using Terraria;
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

		/*#region IDrawableHeldItem

		public IDrawableHeldItem.HeldItemDrawData DrawData { get; set; }
		
		public Rectangle GetSourceRect(Texture2D texture) {
			return texture.MakeSourceRect();
		}

		public void DrawHeldItem(SpriteBatch spriteBatch, Texture2D texture, Color color, SpriteEffects spriteEffects) {
			this.DrawAtItem(spriteBatch, texture, color, spriteEffects);
		}

		public void DrawLayer(SpriteBatch spriteBatch, HeldItemDrawLayer.HeldItemContext context) {
			if (!context.DrawingDisabled || context.DrawSet.drawPlayer.itemAnimation <= 0) return;
			
			Main.NewText(context.DrawSet.drawPlayer.itemAnimation);
			
			Player player = context.DrawSet.drawPlayer;
			DrawData = new IDrawableHeldItem.HeldItemDrawData(player.itemLocation, player.itemRotation, player.gfxOffY, player.direction);
			
			DrawHeldItem(
				Main.spriteBatch,
				ModContent.Request<Texture2D>(Texture).Value,
				Main.DiscoColor,
				DrawUtils.SpriteEffectsFromDirection(DrawData.Direction)
			);
		}

		#endregion*/
	}
}