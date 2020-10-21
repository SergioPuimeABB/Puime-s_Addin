using System;
using System.Drawing;
using System.Windows.Forms;

using ABB.Robotics.RobotStudio;
using ABB.Robotics.RobotStudio.Environment;

namespace Puime_s_Addin
{
    public class main
    {
        public static void AddinMain()
        {
            Logger.AddMessage(new LogMessage("Puime's Addin Loaded", "Puime's Add-in"));
            PuimesAddinToolWindow();
        }

        public static void PuimesAddinToolWindow()
        {
            Project.UndoContext.BeginUndoStep("ToolWindow Creation");
            try
            {
                // Add a ToolWindow where we can put some buttons.
                ToolWindow tw = new ToolWindow();
                tw.Caption = "Puime's Addin";
                tw.PreferredSize = new Size(157, 100);
                // Add it docked to the right of the screen.
                UIEnvironment.Windows.AddDocked(tw, DockStyle.Right);
                // Starts it hidden
                tw.AutoHide = true;

                //
                // Create the buttons.
                //

                // Button "Copy position"
                Button btn = new Button();
                btn.Text = "Copy position";
                btn.Size = new Size(70, 70);
                btn.Location = new Point(5, 5);
                btn.Click += new EventHandler(btn1_clicked);
                btn.Image = Properties.Resources.BT_copy;
                btn.ImageAlign = ContentAlignment.TopCenter;
                btn.TextAlign = ContentAlignment.BottomCenter;
                btn.FlatStyle = FlatStyle.Flat;
                tw.Control.Controls.Add(btn);

                // Button "Set position"
                Button btn2 = new Button();
                btn2.Text = "Set position";
                btn2.Size = new Size(70, 70);
                btn2.Location = new Point(80, 5);
                btn2.Click += new EventHandler(btn2_clicked);
                btn2.Image = Properties.Resources.BT_paste;
                btn2.ImageAlign = ContentAlignment.TopCenter;
                btn2.TextAlign = ContentAlignment.BottomCenter;
                btn2.FlatStyle = FlatStyle.Flat;
                tw.Control.Controls.Add(btn2);

                // Create a separator button.
                Button btn2_2 = new Button();
                btn2_2.Size = new Size(145, 2);
                btn2_2.Location = new Point(5, 84);
                btn2_2.FlatStyle = FlatStyle.Flat;
                btn2_2.Enabled = false;
                btn2_2.ForeColor = Color.Gray;
                tw.Control.Controls.Add(btn2_2);
                
                // Button "Create floor"
                Button btn3 = new Button();
                btn3.Text = "Create floor    ";
                btn3.Size = new Size(145, 40);
                btn3.Location = new Point(5, 95);
                btn3.Click += new EventHandler(btn3_clicked);
                btn3.Image = Properties.Resources.BT_floor;
                btn3.ImageAlign = ContentAlignment.MiddleLeft;
                btn3.TextAlign = ContentAlignment.MiddleRight;
                btn3.FlatStyle = FlatStyle.Flat;
                tw.Control.Controls.Add(btn3);

                // Create a separator button.
                Button btn3_2 = new Button();
                btn3_2.Size = new Size(145, 2);
                btn3_2.Location = new Point(5, 144);
                btn3_2.FlatStyle = FlatStyle.Flat;
                btn3_2.Enabled = false;
                btn3_2.ForeColor = Color.Gray;
                tw.Control.Controls.Add(btn3_2);

                // Button "Create ABB Box"
                Button btn4 = new Button();
                btn4.Text = "Create ABB box  ";
                btn4.Size = new Size(145, 40);
                btn4.Location = new Point(5, 156);
                btn4.Click += new EventHandler(btn4_clicked);
                btn4.Image = Properties.Resources.BT_box_but;
                btn4.ImageAlign = ContentAlignment.MiddleLeft;
                btn4.TextAlign = ContentAlignment.MiddleRight;
                btn4.FlatStyle = FlatStyle.Flat;
                tw.Control.Controls.Add(btn4);

                // Create a separator button.
                Button btn4_2 = new Button();
                btn4_2.Size = new Size(145, 2);
                btn4_2.Location = new Point(5, 205);
                btn4_2.FlatStyle = FlatStyle.Flat;
                btn4_2.Enabled = false;
                btn4_2.ForeColor = Color.Gray;
                tw.Control.Controls.Add(btn4_2);

                // Button "ABB Raiser"
                Button btn5 = new Button();
                btn5.Text = "Create ABB raiser";
                btn5.Size = new Size(145, 40);
                btn5.Location = new Point(5, 217);
                btn5.Click += new EventHandler(btn5_clicked);
                btn5.Image = Properties.Resources.BT_raiser;
                btn5.ImageAlign = ContentAlignment.MiddleLeft;
                btn5.TextAlign = ContentAlignment.MiddleRight;
                btn5.FlatStyle = FlatStyle.Flat;
                tw.Control.Controls.Add(btn5);

                // Label Current version
                Label lbl_version = new Label();
                lbl_version.Text = "V. Alpha 0.1";
                lbl_version.Location = new Point(90, 500);
                tw.Control.Controls.Add(lbl_version);
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
        }

        // The event handler for the "Copy Position" button.
        static void btn1_clicked(object sender, EventArgs e)
        {
            Copy_Position.ObtainPosition();
            
        }

        // The event handler for the "Set Position" button.
        static void btn2_clicked(object sender, EventArgs e)
        {
            Copy_Position.SetPosition();
        }

        // The event handler for the "Create floor" button.
        static void btn3_clicked(object sender, EventArgs e)
        {
            Make_Floor.ObtenerObjetosEstacion();
            Hide_CS.Hide_Objects();
            Hide_CS.ResetFloor(); 
            Logger.AddMessage(new LogMessage("Floor created.", "Puime's Add-in"));
        }


        // The event handler for the "Create ABB box" button.

        // Asigns a variable to the Create_box ToolWindow
        public static Create_box createbox;
        public static void btn4_clicked(object sender, EventArgs e)
        {
            
            // Checks if the variable asigned to the ToolWindow is active (if the ToolWindow is allready created)
            if (createbox == null)
            {
                createbox = new Create_box();
            }

            // Creates a new EventHandler to check when the createbox Dispose is raised
            createbox.ResetObj += new EventHandler(DisposeObj);
        }

        // createbox Dispose is raised
        public static void DisposeObj (object sender, EventArgs e)
        {
            createbox = null; // when the ToolWindow us Disposed, the variable createbox is set to null.
        }


        // The event handler for the "ABB Raiser" button.
        static void btn5_clicked(object sender, EventArgs e)
        {
            Create_Raiser.Create_ABB_Raiser();
        }
    }
}
