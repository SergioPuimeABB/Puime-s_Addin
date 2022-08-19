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
    class RenameMoveTargets
    {

        public static void CheckPathSelected()
        {
            #region CheckPathSelected
            Project.UndoContext.BeginUndoStep("CheckPathSelected");
            try
            {
                // Get the selected Path. 
                #region SelectPath
                RsPathProcedure SelectedPath = Selection.SelectedObjects.SingleSelectedObject as RsPathProcedure;
                #endregion SelectPath

                if (SelectedPath != null)
                {
                    
                    Logger.AddMessage(new LogMessage(SelectedPath.Name.ToString() + " Selected", "Puime's Add-in"));

                    //for (GetTargetsfromPath(SelectedPath))
                    //{

                    //}

                    GetTargetsfromPath(SelectedPath);
                    return;
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

        public static List<RsRobTarget> GetTargetsfromPath(RsPathProcedure pathProcedure)
        {
            List<RsRobTarget> listTarget = new List<RsRobTarget>();
            
            if (pathProcedure != null)
            {
                //Get RsTask object reference from pathProcedure.
                RsTask task = pathProcedure.Parent as RsTask;
                if (task != null)
                {
                    //Iterate instructions in path procedure 
                    for (int count = 0; count < pathProcedure.Instructions.Count; count++)
                    {
                        RsInstruction instruction = pathProcedure.Instructions[count];

                        //Get RsInstruction from pathProcedure and check for the type of instruction.
                        //If instruction type is RsMoveInstruction then execute the further steps.
                        if (instruction.GetType() == typeof(RsMoveInstruction))
                        {
                            // Convert RsInstruction object to RsMoveInstruction. 
                            RsMoveInstruction moveInstruction = instruction as RsMoveInstruction;

                            //Get RsRobTarget from RsMoveInstruction. 
                            string strToPoint = moveInstruction.GetToPointArgument().Value;
                            RsRobTarget robTarget =
                                task.FindDataDeclarationFromModuleScope(strToPoint, pathProcedure.ModuleName)
                                as RsRobTarget;

                            string name = robTarget.Name.ToString();
                            if (name.Trim().StartsWith("Target_"))
                              {
                                listTarget.Add(robTarget);
                                //listTarget.ToString();
                                //Logger.AddMessage(new LogMessage(robTarget.Name.ToString(), "Puime's Add-in"));
                                //Logger.AddMessage(new LogMessage(string.Join(", ",listTarget)));
                            }
                        }
                    }


                }
            }
            return listTarget;

        }
    }
}