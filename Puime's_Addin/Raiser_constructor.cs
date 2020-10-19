using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puime_s_Addin
{
    public class Raiser_constructor
    {
        private string name;
        private string type;
        private double xpos;
        private double ypos;
        private double zpos;
        private double orientation;

        public Raiser_constructor(string name, string type, double xpos, double ypos, double zpos, double orientation)
        {
            this.name = name;
            this.type = type;
            this.xpos = xpos;
            this.ypos = ypos;
            this.zpos = zpos;
            this.orientation = orientation;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        public double Xpos
        {
            get { return xpos; }
            set { xpos = value; }
        }
        public double Ypos
        {
            get { return ypos; }
            set { ypos = value; }
        }
        public double Zpos
        {
            get { return zpos; }
            set { zpos = value; }
        }
        public double Orientation
        {
            get { return orientation; }
            set { orientation = value; }
        }
    }
}
