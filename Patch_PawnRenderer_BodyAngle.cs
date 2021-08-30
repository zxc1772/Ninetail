using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using HarmonyLib;
using RimWorld.Planet;
using RimWorld;
using Verse;
using Verse.AI;
using Verse.AI.Group;
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

	[HarmonyPatch(typeof(ThoughtWorker_VeneratedAnimalOnMapOrCaravan), "ShouldHaveThought", new Type[] { typeof(Pawn) })]
	internal class Harmony_ThoughtWorker_VeneratedAnimalOnMapOrCaravan
	{
		[HarmonyPrefix]
		private static bool Prefix(Pawn p, ref ThoughtState __result)
		{
			__result = ThoughtState.Inactive;
			return p.RaceProps.Humanlike;
		}
	}

	[HarmonyPatch(typeof(ThoughtWorker_Precept_Blind), "ShouldHaveThought", new Type[] { typeof(Pawn) })]
	internal class Harmony_ThoughtWorker_Precept_Blind
	{
		[HarmonyPrefix]
		private static bool Prefix(Pawn p, ref ThoughtState __result)
		{
			__result = ThoughtState.Inactive;
			return p.RaceProps.Humanlike;
		}
	}

	[HarmonyPatch(typeof(ThoughtWorker_Precept_HalfBlind), "ShouldHaveThought", new Type[] { typeof(Pawn) })]
	internal class Harmony_ThoughtWorker_Precept_HalfBlind
	{
		[HarmonyPrefix]
		private static bool Prefix(Pawn p, ref ThoughtState __result)
		{
			__result = ThoughtState.Inactive;
			return p.RaceProps.Humanlike;
		}
	}

	[HarmonyPatch(typeof(ThoughtWorker_Precept_NonBlind), "ShouldHaveThought", new Type[] { typeof(Pawn) })]
	internal class Harmony_ThoughtWorker_Precept_NonBlind
	{
		[HarmonyPrefix]
		private static bool Prefix(Pawn p, ref ThoughtState __result)
		{
			__result = ThoughtState.Inactive;
			return p.RaceProps.Humanlike;
		}
	}

	[HarmonyPatch(typeof(ThoughtWorker_Precept_GroinOrChestUncovered), "ShouldHaveThought", new Type[] { typeof(Pawn) })]
	internal class Harmony_ThoughtWorker_Precept_GroinOrChestUncovered
	{
		[HarmonyPrefix]
		private static bool Prefix(Pawn p, ref ThoughtState __result)
		{
			__result = ThoughtState.Inactive;
			return p.RaceProps.Humanlike;
		}
	}

	[HarmonyPatch(typeof(ThoughtWorker_Precept_GroinUncovered), "ShouldHaveThought", new Type[] { typeof(Pawn) })]
	internal class Harmony_ThoughtWorker_Precept_GroinUncovered
	{
		[HarmonyPrefix]
		private static bool Prefix(Pawn p, ref ThoughtState __result)
		{
			__result = ThoughtState.Inactive;
			return p.RaceProps.Humanlike;
		}
	}

	[HarmonyPatch(typeof(ThoughtWorker_Precept_AnyBodyPartCovered), "ShouldHaveThought", new Type[] { typeof(Pawn) })]
	internal class Harmony_ThoughtWorker_Precept_AnyBodyPartCovered
	{
		[HarmonyPrefix]
		private static bool Prefix(Pawn p, ref ThoughtState __result)
		{
			__result = ThoughtState.Inactive;
			return p.RaceProps.Humanlike;
		}
	}

	[HarmonyPatch(typeof(ThoughtWorker_Precept_AnyBodyPartButGroinCovered), "ShouldHaveThought", new Type[] { typeof(Pawn) })]
	internal class Harmony_ThoughtWorker_Precept_AnyBodyPartButGroinCovered
	{
		[HarmonyPrefix]
		private static bool Prefix(Pawn p, ref ThoughtState __result)
		{
			__result = ThoughtState.Inactive;
			return p.RaceProps.Humanlike;
		}
	}

	[HarmonyPatch(typeof(ThoughtWorker_Precept_GroinChestOrHairUncovered), "ShouldHaveThought", new Type[] { typeof(Pawn) })]
	internal class Harmony_ThoughtWorker_Precept_GroinChestOrHairUncovered
	{
		[HarmonyPrefix]
		private static bool Prefix(Pawn p, ref ThoughtState __result)
		{
			__result = ThoughtState.Inactive;
			return p.RaceProps.Humanlike;
		}
	}

	[HarmonyPatch(typeof(ThoughtWorker_Precept_GroinChestHairOrFaceUncovered), "ShouldHaveThought", new Type[] { typeof(Pawn) })]
	internal class Harmony_ThoughtWorker_Precept_GroinChestHairOrFaceUncovered
	{
		[HarmonyPrefix]
		private static bool Prefix(Pawn p, ref ThoughtState __result)
		{
			__result = ThoughtState.Inactive;
			return p.RaceProps.Humanlike;
		}
	}

	[HarmonyPatch(typeof(ThoughtWorker_Dark), "CurrentStateInternal", new Type[] { typeof(Pawn) })]
	internal class Harmony_ThoughtWorker_Dark
	{
		[HarmonyPrefix]
		private static bool Prefix(Pawn p, ref ThoughtState __result)
		{
			__result = ThoughtState.Inactive;
			return p.RaceProps.Humanlike;
		}
	}

	[HarmonyPatch(typeof(ThoughtWorker_LookChangeDesired), "CurrentStateInternal", new Type[] { typeof(Pawn) })]
	internal class Harmony_ThoughtWorker_LookChangeDesired
	{
		[HarmonyPrefix]
		private static bool Prefix(Pawn p, ref ThoughtState __result)
		{
			__result = ThoughtState.Inactive;
			return p.RaceProps.Humanlike;
		}
	}

	[HarmonyPatch(typeof(Alert_Thought), "GetReport")]
	internal class Harmony_Alert_Thought
	{
		[HarmonyPrefix]
		private static bool Prefix()
		{
			return false;
		}
	}

	[HarmonyPatch(typeof(PawnColumnWorker_Slaughter), "HasCheckbox", new Type[] { typeof(Pawn) })]
	internal class Harmony_PawnColumnWorker_Slaughter
	{
		[HarmonyPrefix]
		private static bool Prefix(Pawn pawn, ref bool __result)
		{
			if (pawn.kindDef == PawnKindDefOf.Ninetailfox || pawn.kindDef == PawnKindDefOf.Ninetailfoxwt)
			{
				__result = false;
				return false;
			}
			return true;
		}
	}

	[HarmonyPatch(typeof(PawnColumnWorker_ReleaseAnimalToWild), "HasCheckbox", new Type[] { typeof(Pawn) })]
	internal class Harmony_PawnColumnWorker_ReleaseAnimalToWild
	{
		[HarmonyPrefix]
		private static bool Prefix(Pawn pawn, ref bool __result)
		{
			if (pawn.kindDef == PawnKindDefOf.Ninetailfox || pawn.kindDef == PawnKindDefOf.Ninetailfoxwt)
			{
				__result = false;
				return false;
			}
			return true;
		}
	}

	[HarmonyPatch(typeof(Designator_ReleaseAnimalToWild), "CanDesignateThing", new Type[] { typeof(Thing) })]
	internal class Harmony_Designator_ReleaseAnimalToWild
	{
		[HarmonyPrefix]
		private static bool Prefix(Thing t)
		{
			Pawn pawn = t as Pawn;
			if (pawn == null)
			{
				return true;
			}
			if (pawn.kindDef == PawnKindDefOf.Ninetailfox || pawn.kindDef == PawnKindDefOf.Ninetailfoxwt)
			{
				return false;
			}
			return true;
		}
	}

	[HarmonyPatch(typeof(Designator_Slaughter), "CanDesignateThing", new Type[] { typeof(Thing) })]
	internal class Harmony_Designator_Slaughter
	{
		[HarmonyPrefix]
		private static bool Prefix(Thing t)
		{
			Pawn pawn = t as Pawn;
			if (pawn == null)
			{
				return true;
			}
			if (pawn.kindDef == PawnKindDefOf.Ninetailfox || pawn.kindDef == PawnKindDefOf.Ninetailfoxwt)
			{
				return false;
			}
			return true;
		}
	}

	[HarmonyPatch(typeof(Pawn), "get_IsColonistPlayerControlled")]
	public class Pawn_get_IsColonistPlayerControlled
	{
		[HarmonyPrefix]
		public static bool Prefix(Pawn __instance, ref bool __result)
		{
			if (__instance.kindDef == PawnKindDefOf.Ninetailfox || __instance.kindDef == PawnKindDefOf.Ninetailfoxwt)
			{
				if (__instance.Faction == Faction.OfPlayer && !__instance.Dead && __instance.MentalStateDef == null)
				{
					__result = true;
					return false;
				}
			}
			return true;
		}
	}

	[HarmonyPatch(typeof(Pawn_StyleTracker), "StyleTrackerTick")]
	public class Harmony_Pawn_StyleTrackerTick
	{
		[HarmonyPrefix]
		public static bool Prefix(Pawn_StyleTracker __instance)
		{
			var p = Traverse.Create(__instance).Field("pawn").GetValue() as Pawn;
			if (p.RaceProps.Animal)
			{
				Rand.MTBEventOccurs(20f, 60000f, 2500f);
				return false;
			}
			return true;
		}
	}

	[HarmonyPatch(typeof(WorkGiver_Train), "JobOnThing", new Type[]
	{
		typeof(Pawn),
		typeof(Thing),
		typeof(bool)
	})]
	internal class Patch_WorkGiver_Train_Prefix
	{
		private static bool Prefix(Pawn pawn, Thing t, ref Job __result)
		{
			Pawn pawn2 = t as Pawn;
			if (pawn == pawn2)
			{
				__result = null;
				return false;
			}
			return true;
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
	/*
	[HarmonyPatch(typeof(FloatMenuMakerMap), "ChoicesAtFor")]
	class FloatMenuMakerMap_Patch_Class
    {
		public static bool predicate(Pawn pawn)
        {
			return pawn.RaceProps.Humanlike || pawn.kindDef == PawnKindDefOf.Ninetailfox || pawn.kindDef == PawnKindDefOf.Ninetailfoxwt;
        }

		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
		{
			var get_HumanLike = AccessTools.PropertyGetter(typeof(RaceProperties), "HumanLike");
			var predicate = AccessTools.Method(typeof(FloatMenuMakerMap_Patch_Class), "predicate");

			var insts = new List<CodeInstruction>(instructions);
			var ret = new List<CodeInstruction>();


			int patchEntryPosition = insts.FindIndex((op) => op.Calls(get_HumanLike)) - 2;

			ret.AddRange(ret.Take(patchEntryPosition + 1));
			ret.Add(new CodeInstruction(OpCodes.Ldarg_1));
			ret.Add(new CodeInstruction(OpCodes.Call, predicate));
			ret.AddRange(insts.Skip(patchEntryPosition + 2));

			foreach(var inst in ret)
            {
				Log.Message($"opcode: {inst.opcode}\toperand: {inst.operand}");
            }

			return ret;
		}
	}
	*/
}
