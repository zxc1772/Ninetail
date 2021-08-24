using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace VoiceroidAsAnimal_Harmony
{
	// Token: 0x02000041 RID: 65
	[HarmonyPatch(typeof(FoodUtility), "BestFoodSourceOnMap", new Type[]
	{
		typeof(Pawn),
		typeof(Pawn),
		typeof(bool),
		typeof(ThingDef),
		typeof(FoodPreferability),
		typeof(bool),
		typeof(bool),
		typeof(bool),
		typeof(bool),
		typeof(bool),
		typeof(bool),
		typeof(bool),
		typeof(bool),
		typeof(bool),
		typeof(bool),
		typeof(bool),
		typeof(FoodPreferability),
		typeof(float)
	}, new ArgumentType[]
	{
		ArgumentType.Normal,
		ArgumentType.Normal,
		ArgumentType.Normal,
		ArgumentType.Out,
		ArgumentType.Normal,
		ArgumentType.Normal,
		ArgumentType.Normal,
		ArgumentType.Normal,
		ArgumentType.Normal,
		ArgumentType.Normal,
		ArgumentType.Normal,
		ArgumentType.Normal,
		ArgumentType.Normal,
		ArgumentType.Normal,
		ArgumentType.Normal,
		ArgumentType.Normal,
		ArgumentType.Normal,
		ArgumentType.Normal
	})]
	internal class Patch_BestFoodSourceOnMap_PrefixTranspiler
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x0000890C File Offset: 0x00006B0C
		private static bool Prefix(ref Thing __result, Pawn getter, Pawn eater, bool desperate, out ThingDef foodDef, FoodPreferability maxPref = FoodPreferability.MealLavish, bool allowPlant = true, bool allowDrug = true, bool allowCorpse = true, bool allowDispenserFull = true, bool allowDispenserEmpty = true, bool allowForbidden = false, bool allowSociallyImproper = false, bool allowHarvest = false, bool forceScanWholeMap = false, bool ignoreReservations = false, bool calculateWantedStackCount = false, FoodPreferability minPrefOverride = FoodPreferability.Undefined, float? minNutrition = null)
		{
			foodDef = null;
			if (!(getter.def.thingClass == typeof(Ninetail.AMP_SpecialBody)))
			{
				return true;
			}
			bool getterCanManipulate = getter.Faction != null && getter.health.capacities.CapableOf(PawnCapacityDefOf.Manipulation);
			if (!getterCanManipulate && getter != eater)
			{
				Log.Error(string.Concat(new object[]
				{
					getter,
					" tried to find food to bring to ",
					eater,
					" but ",
					getter,
					" is incapable of Manipulation."
				}));
				__result = null;
				return false;
			}
			if (eater.def.thingClass == typeof(Ninetail.AMP_SpecialBody) && getter.Faction != null)
			{
					minPrefOverride = FoodPreferability.DesperateOnly;
					maxPref = FoodPreferability.MealLavish;
			}
			FoodPreferability minPref;
			if (minPrefOverride == FoodPreferability.Undefined)
			{
				if (eater.NonHumanlikeOrWildMan())
				{
					minPref = FoodPreferability.NeverForNutrition;
				}
				else if (desperate)
				{
					minPref = FoodPreferability.DesperateOnly;
				}
				else
				{
					minPref = ((eater.needs.food.CurCategory >= HungerCategory.UrgentlyHungry) ? FoodPreferability.RawBad : FoodPreferability.MealAwful);
				}
			}
			else
			{
				minPref = minPrefOverride;
			}
			Predicate<Thing> validator = delegate (Thing t)
			{
				Building_NutrientPasteDispenser building_NutrientPasteDispenser = t as Building_NutrientPasteDispenser;
				if (building_NutrientPasteDispenser != null)
				{
					if (!allowDispenserFull || !getterCanManipulate || ThingDefOf.MealNutrientPaste.ingestible.preferability < minPref || ThingDefOf.MealNutrientPaste.ingestible.preferability > maxPref || !eater.WillEat(ThingDefOf.MealNutrientPaste, getter, true) || (t.Faction != getter.Faction && t.Faction != getter.HostFaction) || (!allowForbidden && t.IsForbidden(getter)) || (!building_NutrientPasteDispenser.powerComp.PowerOn || (!allowDispenserEmpty && !building_NutrientPasteDispenser.HasEnoughFeedstockInHoppers())) || !t.InteractionCell.Standable(t.Map) || !Patch_BestFoodSourceOnMap_PrefixTranspiler.IsFoodSourceOnMapSociallyProper(t, getter, eater, allowSociallyImproper) || !getter.Map.reachability.CanReachNonLocal(getter.Position, new TargetInfo(t.InteractionCell, t.Map, false), PathEndMode.OnCell, TraverseParms.For(getter, Danger.Some, TraverseMode.ByPawn, false, false, false)))
					{
						return false;
					}
				}
				else
				{
					int stackCount = 1;
					float statValue = t.GetStatValue(StatDefOf.Nutrition, true);
					if (minNutrition != null)
					{
						stackCount = FoodUtility.StackCountForNutrition(minNutrition.Value, statValue);
					}
					else if (calculateWantedStackCount)
					{
						stackCount = FoodUtility.WillIngestStackCountOf(eater, t.def, statValue);
					}
					if (t.def.ingestible.preferability < minPref || t.def.ingestible.preferability > maxPref || !eater.WillEat(t, getter, true) || !t.def.IsNutritionGivingIngestible || !t.IngestibleNow || (!allowCorpse && t is Corpse) || (!allowDrug && t.def.IsDrug) || (!allowForbidden && t.IsForbidden(getter)) || (!desperate && t.IsNotFresh()) || (t.IsDessicated() || !Patch_BestFoodSourceOnMap_PrefixTranspiler.IsFoodSourceOnMapSociallyProper(t, getter, eater, allowSociallyImproper) || (!getter.AnimalAwareOf(t) && !forceScanWholeMap)) || (!ignoreReservations && !getter.CanReserve(t, 10, stackCount, null, false)))
					{
						return false;
					}
				}
				return true;
			};
			ThingRequest req;
			if ((eater.RaceProps.foodType & (FoodTypeFlags.Plant | FoodTypeFlags.Tree)) > FoodTypeFlags.None && allowPlant)
			{
				req = ThingRequest.ForGroup(ThingRequestGroup.FoodSource);
			}
			else
			{
				req = ThingRequest.ForGroup(ThingRequestGroup.FoodSourceNotPlantOrTree);
			}
			Thing bestThing = Patch_BestFoodSourceOnMap_PrefixTranspiler.SpawnedFoodSearchInnerScan(eater, getter.Position, getter.Map.listerThings.ThingsMatching(req), PathEndMode.ClosestTouch, TraverseParms.For(getter, Danger.Deadly, TraverseMode.ByPawn, false, false, false), 9999f, validator);
			if (allowHarvest & getterCanManipulate)
			{
				int searchRegionsMax;
				if (forceScanWholeMap && bestThing == null)
				{
					searchRegionsMax = -1;
				}
				else
				{
					searchRegionsMax = 30;
				}
				Thing thing = GenClosest.ClosestThingReachable(getter.Position, getter.Map, ThingRequest.ForGroup(ThingRequestGroup.HarvestablePlant), PathEndMode.Touch, TraverseParms.For(getter, Danger.Deadly, TraverseMode.ByPawn, false, false, false), 9999f, delegate (Thing x)
				{
					Plant plant = (Plant)x;
					if (!plant.HarvestableNow)
					{
						return false;
					}
					ThingDef harvestedThingDef = plant.def.plant.harvestedThingDef;
					return harvestedThingDef.IsNutritionGivingIngestible && eater.WillEat(harvestedThingDef, getter, true) && getter.CanReserve(plant, 1, -1, null, false) && (allowForbidden || !plant.IsForbidden(getter)) && (bestThing == null || FoodUtility.GetFinalIngestibleDef(bestThing, false).ingestible.preferability < harvestedThingDef.ingestible.preferability);
				}, null, 0, searchRegionsMax, false, RegionType.Set_Passable, false);
				if (thing != null)
				{
					bestThing = thing;
					foodDef = FoodUtility.GetFinalIngestibleDef(thing, true);
				}
			}
			if (foodDef == null && bestThing != null)
			{
				foodDef = FoodUtility.GetFinalIngestibleDef(bestThing, false);
			}
			__result = bestThing;
			return false;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00008C58 File Offset: 0x00006E58
		private static bool IsFoodSourceOnMapSociallyProper(Thing t, Pawn getter, Pawn eater, bool allowSociallyImproper)
		{
			if (!allowSociallyImproper)
			{
				bool animalsCare = !getter.RaceProps.Animal;
				if (!t.IsSociallyProper(getter) && !t.IsSociallyProper(eater, eater.IsPrisonerOfColony, animalsCare))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00008C94 File Offset: 0x00006E94
		private static Thing SpawnedFoodSearchInnerScan(Pawn eater, IntVec3 root, List<Thing> searchSet, PathEndMode peMode, TraverseParms traverseParams, float maxDistance = 9999f, Predicate<Thing> validator = null)
		{
			if (searchSet == null)
			{
				return null;
			}
			Pawn pawn = traverseParams.pawn ?? eater;
			int num = 0;
			int num2 = 0;
			Thing result = null;
			float num3 = float.MinValue;
			for (int i = 0; i < searchSet.Count; i++)
			{
				Thing thing = searchSet[i];
				num2++;
				float num4 = (float)(root - thing.Position).LengthManhattan;
				if (num4 <= maxDistance)
				{
					float num5 = FoodUtility.FoodOptimality(eater, thing, FoodUtility.GetFinalIngestibleDef(thing, false), num4, false);
					if (num5 >= num3 && pawn.Map.reachability.CanReach(root, thing, peMode, traverseParams) && thing.Spawned && (validator == null || validator(thing)))
					{
						result = thing;
						num3 = num5;
						num++;
					}
				}
			}
			return result;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00008D64 File Offset: 0x00006F64
		private static int GetMaxRegionsToScan(Pawn getter, bool forceScanWholeMap)
		{
			if (getter.RaceProps.Humanlike)
			{
				return -1;
			}
			if (forceScanWholeMap)
			{
				return -1;
			}
			if (getter.Faction == Faction.OfPlayer)
			{
				return 100;
			}
			return 30;
		}
	}
}
