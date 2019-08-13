using System;
using System.Drawing;
using System.Windows.Forms;

using ABB.Robotics.Math;
using ABB.Robotics.RobotStudio;
using ABB.Robotics.RobotStudio.Environment;
using ABB.Robotics.RobotStudio.Stations;

namespace Puime_s_Addin
{
    class main
    {
        public static void AddinMain()
        {
            Logger.AddMessage(new LogMessage("Puime's Add-in loaded ... 2019.08.12  15:00 ", "Puime's Add-in"));

            //AddMenusAndButtons(); //Botones 
            // ==============================
            // Cambiar por menú del ratón !!!
            // ==============================
            
            PuimesAddinToolWindow();

        }


        // PRUEBA DE MENU --- Buscar como añadir los botones al menu del raton
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
                // Set the text of the button.
                btn.Text = "Copy position";
                // Set the size of the button.
                btn.Size = new Size(70, 70);
                // Set the location of the button (pixels from top left corner).
                btn.Location = new Point(5, 5);
                // Add an event handler to the button.
                btn.Click += new EventHandler(btn1_clicked);
                // Set the image of the button
                btn.Image = Properties.Resources.BT_copy;
                btn.ImageAlign = ContentAlignment.TopCenter;
                // Set the text alignment
                btn.TextAlign = ContentAlignment.BottomCenter;
                // Set the button style
                btn.FlatStyle = FlatStyle.Flat;
                // Add the button to the ToolWindow.
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
            Logger.AddMessage(new LogMessage("Floor created.", "Puime's Add-in"));
        }


        // The event handler for the "Create ABB box" button.
        static void btn4_clicked(object sender, EventArgs e)
        {

            //int tw_width = UIEnvironment.Windows["ObjectBrowser"].Control.Size.Width - 90;
            //ToolWindow tw_CreateBox = new ToolWindow("Create_ABB_Box");
            //tw_CreateBox.Caption = "Create ABB Box";
            //tw_CreateBox.PreferredSize = new Size(tw_width, 330);
            //UIEnvironment.Windows.AddDocked(tw_CreateBox, System.Windows.Forms.DockStyle.Top, UIEnvironment.Windows["ObjectBrowser"] as ToolWindow);
            //tw_CreateBox.Control.Controls.Add(Create_box2);

            //
            // Looks if the "Create ABB Box" window is active, if it's active, closes it.
            //

            if (UIEnvironment.Windows.Contains(UIEnvironment.Windows["Create_ABB_Box"]))
            {
                UIEnvironment.Windows["Create_ABB_Box"].Close();
            }

            //Create_box.create_box();
            
            
            //Create_box.AddCustomControl();
            

            //string windows_count = UIEnvironment.Windows.ToString();
            //Logger.AddMessage(new LogMessage(windows_count.ToString(), "Puime's Add-in"));

            new Create_box();

        }




    }
}
