using RimWorld;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Ninetail
{
    [StaticConstructorOnStartup]
    public class Battle : ThingComp
    {
        public Vector2 drawSize = Vector2.one;

        private static readonly string Folder = "Things/Pawn/Animal/Battle";

        private static readonly Material Battle1 = MaterialPool.MatFrom(Battle.Folder + "/1");

        private static readonly Material Battle2 = MaterialPool.MatFrom(Battle.Folder + "/2");

        private static readonly Material Battle3 = MaterialPool.MatFrom(Battle.Folder + "/3");

        private static readonly Material Battle4 = MaterialPool.MatFrom(Battle.Folder + "/4");

        private static readonly Material Battle5 = MaterialPool.MatFrom(Battle.Folder + "/5");

        private static readonly Material Battle6 = MaterialPool.MatFrom(Battle.Folder + "/6");

        private static readonly Material Battle7 = MaterialPool.MatFrom(Battle.Folder + "/7");

        private static readonly Material Battle8 = MaterialPool.MatFrom(Battle.Folder + "/8");

        private static readonly Material Battle9 = MaterialPool.MatFrom(Battle.Folder + "/9");

        private static readonly Material Battle10 = MaterialPool.MatFrom(Battle.Folder + "/10");

        private static readonly Material Battle11 = MaterialPool.MatFrom(Battle.Folder + "/11");

        private static readonly Material Battle12 = MaterialPool.MatFrom(Battle.Folder + "/12");

        private static readonly Material Battle13 = MaterialPool.MatFrom(Battle.Folder + "/13");

        private static readonly Material Battle14 = MaterialPool.MatFrom(Battle.Folder + "/14");

        private static readonly Material Battle15 = MaterialPool.MatFrom(Battle.Folder + "/15");

        private static readonly Material Battle16 = MaterialPool.MatFrom(Battle.Folder + "/16");

        private static readonly Material Battle17 = MaterialPool.MatFrom(Battle.Folder + "/17");

        private static readonly Material Battle18 = MaterialPool.MatFrom(Battle.Folder + "/18");

        private static readonly Material Battle19 = MaterialPool.MatFrom(Battle.Folder + "/19");

        private static readonly Material Battle20 = MaterialPool.MatFrom(Battle.Folder + "/20");

        private static readonly List<Material> Epact = new List<Material>();

        private int currFrame = 0;

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            Battle.Epact.Add(Battle.Battle1);
            Battle.Epact.Add(Battle.Battle2);
            Battle.Epact.Add(Battle.Battle3);
            Battle.Epact.Add(Battle.Battle4);
            Battle.Epact.Add(Battle.Battle5);
            Battle.Epact.Add(Battle.Battle6);
            Battle.Epact.Add(Battle.Battle7);
            Battle.Epact.Add(Battle.Battle8);
            Battle.Epact.Add(Battle.Battle9);
            Battle.Epact.Add(Battle.Battle10);
            Battle.Epact.Add(Battle.Battle11);
            Battle.Epact.Add(Battle.Battle12);
            Battle.Epact.Add(Battle.Battle13);
            Battle.Epact.Add(Battle.Battle14);
            Battle.Epact.Add(Battle.Battle15);
            Battle.Epact.Add(Battle.Battle16);
            Battle.Epact.Add(Battle.Battle17);
            Battle.Epact.Add(Battle.Battle18);
            Battle.Epact.Add(Battle.Battle19);
            Battle.Epact.Add(Battle.Battle20);
        }

        public override void PostDraw()
        {
            base.PostDraw();
                this.DrawCurrentFrame();
        }

        public override void CompTick()
        {
            base.CompTick();
            checked
            {
                if (this.parent.IsHashIntervalTick(1))
                {
                    if (this.currFrame >= 19)
                    {
                        this.currFrame = -1;
                    }
                    this.currFrame++;
                }
            }
        }

        private void DrawCurrentFrame()
        {
            Matrix4x4 matrix = default(Matrix4x4);
            Vector3 pos = this.parent.TrueCenter();
            pos.y = 10.0f;
                matrix.SetTRS(pos, Quaternion.identity, new Vector3(1.6f, 1.6f, 1.6f));
                Graphics.DrawMesh(MeshPool.plane20, matrix, Battle.Epact[this.currFrame], 0);
                return;
        }
    }
}
