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
            if (!p.Spawned || p.IsPrisoner || p.Faction != Faction.OfPlayer)
            {
                return false;
            }
            if (p.kindDef == PawnKindDefOf.Ninetailfox || p.kindDef == PawnKindDefOf.Ninetailfoxwt)
            {
                p.psychicEntropy?.OffsetPsyfocusDirectly(0.001f);
                return false;
            }
            bool flag4 = false;
            foreach (Verse.Pawn current in p.Map.mapPawns.PawnsInFaction(Faction.OfPlayer))
            {
                if (current.kindDef == PawnKindDefOf.Ninetailfox || current.kindDef == PawnKindDefOf.Ninetailfoxwt)
                {
                    if (current.Faction != null && current.Faction.ideos != null && current.Faction.ideos.PrimaryIdeo != null)
                    {
                        current.ideo.SetIdeo(p.Faction.ideos.PrimaryIdeo);
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
