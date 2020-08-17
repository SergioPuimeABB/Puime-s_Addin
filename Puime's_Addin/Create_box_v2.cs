using ABB.Robotics.Math;
using ABB.Robotics.RobotStudio;
using ABB.Robotics.RobotStudio.Environment;
using ABB.Robotics.RobotStudio.Stations;
using ABB.Robotics.RobotStudio.Stations.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puime_s_Addin
{
    public class Create_box_v2
    {

        public static PictureBox pb_createBox = new PictureBox();
        public static Label lb_reference = new Label();
        public static ComboBox cb_reference = new ComboBox();
        public static PositionControl pos_control = new PositionControl();
        public static OrientationControl orientation_control = new OrientationControl();
        public static Button btn_clear = new Button();
        public static Button btn_create = new Button();
        public static Button btn_close = new Button();
        public static NumericTextBox length_textbox = new NumericTextBox();
        public static NumericTextBox width_textbox = new NumericTextBox();
        public static NumericTextBox height_textbox = new NumericTextBox();
        //((System.ComponentModel.ISupportInitialize)(pb_createBox)).BeginInit();

        public static void create_box_v2()
            {
                Project.UndoContext.BeginUndoStep("AddToolWindow");
            
            #region add toolwindow and elements
            try
            {

                // Add a ToolWindow.
                    int tw_width = UIEnvironment.Windows["ObjectBrowser"].Control.Size.Width - 30;
                    ToolWindow tw_CreateBox = new ToolWindow("Create_ABB_Box");
                    tw_CreateBox.Caption = "Create ABB Box";
                    tw_CreateBox.PreferredSize = new Size(tw_width, 330);
                    UIEnvironment.Windows.AddDocked(tw_CreateBox, System.Windows.Forms.DockStyle.Top, UIEnvironment.Windows["ObjectBrowser"] as ToolWindow);

                if (tw_CreateBox.Control.CanFocus)
                {
                    tw_CreateBox.Control.Focus();
                }


                //
                //Add elements
                //

                // 
                // picture_box_createBox
                // 
                pb_createBox.Location = new System.Drawing.Point(8, 8);
                pb_createBox.Name = "pb_createBox";
                pb_createBox.Size = new System.Drawing.Size(65, 65);
                pb_createBox.BorderStyle = BorderStyle.FixedSingle;
                pb_createBox.Image = Properties.Resources.BT_box_tw;
                //this.pb_createBox.TabIndex = 0;
                pb_createBox.TabStop = false;

                // 
                // lb_reference
                // 
                lb_reference.AutoSize = true;
                lb_reference.Location = new System.Drawing.Point(79, 30);
                lb_reference.Name = "lb_reference";
                lb_reference.Size = new System.Drawing.Size(100, 15);
                //this.lb_reference.TabIndex = 1;
                lb_reference.Text = "Reference";

                // 
                // combo_box_reference
                // 
                //int cb_width = UIEnvironment.Windows["ObjectBrowser"].Control.Size.Width - 10; // the ObjectBrowser width less the position of the cb_reference
                //                                                                               // more a margin of 10, to obtain the width size of the cb_reference
                cb_reference.FormattingEnabled = true;
                cb_reference.Location = new System.Drawing.Point(80, 50);
                cb_reference.Name = "cb_reference";
                cb_reference.Size = new System.Drawing.Size(105, 21);
                cb_reference.FlatStyle = FlatStyle.Flat;
                cb_reference.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                cb_reference.TabIndex = 0;
                //tw_CreateBox.Control.Controls.Add(cb_reference);
                cb_reference.Items.Add("World");
                cb_reference.Items.Add("UCS");
                //        buttonComboBox.SelectionChanged += new EventHandler(btnComboBox_SelectionChanged);
                cb_reference.SelectedIndex = 0;


                // 
                // pos_control
                // 
                int pc_width = UIEnvironment.Windows["ObjectBrowser"].Control.Size.Width - 18; // the ObjectBrowser width less the position of the cb_reference
                                                                                               // more a margin of 18, to obtain the width size of the cb_reference
                pos_control.ErrorProviderControl = null;
                pos_control.ExpressionErrorString = "Bad Expression";
                pos_control.LabelQuantity = ABB.Robotics.RobotStudio.BuiltinQuantity.Length;
                pos_control.LabelText = "Corner Point";
                pos_control.Location = new Point(8, 85);
                pos_control.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                pos_control.MaxValueErrorString = "Value exceeds maximum";
                pos_control.MinValueErrorString = "Value is below minimum";
                pos_control.Name = "pos_control";
                pos_control.NumTextBoxes = 3;
                pos_control.ReadOnly = false;
                pos_control.RefCoordSys = null;
                pos_control.ShowLabel = true;
                pos_control.Size = new Size(177, 34);
                pos_control.TabIndex = 1;
                pos_control.Text = "positionControl1";
                pos_control.VerticalLayout = false;
                //pos_control.Select();


                // 
                // orientation_control
                // 
                //OrientationControl orientation_control = new OrientationControl();
                orientation_control.ErrorProviderControl = null;
                orientation_control.ExpressionErrorString = "Bad Expression";
                orientation_control.LabelQuantity = ABB.Robotics.RobotStudio.BuiltinQuantity.Length;
                orientation_control.LabelText = "Orientation";
                orientation_control.Location = new Point(8, 125);
                orientation_control.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                orientation_control.MaxValueErrorString = "Value exceeds maximum";
                orientation_control.MinValueErrorString = "Value is below minimum";
                orientation_control.Name = "orientation_control";
                orientation_control.NumTextBoxes = 3;
                orientation_control.ReadOnly = false;
                orientation_control.ShowLabel = true;
                orientation_control.Size = new Size(177, 34);
                orientation_control.TabIndex = 2;
                orientation_control.Text = "positionControl1";
                orientation_control.VerticalLayout = false;

                //
                // numericTextBox - Length
                // 
                //NumericTextBox length_textbox = new NumericTextBox();
                length_textbox.ErrorProviderControl = null;
                length_textbox.ExpressionErrorString = "Bad Expression";
                length_textbox.LabelText = "Length (mm)";
                length_textbox.Location = new Point(8, 165);
                length_textbox.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                length_textbox.MaxValue = 1000000000D;
                length_textbox.MaxValueErrorString = "Value exceeds maximum";
                length_textbox.MinValue = -1000000000D;
                length_textbox.MinValueErrorString = "Value is below minimum";
                length_textbox.Name = "length_textbox";
                length_textbox.Quantity = ABB.Robotics.RobotStudio.BuiltinQuantity.None;
                length_textbox.ReadOnly = false;
                length_textbox.ShowLabel = true;
                length_textbox.Size = new Size(177, 34);
                length_textbox.StepSize = 1D;
                length_textbox.TabIndex = 3;
                length_textbox.Text = "numericTextBox1";
                length_textbox.UserEdited = false;
                length_textbox.Value = 0D;
                length_textbox.ValueChanged += new EventHandler(size_TextChanged);

                //
                // numericTextBox - Width
                // 
                //NumericTextBox width_textbox = new NumericTextBox();
                width_textbox.ErrorProviderControl = null;
                width_textbox.ExpressionErrorString = "Bad Expression";
                width_textbox.LabelText = "Width (mm)";
                width_textbox.Location = new Point(8, 205);
                width_textbox.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                width_textbox.MaxValue = 1000000000D;
                width_textbox.MaxValueErrorString = "Value exceeds maximum";
                width_textbox.MinValue = -1000000000D;
                width_textbox.MinValueErrorString = "Value is below minimum";
                width_textbox.Name = "width_textbox";
                width_textbox.Quantity = ABB.Robotics.RobotStudio.BuiltinQuantity.None;
                width_textbox.ReadOnly = false;
                width_textbox.ShowLabel = true;
                width_textbox.Size = new Size(177, 34);
                width_textbox.StepSize = 1D;
                width_textbox.TabIndex = 4;
                width_textbox.Text = "numericTextBox2";
                width_textbox.UserEdited = false;
                width_textbox.Value = 0D;
                width_textbox.TextChanged += new EventHandler(size_TextChanged);

                //
                // numericTextBox - Height
                // 
                //NumericTextBox height_textbox = new NumericTextBox();
                height_textbox.ErrorProviderControl = null;
                height_textbox.ExpressionErrorString = "Bad Expression";
                height_textbox.LabelText = "Height (mm)";
                height_textbox.Location = new Point(8, 245);
                height_textbox.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                height_textbox.MaxValue = 1000000000D;
                height_textbox.MaxValueErrorString = "Value exceeds maximum";
                height_textbox.MinValue = -1000000000D;
                height_textbox.MinValueErrorString = "Value is below minimum";
                height_textbox.Name = "height_textbox";
                height_textbox.Quantity = ABB.Robotics.RobotStudio.BuiltinQuantity.None;
                height_textbox.ReadOnly = false;
                height_textbox.ShowLabel = true;
                height_textbox.Size = new Size(177, 34);
                height_textbox.StepSize = 1D;
                height_textbox.TabIndex = 5;
                height_textbox.Text = "numericTextBox3";
                height_textbox.UserEdited = false;
                height_textbox.Value = 0D;
                height_textbox.ValueChanged += new EventHandler(size_TextChanged);

                //
                // Button Clear
                //
                //Button btn_clear = new Button();
                btn_clear.Text = "Clear";
                btn_clear.Name = "btn_clear";
                btn_clear.Size = new Size(53, 25);
                btn_clear.Location = new Point(12, 295);
                btn_clear.Anchor = AnchorStyles.Right | AnchorStyles.Top;
                btn_clear.Click += new EventHandler(btn_clear_clicked);
                btn_clear.TextAlign = ContentAlignment.MiddleCenter;
                btn_clear.FlatStyle = FlatStyle.Flat;
                btn_clear.UseVisualStyleBackColor = true;
                btn_clear.TabIndex = 6;

                //
                // Button Create
                //
                //Button btn_create = new Button();
                btn_create.Text = "Create";
                btn_create.Name = "btn_create";
                btn_create.Size = new Size(53, 25);
                btn_create.Location = new Point(72, 295);
                btn_create.Anchor = AnchorStyles.Right | AnchorStyles.Top;
                btn_create.TextAlign = ContentAlignment.MiddleCenter;
                btn_create.FlatStyle = FlatStyle.Flat;
                btn_create.UseVisualStyleBackColor = true;
                btn_create.TabIndex = 7;
                btn_create.Enabled = false;
                btn_create.Click += new EventHandler(btn_create_clicked);

                //
                // Button Close
                //
                //Button btn_close = new Button();
                btn_close.Text = "Close";
                btn_close.Name = "btn_close";
                btn_close.Size = new Size(53, 25);
                btn_close.Location = new Point(132, 295);
                btn_close.Anchor = AnchorStyles.Right | AnchorStyles.Top;
                btn_close.TextAlign = ContentAlignment.MiddleCenter;
                btn_close.FlatStyle = FlatStyle.Flat;
                btn_close.UseVisualStyleBackColor = true;
                btn_close.TabIndex = 8;
                btn_close.Click += new EventHandler(btn_close_clicked);

                // 
                // Create_box2
                // 

                tw_CreateBox.Control.Controls.Add(height_textbox);
                tw_CreateBox.Control.Controls.Add(width_textbox);
                tw_CreateBox.Control.Controls.Add(length_textbox);
                tw_CreateBox.Control.Controls.Add(btn_clear);
                tw_CreateBox.Control.Controls.Add(btn_create);
                tw_CreateBox.Control.Controls.Add(btn_close);
                tw_CreateBox.Control.Controls.Add(orientation_control);
                tw_CreateBox.Control.Controls.Add(pos_control);
                tw_CreateBox.Control.Controls.Add(cb_reference);
                tw_CreateBox.Control.Controls.Add(lb_reference);
                tw_CreateBox.Control.Controls.Add(pb_createBox);
                ((System.ComponentModel.ISupportInitialize)(pb_createBox)).EndInit();
            }

            catch (Exception ex)
                {
                    Project.UndoContext.CancelUndoStep(CancelUndoStepType.Rollback);
                    Logger.AddMessage(new LogMessage(ex.Message.ToString()));
                    throw;
                }
                finally
                {
                    Project.UndoContext.EndUndoStep();
                }
            #endregion add toolwindow and elements


            }



        //private static void size_TextChanged(object sender, EventArgs e)
        //{
        //    var bl = (length_textbox.Value != 0) && (width_textbox.Value != 0) && (height_textbox.Value != 0);
        //    btn_create.Enabled = bl;
        //}

        //private static void btn_clear_clicked(object sender, EventArgs e)
        //{
        //    //UIEnvironment.Windows["Create_ABB_Box"].Close();
        //    //Create_box.create_box();
        //    //cb_reference.SelectedIndex = 0;
        //    //pos_control.ResetText();
        //    //orientation_control.ResetText();
        //    //length_textbox.ResetText();
        //    //width_textbox.ResetText();
        //    //height_textbox.ResetText();
        //}

        //private static void btn_close_clicked(object sender, EventArgs e)
        //    {
        //        UIEnvironment.Windows["Create_ABB_Box"].Close();
        //    //Logger.AddMessage(new LogMessage(create_box. + " bbox.min.x = " + bbox.min.x))
        //}

        private static void size_TextChanged(object sender, EventArgs e)
        {
            var bl = (length_textbox.Value != 0) && (width_textbox.Value != 0) && (height_textbox.Value != 0);
            btn_create.Enabled = bl;
        }

        private static void btn_clear_clicked(object sender, EventArgs e)
        {
            UIEnvironment.Windows["Create_ABB_Box"].Close();
            new Create_box_v2();
        }
        private static void btn_close_clicked(object sender, EventArgs e)
        {
            UIEnvironment.Windows["Create_ABB_Box"].Close();
        }


        private static void btn_create_clicked(object sender, EventArgs e)
        {
            #region Create ABB_Box
            Project.UndoContext.BeginUndoStep("Create ABB Box");

            try
            {
                Station station = Project.ActiveProject as Station;

                //
                // Create a part to contain the bodies.
                #region BodyCreateSolidsStep1
                Part p = new Part();
                p.Name = "ABB_Box";
                station.GraphicComponents.Add(p);
                #endregion

                //
                // Create a solid box.
                #region Create Box
                Vector3 vect_position = new Vector3(0, 0, 0);
                Vector3 vect_orientation = new Vector3(0, 0, 0); //uses 0,0,0 as origin to later transform the position to the pos_control values,
                                                                 //so the part origin is allways in the corner of the box.
                Matrix4 matrix_origo = new Matrix4(vect_position, vect_orientation);
                Vector3 size = new Vector3(length_textbox.Value / 1000, width_textbox.Value / 1000, height_textbox.Value / 1000);

                Body b1 = Body.CreateSolidBox(matrix_origo, size);
                b1.Name = "Box";
                p.Bodies.Add(b1);
                #endregion

                //
                // Transform the position of the part to the values of the pos_control values.
                //

                //
                // Evaluate the reference selected item
                bool reference_select_world = cb_reference.SelectedItem.ToString() == "World"; // true if World is selected
                if (reference_select_world) // World is selected
                {
                    //
                    // Transform the position and orientation of the Part to the values of the control.
                    p.Transform.X = pos_control.Value.x;
                    p.Transform.Y = pos_control.Value.y;
                    p.Transform.Z = pos_control.Value.z;
                    p.Transform.RX = orientation_control.Value.x;
                    p.Transform.RY = orientation_control.Value.y;
                    p.Transform.RZ = orientation_control.Value.z;
                }
                else // USC selected
                {
                    //
                    // When UCS is selected we need to translate the Part from the USC position with the UCS orientation.
                    // If not, the World coordinetes are used and it sets wrong result.
                    //

                    //
                    // Transform the position and orientation of the Part to the values of the UCS.
                    // To move it relative to the UCS
                    p.Transform.X = station.UCS.X;
                    p.Transform.Y = station.UCS.Y;
                    p.Transform.Z = station.UCS.Z;
                    p.Transform.RX = station.UCS.RX;
                    p.Transform.RY = station.UCS.RY;
                    p.Transform.RZ = station.UCS.RZ;

                    //
                    // Create a Matrix with the control values.
                    Vector3 vect_pos_input = new Vector3(pos_control.Value.x, pos_control.Value.y, pos_control.Value.z);
                    Vector3 vect_ori_input = new Vector3(orientation_control.Value.x, orientation_control.Value.y, orientation_control.Value.z);
                    Matrix4 matrix_input = new Matrix4(vect_pos_input, vect_ori_input);

                    //
                    // Transfor the position of the Part from it's position the values from the control with the Part orientation.
                    p.Transform.SetRelativeTransform(p, matrix_input);
                }


                //
                // Get the faces from the box.
                Face myFace0 = b1.Shells[0].Faces[0]; // z +
                Face myFace1 = b1.Shells[0].Faces[1]; // z -
                Face myFace2 = b1.Shells[0].Faces[2]; // y -
                Face myFace3 = b1.Shells[0].Faces[3]; // x -
                Face myFace4 = b1.Shells[0].Faces[4]; // y +
                Face myFace5 = b1.Shells[0].Faces[5]; // x +

                //
                // Make sure the faces are visible.
                myFace0.Visible = true;
                myFace1.Visible = true;
                myFace2.Visible = true;
                myFace3.Visible = true;
                myFace4.Visible = true;
                myFace5.Visible = true;

                //
                // Set the material for each face of the box
                Bitmap bmp0 = new Bitmap(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\ABB Industrial IT\\Robotics IT\\Puime's Addin\\Textures\\top.jpg");
                Bitmap bmp1 = new Bitmap(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\ABB Industrial IT\\Robotics IT\\Puime's Addin\\Textures\\bottom.jpg");
                Bitmap bmp2 = new Bitmap(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\ABB Industrial IT\\Robotics IT\\Puime's Addin\\Textures\\long_side.jpg");
                Bitmap bmp3 = new Bitmap(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\ABB Industrial IT\\Robotics IT\\Puime's Addin\\Textures\\short_side2.jpg");
                Bitmap bmp4 = new Bitmap(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\ABB Industrial IT\\Robotics IT\\Puime's Addin\\Textures\\long_side2.jpg");
                Bitmap bmp5 = new Bitmap(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\ABB Industrial IT\\Robotics IT\\Puime's Addin\\Textures\\short_side.jpg");
                Texture texture0 = new Texture(bmp0);
                Texture texture1 = new Texture(bmp1);
                Texture texture2 = new Texture(bmp2);
                Texture texture3 = new Texture(bmp3);
                Texture texture4 = new Texture(bmp4);
                Texture texture5 = new Texture(bmp5);
                Material material0 = new Material(texture0);
                Material material1 = new Material(texture1);
                Material material2 = new Material(texture2);
                Material material3 = new Material(texture3);
                Material material4 = new Material(texture4);
                Material material5 = new Material(texture5);
                myFace0.SetMaterial(material0);
                myFace1.SetMaterial(material1);
                myFace2.SetMaterial(material2);
                myFace3.SetMaterial(material3);
                myFace4.SetMaterial(material4);
                myFace5.SetMaterial(material5);


                //
                // Reset the ToolWindow
                UIEnvironment.Windows["Create_ABB_Box"].Close();
                Create_box_v2.create_box_v2();

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
        }


        //public static void AddCustomControl()
        //{
        //    //Begin UndoStep
        //    Project.UndoContext.BeginUndoStep("AddCustomControlButtons");

        //    try
        //    {
        //        //Create a ribbon group
        //        RibbonGroup customControlGroup = new RibbonGroup("MyCustomControls", "MyCustom Control");

        //        //Create ribbon control layouts
        //        RibbonControlLayout[] comboBoxLayout = new RibbonControlLayout[] { (RibbonControlLayout)2, (RibbonControlLayout)2 };
        //        RibbonControlLayout[] textBoxlayout = new RibbonControlLayout[] { (RibbonControlLayout)2, (RibbonControlLayout)2 };

        //        //Create combobox button control
        //        CommandBarComboBox buttonComboBox = new CommandBarComboBox("MyModule");
        //        buttonComboBox.Caption = "Procedure";
        //        //buttonComboBox.Image = Image.FromFile(@"..\Resources\TpsQuickSetSpeed.jpg");
        //        buttonComboBox.HelpText = "HELPTEXT";
        //        buttonComboBox.DropDown += new EventHandler(btnComboBox_DropDown);

        //        //Create combobox item and add it to commandbar combobox
        //        CommandBarComboBoxItem cmbBoxItem = new CommandBarComboBoxItem("procedure1");
        //        cmbBoxItem.Tag = "procedure1";
        //        buttonComboBox.Items.Add(cmbBoxItem);
        //        buttonComboBox.SelectionChanged += new EventHandler(btnComboBox_SelectionChanged);
        //        buttonComboBox.SelectedIndex = 0;
        //        //Add control to ribbon group
        //        customControlGroup.Controls.Add(buttonComboBox);
        //        //Set layout of ribbon group
        //        customControlGroup.SetControlLayout(buttonComboBox, comboBoxLayout);


        //        // Create TextBox Control
        //        System.Windows.Forms.TextBox textBoxDisplayNumber = new System.Windows.Forms.TextBox();
        //        textBoxDisplayNumber.Enabled = true;
        //        textBoxDisplayNumber.ReadOnly = false;
        //        //Set Color
        //        textBoxDisplayNumber.BackColor = Color.Black;
        //        textBoxDisplayNumber.ForeColor = Color.SpringGreen;
        //        textBoxDisplayNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        //        textBoxDisplayNumber.Text = "Type here";
        //        textBoxDisplayNumber.Width = 150;

        //        //Create a CommandBarCustomControl using TextBox control
        //        CommandBarCustomControl buttonTextBox = new CommandBarCustomControl("MyTextBox", textBoxDisplayNumber);
        //        buttonTextBox.Caption = "Data";
        //        //buttonTextBox.Image = Image.FromFile(@"..\Resources\TpsQuickSetSpeed.jpg");
        //        buttonTextBox.HelpText = "Displays data typed";

        //        //Add control to ribbon group
        //        customControlGroup.Controls.Add(buttonTextBox);

        //        //Set layout of ribbon group
        //        customControlGroup.SetControlLayout(buttonTextBox, textBoxlayout);

        //        //Add ribbon group to ribbon tab
        //        UIEnvironment.ActiveRibbonTab.Groups.Add(customControlGroup);
        //    }
        //    catch (Exception ex)
        //    {
        //        Project.UndoContext.CancelUndoStep(CancelUndoStepType.Rollback);
        //        Logger.AddMessage(new LogMessage(ex.Message.ToString()));
        //    }
        //    finally
        //    {
        //        Project.UndoContext.EndUndoStep();
        //    }
        //}

        //static void btnComboBox_SelectionChanged(object sender, EventArgs e)
        //{
        //    //Called when index in comboxbox changes
        //}

        //static void btnComboBox_DropDown(object sender, EventArgs e)
        //{
        //    //Called when clicked and combobox
        //}


    }

    



}