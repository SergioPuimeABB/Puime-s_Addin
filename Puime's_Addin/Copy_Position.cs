
using System.Windows.Forms;

using ABB.Robotics.Math;
using ABB.Robotics.RobotStudio;
using ABB.Robotics.RobotStudio.Stations;
using System;

namespace Puime_s_Addin
{
    public class Copy_Position
    {
        //Variables para almacenar la posición y orientación de los objetos
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
                    // Asignamos los valores del objeto a las variables de posición y orientación
                    PosX = selectedPart.Transform.X;
                    PosY = selectedPart.Transform.Y;
                    PosZ = selectedPart.Transform.Z;

                    DegX = selectedPart.Transform.RX;
                    DegY = selectedPart.Transform.RY;
                    DegZ = selectedPart.Transform.RZ;

                    // Mensaje del part copiado
                    // (rselectedPart.Transform.RX * 180) / System.Math.PI
                    // Globals.RadToDeg(selectedPart.Transform.RX)

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

                    // La posición que nos muestra directamenta el RsRobTarget es relativo a su WorkObject y nosotros lo queremos
                    // relativo al origen de la estación (mundo).
                    // Obtenemos la distancia relativa entre el punto y el origen de la estación con "GetRelativeTransform"

                    RsWorkObject myWobj = new RsWorkObject(); // Declaramos un WorkObject sin definirle posición para que lo cree en el 0,0,0 (para definir el RsTarget)
                    RsRobTarget myRsRobTarget = new RsRobTarget(); // Declaramos un RsRobTarget sin posición (para definir el RsTarget)
                    RsTarget myRsTarget = new RsTarget(myWobj, myRsRobTarget); // Declaramos el RsTarget para utilizarlo en "GetRelativeTransform"

                    Matrix4 relMx = (selectedRsTarget.Transform.GetRelativeTransform(myRsTarget));

                    // Asignamos los valores del objeto a las variables de posición y orientación

                    PosX = relMx.Translation.x;
                    PosY = relMx.Translation.y;
                    PosZ = relMx.Translation.z;

                    DegX = relMx.EulerZYX.x;
                    DegY = relMx.EulerZYX.y;
                    DegZ = relMx.EulerZYX.z;

                    // Mensaje del RsTarget copiado

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
                    // Asignamos los valores del objeto a las variables de posición y orientación
                    PosX = selectedWorkObject.UserFrame.X;
                    PosY = selectedWorkObject.UserFrame.Y;
                    PosZ = selectedWorkObject.UserFrame.Z;

                    DegX = selectedWorkObject.UserFrame.RX;
                    DegY = selectedWorkObject.UserFrame.RY;
                    DegZ = selectedWorkObject.UserFrame.RZ;

                    // Mensaje del WorkObject copiado
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
                // Get the selected part. 
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
                    // Asignamos los valores las variables de posición y orientación al los valores del objeto
                    selectedPart.Transform.X = PosX;
                    selectedPart.Transform.Y = PosY;
                    selectedPart.Transform.Z = PosZ;

                    selectedPart.Transform.RX = DegX;
                    selectedPart.Transform.RY = DegY;
                    selectedPart.Transform.RZ = DegZ;
                    #endregion
                    // Mensaje del part modificado
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

                    // Asignamos los valores del objeto a las variables de posición y orientación

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
                    // Asignamos los valores del objeto a las variables de posición y orientación
                    selectedWorkObject.UserFrame.X = PosX;
                    selectedWorkObject.UserFrame.Y = PosY;
                    selectedWorkObject.UserFrame.Z = PosZ;

                    selectedWorkObject.UserFrame.RX = DegX;
                    selectedWorkObject.UserFrame.RY = DegY;
                    selectedWorkObject.UserFrame.RZ = DegZ;

                    // Mensaje del WorkObject copiado
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
