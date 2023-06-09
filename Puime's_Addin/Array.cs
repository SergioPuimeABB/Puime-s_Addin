using ABB.Robotics.RobotStudio.Stations;
using ABB.Robotics.RobotStudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuimesAddin
{
    internal class Array
    {
        public static void JointBodies()
        {
            Project.UndoContext.BeginUndoStep("Object array");
            try
            {
                Station station = Station.ActiveStation;
            }

            catch
            {
                Project.UndoContext.CancelUndoStep(CancelUndoStepType.Rollback);
                throw;
            }
            finally
            {
                Project.UndoContext.EndUndoStep();
            }
        }
    }
}
