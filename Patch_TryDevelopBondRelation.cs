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
    [HarmonyPatch(typeof(RelationsUtility), "TryDevelopBondRelation", new Type[]
	{
		typeof(Pawn),
		typeof(Pawn),
		typeof(float)
	})]
	internal class Patch_TryDevelopBondRelation
	{
		private static bool Prefix(Pawn humanlike, Pawn animal, float baseChance, ref bool __result)
		{
			if (!(animal.kindDef == PawnKindDefOf.Ninetailfox || animal.kindDef == PawnKindDefOf.Ninetailfoxwt))
			{ 
				return true;
			}
			if (animal.Faction == Faction.OfPlayer && humanlike.IsQuestLodger())
			{
				__result = false;
				return false;
			}
			if (animal.RaceProps.trainability.intelligenceOrder < TrainabilityDefOf.Intermediate.intelligenceOrder)
			{
				__result = false;
				return false;
			}
			if (humanlike.relations.DirectRelationExists(PawnRelationDefOf.Bond, animal))
			{
				__result = false;
				return false;
			}
			if (animal.relations.GetFirstDirectRelationPawn(PawnRelationDefOf.Bond, (Pawn x) => x.Spawned) != null)
			{
				__result = false;
				return false;
			}
			if (humanlike.story.traits.HasTrait(TraitDefOf.Psychopath))
			{
				__result = false;
				return false;
			}
			if (!new HistoryEvent(HistoryEventDefOf.Bonded, humanlike.Named(HistoryEventArgsNames.Doer), animal.Named(HistoryEventArgsNames.Victim)).DoerWillingToDo())
			{
				__result = false;
				return false;
			}
			int num = 0;
			List<DirectPawnRelation> directRelations = animal.relations.DirectRelations;
			for (int i = 0; i < directRelations.Count; i++)
			{
				if (directRelations[i].def == PawnRelationDefOf.Bond && !directRelations[i].otherPawn.Dead)
				{
					num++;
				}
			}
			int num2 = 0;
			List<DirectPawnRelation> directRelations2 = humanlike.relations.DirectRelations;
			for (int j = 0; j < directRelations2.Count; j++)
			{
				if (directRelations2[j].def == PawnRelationDefOf.Bond && !directRelations2[j].otherPawn.Dead)
				{
					num2++;
				}
			}
			if (num > 0)
			{
				baseChance *= Mathf.Pow(0.2f, (float)num);
			}
			if (num2 > 0)
			{
				baseChance *= Mathf.Pow(0.55f, (float)num2);
			}
			baseChance *= 2;
			baseChance *= humanlike.GetStatValue(StatDefOf.BondAnimalChanceFactor, true);
			if (Rand.Value < baseChance)
			{
				humanlike.relations.AddDirectRelation(PawnRelationDefOf.Bond, animal);
				if (humanlike.Faction == Faction.OfPlayer || animal.Faction == Faction.OfPlayer)
				{
					TaleRecorder.RecordTale(TaleDefOf.BondedWithAnimal, new object[]
					{
						humanlike,
						animal
					});
				}
				bool flag = false;
				string value = null;
				if (animal.Name == null || animal.Name.Numerical)
				{
					flag = true;
					value = ((animal.Name == null) ? animal.LabelIndefinite() : animal.Name.ToStringFull);
					animal.Name = PawnBioAndNameGenerator.GeneratePawnName(animal, NameStyle.Full, null);
				}
				if (PawnUtility.ShouldSendNotificationAbout(humanlike) || PawnUtility.ShouldSendNotificationAbout(animal))
				{
					string text;
					if (flag)
					{
						text = "MessageNewBondRelationNewName".Translate(humanlike.LabelShort, value, animal.Name.ToStringFull, humanlike.Named("HUMAN"), animal.Named("ANIMAL")).AdjustedFor(animal, "PAWN", true).CapitalizeFirst();
					}
					else
					{
						text = "MessageNewBondRelation".Translate(humanlike.LabelShort, animal.LabelShort, humanlike.Named("HUMAN"), animal.Named("ANIMAL")).CapitalizeFirst();
					}
					Messages.Message(text, humanlike, MessageTypeDefOf.PositiveEvent, true);
				}
				humanlike.health.AddHediff(HediffDef.Named("KyulenBlessing"));
				__result = true;
				return false;
			}
			__result = false;
			return false;

		}
	}
}
