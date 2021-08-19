using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;

namespace Ninetail
{
	[DefOf]
	public static class NeedDefOf
	{
		static NeedDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(NeedDefOf));
		}

		public static NeedDef Food;

		public static NeedDef Rest;

		public static NeedDef Joy;

		public static NeedDef Indoors;

		public static NeedDef Mood;

		public static NeedDef Beauty;

		public static NeedDef Comfort;

		public static NeedDef Outdoors;
	}
}
