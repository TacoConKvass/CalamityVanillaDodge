using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace CalamityVanillaDodge;

public class BalanceConfig : ModConfig {
	public override ConfigScope Mode => ConfigScope.ServerSide;

	[Header("Accessories")]
	[Range(0, 1f)]
	[DefaultValue(.08f)]
	public float ScarfDodgeChance;

	[Range(0, 1f)]
	[DefaultValue(.1f)]
	public float AbyssalMirrorDodgeChance;

	[Range(0, 1f)]
	[DefaultValue(.15f)]
	public float EclipseMirrorDodgeChance;

	[Header("Armor")]
	[Range(0, 1f)]
	[DefaultValue(.02f)]
	public float NinjaArmorDodgeChancePerPiece;

	[Range(0, 1f)]
	[DefaultValue(.04f)]
	public float CrystalAssassinArmorDodgeChancePerPiece;

	[Range(0, 1f)]
	[DefaultValue(.04f)]
	public float SpiderArmorDodgeChancePerPiece;
}