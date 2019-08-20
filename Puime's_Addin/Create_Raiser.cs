using ABB.Robotics.Math;
using ABB.Robotics.RobotStudio;
using ABB.Robotics.RobotStudio.Stations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                            string[] complete_name = name.Split('_');
                            string robot_model = complete_name[0];

                            switch (robot_model)
                            {
                                case "IRB52": case "IRB1600": case "IRB1600ID": case "IRB2600": case "IRB2600ID": case "IRB4600":
                                    Logger.AddMessage(new LogMessage("___________  " + name + " ---> Type A.", "Puime's Add-in"));
                                    break;

                                case "IRB2400":
                                    Logger.AddMessage(new LogMessage("___________  " + name + " ---> Type B.", "Puime's Add-in"));
                                    Raiser(name, null, item.Transform.X*1000, item.Transform.Y*1000, item.Transform.Z*1000);
                                    break;
                                    
                                case "IRB6400R": case "IRB6620": case "IRB6640": case "IRB6650S": case "IRB6660": case "IRB6700":
                                case "IRB7600": case "IRB660": case "IRB760": case "IRB460":
                                    Logger.AddMessage(new LogMessage("___________  " + name + " ---> Type C.", "Puime's Add-in"));
                                    break;

                                default:
                                    //Logger.AddMessage(new LogMessage("___________  " + name + " ---> Not supported Robot model.", "Puime's Add-in"));
                                    MessageBox.Show(name + " - Not supported Robot model.", "Puime's Addin - Create ABB Raiser", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                            }

                            //var xpos = item.Transform.X * 1000;
                            //var ypos = item.Transform.Y * 1000;
                            //var zpos = item.Transform.Z * 1000;
                            //Logger.AddMessage(new LogMessage("___________  " + name + "--->" + robot_model, "Puime's Add-in"));
                            //Logger.AddMessage(new LogMessage(xpos.ToString(), "Puime's Add-in"));
                            //Logger.AddMessage(new LogMessage(ypos.ToString(), "Puime's Add-in"));
                            //Logger.AddMessage(new LogMessage(zpos.ToString(), "Puime's Add-in"));
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

        public static void Raiser(string name, string type, double xpos, double ypos, double height)
        {
            if (height < 300)
            {
            MessageBox.Show(name + " - Not supported Robot position or height. Minium position must be 300mm.", "Puime's Addin - Create ABB Raiser", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                try
                {
                    Station station = Project.ActiveProject as Station;
                    //
                    // Create a part to contain the bodies.
                    Part p = new Part();
                    p.Name = "ABB_Box";
                    station.GraphicComponents.Add(p);
                    
                    //
                    // Create a solid box.
                    Vector3 vect_position = new Vector3(0, 0, 0);
                    Vector3 vect_orientation = new Vector3(0,0,0);
                    Matrix4 matrix_origo = new Matrix4(vect_position, vect_orientation);
                    //Vector3 size = new Vector3(length_textbox.Value / 1000, width_textbox.Value / 1000, height_textbox.Value / 1000);

                    //Body b1 = Body.CreateSolidBox(matrix_origo, size);
                    Body b1 = Body.CreateSolidCylinder(matrix_origo, 330, height);
                    b1.Name = "Box";
                    p.Bodies.Add(b1);

                    // Transform the position of the part to the values of the pos_control values. So the part origin is allways in the corner of the box.
                    p.Transform.X = xpos;
                    p.Transform.X = ypos;
                    p.Transform.Z = 0;
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




    } // class Raiser
}
