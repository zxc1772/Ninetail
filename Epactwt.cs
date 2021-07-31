using RimWorld;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Ninetail
{
    [StaticConstructorOnStartup]
    public class Battlewt : ThingComp
    {
        public Vector2 drawSize = Vector2.one;

        private static readonly string Folder = "Things/Pawn/Animal/Battlewt";

        private static readonly Material Battlewt1 = MaterialPool.MatFrom(Battlewt.Folder + "/1");

        private static readonly Material Battlewt2 = MaterialPool.MatFrom(Battlewt.Folder + "/2");

        private static readonly Material Battlewt3 = MaterialPool.MatFrom(Battlewt.Folder + "/3");

        private static readonly Material Battlewt4 = MaterialPool.MatFrom(Battlewt.Folder + "/4");

        private static readonly Material Battlewt5 = MaterialPool.MatFrom(Battlewt.Folder + "/5");

        private static readonly Material Battlewt6 = MaterialPool.MatFrom(Battlewt.Folder + "/6");

        private static readonly Material Battlewt7 = MaterialPool.MatFrom(Battlewt.Folder + "/7");

        private static readonly Material Battlewt8 = MaterialPool.MatFrom(Battlewt.Folder + "/8");

        private static readonly Material Battlewt9 = MaterialPool.MatFrom(Battlewt.Folder + "/9");

        private static readonly Material Battlewt10 = MaterialPool.MatFrom(Battlewt.Folder + "/10");

        private static readonly Material Battlewt11 = MaterialPool.MatFrom(Battlewt.Folder + "/11");

        private static readonly Material Battlewt12 = MaterialPool.MatFrom(Battlewt.Folder + "/12");

        private static readonly Material Battlewt13 = MaterialPool.MatFrom(Battlewt.Folder + "/13");

        private static readonly Material Battlewt14 = MaterialPool.MatFrom(Battlewt.Folder + "/14");

        private static readonly Material Battlewt15 = MaterialPool.MatFrom(Battlewt.Folder + "/15");

        private static readonly Material Battlewt16 = MaterialPool.MatFrom(Battlewt.Folder + "/16");

        private static readonly Material Battlewt17 = MaterialPool.MatFrom(Battlewt.Folder + "/17");

        private static readonly Material Battlewt18 = MaterialPool.MatFrom(Battlewt.Folder + "/18");

        private static readonly Material Battlewt19 = MaterialPool.MatFrom(Battlewt.Folder + "/19");

        private static readonly Material Battlewt20 = MaterialPool.MatFrom(Battlewt.Folder + "/20");

        private static readonly List<Material> Epact = new List<Material>();

        private int currFrame = 1;

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            Battlewt.Epact.Add(Battlewt.Battlewt1);
            Battlewt.Epact.Add(Battlewt.Battlewt2);
            Battlewt.Epact.Add(Battlewt.Battlewt3);
            Battlewt.Epact.Add(Battlewt.Battlewt4);
            Battlewt.Epact.Add(Battlewt.Battlewt5);
            Battlewt.Epact.Add(Battlewt.Battlewt6);
            Battlewt.Epact.Add(Battlewt.Battlewt7);
            Battlewt.Epact.Add(Battlewt.Battlewt8);
            Battlewt.Epact.Add(Battlewt.Battlewt9);
            Battlewt.Epact.Add(Battlewt.Battlewt10);
            Battlewt.Epact.Add(Battlewt.Battlewt11);
            Battlewt.Epact.Add(Battlewt.Battlewt12);
            Battlewt.Epact.Add(Battlewt.Battlewt13);
            Battlewt.Epact.Add(Battlewt.Battlewt14);
            Battlewt.Epact.Add(Battlewt.Battlewt15);
            Battlewt.Epact.Add(Battlewt.Battlewt16);
            Battlewt.Epact.Add(Battlewt.Battlewt17);
            Battlewt.Epact.Add(Battlewt.Battlewt18);
            Battlewt.Epact.Add(Battlewt.Battlewt19);
            Battlewt.Epact.Add(Battlewt.Battlewt20);
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
                Graphics.DrawMesh(MeshPool.plane20, matrix, Battlewt.Epact[this.currFrame], 0);
            return;
        }
    }
}
