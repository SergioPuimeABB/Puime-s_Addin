using System.Windows.Forms;
using System;
using ABB.Robotics.Math;
using ABB.Robotics.RobotStudio;
using ABB.Robotics.RobotStudio.Stations;


namespace Puime_s_Addin
{
    public class Copy_Position
    {
        //Vars to store the objects position and orientation
        static double PosX, PosY, PosZ, DegX, DegY, DegZ;

        public static void ObtainPosition()
        {
            #region Get the position of the selected object
            Project.UndoContext.BeginUndoStep("Get the position of the selected object");
            try
            {
                // Get the selected Part. 
                #region SelectPart1
                Part selectedPart = Selection.SelectedObjects.SingleSelectedObject as Part;
                #endregion

                // Get the selected WorkObject. 
                #region SelectWorkObject1
                RsWorkObject selectedWorkObject = Selection.SelectedObjects.SingleSelectedObject as RsWorkObject;
                #endregion

                // Get the selected RsTarget. 
                #region SelectRsTarget1
                RsTarget selectedRsTarget = Selection.SelectedObjects.SingleSelectedObject as RsTarget;
                #endregion

                // Check if there is a part selected. 
                #region SelectPart2
                if (selectedPart != null)
                {
                    // Asign the object position and orientation values to the vars
                    PosX = selectedPart.Transform.X;
                    PosY = selectedPart.Transform.Y;
                    PosZ = selectedPart.Transform.Z;

                    DegX = selectedPart.Transform.RX;
                    DegY = selectedPart.Transform.RY;
                    DegZ = selectedPart.Transform.RZ;

                    Logger.AddMessage(new LogMessage(selectedPart.Name.ToString() + " Position copied" +
                                                        " [X = " + selectedPart.Transform.X * 1000 +
                                                        ", Y = " + selectedPart.Transform.Y * 1000 +
                                                        ", Z = " + selectedPart.Transform.Z * 1000 +
                                                        ", Rx = " + Globals.RadToDeg(selectedPart.Transform.RX) +
                                                        ", Ry = " + Globals.RadToDeg(selectedPart.Transform.RY) +
                                                        ", Rz = " + Globals.RadToDeg(selectedPart.Transform.RZ) +
                                                        "]", "Puime's Add-in"));
                    return;
                }
                #endregion

                // Check if there is a RsTarget selected. 
                #region SelectRsTarget2
                if (selectedRsTarget != null)
                {
                    // The position obtained in RsRobTarget is relative to its WorkObject and we want it from the station origin (World).
                    // We can get the relative distance betwen the Target and the station origin with "GetRelativeTrasform"
                    RsWorkObject myWobj = new RsWorkObject(); // New WorkObject without position (it'll be created at 0,0,0) to define the RsTarget
                    RsRobTarget myRsRobTarget = new RsRobTarget(); // New RsRobTarget without position to define the RsTarget.
                    RsTarget myRsTarget = new RsTarget(myWobj, myRsRobTarget); // New RsTarget to use in the "GetRelativeTransform"

                    Matrix4 relMx = (selectedRsTarget.Transform.GetRelativeTransform(myRsTarget));

                    // Asing the objetc values to the position and orientation vars
                    PosX = relMx.Translation.x;
                    PosY = relMx.Translation.y;
                    PosZ = relMx.Translation.z;

                    DegX = relMx.EulerZYX.x;
                    DegY = relMx.EulerZYX.y;
                    DegZ = relMx.EulerZYX.z;

                    // RsTarget copied message
                    Logger.AddMessage(new LogMessage(selectedRsTarget.Name.ToString() + " Position copied" +
                                                        " [X = " + relMx.Translation.x * 1000 +
                                                        ", Y = " + relMx.Translation.y * 1000 +
                                                        ", Z = " + relMx.Translation.z * 1000 +
                                                        ", Rx = " + Globals.RadToDeg(relMx.EulerZYX.x) +
                                                        ", Ry = " + Globals.RadToDeg(relMx.EulerZYX.y) +
                                                        ", Rz = " + Globals.RadToDeg(relMx.EulerZYX.z) +
                                                        "]", "Puime's Add-in"));
                    return;
                }
                #endregion

                // Check if there is a WorkObject selected. 
                #region SelectedWorkObject2
                if (selectedWorkObject != null)
                {
                    // Asign the object position and orientation values to the vars
                    PosX = selectedWorkObject.UserFrame.X;
                    PosY = selectedWorkObject.UserFrame.Y;
                    PosZ = selectedWorkObject.UserFrame.Z;

                    DegX = selectedWorkObject.UserFrame.RX;
                    DegY = selectedWorkObject.UserFrame.RY;
                    DegZ = selectedWorkObject.UserFrame.RZ;

                    Logger.AddMessage(new LogMessage(selectedWorkObject.Name.ToString() + " Position copied" +
                                                        " [X = " + selectedWorkObject.UserFrame.X * 1000 +
                                                        ", Y = " + selectedWorkObject.UserFrame.Y * 1000 +
                                                        ", Z = " + selectedWorkObject.UserFrame.Z * 1000 +
                                                        ", Rx = " + Globals.RadToDeg(selectedWorkObject.UserFrame.RX) +
                                                        ", Ry = " + Globals.RadToDeg(selectedWorkObject.UserFrame.RY) +
                                                        ", Rz = " + Globals.RadToDeg(selectedWorkObject.UserFrame.RZ) +
                                                        "]", "Puime's Add-in"));
                    return;
                }

                else
                {
                    MessageBox.Show("Please, select a Part, a Target or a WorkObject.");
                }

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
            #endregion

            #endregion
        }

        public static void SetPosition()
        {
            #region Set the position of the selected object
            Project.UndoContext.BeginUndoStep("Set the position of the selected object");
            try
            {
                // Get the selected Part. 
                #region SelectPart1
                Part selectedPart = Selection.SelectedObjects.SingleSelectedObject as Part;
                #endregion

                // Get the selected WorkObject. 
                #region SelectWorkObject1
                RsWorkObject selectedWorkObject = Selection.SelectedObjects.SingleSelectedObject as RsWorkObject;
                #endregion

                // Get the selected RsTarget. 
                #region SelectRsTarget1
                RsTarget selectedRsTarget = Selection.SelectedObjects.SingleSelectedObject as RsTarget;
                #endregion

                // Check if there is a part selected. 
                #region SelectPart2
                if (selectedPart != null)

                {
                    #region Log Position and Orientation
                    // Asign the var values to the object position and orientation
                    selectedPart.Transform.X = PosX;
                    selectedPart.Transform.Y = PosY;
                    selectedPart.Transform.Z = PosZ;

                    selectedPart.Transform.RX = DegX;
                    selectedPart.Transform.RY = DegY;
                    selectedPart.Transform.RZ = DegZ;
                    #endregion
                    
                    Logger.AddMessage(new LogMessage("Position applied to " + selectedPart.Name.ToString() +
                                                     " [X = " + selectedPart.Transform.X * 1000 +
                                                     ", Y = " + selectedPart.Transform.Y * 1000 +
                                                     ", Z = " + selectedPart.Transform.Z * 1000 +
                                                     ", Rx = " + Globals.RadToDeg(selectedPart.Transform.RX) +
                                                     ", Ry = " + Globals.RadToDeg(selectedPart.Transform.RY) +
                                                     ", Rz = " + Globals.RadToDeg(selectedPart.Transform.RZ) +
                                                     "]", "Puime's Add-in"));
                    return;
                }
                #endregion


                // Check if there is a RsTarget selected. 
                #region SelectRsTarget2
                if (selectedRsTarget != null)
                {
                    RsWorkObject myWobj_origen = selectedRsTarget.WorkObject;
                    RsRobTarget myRsRobTarget_origen = new RsRobTarget();
                    RsTarget myRsTarget_origen = new RsTarget(myWobj_origen, myRsRobTarget_origen);

                    RsWorkObject myWobj_dest = new RsWorkObject();
                    RsRobTarget myRsRobTarget_dest = new RsRobTarget();
                    myRsRobTarget_dest.Frame.X = PosX;
                    myRsRobTarget_dest.Frame.Y = PosY;
                    myRsRobTarget_dest.Frame.Z = PosZ;
                    myRsRobTarget_dest.Frame.RX = DegX;
                    myRsRobTarget_dest.Frame.RY = DegY;
                    myRsRobTarget_dest.Frame.RZ = DegZ;
                    RsTarget myRsTarget_dest = new RsTarget(myWobj_dest, myRsRobTarget_dest);

                    Matrix4 relMx = (myRsTarget_dest.Transform.GetRelativeTransform(myRsTarget_origen));

                    // Asign the var values to the object position and orientation
                    selectedRsTarget.Transform.X = relMx.Translation.x;
                    selectedRsTarget.Transform.Y = relMx.Translation.y;
                    selectedRsTarget.Transform.Z = relMx.Translation.z;

                    selectedRsTarget.Transform.RX = relMx.EulerZYX.x;
                    selectedRsTarget.Transform.RY = relMx.EulerZYX.y;
                    selectedRsTarget.Transform.RZ = relMx.EulerZYX.z;

                    Logger.AddMessage(new LogMessage("Position applied to " + selectedRsTarget.Name.ToString() +
                                                        " [X = " + myRsTarget_dest.Transform.X * 1000 +
                                                        ", Y = " + myRsTarget_dest.Transform.Y * 1000 +
                                                        ", Z = " + myRsTarget_dest.Transform.Z * 1000 +
                                                        ", Rx = " + Globals.RadToDeg(myRsTarget_dest.Transform.RX) +
                                                        ", Ry = " + Globals.RadToDeg(myRsTarget_dest.Transform.RY) +
                                                        ", Rz = " + Globals.RadToDeg(myRsTarget_dest.Transform.RZ) +
                                                        "]", "Puime's Add-in"));
                    return;
                }
                #endregion

                // Check if there is a WorkObject selected. 
                #region SelectedWorkObject2
                if (selectedWorkObject != null)
                {
                    // Asign the object values to the vars position and orientation
                    selectedWorkObject.UserFrame.X = PosX;
                    selectedWorkObject.UserFrame.Y = PosY;
                    selectedWorkObject.UserFrame.Z = PosZ;

                    selectedWorkObject.UserFrame.RX = DegX;
                    selectedWorkObject.UserFrame.RY = DegY;
                    selectedWorkObject.UserFrame.RZ = DegZ;

                    Logger.AddMessage(new LogMessage("Position applied to " + selectedWorkObject.Name.ToString() +
                                                        " [X = " + selectedWorkObject.UserFrame.X * 1000 +
                                                        ", Y = " + selectedWorkObject.UserFrame.Y * 1000 +
                                                        ", Z = " + selectedWorkObject.UserFrame.Z * 1000 +
                                                        ", Rx = " + Globals.RadToDeg(selectedWorkObject.UserFrame.RX) +
                                                        ", Ry = " + Globals.RadToDeg(selectedWorkObject.UserFrame.RY) +
                                                        ", Rz = " + Globals.RadToDeg(selectedWorkObject.UserFrame.RZ) +
                                                        "]", "Puime's Add-in"));
                    return;
                }
                #endregion

                else
                {
                    MessageBox.Show("Please, select a Part, a Target or a WorkObject.");
                }

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
            #endregion
        }
    }
}
