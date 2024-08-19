using ABB.Robotics.RobotStudio.Stations.Forms;
using ABB.Robotics.RobotStudio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ABB.Robotics.Math;
using ABB.Robotics.RobotStudio.Environment;
using ABB.Robotics.RobotStudio.Stations;
using PuimesAddin.Properties;



namespace PuimesAddin
{
    public partial class frmZoom : Form
    {

        float zoomfactor = 10;

        public frmZoom()
        {
            InitializeComponent();
        }

        

        private void btn_more_Click(object sender, EventArgs e)
        {
            Project.UndoContext.BeginUndoStep("Zoom");
            try
            {
                //TODO:
                //Igual hacer ventana con más y menos
                GraphicControl.ActiveGraphicControl.Zoom(zoomfactor, 0.2F);
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

        private void btn_less_Click(object sender, EventArgs e)
        {
            Project.UndoContext.BeginUndoStep("Zoom");
            try
            {
                //TODO:
                //Igual hacer ventana con más y menos
                GraphicControl.ActiveGraphicControl.Zoom(-zoomfactor, 0.2F);
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

        private void nUD_ZoomFactor_ValueChanged(object sender, EventArgs e)
        {
            zoomfactor = (float)nUD_ZoomFactor.Value;
        }
    }
}
