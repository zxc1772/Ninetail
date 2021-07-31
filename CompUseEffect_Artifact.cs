using System;
using Verse;
using Verse.Sound;
using RimWorld;

namespace Ninetail
{
    public class CompUseEffect_Artifact : CompUseEffect
    {
        public override void DoEffect(Pawn usedBy)
        {
            base.DoEffect(usedBy);
            SoundDefOf.FoxbeadActive.PlayOneShotOnCamera(usedBy.MapHeld);
            usedBy.records.Increment(RecordDefOf.ArtifactsActivated);
        }
    }
}
