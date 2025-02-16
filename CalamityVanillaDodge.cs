using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.ILEditing;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using Terraria;
using Terraria.ModLoader;
using System.Reflection;
using System;
using CalamityMod.CalPlayer.Dashes;
using CalamityMod.Items;
using System.Collections.Generic;
using Terraria.ID;

namespace CalamityVanillaDodge;

public class CalamityVanillaDodge : Mod {
	public ILHook[] ILHookList = [
		Cal_CounterScarfChance,
		Cal_ConsumableDodge,
		RemoveCooldownAdd("CounterScarfDodge", 3, BindingFlags.NonPublic | BindingFlags.Instance),
		RemoveCooldownAdd("EclipseMirrorDodge", 4),
		RemoveCooldownAdd("AbyssMirrorDodge", 4)
	];
	
	public override void Load() {
		foreach (ILHook hook in ILHookList) hook.Apply();
		Cal_VanillaDodgeTooltips.Apply();
		IL_Player.Hurt_PlayerDeathReason_int_int_refHurtInfo_bool_bool_int_bool_float_float_float -= ILChanges.DodgeMechanicAdjustments;
		IL_Player.Hurt_PlayerDeathReason_int_int_refHurtInfo_bool_bool_int_bool_float_float_float += (ILContext il) => {
			ILCursor cursor = new ILCursor(il);
			if (!cursor.TryGotoNext(MoveType.After, i => i.MatchCall<Player.HurtModifiers>("ToHurtInfo"))) return;
			if (!cursor.TryGotoNext(MoveType.Before, i => i.MatchBrfalse(out ILLabel _))) return;

			cursor.Emit(OpCodes.Ldarg_0);
			cursor.EmitDelegate((Player player) => !player.Calamity().disableAllDodges);
			cursor.Emit(OpCodes.And);
		};
	}

	public override void Unload() {
		foreach (ILHook hook in ILHookList) hook.Undo();
		Cal_VanillaDodgeTooltips.Undo();
	}

	public static ILHook Cal_ConsumableDodge = new ILHook(
		typeof(CalamityPlayer).GetMethod("ConsumableDodge", BindingFlags.Public | BindingFlags.Instance),
		(ILContext il) => {
			ILCursor cursor = new ILCursor(il);

			if (cursor.TryGotoNext(MoveType.After, i => i.MatchConvI4()))
				cursor.EmitDelegate((int value) => 0);

			if (cursor.TryGotoNext(MoveType.After, i => i.MatchAnd())) 
				cursor.EmitDelegate((bool flag) => false);

			if (cursor.TryGotoNext(MoveType.After, i => i.MatchLdfld<CalamityPlayer>("eclipseMirror"))) 
				cursor.EmitDelegate((bool value) => value && Main.rand.NextFloat() < ModContent.GetInstance<BalanceConfig>().EclipseMirrorDodgeChance);
			if (cursor.TryGotoNext(MoveType.After, i => i.MatchLdfld<CalamityPlayer>("abyssalMirror")))
				cursor.EmitDelegate((bool value) => value && Main.rand.NextFloat() < ModContent.GetInstance<BalanceConfig>().AbyssalMirrorDodgeChance);
		}, 
		false
	);

	public static ILHook Cal_CounterScarfChance = new ILHook(
		typeof(CalamityPlayer).GetMethod("HandleDashDodges", BindingFlags.Public | BindingFlags.Instance),
		(ILContext il) => {
			ILCursor cursor = new ILCursor(il);

			if (cursor.TryGotoNext(MoveType.After, 
				i => i.MatchCall<CounterScarfDash>("get_ID"),
				i => i.MatchCall<String>("op_Equality"))
			) 
				cursor.EmitDelegate((bool value) => value && Main.rand.NextFloat() < ModContent.GetInstance<BalanceConfig>().ScarfDodgeChance);
		},
		false
	);

	public static Hook Cal_VanillaDodgeTooltips = new Hook(
		typeof(CalamityGlobalItem).GetMethod("ModifyVanillaTooltips", BindingFlags.NonPublic | BindingFlags.Static),
		(Action<Item, IList<TooltipLine>> orig, Item item, IList<TooltipLine> tooltips) => {
			if (item.type == ItemID.BrainOfConfusion || item.type == ItemID.BlackBelt || item.type == ItemID.MasterNinjaGear) return;
			orig(item, tooltips);
		},
		false
	);

	public static ILHook RemoveCooldownAdd(string methodName, int popCount, BindingFlags flags = BindingFlags.Public | BindingFlags.Instance) {
		return new ILHook(
			typeof(CalamityPlayer).GetMethod(methodName, flags),
			(ILContext il) => {
				ILCursor cursor = new ILCursor(il);
				while (cursor.TryGotoNext(MoveType.Before, i => i.MatchCall(typeof(CalamityUtils), "AddCooldown"))) {
					cursor.Remove();
					for (int i = 0; i < popCount; i++) {
						cursor.EmitPop();
					}
				}
			},
			false
		);
	}
}