using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using HarmonyLib;
using RimWorld;
using Verse;

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

	[HarmonyPatch(typeof(Ideo), "IsVeneratedAnimal",
				  new Type[] { typeof(ThingDef).MakeByRefType() })]
	class IsVeneratedAnimal_Patch_Class
	{
		public static bool Prefix(ThingDef thingDef, Ideo __instance, ref bool __result)
		{
			var precepts = Traverse.Create(__instance).Field("precepts = new List<Precept>()").GetValue() as List<Precept>;
			if (!ModsConfig.IdeologyActive)
			{
				__result = false;
				return false;
			}
			if (thingDef == null || !thingDef.race.Animal)
			{
				__result = false;
				return false;
			}
			using (List<Precept>.Enumerator enumerator = precepts.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Precept_Animal precept_Animal;
					if ((precept_Animal = (enumerator.Current as Precept_Animal)) != null && thingDef == precept_Animal.ThingDef)
					{
						__result = true;
						return false;
					}
				}
			}
			__result = false;
			return false;
		}
	}
}
