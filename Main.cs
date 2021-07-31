using System;
using System.Reflection;
using HarmonyLib;
using Verse;

namespace Ninetail
{
	// Token: 0x02000004 RID: 4
	[StaticConstructorOnStartup]
	internal class Main
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002146 File Offset: 0x00000346
		static Main()
		{
			new Harmony("Ninetail.HarmonyPatch").PatchAll(Assembly.GetExecutingAssembly());
		}
	}
}
