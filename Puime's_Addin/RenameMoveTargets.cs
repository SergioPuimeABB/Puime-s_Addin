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

                    // No ejecuta la llamada a GetTargetsfromPath(SelectedPath);
                    GetTargetsfromPath(SelectedPath);

                    // To test if it works
                    //Test(SelectedPath.ToString());
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
        public static IEnumerable<RsRobTarget> GetTargetsfromPath(RsPathProcedure pathProcedure)
        {
            // para comprobar que lo ejecuta
            Logger.AddMessage(new LogMessage(pathProcedure.Name.ToString() + " Selected to renamte targets", "Puime's Add-in"));
            
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
                            Logger.AddMessage(new LogMessage("Before ->" + robTarget.Name, "Puime's Add-in"));
                            //de la ayuda SDK-> target6.Name = station.ActiveTask.GetValidRapidName("Target", "_", 10);
                            robTarget.Name = toPointArg.Value = task.GetValidRapidName("p", "", 10);
                            Logger.AddMessage(new LogMessage("After ->" + robTarget.Name, "Puime's Add-in"));
                            // Also rename RsTargets
                            foreach (var target in task.FindTargets(moveInstruction.GetWorkObject(), robTarget))
                            {
                                target.Name = robTarget.Name;
                            }
                            yield return robTarget;
                        }
                    }
                }
            }
        }


        //public static List<RsRobTarget> GetTargetsfromPath(RsPathProcedure pathProcedure)
        //{
        //    List<RsRobTarget> listRsRobTarget = new List<RsRobTarget>();
        //    if (pathProcedure != null)
        //    {
        //        //Get RsTask object reference from pathProcedure.
        //        RsTask task = pathProcedure.Parent as RsTask;
        //        if (task != null)
        //        {
        //            //Iterate instructions in path procedure 
        //            for (int count = 0; count < pathProcedure.Instructions.Count; count++)
        //            {
        //                RsInstruction instruction = pathProcedure.Instructions[count];

        //                //Get RsInstruction from pathProcedure and check for the type of instruction.
        //                //If instruction type is RsMoveInstruction then execute the further steps.
        //                if (instruction.GetType() == typeof(RsMoveInstruction))
        //                {
        //                    // Convert RsInstruction object to RsMoveInstruction. 
        //                    RsMoveInstruction moveInstruction = instruction as RsMoveInstruction;

        //                    //Get RsRobTarget from RsMoveInstruction. 
        //                    string strToPoint = moveInstruction.GetToPointArgument().Value;
        //                    RsRobTarget rsrobTarget =
        //                        task.FindDataDeclarationFromModuleScope(strToPoint, pathProcedure.ModuleName)
        //                        as RsRobTarget;

        //                    string name = rsrobTarget.Name.ToString();
        //                    if (name.Trim().StartsWith("Target_"))
        //                    {
        //                        listRsRobTarget.Add(rsrobTarget);
        //                        //listTarget.ToString();
        //                        //Logger.AddMessage(new LogMessage(robTarget.Name.ToString(), "Puime's Add-in"));
        //                        //Logger.AddMessage(new LogMessage(string.Join(", ",listTarget)));
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    int pointnumber = 1;

        //    foreach (var item in listRsRobTarget)
        //    {
        //        Logger.AddMessage(new LogMessage("Before ->" + item.Name.ToString(), "Puime's Add-in"));
        //        //item.Name = "p" + pointnumber.ToString();
        //        item.Name = Station.ActiveStation.ActiveTask.GetValidRapidName("p", "", 10);

        //        //item.DisplayName = Station.ActiveStation.ActiveTask.GetValidRapidName("p", "", 10);
        //        pointnumber++;



        //        Logger.AddMessage(new LogMessage("After ->" + item.Name.ToString(), "Puime's Add-in"));
        //        //Station.ActiveStation.ActiveTask.DataDeclarations.Add(item);
        //    }
        //    return listRsRobTarget;
        //}



        static void Test(string test)
        {
            Logger.AddMessage(new LogMessage(test.ToString() + "   Test procedure ....", "Puime's Add-in"));
        }

    }
}