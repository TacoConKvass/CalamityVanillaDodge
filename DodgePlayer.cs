using CalamityMod.NPCs.Perforator;
using Terraria;
using Terraria.ModLoader;

namespace CalamityVanillaDodge;

public class DodgePlayer : ModPlayer {
	public float DodgeChance = 0f;

	public override void ResetEffects() => DodgeChance = 0f;

	public override bool ConsumableDodge(Player.HurtInfo info) {
		if (Main.rand.NextFloat() < DodgeChance && Player.whoAmI == Main.myPlayer) {
			Player.NinjaDodge();
			return true;
		}

		return false;
	}
}