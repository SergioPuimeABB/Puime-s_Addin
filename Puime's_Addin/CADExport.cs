using ABB.Robotics.RobotStudio.Stations;
using ABB.Robotics.RobotStudio;
using Puime_s_Addin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ABB.Robotics.Math;

namespace PuimesAddin
{
    internal class CADExport
    {
        public static void ExportCAD()
        {

            // look at the SDK Help - Class GraphicConverter

            #region try
            Project.UndoContext.BeginUndoStep("CAD Export");
            try
            {
                Station stn = Station.ActiveStation;
                if (stn == null) return;

                Logger.AddMessage(new LogMessage("Station :" + stn.ToString(), "Puime's Add-in"));
               

                
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
            #endregion try

        }
    }
}
