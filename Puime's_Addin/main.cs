using System;
using System.Drawing;
using System.Windows.Forms;

using ABB.Robotics.RobotStudio;
using ABB.Robotics.RobotStudio.Environment;
using ABB.Robotics.RobotStudio.Stations.Forms;

namespace Puime_s_Addin
{
    public class main
    {
        private static ToolWindow twPuimesAddin;
        private static Button btnCopyPosition;
        private static Button btnSetPosition;
        private static Button btnSeparator1;
        private static Button btnCreateFloor;
        private static Button btnSeparator2;
        private static Button btnCreateABBBox;
        //private static CommandBarButton btnCreateABBBox;
        private static Button btnSeparator3;
        private static Button btnCreateABBRaiser;
        private static Label lblCurrenteVersion;



        public static void AddinMain()
        {
            Logger.AddMessage(new LogMessage("Puime's Addin Loaded ... 2121.07.13 - 11:29", "Puime's Add-in"));
            if (twPuimesAddin == null)
            {
                addToolWindow();
            }
         }

        public static void addToolWindow()
        {
            Project.UndoContext.BeginUndoStep("ToolWindow Creation");
            try
            {
                twPuimesAddin = new ToolWindow("twPuimeAddin");
                btnCopyPosition = new Button();
                btnSetPosition = new Button();
                btnSeparator1 = new Button();
                btnCreateFloor = new Button();
                btnSeparator2 = new Button();
                btnCreateABBBox = new Button();
                //btnCreateABBBox = new CommandBarButton("CreateABBBox", "Create ABB Box");
                btnSeparator3 = new Button();
                btnCreateABBRaiser = new Button();
                lblCurrenteVersion = new Label();


                twPuimesAddin.Caption = "Puime's Addin";
                twPuimesAddin.PreferredSize = new Size(157, 100);
                UIEnvironment.Windows.AddDocked(twPuimesAddin, DockStyle.Right);
                twPuimesAddin.AutoHide = true;

                //
                // Create the buttons.
                //

                // Button "Copy position"
                btnCopyPosition.Text = "Copy position";
                btnCopyPosition.Size = new Size(70, 70);
                btnCopyPosition.Location = new Point(5, 5);
                btnCopyPosition.Click += new EventHandler(btnCopyPosition_clicked);
                btnCopyPosition.Image = Properties.Resources.BT_copy;
                btnCopyPosition.ImageAlign = ContentAlignment.TopCenter;
                btnCopyPosition.TextAlign = ContentAlignment.BottomCenter;
                btnCopyPosition.FlatStyle = FlatStyle.Flat;

                // Button "Set position"
                btnSetPosition.Text = "Set position";
                btnSetPosition.Size = new Size(70, 70);
                btnSetPosition.Location = new Point(80, 5);
                btnSetPosition.Click += new EventHandler(btnSetPosition_clicked);
                btnSetPosition.Image = Properties.Resources.BT_paste;
                btnSetPosition.ImageAlign = ContentAlignment.TopCenter;
                btnSetPosition.TextAlign = ContentAlignment.BottomCenter;
                btnSetPosition.FlatStyle = FlatStyle.Flat;

                // Create a separator button.
                btnSeparator1.Size = new Size(145, 2);
                btnSeparator1.Location = new Point(5, 84);
                btnSeparator1.FlatStyle = FlatStyle.Flat;
                btnSeparator1.Enabled = false;
                btnSeparator1.ForeColor = Color.Gray;

                // Button "Create floor"
                btnCreateFloor.Text = "Create floor    ";
                btnCreateFloor.Size = new Size(145, 40);
                btnCreateFloor.Location = new Point(5, 95);
                btnCreateFloor.Click += new EventHandler(bbtnCreateFloor_clicked);
                btnCreateFloor.Image = Properties.Resources.BT_floor;
                btnCreateFloor.ImageAlign = ContentAlignment.MiddleLeft;
                btnCreateFloor.TextAlign = ContentAlignment.MiddleRight;
                btnCreateFloor.FlatStyle = FlatStyle.Flat;

                // Create a separator button.
                btnSeparator2.Size = new Size(145, 2);
                btnSeparator2.Location = new Point(5, 144);
                btnSeparator2.FlatStyle = FlatStyle.Flat;
                btnSeparator2.Enabled = false;
                btnSeparator2.ForeColor = Color.Gray;

                // Button "Create ABB Box"
                btnCreateABBBox.Text = "Create ABB box  ";
                btnCreateABBBox.Size = new Size(145, 40);
                btnCreateABBBox.Location = new Point(5, 156);
                btnCreateABBBox.Image = Properties.Resources.BT_box_but;
                btnCreateABBBox.ImageAlign = ContentAlignment.MiddleLeft;
                btnCreateABBBox.TextAlign = ContentAlignment.MiddleRight;
                btnCreateABBBox.FlatStyle = FlatStyle.Flat;
                btnCreateABBBox.Click += new EventHandler(btnCreateABBBox_clicked);

                // Buscar cómo poner ID al boton
                // Para poder utilizarlo cómo un ToolControl
                --> ToolControlManager.RegisterToolCommand("CreateBoxButtonTag", ToolControlManager.FindToolHost("ElementBrowser"));
                // O pasar todo al Ribbon (cómo el EquipmentBuilder - puesto 7)


                // Create a separator button.
                btnSeparator3.Size = new Size(145, 2);
                btnSeparator3.Location = new Point(5, 205);
                btnSeparator3.FlatStyle = FlatStyle.Flat;
                btnSeparator3.Enabled = false;
                btnSeparator3.ForeColor = Color.Gray;

                // Button "ABB Raiser"
                btnCreateABBRaiser.Text = "Create ABB raiser";
                btnCreateABBRaiser.Size = new Size(145, 40);
                btnCreateABBRaiser.Location = new Point(5, 217);
                btnCreateABBRaiser.Click += new EventHandler(btnCreateABBRaiser_clicked);
                btnCreateABBRaiser.Image = Properties.Resources.BT_raiser;
                btnCreateABBRaiser.ImageAlign = ContentAlignment.MiddleLeft;
                btnCreateABBRaiser.TextAlign = ContentAlignment.MiddleRight;
                btnCreateABBRaiser.FlatStyle = FlatStyle.Flat;

                // Label Current version
                lblCurrenteVersion.Text = "Puime's addin v.1.2";
                lblCurrenteVersion.Location = new Point(50, 500);


                twPuimesAddin.Control.Controls.Add(btnCopyPosition);
                twPuimesAddin.Control.Controls.Add(btnSetPosition);
                twPuimesAddin.Control.Controls.Add(btnSeparator1);
                twPuimesAddin.Control.Controls.Add(btnCreateFloor);
                twPuimesAddin.Control.Controls.Add(btnSeparator2);
                twPuimesAddin.Control.Controls.Add(btnCreateABBBox);
                twPuimesAddin.Control.Controls.Add(btnSeparator3);
                twPuimesAddin.Control.Controls.Add(btnCreateABBRaiser);
                twPuimesAddin.Control.Controls.Add(lblCurrenteVersion);
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
        static void btnCopyPosition_clicked(object sender, EventArgs e)
        {
            Copy_Position.ObtainPosition();
            
        }

        // The event handler for the "Set Position" button.
        static void btnSetPosition_clicked(object sender, EventArgs e)
        {
            Copy_Position.SetPosition();
        }

        // The event handler for the "Create floor" button.
        static void bbtnCreateFloor_clicked(object sender, EventArgs e)
        {
            Make_Floor.ObtenerObjetosEstacion();
            Hide_CS.Hide_Objects();
            Hide_CS.ResetFloor(); 
            Logger.AddMessage(new LogMessage("Floor created.", "Puime's Add-in"));
        }


        // The event handler for the "Create ABB box" button.
        // Asigns a variable to the Create_box ToolWindow
        public static Create_box createbox;
        public static void btnCreateABBBox_clicked(object sender, EventArgs e)
        {
            //// Checks if the variable asigned to the ToolWindow is active (if the ToolWindow is allready created)
            //if (createbox == null)
            //{
            //    createbox = new Create_box();
            //}

            //// Creates a new EventHandler to check when the createbox Dispose is raised
            //createbox.ResetObj += new EventHandler(DisposeObj);


            //ToolControlManager.ShowTool(typeof(frmAutoMarkUpBuilder), e.Id);


        }

        // createbox Dispose is raised
        public static void DisposeObj (object sender, EventArgs e)
        {
            createbox = null; // when the ToolWindow us Disposed, the variable createbox is set to null.
        }


        // The event handler for the "ABB Raiser" button.
        static void btnCreateABBRaiser_clicked(object sender, EventArgs e)
        {
            Create_Raiser.Create_ABB_Raiser();
        }
    }
}
