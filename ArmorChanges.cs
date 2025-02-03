using Humanizer;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityVanillaDodge;

public class ArmorChanges : GlobalItem {
	public override void UpdateEquip(Item item, Player player) {
		DodgePlayer modPlayer = player.GetModPlayer<DodgePlayer>();
		BalanceConfig balanceConfig = ModContent.GetInstance<BalanceConfig>();

		if (item.type == ItemID.NinjaHood || item.type == ItemID.NinjaPants || item.type == ItemID.NinjaShirt) 
			modPlayer.DodgeChance += balanceConfig.NinjaArmorDodgeChancePerPiece;
		
		if (item.type == ItemID.CrystalNinjaChestplate || item.type == ItemID.CrystalNinjaHelmet || item.type == ItemID.CrystalNinjaLeggings) 
			modPlayer.DodgeChance += balanceConfig.CrystalAssassinArmorDodgeChancePerPiece;
		
		if (item.type == ItemID.SpiderGreaves || item.type == ItemID.SpiderBreastplate || item.type == ItemID.SpiderMask) 
			modPlayer.DodgeChance += balanceConfig.SpiderArmorDodgeChancePerPiece;
	}

	public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
		BalanceConfig balanceConfig = ModContent.GetInstance<BalanceConfig>();

		if (item.type == ItemID.NinjaHood || item.type == ItemID.NinjaPants || item.type == ItemID.NinjaShirt) {
			tooltips.Add(new TooltipLine(Mod, "Dodge", Language.GetTextValue("Mods.CalamityVanillaDodge.GrantsDodge")));
			tooltips.Add(new TooltipLine(Mod, "Dodge", Language.GetTextValue("Mods.CalamityVanillaDodge.DodgeChanceLine").FormatWith(balanceConfig.NinjaArmorDodgeChancePerPiece * 100)));
		}

		if (item.type == ItemID.CrystalNinjaChestplate || item.type == ItemID.CrystalNinjaHelmet || item.type == ItemID.CrystalNinjaLeggings) {
			tooltips.Add(new TooltipLine(Mod, "Dodge", Language.GetTextValue("Mods.CalamityVanillaDodge.GrantsDodge")));
			tooltips.Add(new TooltipLine(Mod, "Dodge", Language.GetTextValue("Mods.CalamityVanillaDodge.DodgeChanceLine").FormatWith(balanceConfig.CrystalAssassinArmorDodgeChancePerPiece * 100)));
		}

		if (item.type == ItemID.SpiderGreaves || item.type == ItemID.SpiderBreastplate || item.type == ItemID.SpiderMask) {
			tooltips.Add(new TooltipLine(Mod, "Dodge", Language.GetTextValue("Mods.CalamityVanillaDodge.GrantsDodge")));
			tooltips.Add(new TooltipLine(Mod, "Dodge", Language.GetTextValue("Mods.CalamityVanillaDodge.DodgeChanceLine").FormatWith(balanceConfig.SpiderArmorDodgeChancePerPiece * 100)));
		}
	}
}