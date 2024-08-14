using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ABB.Robotics.Controllers.RapidDomain;
using ABB.Robotics.Math;
using ABB.Robotics.RobotStudio;
using ABB.Robotics.RobotStudio.Stations;
using ABB.Robotics.RobotStudio.Stations.Forms;

namespace PuimesAddin
{
    class Camera_Zoom
    { 
        public static void Create_Zoom()
        {
            Project.UndoContext.BeginUndoStep("Zoom");
            try
            {
                //TODO:
                //Igual hacer ventana con más y menos
                GraphicControl.ActiveGraphicControl.Zoom (10,1);
            }
            catch (Exception execption)
            {
                Project.UndoContext.CancelUndoStep(CancelUndoStepType.Rollback);
                Logger.AddMessage(new LogMessage(execption.Message.ToString()));
                throw;
            }
            finally
            {
                Project.UndoContext.EndUndoStep();
            }
        }
    }

}
