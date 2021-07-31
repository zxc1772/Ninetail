using RimWorld;
using System;
using Verse;

namespace Ninetail
{
    public class NinetailarmorShield : ShieldBelt
    {
        public override bool AllowVerbCast(Verb verb)
        {
            return true;
        }
    }
}
