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

        public static void CheckPathSelected()
        {
            #region CheckPathSelected
            Project.UndoContext.BeginUndoStep("CheckPathSelected");
            try
            {
                // Get the selected Path. 
                #region SelectParts
                //RsPathProcedure SelectedPath = Selection.SelectedObjects.SingleSelectedObject as RsPathProcedure;

                List<Part> listParts = new List<Part>();

                foreach (var item in Selection.SelectedObjects)
                {
                    Part SelectedPart = item as Part;
                    Logger.AddMessage(new LogMessage(SelectedPart.ToString() + " Selected -foreach", "Puime's Add-in"));
                    listParts.Add(SelectedPart);
                }
                #endregion SelectParts
                
                if (listParts.Count > 0)
                {

                    Logger.AddMessage(new LogMessage(listParts.Count.ToString() + " Selected - listParts", "Puime's Add-in"));
                    //GetBodiesfromPart(SelectedParts);
                    //return;
                }

                else
                {
                    MessageBox.Show("Please, select a Path.");
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
            #endregion CheckPathSelected
        }


        public static void GetBodiesfromPart(Part partselected)
        {
            //List<RsRobTarget> listRsRobTarget = new List<RsRobTarget>();
            if (partselected != null)
            {
                Logger.AddMessage(new LogMessage("GetBodiesFromPart", "Puime's Add-in"));

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