using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter
{
    class TriangleHitBox
    {
        public int x1;
        public int x2;
        public int x3;
        public int y1;
        public int y2;
        public int y3;

        public TriangleHitBox(int x1, int x2, int x3, int y1, int y2, int y3)
        {
            this.x1 = x1;
            this.x2 = x2;
            this.x3 = x3;
            this.y1 = y1;
            this.y2 = y2;
            this.y3 = y3;
        }
    }
}
