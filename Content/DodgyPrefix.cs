using Humanizer;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityVanillaDodge.Content;

public class DodgyPrefix : ModPrefix {
	public float DodgeChance => ModContent.GetInstance<BalanceConfig>().DodgeBuffDodgeChance;

	public override PrefixCategory Category => PrefixCategory.Accessory;

	public override void ApplyAccessoryEffects(Player player) {
		player.GetModPlayer<DodgePlayer>().DodgeChance += DodgeChance;
	}

	public override void ModifyValue(ref float valueMult) => valueMult += .1f;

	public override IEnumerable<TooltipLine> GetTooltipLines(Item item) {
		yield return new TooltipLine(Mod, "DodgePrefix", Language.GetTextValue("Mods.CalamityVanillaDodge.DodgyPrefix").FormatWith(DodgeChance * 100)) { 
			IsModifier = true
		};
	}
}