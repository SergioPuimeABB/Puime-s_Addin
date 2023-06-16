using ABB.Robotics.RobotStudio.Stations;
using ABB.Robotics.RobotStudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuimesAddin
{
    internal class AutoMoveParam
    {
        public static void AutoMoveParams()
        {
            Project.UndoContext.BeginUndoStep("Auto move parameters");
            try
            {
                //code
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
