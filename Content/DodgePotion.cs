using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityVanillaDodge.Content;

public class DodgePotion : ModItem {
	public override void SetStaticDefaults() {
		Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(240, 240, 240),
			new Color(200, 200, 200),
			new Color(140, 140, 140)
		];
	}

	public override void SetDefaults() {
		Item.Size = new Vector2(20, 26);
		Item.maxStack = Item.CommonMaxStack;
		Item.consumable = true;

		Item.useStyle = ItemUseStyleID.DrinkLiquid;
		Item.useAnimation = 15;
		Item.useTime = 15;
		Item.useTurn = true;
		Item.UseSound = SoundID.Item3;

		Item.buffType = ModContent.BuffType<DodgeBuff>();
		Item.buffTime = 2 * 60 * 60; // 2 minutes

		Item.rare = ItemRarityID.Orange;
		Item.value = Item.buyPrice(gold: 1);
	}

	public override void AddRecipes() {
		Recipe recipe = CreateRecipe()
			.AddIngredient(ItemID.BottledWater, 1)
			.AddIngredient(ItemID.Blinkroot, 1)
			.AddIngredient(ItemID.SandBlock, 1)
			.AddTile(TileID.Bottles)
			.Register();

		Recipe recipe2 = CreateRecipe(3)
			.AddIngredient(ItemID.BottledWater, 1)
			.AddIngredient(ItemID.Blinkroot, 1)
			.AddIngredient(ItemID.SandBlock, 1)
			.AddTile(TileID.AlchemyTable)
			.Register();
	}
}

public class DodgeBuff : ModBuff {
	public float DodgeChance => ModContent.GetInstance<BalanceConfig>().DodgeBuffDodgeChance;
	
	public override void Update(Player player, ref int buffIndex) {
		player.GetModPlayer<DodgePlayer>().DodgeChance += DodgeChance;
	}
}