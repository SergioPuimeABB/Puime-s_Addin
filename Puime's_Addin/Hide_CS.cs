using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ABB.Robotics.Math;
using ABB.Robotics.RobotStudio.Stations;
using ABB.Robotics.RobotStudio;

using ABB.Robotics.RobotStudio.Environment;
using ABB.Robotics.RobotStudio.Stations.Forms;

namespace Puime_s_Addin
{

    // Hide Coordinated systems (Targets, WorkObjetcs, Paths)
    //
    // Only works on the active Task
    // Only works on the active WorkObject
    // Only works on the active Tool
    //
    public class Hide_CS
    {
        public static void Hide_Objects()
        {
            Station stn = Station.ActiveStation;
            if (stn == null) return;
            
            RsTask myTask = stn.ActiveTask;

            // Hide Targets
            foreach (RsTarget tgr in myTask.Targets)
            {
                tgr.Visible = false;
            }

            // Hide WorkObjects
            myTask.ActiveWorkObject.Visible = false;

            // Hide Paths
            foreach (RsPathProcedure pth in myTask.PathProcedures)
            {
                pth.Visible = false;
            }

            //Hide Tool
            myTask.ActiveTool.Visible = false;

        }
    }
}
