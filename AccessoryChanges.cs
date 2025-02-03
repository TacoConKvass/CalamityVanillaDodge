using CalamityMod.Items.Accessories;
using Humanizer;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityVanillaDodge;

public class AccessoryChanges : GlobalItem {
	public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
		BalanceConfig balanceConfig = ModContent.GetInstance<BalanceConfig>();
		
		if (item.type == ModContent.ItemType<AbyssalMirror>()) 
			ReplaceDodgeLine(ref tooltips, balanceConfig.AbyssalMirrorDodgeChance);

		if (item.type == ModContent.ItemType<EclipseMirror>()) 
			ReplaceDodgeLine(ref tooltips, balanceConfig.EclipseMirrorDodgeChance);
		
		if (item.type == ModContent.ItemType<StatisNinjaBelt>() || item.type == ModContent.ItemType<StatisVoidSash>()) 
			ReplaceDodgeLine(ref tooltips, .1f);

		if (item.type == ModContent.ItemType<AmalgamatedBrain>() || item.type == ModContent.ItemType<TheAmalgam>())
			ReplaceDodgeLine(ref tooltips, .16f);

		if (item.type == ModContent.ItemType<EvasionScarf>() || item.type == ModContent.ItemType<CounterScarf>()) {
			int startIndex = tooltips.FindIndex(0, tooltips.Count, (line) => line.Text.StartsWith("Grants the ability to dash"));
			tooltips[startIndex].Text += Language.GetTextValue("Mods.CalamityVanillaDodge.DodgeChanceOnDash").FormatWith(balanceConfig.ScarfDodgeChance * 100);
			tooltips[startIndex + 1].Hide();
		}
	}

	public static void ReplaceDodgeLine(ref List<TooltipLine> tooltips, float chance) {
		int startIndex = tooltips.FindIndex(0, tooltips.Count, (line) => line.Text.EndsWith("and dodge attacks") || line.Text.StartsWith("Grants the ability to evade") || line.Text.StartsWith("Grants the ability to dodge"));
		if (startIndex < 0) return;
		
		for (int i = 0; i < 2; i++) tooltips[startIndex + 1 + i].Hide();
		tooltips[startIndex].Text += "\n" + Language.GetTextValue("Mods.CalamityVanillaDodge.DodgeChanceLine").FormatWith(chance * 100);
		
		startIndex = tooltips.FindIndex(0, tooltips.Count, (line) => line.Text.StartsWith("This cooldown"));
		if (startIndex < 0) return;

		tooltips[startIndex].Hide();
	}
}