using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace Ninetail
{
	// Token: 0x02000004 RID: 4
	public class CompAssignableToPawn_Bed : CompAssignableToPawn
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002170 File Offset: 0x00000370
		public override IEnumerable<Pawn> AssigningCandidates
		{
			get
			{
				bool flag = !this.parent.Spawned;
				bool flag2 = flag;
				IEnumerable<Pawn> result;
				if (flag2)
				{
					result = Enumerable.Empty<Pawn>();
				}
				else
				{
					bool flag3 = !this.parent.def.building.bed_humanlike;
					bool flag4 = flag3;
					if (flag4)
					{
						PawnKindDef ninetailfox = PawnKindDefOf.Ninetailfox;
						PawnKindDef ninetailfoxwt = PawnKindDefOf.Ninetailfoxwt;
						result = Enumerable.OrderByDescending<Pawn, bool>(this.parent.Map.mapPawns.SpawnedColonyAnimals, (Pawn p) => this.CanAssignTo(p).Accepted);
					}
					else
					{
						result = Enumerable.OrderByDescending<Pawn, bool>(this.parent.Map.mapPawns.FreeColonists, (Pawn p) => this.CanAssignTo(p).Accepted && !this.IdeoligionForbids(p));
					}
				}
				return result;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000222C File Offset: 0x0000042C
		protected override string GetAssignmentGizmoDesc()
		{
			return "CommandBedSetOwnerDesc".Translate();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002250 File Offset: 0x00000450
		public override bool AssignedAnything(Pawn pawn)
		{
			return pawn.ownership.OwnedBed != null;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002270 File Offset: 0x00000470
		public override void TryAssignPawn(Pawn pawn)
		{
			Building_Bed building_Bed = (Building_Bed)this.parent;
			pawn.ownership.ClaimBedIfNonMedical(building_Bed);
			building_Bed.NotifyRoomAssignedPawnsChanged();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022A0 File Offset: 0x000004A0
		public override void TryUnassignPawn(Pawn pawn, bool sort = true)
		{
			Building_Bed ownedBed = pawn.ownership.OwnedBed;
			pawn.ownership.UnclaimBed();
			bool flag = ownedBed != null;
			bool flag2 = flag;
			if (flag2)
			{
				ownedBed.NotifyRoomAssignedPawnsChanged();
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022DC File Offset: 0x000004DC
		protected override bool ShouldShowAssignmentGizmo()
		{
			Building_Bed building_Bed = (Building_Bed)this.parent;
			return building_Bed.Faction == Faction.OfPlayer && !building_Bed.ForPrisoners && !building_Bed.Medical;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000231C File Offset: 0x0000051C
		public override AcceptanceReport CanAssignTo(Pawn pawn)
		{
			Building_Bed building_Bed = (Building_Bed)this.parent;
			PawnKindDef ninetailfox = PawnKindDefOf.Ninetailfox;
			PawnKindDef ninetailfoxwt = PawnKindDefOf.Ninetailfoxwt;
			bool flag = pawn.kindDef != ninetailfox && pawn.kindDef != ninetailfoxwt;
			bool flag2 = flag;
			AcceptanceReport result;
			if (flag2)
			{
				result = "NotKyulen".Translate();
			}
			else
			{
				bool flag3 = pawn.BodySize == building_Bed.def.building.bed_maxBodySize;
				bool flag4 = flag3;
				if (flag4)
				{
					result = "TooLargeForBed".Translate();
				}
				else
				{
					bool flag5 = building_Bed.ForSlaves && !pawn.IsSlave;
					bool flag6 = flag5;
					if (flag6)
					{
						result = "CannotAssignBedToColonist".Translate();
					}
					else
					{
						bool flag7 = building_Bed.ForColonists && pawn.IsSlave;
						bool flag8 = flag7;
						if (flag8)
						{
							result = "CannotAssignBedToSlave".Translate();
						}
						else
						{
							result = AcceptanceReport.WasAccepted;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000242C File Offset: 0x0000062C
		public override bool IdeoligionForbids(Pawn pawn)
		{
			bool flag = !ModsConfig.IdeologyActive || base.Props.maxAssignedPawnsCount == 1;
			bool flag2 = flag;
			bool result;
			if (flag2)
			{
				result = base.IdeoligionForbids(pawn);
			}
			else
			{
				foreach (Pawn pawn2 in base.AssignedPawns)
				{
					bool flag3 = !BedUtility.WillingToShareBed(pawn, pawn2);
					bool flag4 = flag3;
					if (flag4)
					{
						return true;
					}
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000024C8 File Offset: 0x000006C8
		public override void PostExposeData()
		{
			base.PostExposeData();
			bool flag = Scribe.mode == LoadSaveMode.PostLoadInit && this.assignedPawns.RemoveAll((Pawn x) => x.ownership.OwnedBed != this.parent) > 0;
			bool flag2 = flag;
			if (flag2)
			{
				Log.Warning(this.parent.ToStringSafe<ThingWithComps>() + " had pawns assigned that don't have it as an assigned bed. Removing.");
			}
		}
	}
}
