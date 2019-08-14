using ABB.Robotics.RobotStudio;
using ABB.Robotics.RobotStudio.Stations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puime_s_Addin
{
    class Create_Raiser
    {
        public static void Create_ABB_Raiser()
        {
            #region try
            //Begin UndoStep
            Project.UndoContext.BeginUndoStep("RotateBasedOnAxis");
            try
            {

                Logger.AddMessage(new LogMessage("===========", "Puime's Add-in"));
                Logger.AddMessage(new LogMessage("Raiser", "Puime's Add-in"));
                Logger.AddMessage(new LogMessage("===========", "Puime's Add-in"));

                Station stn = Station.ActiveStation;
                if (stn == null) return;

                //Logger.AddMessage(new LogMessage(stn.GraphicComponents.ToString(), "Puime's Add-in"));

                //foreach (Mechanism item in stn.GraphicComponents)

                foreach (GraphicComponent item in stn.GraphicComponents)
                {
                    bool mec = item.TypeDisplayName == "Mechanism";
                    if (mec)
                    {
                        Mechanism mec2 = item as Mechanism;
                        bool mec3 = mec2.MechanismType.ToString() == "Robot";
                        if (mec3)
                        {
                            string name = item.DisplayName.ToString();
                            //string IRB = name[0].ToString() + name[1].ToString() + name[2].ToString();
                            //bool robot = IRB == "IRB";
                            //if (robot)
                            //{
                            var xpos = item.Transform.X * 1000;
                            var ypos = item.Transform.Y * 1000;
                            var zpos = item.Transform.Z * 1000;
                            Logger.AddMessage(new LogMessage("___________  " + name, "Puime's Add-in"));
                            Logger.AddMessage(new LogMessage(xpos.ToString(), "Puime's Add-in"));
                            Logger.AddMessage(new LogMessage(ypos.ToString(), "Puime's Add-in"));
                            Logger.AddMessage(new LogMessage(zpos.ToString(), "Puime's Add-in"));
                            //}

                        }
                    } 
                    
                }


                    //RsTask myTask = stn.ActiveTask;

                    //// Hide Targets
                    //foreach (RsTarget tgr in myTask.Targets)
                    //{
                    //    tgr.Visible = false;
                    //}

                    //// Hide WorkObjects
                    //myTask.ActiveWorkObject.Visible = false;

                    //// Hide Paths
                    //foreach (RsPathProcedure pth in myTask.PathProcedures)
                    //{
                    //    pth.Visible = false;
                    //}

                    ////Hide Tool
                    //myTask.ActiveTool.Visible = false;

                }
            #endregion try

            catch (Exception execption)
                {
                    //Cancel UndoStep
                    Project.UndoContext.CancelUndoStep(CancelUndoStepType.Rollback);
                    Logger.AddMessage(new LogMessage(execption.Message.ToString()));
                    throw;
                }
            finally
                {
                    //End UndoStep
                     Project.UndoContext.EndUndoStep();
                }

        } // public static void Hide_Objects()
    } // class Raiser
}
