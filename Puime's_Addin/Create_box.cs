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
    public class Create_box
    {
        //
        // variables definition. 
        //
        public static ComboBox cb_reference = new ComboBox();
        public static PositionControl pos_control = new PositionControl();
        public static OrientationControl orientation_control = new OrientationControl();
        public static NumericTextBox length_textbox = new NumericTextBox();
        public static NumericTextBox width_textbox = new NumericTextBox();
        public static NumericTextBox height_textbox = new NumericTextBox();
        public static Button btn_create = new Button();

        public static void create_box()
            {
                Project.UndoContext.BeginUndoStep("AddToolWindow");

                //
                // Looks if the "Create ABB Box" window is active, if it's active, closes it.
                //
                if (UIEnvironment.Windows.Contains(UIEnvironment.Windows["Create_ABB_Box"]))
                {
                    UIEnvironment.Windows["Create_ABB_Box"].Close();
                }

            #region add toolwindow and elements
            try
            {
                    // Add a ToolWindow.
                    int tw_width = UIEnvironment.Windows["ObjectBrowser"].Control.Size.Width - 90;
                    ToolWindow tw_CreateBox = new ToolWindow("Create_ABB_Box");
                    tw_CreateBox.Caption = "Create ABB Box";
                    tw_CreateBox.PreferredSize = new Size(tw_width, 330);
                    UIEnvironment.Windows.AddDocked(tw_CreateBox, System.Windows.Forms.DockStyle.Top, UIEnvironment.Windows["ObjectBrowser"] as ToolWindow);

                    //
                    //Add elements
                    //

                    //Picturebox
                    PictureBox pb_createBox = new PictureBox();
                    pb_createBox.Size = new Size(65, 65);
                    pb_createBox.BorderStyle = BorderStyle.FixedSingle;
                    pb_createBox.Image = Properties.Resources.BT_box;
                    pb_createBox.Location = new Point(8, 8);
                    tw_CreateBox.Control.Controls.Add(pb_createBox);

                    //Label 
                    Label lb_reference = new Label();
                    lb_reference.Location = new Point(79, 30);
                    lb_reference.Size = new Size(100, 15);
                    lb_reference.Text = "Reference";
                    //lb_reference.BackColor = Color.Blue;
                    tw_CreateBox.Control.Controls.Add(lb_reference);

                    //ComboBox
                    int cb_width = UIEnvironment.Windows["ObjectBrowser"].Control.Size.Width - 90; // the ObjectBrowser width less the position of the cb_reference
                                                                                                   // more a margin of 10, to obtain the width size of the cb_reference
                    //ComboBox cb_reference = new ComboBox();
                    cb_reference.Location = new Point(80, 50);
                    cb_reference.Size = new Size(cb_width, 20);
                    cb_reference.FlatStyle = FlatStyle.Flat;
                    cb_reference.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                    cb_reference.TabIndex = 1;
                    tw_CreateBox.Control.Controls.Add(cb_reference);

                    ////ReferenceComboBox
                    //int cb_width = UIEnvironment.Windows["ObjectBrowser"].Control.Size.Width - 90; // the ObjectBrowser width less the position of the cb_reference
                    //                                                                               // more a margin of 10, to obtain the width size of the cb_reference
                    //ReferenceComboBox cb_reference = new ReferenceComboBox();
                    //cb_reference.Location = new Point(80, 50);
                    //cb_reference.Size = new Size(cb_width, 20);
                    ////cb_reference.FlatStyle = FlatStyle.Flat;
                    //cb_reference.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                    //tw_CreateBox.Control.Controls.Add(cb_reference);
                    ////cb_reference.RefCoordSys.Items.Add();

                    //Create combobox item and add it to commandbar combobox
                    //CommandBarComboBoxItem cmbBoxItem = new CommandBarComboBoxItem("procedure1");
                    //cmbBoxItem.Tag = "procedure1";
                    cb_reference.Items.Add("World");
                    //        buttonComboBox.SelectionChanged += new EventHandler(btnComboBox_SelectionChanged);
                    cb_reference.Items.Add("UCS");
                    cb_reference.SelectedIndex = 0;
                    //Add control to ribbon group
                    tw_CreateBox.Control.Controls.Add(cb_reference);

                    // PositionControl
                    int pc_width = UIEnvironment.Windows["ObjectBrowser"].Control.Size.Width - 18; // the ObjectBrowser width less the position of the cb_reference
                                                                                                   // more a margin of 10, to obtain the width size of the cb_reference
                    //PositionControl pos_control = new PositionControl();
                    pos_control.ErrorProviderControl = null;
                    pos_control.ExpressionErrorString = "Bad Expression";
                    pos_control.LabelQuantity = ABB.Robotics.RobotStudio.BuiltinQuantity.Length;
                    pos_control.LabelText = "Corner Point";
                    pos_control.Location = new Point(8, 85);
                    pos_control.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                    pos_control.MaxValueErrorString = "Value exceeds maximum";
                    pos_control.MinValueErrorString = "Value is below minimum";
                    pos_control.Name = "positionControl1";
                    pos_control.NumTextBoxes = 3;
                    pos_control.ReadOnly = false;
                    pos_control.RefCoordSys = null;
                    pos_control.ShowLabel = true;
                    pos_control.Size = new Size(pc_width, 34);
                    pos_control.TabIndex = 2;
                    pos_control.Text = "positionControl1";
                    pos_control.VerticalLayout = false;
                    pos_control.Focus();
                    pos_control.Select();
                    tw_CreateBox.Control.Controls.Add(pos_control);

                    // OrientationControl
                    //OrientationControl orientation_control = new OrientationControl();
                    orientation_control.ErrorProviderControl = null;
                    orientation_control.ExpressionErrorString = "Bad Expression";
                    orientation_control.LabelQuantity = ABB.Robotics.RobotStudio.BuiltinQuantity.Length;
                    orientation_control.LabelText = "Orientation";
                    orientation_control.Location = new Point(8, 125);
                    orientation_control.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                    orientation_control.MaxValueErrorString = "Value exceeds maximum";
                    orientation_control.MinValueErrorString = "Value is below minimum";
                    orientation_control.Name = "positionControl1";
                    orientation_control.NumTextBoxes = 3;
                    orientation_control.ReadOnly = false;
                    //orientation_control.RefCoordSys = null;
                    orientation_control.ShowLabel = true;
                    orientation_control.Size = new Size(pc_width, 34);
                    orientation_control.TabIndex = 3;
                    orientation_control.Text = "positionControl1";
                    orientation_control.VerticalLayout = false;
                    tw_CreateBox.Control.Controls.Add(orientation_control);


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
                    length_textbox.Name = "numericTextBox1";
                    length_textbox.Quantity = ABB.Robotics.RobotStudio.BuiltinQuantity.None;
                    length_textbox.ReadOnly = false;
                    length_textbox.ShowLabel = true;
                    length_textbox.Size = new Size(pc_width, 34);
                    length_textbox.StepSize = 1D;
                    length_textbox.TabIndex = 4;
                    length_textbox.Text = "numericTextBox1";
                    length_textbox.UserEdited = false;
                    length_textbox.Value = 0D;
                    length_textbox.ValueChanged += new EventHandler(size_TextChanged);

                    tw_CreateBox.Control.Controls.Add(length_textbox);


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
                    width_textbox.Name = "numericTextBox2";
                    width_textbox.Quantity = ABB.Robotics.RobotStudio.BuiltinQuantity.None;
                    width_textbox.ReadOnly = false;
                    width_textbox.ShowLabel = true;
                    width_textbox.Size = new Size(pc_width, 34);
                    width_textbox.StepSize = 1D;
                    width_textbox.TabIndex = 5;
                    width_textbox.Text = "numericTextBox2";
                    width_textbox.UserEdited = false;
                    width_textbox.Value = 0D;
                    width_textbox.TextChanged += new EventHandler(size_TextChanged);

                    tw_CreateBox.Control.Controls.Add(width_textbox);


                    // numericTextBox3 - Height
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
                    height_textbox.Name = "numericTextBox3";
                    height_textbox.Quantity = ABB.Robotics.RobotStudio.BuiltinQuantity.None;
                    height_textbox.ReadOnly = false;
                    height_textbox.ShowLabel = true;
                    height_textbox.Size = new Size(pc_width, 34);
                    height_textbox.StepSize = 1D;
                    height_textbox.TabIndex = 6;
                    height_textbox.Text = "numericTextBox3";
                    height_textbox.UserEdited = false;
                    height_textbox.Value = 0D;
                    height_textbox.ValueChanged += new EventHandler(size_TextChanged);

                    tw_CreateBox.Control.Controls.Add(height_textbox);


                    // Button Clear
                    Button btn_clear = new Button();
                    btn_clear.Text = "Clear";
                    btn_clear.Size = new Size(53, 25);
                    btn_clear.Location = new Point(pc_width - 165, 290);
                    btn_clear.Anchor = AnchorStyles.Right | AnchorStyles.Top;
                    btn_clear.Click += new EventHandler(btn_clear_clicked);
                    //btn1.Image = Properties.Resources.BT_box;
                    //btn1.ImageAlign = ContentAlignment.MiddleLeft;
                    btn_clear.TextAlign = ContentAlignment.MiddleCenter;
                    btn_clear.FlatStyle = FlatStyle.Flat;
                    btn_clear.TabIndex = 7;
                    tw_CreateBox.Control.Controls.Add(btn_clear);

                    // Button Create
                    //Button btn_create = new Button();
                    btn_create.Text = "Create";
                    btn_create.Size = new Size(53, 25);
                    btn_create.Location = new Point(pc_width - 105, 290);
                    btn_create.Anchor = AnchorStyles.Right | AnchorStyles.Top;
                    btn_create.Click += new EventHandler(btn_create_clicked);
                    //btn1.Image = Properties.Resources.BT_box;
                    //btn1.ImageAlign = ContentAlignment.MiddleLeft;
                    btn_create.TextAlign = ContentAlignment.MiddleCenter;
                    btn_create.FlatStyle = FlatStyle.Flat;
                    btn_create.TabIndex = 8;
                //if (!string.IsNullOrEmpty(length_textbox.Text))
                //    btn_create.Enabled = true;
                //else
                //    btn_create.Enabled = false;
                btn_create.Enabled = false;
                tw_CreateBox.Control.Controls.Add(btn_create);

                // Button Close
                Button btn_close = new Button();
                    btn_close.Text = "Close";
                    btn_close.Size = new Size(53, 25);
                    btn_close.Location = new Point(pc_width - 45, 290);
                    btn_close.Anchor = AnchorStyles.Right | AnchorStyles.Top;
                    btn_close.Click += new EventHandler(btn_close_clicked);
                    //btn1.Image = Properties.Resources.BT_box;
                    //btn1.ImageAlign = ContentAlignment.MiddleLeft;
                    btn_close.TextAlign = ContentAlignment.MiddleCenter;
                    btn_close.FlatStyle = FlatStyle.Flat;
                    btn_close.TabIndex = 9;
                    tw_CreateBox.Control.Controls.Add(btn_close);

                    // Button Textures
                    Button btn_textures = new Button();
                    btn_textures.Text = "Textures";
                    btn_textures.Size = new Size(60, 25);
                    btn_textures.Location = new Point(8, 290);
                    btn_textures.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                    //btn1.Click += new EventHandler(btn1_clicked);
                    //btn1.Image = Properties.Resources.BT_box;
                    //btn1.ImageAlign = ContentAlignment.MiddleLeft;
                    btn_textures.TextAlign = ContentAlignment.MiddleCenter;
                    btn_textures.FlatStyle = FlatStyle.Flat;
                    btn_textures.TabIndex = 10;
                    tw_CreateBox.Control.Controls.Add(btn_textures);
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

        private static void size_TextChanged(object sender, EventArgs e)
        {
            //var bl = !string.IsNullOrEmpty(length_textbox.Text) &&
            //         !string.IsNullOrEmpty(width_textbox.Text) &&
            //         !string.IsNullOrEmpty(height_textbox.Text);

            var bl = (length_textbox.Value != 0) && (width_textbox.Value != 0) && (height_textbox.Value != 0);

            btn_create.Enabled = bl;
        }

        private static void btn_clear_clicked(object sender, EventArgs e)
        {
            //UIEnvironment.Windows["Create_ABB_Box"].Close();
            //Create_box.create_box();
            //cb_reference.SelectedIndex = 0;
            //pos_control.ResetText();
            //orientation_control.ResetText();
            //length_textbox.ResetText();
            //width_textbox.ResetText();
            //height_textbox.ResetText();
        }

        private static void btn_close_clicked(object sender, EventArgs e)
            {
                UIEnvironment.Windows["Create_ABB_Box"].Close();
            //Logger.AddMessage(new LogMessage(create_box. + " bbox.min.x = " + bbox.min.x))
        }

        private static void btn_create_clicked(object sender, EventArgs e)
        {
            //Logger.AddMessage(new LogMessage("Reference : " + cb_reference.SelectedItem.ToString() + "."));
            //Logger.AddMessage(new LogMessage("Position : "+ "X:" + pos_control.Value.x * 1000 + " Y:" + pos_control.Value.y * 1000 + " Z:" + pos_control.Value.z * 1000 + "."));
            //Logger.AddMessage(new LogMessage("Orientation : " + "X:" + Globals.RadToDeg(orientation_control.Value.x) + " Y:" + Globals.RadToDeg(orientation_control.Value.y) + " Z:" + Globals.RadToDeg(orientation_control.Value.z) + "."));
            //Logger.AddMessage(new LogMessage("Length : " + length_textbox.Value.ToString() + "."));
            //Logger.AddMessage(new LogMessage("Width : " + width_textbox.Value.ToString() + "."));
            //Logger.AddMessage(new LogMessage("Height : " + height_textbox.Value.ToString() + "."));

            //NewStation();
            #region Create ABB_Box
            Project.UndoContext.BeginUndoStep("BodyCreateSolids");
            try
            {
                Station station = Project.ActiveProject as Station;

                // Create a part to contain the bodies.
                #region BodyCreateSolidsStep1
                Part p = new Part();
                p.Name = "ABB_Box";
                station.GraphicComponents.Add(p);
                #endregion

                // Create a solid box.
                #region Create Box
                Vector3 vect_position = new Vector3(pos_control.Value.x, pos_control.Value.y, pos_control.Value.z);
                Vector3 vect_orientation = new Vector3(orientation_control.Value.x, orientation_control.Value.y, orientation_control.Value.z);
                Matrix4 matrix_origo = new Matrix4(vect_position, vect_orientation);
                Vector3 size = new Vector3(length_textbox.Value/1000,width_textbox.Value / 1000, height_textbox.Value / 1000);

                Body b1 = Body.CreateSolidBox(matrix_origo, size);
                b1.Name = "Box";
                p.Bodies.Add(b1);
                #endregion

                // Get a face from the box.
                Face myFace0 = b1.Shells[0].Faces[0]; // z +
                Face myFace1 = b1.Shells[0].Faces[1]; // z -
                Face myFace2 = b1.Shells[0].Faces[2]; // y -
                Face myFace3 = b1.Shells[0].Faces[3]; // x -
                Face myFace4 = b1.Shells[0].Faces[4]; // y +
                Face myFace5 = b1.Shells[0].Faces[5]; // x +

                // Make sure the face is visible.
                myFace0.Visible = true;
                myFace1.Visible = true;
                myFace2.Visible = true;
                myFace3.Visible = true;
                myFace4.Visible = true;
                myFace5.Visible = true;

                // Make the the face yellow.
                //myFace0.Color = Color.Yellow; // z +
                myFace1.Color = Color.Blue; // z -
                myFace2.Color = Color.Red; // y -
                myFace3.Color = Color.Green; // x -
                myFace4.Color = Color.Black; // y +
                myFace5.Color = Color.White; // x +

                var fileName = @"E:\_RobotStudio\SDK\Practicas\Puime's_Addin\Puime's_Addin\Resources\BT_paste.png";
                Texture box_texture = new Texture();
                //
                // da error en la ruta.
                //
                box_texture.FileName = @"E:\_RobotStudio\SDK\Practicas\Puime's_Addin\Puime's_Addin\Resources\BT_paste.png";
                Material box_material = new Material();
                box_material.BaseTexture = box_texture;
                //myFace0.SetMaterial(box_material);

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