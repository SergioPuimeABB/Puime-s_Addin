using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ABB.Robotics.RobotStudio;
using ABB.Robotics.RobotStudio.Stations;


//////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////
//
// It doesn't works at all. If a Move to Target_xxx is duplicate at the Path (same target), it can't be changed.
//
//////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////


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

                    foreach (var item in GetTargetsfromPath(SelectedPath))
                    {

                    }

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




        // Sugested by Johannes Weiman in RobotStudio/Developers Team group https://teams.microsoft.com/l/message/19:fc87de66a8304ab7ad3bf639d12477a9@thread.skype/1662028694687?tenantId=372ee9e0-9ce0-4033-a64a-c07073a91ecd&groupId=77cfb933-c26c-4def-bb9f-893725a4f6b1&parentMessageId=1662028694687&teamName=RobotStudio&channelName=Developers&createdTime=1662028694687&allowXTenantAccess=false
        static IEnumerable<RsRobTarget> GetTargetsfromPath(RsPathProcedure pathProcedure)
        {

            var lst_before = new List<string>();
            var lst_after = new List<string>();

            if (pathProcedure?.Parent is RsTask task)
            {
                foreach (var moveInstruction in pathProcedure.Instructions.OfType<RsMoveInstruction>())
                {
                    var toPointArg = moveInstruction.GetToPointArgument();
                    if (toPointArg != null)
                    {
                        RsRobTarget robTarget = task.FindDataDeclarationFromModuleScope(toPointArg.Value, pathProcedure.ModuleName) as RsRobTarget;
                        if (robTarget != null && robTarget.Name.Trim().StartsWith("Target_"))
                        {
                            //Adds the name before the change to show it in the RS log 
                            lst_before.Add(robTarget.Name); 

                            //from the SDK help-> target6.Name = station.ActiveTask.GetValidRapidName("Target", "_", 10);
                            robTarget.Name = toPointArg.Value = task.GetValidRapidName("p", "", 10);
                            
                            //Adds the name after the change to show it in the RS log 
                            lst_after.Add(robTarget.Name);

                            // Also rename RsTargets
                            foreach (var target in task.FindTargets(moveInstruction.GetWorkObject(), robTarget))
                            {
                                target.Name = robTarget.Name;
                            }
                            yield return robTarget;
                        }
                    }
                }


                for(int i = 0; i < lst_before.Count; i++)
                {
                    Logger.AddMessage(new LogMessage(lst_before[i] + " renamed to " + lst_after[i], "Puime's Add-in"));
                }

                lst_before.Clear();
                lst_after.Clear();

            }
        }
    }
}