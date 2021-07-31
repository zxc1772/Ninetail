using System;
using System.Linq;
using Verse;
using RimWorld;

namespace Ninetail
{
	// Token: 0x020002A5 RID: 677
	public class HediffComp_HealNinetail : HediffComp
	{
		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001294 RID: 4756 RVA: 0x0006AF9B File Offset: 0x0006919B
		public HediffCompProperties_HealNinetail Props
		{
			get
			{
				return (HediffCompProperties_HealNinetail)this.props;
			}
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x0006AFA8 File Offset: 0x000691A8
		public override void CompPostMake()
		{
			base.CompPostMake();
			this.ResetTicksToHeal();
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x0006AFB6 File Offset: 0x000691B6
		private void ResetTicksToHeal()
		{
			this.ticksToHeal = 6000;
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x0006AFCD File Offset: 0x000691CD
		public override void CompPostTick(ref float severityAdjustment)
		{
			this.ticksToHeal--;
			if (this.ticksToHeal <= 0)
			{
				this.TryHealRandomPermanentWound();
				this.ResetTicksToHeal();
			}
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x0006AFF4 File Offset: 0x000691F4
		private void TryHealRandomPermanentWound()
		{
			Hediff hediff;
			if (!(from hd in base.Pawn.health.hediffSet.hediffs
				  where hd.IsPermanent() || hd.def.chronic
				  select hd).TryRandomElement(out hediff))
			{
				return;
			}
			HealthUtility.Cure(hediff);
			if (PawnUtility.ShouldSendNotificationAbout(base.Pawn))
			{
				Messages.Message("MessagePermanentWoundHealed".Translate(this.parent.LabelCap, base.Pawn.LabelShort, hediff.Label, base.Pawn.Named("PAWN")), base.Pawn, MessageTypeDefOf.PositiveEvent, true);
			}
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x0006B0B8 File Offset: 0x000692B8
		public override void CompExposeData()
		{
			Scribe_Values.Look<int>(ref this.ticksToHeal, "ticksToHeal", 0, false);
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x0006B0CC File Offset: 0x000692CC
		public override string CompDebugString()
		{
			return "ticksToHeal: " + this.ticksToHeal;
		}

		// Token: 0x04000E14 RID: 3604
		private int ticksToHeal;
	}
}
