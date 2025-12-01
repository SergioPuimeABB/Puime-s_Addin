using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

                //Vars to store the objects position
                double PosX, PosY, PosZ;

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
                                PosX = MyBodie.Transform.X;
                                PosY = MyBodie.Transform.Y;
                                PosZ = MyBodie.Transform.Z;

                                MyBodie.Transform.X = item.Transform.X + PosX;
                                MyBodie.Transform.Y = item.Transform.Y + PosY;
                                MyBodie.Transform.Z = item.Transform.Z + PosZ;

                                listBodies.Add(MyBodie);
                                }
                            }
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
                    MessageBox.Show("Please, select a Part.", "Joint parts - Puime's Addin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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