// Decompiled with JetBrains decompiler
// Type: VoiceroidAsAnimal_Harmony.Patch_Pawn_NeedsTrackerPostfix
// Assembly: VoiceroidAsAnimal, Version=1.6.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A857463-406C-402E-9847-0B68F0E6F878
// Assembly location: C:\SteamLibrary\SteamApps\workshop\content\294100\2073559411\1.3\Assemblies\VoiceroidAsAnimal.dll

using HarmonyLib;
using RimWorld;
using Verse;

namespace Ninetail
{
    [HarmonyPatch(typeof(Pawn_NeedsTracker), "ShouldHaveNeed", new System.Type[] { typeof(NeedDef) })]
    internal class Patch_Pawn_NeedsTrackerPostfix1
    {
        private static void Postfix(NeedDef nd, Pawn_NeedsTracker __instance, ref bool __result)
        {
            if (!(nd == Ninetail.NeedDefOf.Mood & ((Thing)Traverse.Create((object)__instance).Field("pawn").GetValue<Pawn>()).def.thingClass == typeof(Ninetail.AMP_SpecialBody)))
                return;
            __result = true;
        }
    }
    [HarmonyPatch(typeof(Pawn_NeedsTracker), "ShouldHaveNeed", new System.Type[] { typeof(NeedDef) })]
    internal class Patch_Pawn_NeedsTrackerPostfix2
    {
        private static void Postfix(NeedDef nd, Pawn_NeedsTracker __instance, ref bool __result)
        {
            if (!(nd == Ninetail.NeedDefOf.Beauty & ((Thing)Traverse.Create((object)__instance).Field("pawn").GetValue<Pawn>()).def.thingClass == typeof(Ninetail.AMP_SpecialBody)))
                return;
            __result = true;
        }
    }
    [HarmonyPatch(typeof(Pawn_NeedsTracker), "ShouldHaveNeed", new System.Type[] { typeof(NeedDef) })]
    internal class Patch_Pawn_NeedsTrackerPostfix3
    {
        private static void Postfix(NeedDef nd, Pawn_NeedsTracker __instance, ref bool __result)
        {
            if (!(nd == Ninetail.NeedDefOf.Comfort & ((Thing)Traverse.Create((object)__instance).Field("pawn").GetValue<Pawn>()).def.thingClass == typeof(Ninetail.AMP_SpecialBody)))
                return;
            __result = true;
        }
    }

    [HarmonyPatch(typeof(Pawn_NeedsTracker), "ShouldHaveNeed", new System.Type[] { typeof(NeedDef) })]
    internal class Patch_Pawn_NeedsTrackerPostfix4
    {
        private static void Postfix(NeedDef nd, Pawn_NeedsTracker __instance, ref bool __result)
        {
            if (!(nd == Ninetail.NeedDefOf.Outdoors & ((Thing)Traverse.Create((object)__instance).Field("pawn").GetValue<Pawn>()).def.thingClass == typeof(Ninetail.AMP_SpecialBody)))
                return;
            __result = true;
        }
    }
}
