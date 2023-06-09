using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ABB.Robotics.Math;
using ABB.Robotics.RobotStudio;
using ABB.Robotics.RobotStudio.Stations;
using RobotStudio.API.Internal;

namespace Puime_s_Addin
{
    class JoinParts
    {

        public static void JointBodies()
        {
            Project.UndoContext.BeginUndoStep("JointBodies"); 
            try
            {
                Station station = Station.ActiveStation;

                //Vars to store the objects position and orientation
                //double PosX, PosY, PosZ, DegX, DegY, DegZ;




                // Get the selected Parts. 
                #region SelectParts

                List<Part> listParts = new List<Part>();

                foreach (var item in Selection.SelectedObjects)
                {
                    Part SelectedPart = item as Part;
                        if (SelectedPart != null)
                        {
                            listParts.Add(SelectedPart);
                        }
                }


                if (listParts.Count > 0)
                {
                    List<Body> listBodies = new List<Body>();
                    foreach (var item in listParts)
                    {
                            foreach (var item_bodies in item.Bodies)
                            { 
                                Body MyBodie = item_bodies as Body;
                                if (MyBodie != null)

                                {
                                //Body MyresBodie = MyBodie as Body;
                                //MyBodie.MoveToPart(JoinedPart);
                                //item.Bodies.Remove(MyBodie);
                                //listBodies.Add(MyBodie);
                                //listBodies.Add((Body)MyBodie);
                                //listBodies.Insert(0, MyBodie);
                                
                                Logger.AddMessage(new LogMessage("Transform: X: " + MyBodie.Transform.X.ToString() +
                                                    " Y: " + MyBodie.Transform.Y.ToString() +
                                                    " Z: " + MyBodie.Transform.Z.ToString() +
                                                    " || Rx: " + MyBodie.Transform.RX.ToString() +
                                                    " Ry: " + MyBodie.Transform.RY.ToString() +
                                                    " Rz: " + MyBodie.Transform.RZ.ToString()));
                                listBodies.Add(MyBodie);
                            }
                            }

                            //station.GraphicComponents.Remove(item);
                    }

                    ////Resultant part with all the parts bodies
                    Part JoinedPart = new Part();
                    JoinedPart.Name = "JoinedPart";
                    station.GraphicComponents.Add(JoinedPart);

                    ////Transfert all the Bodies to the part JoinedPart
                    foreach (var item in listBodies)
                    {
                        JoinedPart.Bodies.Add(item);
                    }

                    ////Delete the selected Parts
                    foreach (var item in listParts.ToList())
                    {
                        station.GraphicComponents.Remove(item);
                    }

                    //Clear the Parts list
                    listParts.Clear();
                }

                else
                {
                    MessageBox.Show("Please, select a Part.");
                }

                #endregion SelectParts

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