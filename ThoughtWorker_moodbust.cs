using RimWorld;
using System;
using Verse;
using HardworkingNinetail;

namespace Ninetail
{
    public class ThoughtWorker_moodbust : ThoughtWorker
    {
        protected override ThoughtState CurrentStateInternal(Verse.Pawn p)
        {
            if (!p.Spawned)
            {
                return false;
            }
            if (p.Faction != Faction.OfPlayer)
            {
                return false;
            }
            if (p.kindDef == PawnKindDefOf.Ninetailfox || p.kindDef == PawnKindDefOf.Ninetailfoxwt)
            {
                return false;
            }
            bool flag4 = false;
            foreach (Verse.Pawn current in p.Map.mapPawns.PawnsInFaction(Faction.OfPlayer))
            {
                if (current.kindDef == PawnKindDefOf.Ninetailfox || current.kindDef == PawnKindDefOf.Ninetailfoxwt)
                {
                    Verse.Pawn pawn = p;
                    Verse.Pawn firstDirectRelationPawn = pawn.relations.GetFirstDirectRelationPawn(PawnRelationDefOf.Bond, (Verse.Pawn req) => req.Spawned);
                    bool flag = pawn != null && pawn.Faction != null && pawn.Faction.IsPlayer && pawn.RaceProps.Humanlike && !pawn.RaceProps.Animal;
                    if (flag)
                    {
                        bool flag2 = firstDirectRelationPawn != null;
                        if (flag2)
                        {
                            bool flag3 = firstDirectRelationPawn.def == MiscDefOf.Ninetailfox || firstDirectRelationPawn.def == MiscDefOf.Ninetailfoxwt;
                            if (flag3)
                            {
                                pawn.health.AddHediff(HediffDef.Named("KyulenBlessing"));
                            }
                        }
                    }
                    if (current.Faction != null && current.Faction.ideos != null && current.Faction.ideos.PrimaryIdeo != null)
                    {
                        current.ideo.SetIdeo(pawn.Faction.ideos.PrimaryIdeo);
                    }
                    flag4 = true;
                }
            }
            if (flag4 == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
