using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;
using Verse.AI.Group;
using Verse.Sound;
using HarmonyLib;

namespace Ninetail
{
    [HarmonyPatch(typeof(Pawn), "get_IsColonistPlayerControlled")]
    public class Pawn_get_IsColonistPlayerControlled
    {
        [HarmonyPrefix]
        public static bool Prefix(Pawn __instance, ref bool __result)
        {
            if (__instance.kindDef==PawnKindDefOf.Ninetailfox || __instance.kindDef == PawnKindDefOf.Ninetailfoxwt)
            {
                if (__instance.Faction == Faction.OfPlayer && !__instance.Dead)
                {
                    __result = true;
                    return false;
                }
            }
            return true;
        }
    }
}
