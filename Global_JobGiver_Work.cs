using System;
using System.Collections.Generic;
using System.Linq;
using HardworkingNinetail;
using RimWorld;
using Verse;
using Verse.AI;

namespace Ninetail
{
	public class Global_JobGiver_Work : ThinkNode
	{
		public override ThinkNode DeepCopy(bool resolve = true)
		{
			Global_JobGiver_Work global_JobGiver_Work = (Global_JobGiver_Work)base.DeepCopy(resolve);
			global_JobGiver_Work.emergency = this.emergency;
			return global_JobGiver_Work;
		}

		public override float GetPriority(Pawn pawn)
		{
			bool flag = pawn.workSettings == null || !pawn.workSettings.EverWork;
			float result;
			if (flag)
			{
				result = 0f;
			}
			else
			{
				TimeAssignmentDef timeAssignmentDef = (pawn.timetable != null) ? pawn.timetable.CurrentAssignment : TimeAssignmentDefOf.Anything;
				bool flag2 = timeAssignmentDef == TimeAssignmentDefOf.Anything;
				if (flag2)
				{
					result = 5.5f;
				}
				else
				{
					bool flag3 = timeAssignmentDef == TimeAssignmentDefOf.Work;
					if (flag3)
					{
						result = 9f;
					}
					else
					{
						bool flag4 = timeAssignmentDef == TimeAssignmentDefOf.Sleep;
						if (flag4)
						{
							result = 3f;
						}
						else
						{
							bool flag5 = timeAssignmentDef == TimeAssignmentDefOf.Joy;
							if (flag5)
							{
								result = 2f;
							}
							else
							{
								bool flag6 = timeAssignmentDef == TimeAssignmentDefOf.Meditate;
								if (!flag6)
								{
									throw new NotImplementedException();
								}
								result = 2f;
							}
						}
					}
				}
			}
			return result;
		}

		public override ThinkResult TryIssueJobPackage(Pawn pawn, JobIssueParams jobParams)
		{
			List<WorkGiver> list = new List<WorkGiver>();
			List<WorkTypeDef> list2 = new List<WorkTypeDef>();
			bool flag = pawn is HardworkingNinetail.Ninetail;
			if (flag)
			{
				HardworkingNinetail.Ninetail Ninetail = pawn as HardworkingNinetail.Ninetail;
				list = Ninetail.GetWorkGivers();
				list2 = HardworkingNinetail.Ninetail.disableWorkType;
				bool dflag = St.dflag;
				if (dflag)
				{
					HardworkingNinetail.Ninetail Ninetail2 = Ninetail;
				}
			}
			bool flag2 = false;
			int num = -999;
			TargetInfo targetInfo = TargetInfo.Invalid;
			WorkGiver_Scanner workGiver_Scanner = null;
			IEnumerable<int> enumerable = Enumerable.Select(Enumerable.Where(Enumerable.Select(LoadedModManager.RunningMods, (ModContentPack p, int i) => new
			{
				Content = p,
				Index = i
			}), ano => ano.Content.Name.IndexOf("Animal Gear") >= 0 && ano.Index >= 0), ano => ano.Index);
			bool dflag2 = St.dflag;
			if (dflag2)
			{
			}
			bool flag3 = Enumerable.Count<int>(enumerable) > 0;
			if (flag3)
			{
				ThinkResult thinkResult = this.EquipWear(pawn);
				bool flag4 = thinkResult != ThinkResult.NoJob;
				if (flag4)
				{
					return thinkResult;
				}
			}
			int j = 0;
			while (j < list.Count)
			{
				WorkGiver workGiver = list[j];
				WorkTypeDef workType = list[j].def.workType;
				bool flag5 = workGiver.def.priorityInType != num && targetInfo.IsValid;
				if (flag5)
				{
					break;
				}
				bool flag6 = !(pawn is HardworkingNinetail.Ninetail);
				if (flag6)
				{
					goto IL_3AB;
				}
				bool flag7 = pawn.training != null && !pawn.training.HasLearned(HardworkingNinetail.TrainableDefOf.Obedience) && (workType == HardworkingNinetail.WorkTypeDefOf.Firefighter || workType == HardworkingNinetail.WorkTypeDefOf.Cleaning);
				if (flag7)
				{
					bool dflag3 = St.dflag;
					if (dflag3)
					{
					}
				}
				else
				{
					bool flag8 = pawn.training != null && !pawn.training.HasLearned(HardworkingNinetail.TrainableDefOf.Release) && (workType == HardworkingNinetail.WorkTypeDefOf.Growing || workType == HardworkingNinetail.WorkTypeDefOf.PlantCutting || workType == HardworkingNinetail.WorkTypeDefOf.Mining || workType == HardworkingNinetail.WorkTypeDefOf.Crafting || workType == HardworkingNinetail.WorkTypeDefOf.Hunting);
					if (flag8)
					{
						bool dflag4 = St.dflag;
						if (dflag4)
						{
						}
					}
					else
					{
						bool flag9 = pawn.training != null && !pawn.training.HasLearned(HardworkingNinetail.TrainableDefOf.Rescue) && (workType == HardworkingNinetail.WorkTypeDefOf.Doctor || workType == HardworkingNinetail.WorkTypeDefOf.Cooking || workType == HardworkingNinetail.WorkTypeDefOf.Warden || workType == HardworkingNinetail.WorkTypeDefOf.Art);
						if (flag9)
						{
							bool dflag5 = St.dflag;
							if (dflag5)
							{
							}
						}
						else
						{
							bool flag10 = pawn.training == null || pawn.training.HasLearned(HardworkingNinetail.TrainableDefOf.Haul) || (workType != HardworkingNinetail.WorkTypeDefOf.Hauling && workType != HardworkingNinetail.WorkTypeDefOf.Smithing && workType != HardworkingNinetail.WorkTypeDefOf.Tailoring && workType != HardworkingNinetail.WorkTypeDefOf.Construction && workType != HardworkingNinetail.WorkTypeDefOf.Research);
							if (flag10)
							{
								goto IL_3AB;
							}
							bool dflag6 = St.dflag;
							if (dflag6)
							{
							}
						}
					}
				}
			IL_39F:
				j++;
				continue;
			IL_3AB:
				bool flag11 = workGiver.def == HardworkingNinetail.WorkGiverDefOf.DoctorFeedHumanlikes || workGiver.def == HardworkingNinetail.WorkGiverDefOf.FeedPrisoner || workGiver.def == HardworkingNinetail.WorkGiverDefOf.DeliverFoodToPrisoner || workGiver.def == HardworkingNinetail.WorkGiverDefOf.FightFires || workGiver.def == HardworkingNinetail.WorkGiverDefOf.HunterHunt;
				if (flag11)
				{
					goto IL_39F;
				}
				foreach (WorkTypeDef workTypeDef in list2)
				{
					bool flag12 = workType == workTypeDef;
					if (flag12)
					{
						flag2 = true;
					}
				}
				bool flag13 = flag2;
				if (flag13)
				{
					flag2 = false;
					goto IL_39F;
				}
				bool flag14 = this.PawnCanUseWorkGiver(pawn, workGiver);
				if (flag14)
				{
					try
					{
						Job job = workGiver.NonScanJob(pawn);
						bool flag15 = job != null;
						if (flag15)
						{
							return new ThinkResult(job, this, new JobTag?(list[j].def.tagToGive), false);
						}
						WorkGiver_Scanner scanner = workGiver as WorkGiver_Scanner;
						bool flag16 = scanner != null;
						if (flag16)
						{
							bool scanThings = scanner.def.scanThings;
							if (scanThings)
							{
								Predicate<Thing> predicate = (Thing t) => !t.IsForbidden(pawn) && scanner.HasJobOnThing(pawn, t, false);
								IEnumerable<Thing> enumerable2 = scanner.PotentialWorkThingsGlobal(pawn);
								bool prioritized = scanner.Prioritized;
								Thing thing;
								if (prioritized)
								{
									IEnumerable<Thing> enumerable3 = enumerable2;
									bool flag17 = enumerable3 == null;
									if (flag17)
									{
										enumerable3 = pawn.Map.listerThings.ThingsMatching(scanner.PotentialWorkThingRequest);
									}
									bool allowUnreachable = scanner.AllowUnreachable;
									if (allowUnreachable)
									{
										IntVec3 position = pawn.Position;
										IEnumerable<Thing> searchSet = enumerable3;
										Predicate<Thing> validator = predicate;
										thing = GenClosest.ClosestThing_Global(position, searchSet, 99999f, validator, (Thing x) => scanner.GetPriority(pawn, x));
									}
									else
									{
										IntVec3 position2 = pawn.Position;
										Map map = pawn.Map;
										IEnumerable<Thing> searchSet2 = enumerable3;
										PathEndMode pathEndMode = scanner.PathEndMode;
										TraverseParms traverseParams = TraverseParms.For(pawn, scanner.MaxPathDanger(pawn), TraverseMode.ByPawn, false, false, false);
										Predicate<Thing> validator2 = predicate;
										thing = GenClosest.ClosestThing_Global_Reachable(position2, map, searchSet2, pathEndMode, traverseParams, 9999f, validator2, (Thing x) => scanner.GetPriority(pawn, x));
									}
								}
								else
								{
									bool allowUnreachable2 = scanner.AllowUnreachable;
									if (allowUnreachable2)
									{
										IEnumerable<Thing> enumerable4 = enumerable2;
										bool flag18 = enumerable4 == null;
										if (flag18)
										{
											enumerable4 = pawn.Map.listerThings.ThingsMatching(scanner.PotentialWorkThingRequest);
										}
										IntVec3 position3 = pawn.Position;
										IEnumerable<Thing> searchSet3 = enumerable4;
										Predicate<Thing> validator3 = predicate;
										thing = GenClosest.ClosestThing_Global(position3, searchSet3, 99999f, validator3, null);
									}
									else
									{
										TraverseParms traverseParams2 = TraverseParms.For(pawn, scanner.MaxPathDanger(pawn), TraverseMode.ByPawn, false, false, false);
										Predicate<Thing> validator4 = predicate;
										bool flag19 = enumerable2 != null;
										thing = GenClosest.ClosestThingReachable(pawn.Position, pawn.Map, scanner.PotentialWorkThingRequest, scanner.PathEndMode, traverseParams2, 9999f, validator4, enumerable2, 0, scanner.MaxRegionsToScanBeforeGlobalSearch, enumerable != null, RegionType.Set_Passable, false);
									}
								}
								bool flag20 = thing != null;
								if (flag20)
								{
									targetInfo = thing;
									workGiver_Scanner = scanner;
								}
							}
							bool scanCells = scanner.def.scanCells;
							if (scanCells)
							{
								IntVec3 position4 = pawn.Position;
								float num2 = 99999f;
								float num3 = float.MinValue;
								bool prioritized2 = scanner.Prioritized;
								bool allowUnreachable3 = scanner.AllowUnreachable;
								Danger maxDanger = scanner.MaxPathDanger(pawn);
								foreach (IntVec3 intVec in scanner.PotentialWorkCellsGlobal(pawn))
								{
									bool flag21 = false;
									float num4 = (float)(intVec - position4).LengthHorizontalSquared;
									float num5 = 0f;
									bool flag22 = prioritized2;
									if (flag22)
									{
										bool flag23 = !intVec.IsForbidden(pawn) && scanner.HasJobOnCell(pawn, intVec, false);
										if (flag23)
										{
											bool flag24 = !allowUnreachable3 && !pawn.CanReach(intVec, scanner.PathEndMode, maxDanger, false, false, TraverseMode.ByPawn);
											if (flag24)
											{
												continue;
											}
											num5 = scanner.GetPriority(pawn, intVec);
											bool flag25 = num5 > num3 || (num5 == num3 && num4 < num2);
											if (flag25)
											{
												flag21 = true;
											}
										}
									}
									else
									{
										bool flag26 = num4 < num2 && !intVec.IsForbidden(pawn) && scanner.HasJobOnCell(pawn, intVec, false);
										if (flag26)
										{
											bool flag27 = !allowUnreachable3 && !pawn.CanReach(intVec, scanner.PathEndMode, maxDanger, false, false, TraverseMode.ByPawn);
											if (flag27)
											{
												continue;
											}
											flag21 = true;
										}
									}
									bool flag28 = flag21;
									if (flag28)
									{
										targetInfo = new TargetInfo(intVec, pawn.Map, false);
										workGiver_Scanner = scanner;
										num2 = num4;
										num3 = num5;
									}
								}
							}
						}
					}
					catch (Exception ex)
					{
					}
					bool isValid = targetInfo.IsValid;
					if (isValid)
					{
						bool hasThing = targetInfo.HasThing;
						Job job2;
						if (hasThing)
						{
							bool dflag7 = St.dflag;
							if (dflag7)
							{
							}
							bool dflag8 = St.dflag;
							if (dflag8)
							{
								string str2 = "Thing:";
								Thing thing2 = targetInfo.Thing;
							}
							job2 = workGiver_Scanner.JobOnThing(pawn, targetInfo.Thing, false);
						}
						else
						{
							bool dflag9 = St.dflag;
							if (dflag9)
							{
							}
							job2 = workGiver_Scanner.JobOnCell(pawn, targetInfo.Cell, false);
						}
						bool flag29 = job2 != null;
						if (flag29)
						{
							bool dflag10 = St.dflag;
							if (dflag10)
							{
							}
							return new ThinkResult(job2, this, new JobTag?(list[j].def.tagToGive), false);
						}
					}
					num = workGiver.def.priorityInType;
					goto IL_39F;
				}
				goto IL_39F;
			}
			return ThinkResult.NoJob;
		}
		private bool PawnCanUseWorkGiver(Pawn pawn, WorkGiver giver)
		{
			bool result;
			try
			{
				result = (!pawn.DestroyedOrNull() && pawn.Spawned && giver.MissingRequiredCapacity(pawn) == null && !giver.ShouldSkip(pawn, false));
			}
			catch
			{
				result = false;
			}
			return result;
		}

		private ThinkResult EquipWear(Pawn pawn)
		{
			return ThinkResult.NoJob;
		}

		public bool emergency;
	}
}
