using ABB.Robotics.RobotStudio.Stations;
using ABB.Robotics.RobotStudio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ABB.Robotics.Math;
using RobotStudio.API.Internal;
using RobotStudio.API.Internal.RapidModel;
using System.Security.Policy;
using static System.Collections.Specialized.BitVector32;
using ABB.Robotics.Controllers.MotionDomain;
using System.Security.Cryptography;

namespace PuimesAddin
{
    public partial class frmAutoMoveParam : Form
    {
        public frmAutoMoveParam()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Project.UndoContext.BeginUndoStep("Auto move parameters");
            try
            {
                // Get the selected Move instructions
                #region SelectMoveInstructions

                //foreach (var item in Selection.SelectedObjects)
                //{
                //    RsMoveInstruction SelectedMove = item as RsMoveInstruction;
                //    if (SelectedMove != null)
                //    {
                //        SelectedMove.Name = "MoveJ";
                //        SelectedMove.Color = Color.Yellow;
                //        SelectedMove.InstructionArguments.ElementAt(3).Value = "vmax";
                //        SelectedMove.InstructionArguments.ElementAt(6).Value = "z200";

                //    }
                //    else
                //    {
                //        MessageBox.Show("Please, select a Move instruction.");
                //    }
                //}

                foreach (var item in Selection.SelectedObjects)
                {
                    RsInstruction SelectedMove = item as RsInstruction;
                    RsInstructionTemplate MyMoveTemplate = new RsInstructionTemplate();    
                    MyMoveTemplate.
                    
                    SelectedMove.SetInstructionTemplate(MyMoveTemplate);


                    if (SelectedMove != null)
                    {
                        SelectedMove.Name = "MoveJ";
                        SelectedMove.Color = Color.Yellow;
                        SelectedMove.InstructionArguments.ElementAt(3).Value = "vmax";
                        SelectedMove.InstructionArguments.ElementAt(6).Value = "z200";

                    }
                    else
                    {
                        MessageBox.Show("Please, select a Move instruction.");
                    }
                }

                #endregion SelectMoveInstructions
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

        private void button2_Click(object sender, EventArgs e)
        {
            Project.UndoContext.BeginUndoStep("Auto move parameters");
            try
            {
                // Get the selected Move instructions
                #region SelectMoveInstructions

                foreach (var item in Selection.SelectedObjects)
                {
                    RsMoveInstruction SelectedMove = item as RsMoveInstruction;
                    if (SelectedMove != null)
                    {
                        SelectedMove.Name = "MoveL";
                        SelectedMove.Color = Color.Green;
                        SelectedMove.InstructionArguments.ElementAt(3).Value = "vmax";
                        SelectedMove.InstructionArguments.ElementAt(6).Value = "z200";
                    }
                    else
                    {
                        MessageBox.Show("Please, select a Move instruction.");
                    }
                }

                #endregion SelectMoveInstructions
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

        private void button3_Click(object sender, EventArgs e)
        {
            Project.UndoContext.BeginUndoStep("Auto move parameters");
            try
            {
                // Get the selected Move instructions
                #region SelectMoveInstructions

                foreach (var item in Selection.SelectedObjects)
                {
                    RsMoveInstruction SelectedMove = item as RsMoveInstruction;
                    if (SelectedMove != null)
                    {
                        SelectedMove.Name = "MoveJ";
                        SelectedMove.Color = Color.Brown;
                        SelectedMove.InstructionArguments.ElementAt(3).Value = "vmax";
                        SelectedMove.InstructionArguments.ElementAt(6).Value = "z10";
                    }
                    else
                    {
                        MessageBox.Show("Please, select a Move instruction.");
                    }
                }

                #endregion SelectMoveInstructions
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

        private void button4_Click(object sender, EventArgs e)
        {
            Project.UndoContext.BeginUndoStep("Auto move parameters");
            try
            {
                // Get the selected Move instructions
                #region SelectMoveInstructions

                foreach (var item in Selection.SelectedObjects)
                {
                    RsMoveInstruction SelectedMove = item as RsMoveInstruction;
                    if (SelectedMove != null)
                    {
                        SelectedMove.Name = "MoveL";
                        SelectedMove.Color = Color.White;
                        SelectedMove.InstructionArguments.ElementAt(3).Value = "vmax";
                        SelectedMove.InstructionArguments.ElementAt(6).Value = "z10";
                    }
                    else
                    {
                        MessageBox.Show("Please, select a Move instruction.");
                    }
                }

                #endregion SelectMoveInstructions
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

        private void button5_Click(object sender, EventArgs e)
        {
            Project.UndoContext.BeginUndoStep("Auto move parameters");
            try
            {
                // Get the selected Move instructions
                #region SelectMoveInstructions

                foreach (var item in Selection.SelectedObjects)
                {
                    RsMoveInstruction SelectedMove = item as RsMoveInstruction;
                    if (SelectedMove != null)
                    {
                        SelectedMove.Name = "MoveJ";
                        SelectedMove.Color = Color.Orange;
                        SelectedMove.InstructionArguments.ElementAt(3).Value = "v200";
                        SelectedMove.InstructionArguments.ElementAt(6).Value = "fine";
                    }
                    else
                    {
                        MessageBox.Show("Please, select a Move instruction.");
                    }
                }

                #endregion SelectMoveInstructions
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


        private void button6_Click(object sender, EventArgs e)
        {
            Project.UndoContext.BeginUndoStep("Auto move parameters");
            try
            {
                // Get the selected Move instructions
                #region SelectMoveInstructions

                foreach (var item in Selection.SelectedObjects)
                {
                    RsMoveInstruction SelectedMove = item as RsMoveInstruction;
                    if (SelectedMove != null)
                    {
                        SelectedMove.Name = "MoveL";
                        SelectedMove.Color = Color.Blue;
                        SelectedMove.InstructionArguments.ElementAt(3).Value = "v200";
                        SelectedMove.InstructionArguments.ElementAt(6).Value = "fine";
                    }
                    else
                    {
                        MessageBox.Show("Please, select a Move instruction.");
                    }
                }

                #endregion SelectMoveInstructions
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

        private void button7_Click(object sender, EventArgs e)
        {
            Project.UndoContext.BeginUndoStep("Auto move parameters");
            try
            {
                // Get the selected Move instructions
                #region SelectMoveInstructions

                foreach (var item in Selection.SelectedObjects)
                {
                    RsMoveInstruction SelectedMove = item as RsMoveInstruction;
                    if (SelectedMove != null)
                    {
                        SelectedMove.Name = "MoveJ";
                        SelectedMove.Color = Color.DeepSkyBlue;
                        SelectedMove.InstructionArguments.ElementAt(3).Value = "v200";
                        SelectedMove.InstructionArguments.ElementAt(6).Value = "z10";
                    }
                    else
                    {
                        MessageBox.Show("Please, select a Move instruction.");
                    }
                }

                #endregion SelectMoveInstructions
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

        private void button8_Click(object sender, EventArgs e)
        {
            Project.UndoContext.BeginUndoStep("Auto move parameters");
            try
            {
                // Get the selected Move instructions
                #region SelectMoveInstructions

                foreach (var item in Selection.SelectedObjects)
                {
                    RsMoveInstruction SelectedMove = item as RsMoveInstruction;
                    if (SelectedMove != null)
                    {
                        SelectedMove.Name = "MoveL";
                        SelectedMove.Color = Color.SkyBlue;
                        SelectedMove.InstructionArguments.ElementAt(3).Value = "v200";
                        SelectedMove.InstructionArguments.ElementAt(6).Value = "z10";
                    }
                    else
                    {
                        MessageBox.Show("Please, select a Move instruction.");
                    }
                }

                #endregion SelectMoveInstructions
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
