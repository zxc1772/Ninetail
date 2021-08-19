using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using Verse.Sound;


namespace Ninetail
{
    public class IncidentWorker_Ninetailappear : IncidentWorker
    {
        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            IntVec3 intVec;
            if (!this.TryFindEntryCell(map, out intVec))
            {
                return false;
            }
            PawnKindDef Ninetail = PawnKindDefOf.Ninetailfox;
            List<Pawn> list = Enumerable.ToList<Pawn>(Enumerable.Where<Pawn>(map.mapPawns.AllPawnsSpawned, (Pawn col) => col.kindDef == Ninetail));
            bool flag = list.Count >= 1;
            if (flag)
            {
                return false;
            }
            IntVec3 invalid = IntVec3.Invalid;
            if (!RCellFinder.TryFindRandomCellOutsideColonyNearTheCenterOfTheMap(intVec, map, 10f, out invalid))
            {
                invalid = IntVec3.Invalid;
            }
            Pawn pawn = null;
            for (int i = 0; i < 1; i++)
            {
                IntVec3 loc = CellFinder.RandomClosewalkCellNear(intVec, map, 10, null);
                pawn = PawnGenerator.GeneratePawn(Ninetail, null);
                pawn.gender = Gender.Female;
                pawn.Name = PawnBioAndNameGenerator.GeneratePawnName(pawn, NameStyle.Full);
                GenSpawn.Spawn(pawn, loc, map, Rot4.Random, WipeMode.Vanish, false);
                if (invalid.IsValid)
                {
                    pawn.mindState.forcedGotoPosition = CellFinder.RandomClosewalkCellNear(invalid, map, 10, null);
                }
            }
            Find.LetterStack.ReceiveLetter("LetterLabelNinetailappear".Translate(Ninetail.label).CapitalizeFirst(), "LetterNinetailappear".Translate(Ninetail.label), LetterDefOf.PositiveEvent, pawn, null, null);
            return true;
        }

        private bool TryFindEntryCell(Map map, out IntVec3 cell)
        {
            return RCellFinder.TryFindRandomPawnEntryCell(out cell, map, CellFinder.EdgeRoadChance_Animal + 0.2f, false, null);
        }
    }
}