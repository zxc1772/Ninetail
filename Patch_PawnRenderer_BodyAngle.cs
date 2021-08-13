using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using HarmonyLib;
using RimWorld;
using Verse;
using System.Data.OleDb;

namespace Ninetail
{
	[HarmonyPatch(typeof(PawnRenderer))]
	[HarmonyPatch("BodyAngle")]
	internal class Patch_PawnRenderer_BodyAngle
	{
		private static void Postfix(PawnRenderer __instance, ref float __result)
		{
			Pawn value = Traverse.Create(__instance).Field("pawn").GetValue<Pawn>();
			if (value.def.thingClass.ToString() == "Ninetail.AMP_SpecialBody" && !value.Awake() && !value.Dead)
			{
				__result = 0f;
			}
		}
	}

	[HarmonyPatch(typeof(Pawn_IdeoTracker))]
	[HarmonyPatch("IdeoTrackerTick")]
	internal class Harmony_Pawn_IdeoTracker
	{
		private static bool Prefix(Pawn_IdeoTracker __instance)
		{
			var p = Traverse.Create(__instance).Field("pawn").GetValue() as Pawn;
			if (p != null && !p.Destroyed && !p.InMentalState && p.RaceProps.Humanlike)
			{
				return true;
			}
			return false;
		}
	}

	[HarmonyPatch(typeof(ThoughtWorker_BondedAnimalMaster), "GetAnimals", new Type[]{	typeof(Pawn), typeof(List<string>) })]
	internal class Harmony_GetAnimals
	{
		[HarmonyPrefix]
		private static bool Prefix(Pawn p, List<string> outAnimals)
		{
			outAnimals.Clear();
			return p.RaceProps.Humanlike;
		}
	}

	[HarmonyPatch(typeof(ThoughtWorker_ApparelDamaged), "CurrentStateInternal", new Type[] { typeof(Pawn)})]
	internal class Harmony_ThoughtWorker_ApparelDamaged
	{
		[HarmonyPrefix]
		private static bool Prefix(Pawn p, ref ThoughtState __result)
		{
			__result = ThoughtState.Inactive;
			return p.RaceProps.Humanlike;
		}
	}

	[HarmonyPatch(typeof(ThoughtWorker_WrongApparelGender), "CurrentStateInternal", new Type[] { typeof(Pawn) })]
	internal class Harmony_ThoughtWorker_WrongApparelGender
	{
		[HarmonyPrefix]
		private static bool Prefix(Pawn p, ref ThoughtState __result)
		{
			__result = ThoughtState.Inactive;
			return p.RaceProps.Humanlike;
		}
	}

	[HarmonyPatch(typeof(ThoughtWorker_DeadMansApparel), "CurrentStateInternal", new Type[] { typeof(Pawn) })]
	internal class Harmony_ThoughtWorker_DeadMansApparel
	{
		[HarmonyPrefix]
		private static bool Prefix(Pawn p, ref ThoughtState __result)
		{
			__result= ThoughtState.Inactive;
			return p.RaceProps.Humanlike;
		}
	}

	[HarmonyPatch(typeof(Ideo), "IsVeneratedAnimal", argumentTypes: new Type[] { typeof(ThingDef) })]
	class IsVeneratedAnimal_Patch_Class
	{
		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
			var insts = new List<CodeInstruction>(instructions);
			var ret = new List<CodeInstruction>();
			var opInequalityMethod = AccessTools.Method(typeof(Type), "op_Inequality");
			var getTypeFromHandleMethod = AccessTools.Method(typeof(Type), "GetTypeFromHandle");
			var thingClassFieldInfo = AccessTools.Field(typeof(ThingDef), "thingClass");

			int i = 0;
			for (; i < insts.Count; i++)
            {
				var oldCode = insts.ElementAt(i);

				// keep continuing until target IL code appears
				var matched = oldCode.opcode == OpCodes.Brtrue_S && insts[i - 1].opcode == OpCodes.Call && insts[i + 1].opcode == OpCodes.Ldarg_1;
				if (!matched)
                {
					ret.Add(oldCode);
					continue;
                }

				// inject "&& thingDef.thingClass != typeof(Ninetail.AMP_SpecialBody)"
				ret.Add(new CodeInstruction(OpCodes.Ldarg_1));
				ret.Add(new CodeInstruction(OpCodes.Ldfld, operand: thingClassFieldInfo));
				ret.Add(new CodeInstruction(OpCodes.Ldtoken, operand: typeof(Ninetail.AMP_SpecialBody)));
				ret.Add(new CodeInstruction(OpCodes.Call, operand: getTypeFromHandleMethod));
				ret.Add(new CodeInstruction(OpCodes.Call, operand: opInequalityMethod));
				ret.Add(new CodeInstruction(OpCodes.And));
				ret.Add(oldCode); // brtrue.s -> return false
            }

			// copy rest code
			for (; i < insts.Count; i++)
            {
				var oldCode = insts.ElementAt(i);
				ret.Add(oldCode);
            }

			return ret;
        }
	}
}
