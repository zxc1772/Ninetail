using System;
using Verse;
using RimWorld;

namespace Ninetail
{
    public class CompTargetEffect_FoxbeadMoodBoost : CompTargetEffect
    {
        public override void DoEffectOn(Pawn user, Thing target)
        {
            Pawn pawn = (Pawn)target;
            if (pawn.Dead || pawn.needs == null || pawn.needs.mood == null)
            {
                return;
            }
            pawn.needs.mood.thoughts.memories.TryGainMemory((Thought_Memory)ThoughtMaker.MakeThought(ThoughtDefOf.FoxbeadMoodBoost), null);
        }
    }
}
