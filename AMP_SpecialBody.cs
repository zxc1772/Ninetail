using System;
using RimWorld;
using UnityEngine;
using HardworkingNinetail;
using Verse;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;

namespace Ninetail
{
	// Token: 0x02000015 RID: 21
	public class AMP_SpecialBody : HardworkingNinetail.Ninetail
	{
		public ThingDef_AnimalMultiPostures CustomStatus
		{
			get
			{
				return this.def as ThingDef_AnimalMultiPostures;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002068 File Offset: 0x00000268
		public override void Tick()
		{
			base.Tick();
			if (this.Awake() && !this.PreAwake)
			{
				PawnKindLifeStage curKindLifeStage = this.ageTracker.CurKindLifeStage;
				base.Drawer.renderer.graphics.nakedGraphic = curKindLifeStage.bodyGraphicData.Graphic;
			}
			else if (!this.Awake() && this.CustomStatus.SleepBodyData != null)
			{
				base.Drawer.renderer.graphics.ClearCache();
				base.Rotation = Rot4.West;
				base.Drawer.renderer.graphics.nakedGraphic = GraphicDatabase.Get<Graphic_Multi>(this.CustomStatus.SleepBodyData.texPath, ShaderDatabase.Cutout, this.CustomStatus.SleepBodyData.drawSize, Color.white);
			}
			this.PreAwake = this.Awake();
		}

		// Token: 0x04000002 RID: 2
		private bool PreAwake;
	}
}
