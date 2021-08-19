using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld.Planet;
using UnityEngine;
using Verse;
using Verse.AI;
using Verse.AI.Group;
using RimWorld;

namespace Ninetail
{
	// Token: 0x0200097C RID: 2428
	public class ThoughtWorker_VeneratedAnimalKyulenwt : ThoughtWorker
	{
		protected override ThoughtState CurrentStateInternal(Verse.Pawn p)
		{
			if (!p.Spawned || p.Faction != Faction.OfPlayer || p.Faction.ideos == null || !(p.kindDef == PawnKindDefOf.Ninetailfoxwt))
			{
				return false;
			}
			List<Pawn> list = PawnsOfFactionOnMapOrInCaravan(p);
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].Faction == p.Faction && !p.IsQuestLodger())
				{
					if (p.Ideo.IsVeneratedAnimal(list[i]) && list[i].kindDef == PawnKindDefOf.Ninetailfoxwt)
					{
						return true;
					}
				}
			}
			return false;
		}
		private static List<Pawn> PawnsOfFactionOnMapOrInCaravan(Pawn pawn)
		{
			List<Pawn> result;
			if (pawn.Spawned)
			{
				result = pawn.Map.mapPawns.SpawnedPawnsInFaction(pawn.Faction);
			}
			else
			{
				Caravan caravan = pawn.GetCaravan();
				if (caravan == null)
				{
					return null;
				}
				result = caravan.PawnsListForReading;
			}
			return result;
		}
	}
}
