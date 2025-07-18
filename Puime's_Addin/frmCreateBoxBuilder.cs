using System;
using System.Drawing;
using System.Windows.Forms;
using ABB.Robotics.Math;
using ABB.Robotics.RobotStudio;
using ABB.Robotics.RobotStudio.Environment;
using ABB.Robotics.RobotStudio.Stations;
using ABB.Robotics.RobotStudio.Stations.Forms;
using PuimesAddin.Properties;

namespace Puime_s_Addin
{
    public partial class frmCreateBoxBuilder : ToolControlBase
    {
        private PictureBox pictureBoxCreateBox;
        private Label labelReference;
        private ComboBox comboBoxReference;
        private PositionControl positionControlPC;
        private OrientationControl orientationControlOC;
        private Button buttonClear;
        private Button buttonCreate;
        private Button buttonClose;
        private NumericTextBox numericTextBoxLength;
        private NumericTextBox numericTextBoxWidth;
        private NumericTextBox numericTextBoxHeight;
        private TemporaryGraphic previewBox;

        public frmCreateBoxBuilder()
        {
            Project.UndoContext.BeginUndoStep("AddToolWindow");

            #region add toolwindow and elements
            try
            {
                InitializeComponent();
                base.Activate += frmCreateBoxBuilder_Activate;
                base.Deactivate += frmCreateBoxBuilder_Desactivate;
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

        private void frmCreateBoxBuilder_Activate(object sender, EventArgs e)
        {
        }

        private void frmCreateBoxBuilder_Desactivate (object sender, EventArgs e)
        {
            CloseTool();
        }

        //Enable the create button if the lenght, with and height has values diferent from 0
        private void size_TextChanged(object sender, EventArgs e)
        {
            var bl = (numericTextBoxLength.Value != 0) && (numericTextBoxWidth.Value != 0) && (numericTextBoxHeight.Value != 0);
            buttonCreate.Enabled = bl;

            ValidateToolControl();
        }

        private void btn_clear_clicked(object sender, EventArgs e)
        {
            CleanValues();
        }

        private void CleanValues()
        {
            comboBoxReference.SelectedIndex = 0;
            positionControlPC.Value = new Vector3(0, 0, 0);
            orientationControlOC.Value = new Vector3(0, 0, 0);
            numericTextBoxLength.Value = 0;
            numericTextBoxWidth.Value = 0;
            numericTextBoxHeight.Value = 0;
        }

        public void btn_close_clicked(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void btn_create_clicked(object sender, EventArgs e)
        {
            if (ValidateToolControl())
            {
                Project.UndoContext.BeginUndoStep();
                try
                {
                    CreateABBBox(preview: false);
                }
                finally
                {
                    Project.UndoContext.EndUndoStep();
                }
            }
        }

        private void CreateABBBox(bool preview)
        {
            #region Create ABB_Box
            Project.UndoContext.BeginUndoStep("Create ABB Box");

            try
            {
                Vector3 value = positionControlPC.Value;
                Vector3 value2 = orientationControlOC.Value;

                double Xvalue = numericTextBoxLength.Value;
                double Yvalue = numericTextBoxWidth.Value;
                double Zvalue = numericTextBoxHeight.Value;
                if (Yvalue == 0.0)
                {
                    Yvalue = Xvalue;
                }
                if (Zvalue == 0.0)
                {
                    Zvalue = Xvalue;
                }
                
                // Create a solid box.
                #region Create Box
                Vector3 vect_position = new Vector3(0, 0, 0);
                Vector3 vect_orientation = new Vector3(0, 0, 0); //uses 0,0,0 as origin to later transform the position to the pos_control values,
                                                                 //so the part origin is allways in the corner of the box.
                Matrix4 matrix_origo = new Matrix4(vect_position, vect_orientation);
                Vector3 size = new Vector3(Xvalue/1000, Yvalue/1000, Zvalue/1000);

                Matrix4 matrix = new Matrix4(value, value2);

                Station station = Project.ActiveProject as Station;
                if (preview)
                {
                    previewBox = station.TemporaryGraphics.DrawBox(matrix, size, Color.FromArgb(128, Color.Gray));
                    return;
                }
                Part p = new Part();
                p.Name = "ABB_Box_" + Xvalue.ToString() + "x" + Yvalue.ToString() + "x" + Zvalue.ToString();
                station.GraphicComponents.Add(p);

                Body b1 = Body.CreateSolidBox(matrix_origo, size);
                b1.Name = "Box";
                p.Bodies.Add(b1);
                #endregion

                //
                // Transform the position of the part to the values of the pos_control values.
                //

                // Evaluate the reference selected item
                bool reference_select_world = comboBoxReference.SelectedItem.ToString() == "World"; // true if World is selected
                if (reference_select_world) // World is selected
                {
                    // Transform the position and orientation of the Part to the values of the control.
                    p.Transform.X = positionControlPC.Value.x;
                    p.Transform.Y = positionControlPC.Value.y;
                    p.Transform.Z = positionControlPC.Value.z;
                    p.Transform.RX = orientationControlOC.Value.x;
                    p.Transform.RY = orientationControlOC.Value.y;
                    p.Transform.RZ = orientationControlOC.Value.z;
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

                    // Create a Matrix with the control values.
                    Vector3 vect_pos_input = new Vector3(positionControlPC.Value.x, positionControlPC.Value.y, positionControlPC.Value.z);
                    Vector3 vect_ori_input = new Vector3(orientationControlOC.Value.x, orientationControlOC.Value.y, orientationControlOC.Value.z);
                    Matrix4 matrix_input = new Matrix4(vect_pos_input, vect_ori_input);

                    // Transfor the position of the Part from it's position the values from the control with the Part orientation.
                    p.Transform.SetRelativeTransform(p, matrix_input);
                }

                // Get the faces from the box.
                Face myFace0 = b1.Shells[0].Faces[0]; // z +
                Face myFace1 = b1.Shells[0].Faces[1]; // z -
                Face myFace2 = b1.Shells[0].Faces[2]; // y -
                Face myFace3 = b1.Shells[0].Faces[3]; // x -
                Face myFace4 = b1.Shells[0].Faces[4]; // y +
                Face myFace5 = b1.Shells[0].Faces[5]; // x +

                // Make sure the faces are visible.
                myFace0.Visible = true;
                myFace1.Visible = true;
                myFace2.Visible = true;
                myFace3.Visible = true;
                myFace4.Visible = true;
                myFace5.Visible = true;

                // Set the material for each face of the box
                Bitmap bmp0 = new Bitmap("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-4.0\\RobotStudio\\Add-In\\Textures\\top.jpg");
                Bitmap bmp1 = new Bitmap("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-4.0\\RobotStudio\\Add-In\\Textures\\bottom.jpg");
                Bitmap bmp2 = new Bitmap("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-4.0\\RobotStudio\\Add-In\\Textures\\long_side.jpg");
                Bitmap bmp3 = new Bitmap("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-4.0\\RobotStudio\\Add-In\\Textures\\short_side2.jpg");
                Bitmap bmp4 = new Bitmap("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-4.0\\RobotStudio\\Add-In\\Textures\\long_side2.jpg");
                Bitmap bmp5 = new Bitmap("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-4.0\\RobotStudio\\Add-In\\Textures\\short_side.jpg");
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
                CleanValues();
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

        protected override bool ValidateToolControl()
        {
            double value = numericTextBoxLength.Value;
            double value2 = numericTextBoxWidth.Value;
            double value3 = numericTextBoxHeight.Value;
            bool flag = value > 0.0 && value <= 100000000.0 && value2 >= 0.0 && value2 <= 100000000.0 && value3 >= 0.0 && value3 <= 100000000.0;
            UpdatePreview(valid: flag);
            return flag;
        }

        private void UpdatePreview(bool valid)
        {
            if (previewBox != null)
            {
                previewBox.Delete();
                previewBox = null;
            }
            if (valid)
            {
                CreateABBBox(preview: true);
            }
        }

        private void CreateABBBox_Deactivate(object sender, EventArgs e)
        {
            UpdatePreview(valid: false);
        }

        private void InitializeComponent()
        {
            int tw_width = UIEnvironment.Windows["ObjectBrowser"].Control.Size.Width -25;

            pictureBoxCreateBox = new PictureBox();
            labelReference = new Label();
            comboBoxReference = new ComboBox();
            positionControlPC = new PositionControl();
            orientationControlOC = new OrientationControl();
            buttonClear = new Button();
            buttonCreate = new Button();
            buttonClose = new Button(); 
            numericTextBoxLength = new NumericTextBox();
            numericTextBoxWidth = new NumericTextBox();
            numericTextBoxHeight = new NumericTextBox();

            pictureBoxCreateBox.SuspendLayout();
            SuspendLayout();

            pictureBoxCreateBox.Location = new Point(8, 8);
            pictureBoxCreateBox.Name = "pictureBoxCB";
            pictureBoxCreateBox.Size = new Size(65, 65);
            pictureBoxCreateBox.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxCreateBox.Image = Resources.BT_box_65;
            pictureBoxCreateBox.TabStop = false;

            labelReference.Location = new Point(79, 30);
            labelReference.Size = new Size(100, 15);
            labelReference.Text = "Reference";

            comboBoxReference.Location = new Point(80, 52);
            comboBoxReference.Size = new Size(tw_width -65, 21);
            comboBoxReference.TabIndex = 0;
            comboBoxReference.Items.Add("World");
            comboBoxReference.Items.Add("UCS");
            comboBoxReference.SelectedIndex = 0;

            positionControlPC.ErrorProviderControl = null;
            positionControlPC.ExpressionErrorString = "Bad Expression";
            positionControlPC.LabelQuantity = BuiltinQuantity.Length;
            positionControlPC.LabelText = "Corner Point";
            positionControlPC.Location = new Point(8, 85);
            positionControlPC.MaxValueErrorString = "Value exceeds maximum";
            positionControlPC.MinValueErrorString = "Value is below minimum";
            positionControlPC.Name = "pos_control";
            positionControlPC.NumTextBoxes = 3;
            positionControlPC.ReadOnly = false;
            positionControlPC.RefCoordSys = null;
            positionControlPC.ShowLabel = true;
            positionControlPC.Size = new Size(tw_width +7 , 34);
            positionControlPC.TabIndex = 1;
            positionControlPC.Text = "Position Control";
            positionControlPC.VerticalLayout = false;

            orientationControlOC.ErrorProviderControl = null;
            orientationControlOC.ExpressionErrorString = "Bad Expression";
            orientationControlOC.LabelQuantity = BuiltinQuantity.Length;
            orientationControlOC.LabelText = "Orientation";
            orientationControlOC.Location = new Point(8, 125);
            orientationControlOC.MaxValueErrorString = "Value exceeds maximum";
            orientationControlOC.MinValueErrorString = "Value is below minimum";
            orientationControlOC.Name = "Orientation Control";
            orientationControlOC.NumTextBoxes = 3;
            orientationControlOC.ReadOnly = false;
            orientationControlOC.ShowLabel = true;
            orientationControlOC.Size = new Size(tw_width + 7, 34);
            orientationControlOC.TabIndex = 2;
            orientationControlOC.Text = "positionControl1";
            orientationControlOC.VerticalLayout = false;

            numericTextBoxLength.ErrorProviderControl = null;
            numericTextBoxLength.ExpressionErrorString = "Bad Expression";
            numericTextBoxLength.LabelText = "Length (mm)";
            numericTextBoxLength.Location = new Point(8, 165);
            numericTextBoxLength.MaxValue = 1000000000D;
            numericTextBoxLength.MaxValueErrorString = "Value exceeds maximum";
            numericTextBoxLength.MinValue = -1000000000D;
            numericTextBoxLength.MinValueErrorString = "Value is below minimum";
            numericTextBoxLength.Name = "length_textbox";
            numericTextBoxLength.Quantity = BuiltinQuantity.None;
            numericTextBoxLength.ReadOnly = false;
            numericTextBoxLength.ShowLabel = true;
            numericTextBoxLength.Size = new Size(tw_width + 7, 34);
            numericTextBoxLength.StepSize = 1D;
            numericTextBoxLength.TabIndex = 3;
            numericTextBoxLength.Text = "numericTextBox1";
            numericTextBoxLength.UserEdited = false;
            numericTextBoxLength.Value = 0D;
            numericTextBoxLength.ValueChanged += new EventHandler(size_TextChanged);

            numericTextBoxWidth.ErrorProviderControl = null;
            numericTextBoxWidth.ExpressionErrorString = "Bad Expression";
            numericTextBoxWidth.LabelText = "Width (mm)";
            numericTextBoxWidth.Location = new Point(8, 205);
            numericTextBoxWidth.MaxValue = 1000000000D;
            numericTextBoxWidth.MaxValueErrorString = "Value exceeds maximum";
            numericTextBoxWidth.MinValue = -1000000000D;
            numericTextBoxWidth.MinValueErrorString = "Value is below minimum";
            numericTextBoxWidth.Name = "width_textbox";
            numericTextBoxWidth.Quantity = BuiltinQuantity.None;
            numericTextBoxWidth.ReadOnly = false;
            numericTextBoxWidth.ShowLabel = true;
            numericTextBoxWidth.Size = new Size(tw_width + 7, 34);
            numericTextBoxWidth.StepSize = 1D;
            numericTextBoxWidth.TabIndex = 4;
            numericTextBoxWidth.Text = "numericTextBox2";
            numericTextBoxWidth.UserEdited = false;
            numericTextBoxWidth.Value = 0D;
            numericTextBoxWidth.ValueChanged += new EventHandler(size_TextChanged);

            numericTextBoxHeight.ErrorProviderControl = null;
            numericTextBoxHeight.ExpressionErrorString = "Bad Expression";
            numericTextBoxHeight.LabelText = "Height (mm)";
            numericTextBoxHeight.Location = new Point(8, 245);
            numericTextBoxHeight.MaxValue = 1000000000D;
            numericTextBoxHeight.MaxValueErrorString = "Value exceeds maximum";
            numericTextBoxHeight.MinValue = -1000000000D;
            numericTextBoxHeight.MinValueErrorString = "Value is below minimum";
            numericTextBoxHeight.Name = "height_textbox";
            numericTextBoxHeight.Quantity = BuiltinQuantity.None;
            numericTextBoxHeight.ReadOnly = false;
            numericTextBoxHeight.ShowLabel = true;
            numericTextBoxHeight.Size = new Size(tw_width + 7, 34);
            numericTextBoxHeight.StepSize = 1D;
            numericTextBoxHeight.TabIndex = 5;
            numericTextBoxHeight.Text = "numericTextBox3";
            numericTextBoxHeight.UserEdited = false;
            numericTextBoxHeight.Value = 0D;
            numericTextBoxHeight.ValueChanged += new EventHandler(size_TextChanged);

            buttonClear.Text = "Clear";
            buttonClear.Size = new Size(53, 25);
            buttonClear.Location = new Point(tw_width - 158, 295);
            buttonClear.FlatStyle = FlatStyle.Flat;
            buttonClear.UseVisualStyleBackColor = true;
            buttonClear.TabIndex = 6;
            buttonClear.Click += new EventHandler(btn_clear_clicked);

            buttonCreate.Text = "Create";
            buttonCreate.Size = new Size(53, 25);
            buttonCreate.Location = new Point(tw_width - 98, 295);
            buttonCreate.FlatStyle = FlatStyle.Flat;
            buttonCreate.UseVisualStyleBackColor = true;
            buttonCreate.TabIndex = 7;
            buttonCreate.Enabled = false;
            buttonCreate.Click += new EventHandler(btn_create_clicked);

            buttonClose.Text = "Close";
            buttonClose.Size = new Size(53, 25);
            buttonClose.Location = new Point(tw_width - 38, 295);
            buttonClose.TextAlign = ContentAlignment.MiddleCenter;
            buttonClose.FlatStyle = FlatStyle.Flat;
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.TabIndex = 8;
            buttonClose.Click += new EventHandler(btn_close_clicked);

            AutoScroll = true;
            base.AdjustableHeight = true;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.Caption = "Create ABB Box";
            base.Controls.Add(pictureBoxCreateBox);
            base.Controls.Add(labelReference);
            base.Controls.Add(comboBoxReference);
            base.Controls.Add(positionControlPC);
            base.Controls.Add(orientationControlOC);
            base.Controls.Add(buttonClear);
            base.Controls.Add(buttonCreate);
            base.Controls.Add(buttonClose);
            base.Controls.Add(numericTextBoxLength);
            base.Controls.Add(numericTextBoxWidth);
            base.Controls.Add(numericTextBoxHeight);
            base.Name = "frmCreateBoxBuilder";
            base.Size = new Size(tw_width, 330);
            base.Apply += new System.EventHandler(btn_create_clicked);
            base.Deactivate += new System.EventHandler(CreateABBBox_Deactivate);
            pictureBoxCreateBox.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
