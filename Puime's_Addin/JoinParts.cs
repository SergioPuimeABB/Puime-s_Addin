using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ABB.Robotics.Math;
using ABB.Robotics.RobotStudio;
using ABB.Robotics.RobotStudio.Stations;

namespace Puime_s_Addin
{
    class JoinParts
    {

        public static void JointBodies()
        {
            #region JointBodies
            Project.UndoContext.BeginUndoStep("JointBodies"); 
            try
            {
                Station station = Station.ActiveStation;

                Part JoinedPart = new Part();
                JoinedPart.Name = "JoinedPart";
                station.GraphicComponents.Add(JoinedPart);

                //Create a new part.
                //Part p = new Part();
                // poner el nombre válido automático
                //robTarget.Name = toPointArg.Value = task.GetValidRapidName("p", "", 10);


                // Get the selected Parts. 
                #region SelectParts
                //RsPathProcedure SelectedPath = Selection.SelectedObjects.SingleSelectedObject as RsPathProcedure;

                List<Part> listParts = new List<Part>();

                foreach (var item in Selection.SelectedObjects)
                {
                    Part SelectedPart = item as Part;
                 //   Logger.AddMessage(new LogMessage(SelectedPart.ToString() + " Selected -foreach", "Puime's Add-in"));
                    listParts.Add(SelectedPart);
                }


                if (listParts.Count > 0)
                {
                    foreach (var item in listParts)
                    {
                        
                        List<Body> listBodies = new List<Body>();

                        foreach (var bodiesList in item.Bodies.AsParallel()) //// No funciona, ver que hace "AsParallel" - Tiene que listar los bodies
                        {
                            listBodies.Add(bodiesList);
                        }
                    }
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
            #endregion CheckPathSelected
        }


        public static void GetBodiesfromPart(Part partselected)
        {
            //List<RsRobTarget> listRsRobTarget = new List<RsRobTarget>();
            

            if (partselected != null)
            {
                Logger.AddMessage(new LogMessage("GetBodiesFromPart", "Puime's Add-in"));
                
                //Body selectedbody = partselected as Body;

                List<Body> listBodies = new List<Body>();

               



                foreach (var body in listBodies)
                {

                }


                ////Get RsTask object reference from pathProcedure.
                //RsTask task = pathProcedure.Parent as RsTask;
                //if (task != null)
                //{
                //    //Iterate instructions in path procedure 
                //    for (int count = 0; count < pathProcedure.Instructions.Count; count++)
                //    {
                //        RsInstruction instruction = pathProcedure.Instructions[count];

                //        //Get RsInstruction from pathProcedure and check for the type of instruction.
                //        //If instruction type is RsMoveInstruction then execute the further steps.
                //        if (instruction.GetType() == typeof(RsMoveInstruction))
                //        {
                //            // Convert RsInstruction object to RsMoveInstruction. 
                //            RsMoveInstruction moveInstruction = instruction as RsMoveInstruction;

                //            //Get RsRobTarget from RsMoveInstruction. 
                //            string strToPoint = moveInstruction.GetToPointArgument().Value;
                //            RsRobTarget rsrobTarget =
                //                task.FindDataDeclarationFromModuleScope(strToPoint, pathProcedure.ModuleName)
                //                as RsRobTarget;

                //            string name = rsrobTarget.Name.ToString();
                //            if (name.Trim().StartsWith("Target_"))
                //            {
                //                listRsRobTarget.Add(rsrobTarget);
                //                //listTarget.ToString();
                //                //Logger.AddMessage(new LogMessage(robTarget.Name.ToString(), "Puime's Add-in"));
                //                //Logger.AddMessage(new LogMessage(string.Join(", ",listTarget)));
                //            }
                //        }
                //    }
                //}
                //}
                //int pointnumber = 1;

                //foreach (var item in listRsRobTarget)
                //{
                //    Logger.AddMessage(new LogMessage("Before ->" + item.Name.ToString(), "Puime's Add-in"));
                //    //item.Name = "p" + pointnumber.ToString();
                //    item.Name = Station.ActiveStation.ActiveTask.GetValidRapidName("p", "", 10);
                //    pointnumber++;
                //    Logger.AddMessage(new LogMessage("After ->" + item.Name.ToString(), "Puime's Add-in"));
                //    //Station.ActiveStation.ActiveTask.DataDeclarations.Add(item);
                //}
                //return null;

            }
        }

    }
}