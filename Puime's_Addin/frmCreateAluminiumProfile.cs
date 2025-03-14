using System;
using System.Drawing;
using System.Windows.Forms;
using ABB.Robotics.Math;
using ABB.Robotics.RobotStudio;
using ABB.Robotics.RobotStudio.Environment;
using ABB.Robotics.RobotStudio.Stations;
using ABB.Robotics.RobotStudio.Stations.Forms;
using PuimesAddin.Properties;

namespace PuimesAddin
{
    public partial class frmCreateAluminiumProfile : ToolControlBase
    {
        private PictureBox pictureBoxModel;
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

        public frmCreateAluminiumProfile()
        {

        //InitializeComponent();

        Project.UndoContext.BeginUndoStep("AddToolWindow");

            #region add toolwindow and elements
            try
            {
                InitializeComponent();
                base.Activate += frmCreateAluminiumProfile_Activate;
                base.Deactivate += frmCreateAluminiumProfile_Desactivate;
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


        private void frmCreateAluminiumProfile_Activate(object sender, EventArgs e)
        {
        }

        private void frmCreateAluminiumProfile_Desactivate(object sender, EventArgs e)
        {
            CloseTool();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBoxModel = new System.Windows.Forms.PictureBox();
            this.labelReference = new System.Windows.Forms.Label();
            this.comboBoxReference = new System.Windows.Forms.ComboBox();
            this.positionControlPC = new ABB.Robotics.RobotStudio.Stations.Forms.PositionControl();
            this.orientationControlOC = new ABB.Robotics.RobotStudio.Stations.Forms.OrientationControl();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.numericTextBoxLength = new ABB.Robotics.RobotStudio.Stations.Forms.NumericTextBox();
            this.numericTextBoxWidth = new ABB.Robotics.RobotStudio.Stations.Forms.NumericTextBox();
            this.numericTextBoxHeight = new ABB.Robotics.RobotStudio.Stations.Forms.NumericTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxModel)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxCreateBox
            // 
            this.pictureBoxModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxModel.Image = global::PuimesAddin.Properties.Resources.BT_box_65;
            this.pictureBoxModel.Location = new System.Drawing.Point(8, 8);
            this.pictureBoxModel.Name = "pictureBoxModel";
            this.pictureBoxModel.Size = new System.Drawing.Size(65, 65);
            this.pictureBoxModel.TabIndex = 0;
            this.pictureBoxModel.TabStop = false;
            // 
            // labelReference
            // 
            this.labelReference.Location = new System.Drawing.Point(79, 30);
            this.labelReference.Name = "labelReference";
            this.labelReference.Size = new System.Drawing.Size(100, 15);
            this.labelReference.TabIndex = 1;
            this.labelReference.Text = "Model";
            // 
            // comboBoxReference
            // 
            this.comboBoxReference.Items.AddRange(new object[] {
            "20 x 20",
            "30 x 30",
            "40 x 40",
            "50 x 50",
            "60 x 60",
            "80 x 80",
            "90 x 90"});
            this.comboBoxReference.Location = new System.Drawing.Point(80, 52);
            this.comboBoxReference.Name = "comboBoxReference";
            this.comboBoxReference.Size = new System.Drawing.Size(133, 21);
            this.comboBoxReference.TabIndex = 0;
            this.comboBoxReference.SelectedIndexChanged += new System.EventHandler(this.comboBoxReference_SelectedIndexChanged);
            // 
            // positionControlPC
            // 
            this.positionControlPC.AccessibleName = "Corner Point";
            this.positionControlPC.ErrorProviderControl = null;
            this.positionControlPC.ExpressionErrorString = "Bad Expression";
            this.positionControlPC.LabelQuantity = ABB.Robotics.RobotStudio.BuiltinQuantity.Length;
            this.positionControlPC.LabelText = "Corner Point";
            this.positionControlPC.Location = new System.Drawing.Point(8, 85);
            this.positionControlPC.MaxValueErrorString = "Value exceeds maximum";
            this.positionControlPC.MinValueErrorString = "Value is below minimum";
            this.positionControlPC.Name = "positionControlPC";
            this.positionControlPC.NumTextBoxes = 3;
            this.positionControlPC.ReadOnly = false;
            this.positionControlPC.RefCoordSys = null;
            this.positionControlPC.ShowLabel = true;
            this.positionControlPC.Size = new System.Drawing.Size(200, 34);
            this.positionControlPC.TabIndex = 1;
            this.positionControlPC.Text = "Position Control";
            this.positionControlPC.VerticalLayout = false;
            // 
            // orientationControlOC
            // 
            this.orientationControlOC.AccessibleName = "Orientation";
            this.orientationControlOC.EnableModeChange = true;
            this.orientationControlOC.ErrorProviderControl = null;
            this.orientationControlOC.ExpressionErrorString = "Bad Expression";
            this.orientationControlOC.LabelQuantity = ABB.Robotics.RobotStudio.BuiltinQuantity.Angle;
            this.orientationControlOC.LabelText = "Orientation";
            this.orientationControlOC.Location = new System.Drawing.Point(8, 125);
            this.orientationControlOC.MaxValueErrorString = "Value exceeds maximum";
            this.orientationControlOC.MinValueErrorString = "Value is below minimum";
            this.orientationControlOC.Name = "orientationControlOC";
            this.orientationControlOC.NumTextBoxes = 3;
            this.orientationControlOC.QuaternionMode = false;
            this.orientationControlOC.ReadOnly = false;
            this.orientationControlOC.ShowLabel = true;
            this.orientationControlOC.Size = new System.Drawing.Size(200, 34);
            this.orientationControlOC.TabIndex = 2;
            this.orientationControlOC.Text = "positionControl1";
            this.orientationControlOC.VerticalLayout = false;
            // 
            // buttonClear
            // 
            this.buttonClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClear.Location = new System.Drawing.Point(50, 295);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(53, 25);
            this.buttonClear.TabIndex = 6;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            // 
            // buttonCreate
            // 
            this.buttonCreate.Enabled = false;
            this.buttonCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCreate.Location = new System.Drawing.Point(100, 295);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(53, 25);
            this.buttonCreate.TabIndex = 7;
            this.buttonCreate.Text = "Create";
            this.buttonCreate.UseVisualStyleBackColor = true;
            // 
            // buttonClose
            // 
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Location = new System.Drawing.Point(160, 295);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(53, 25);
            this.buttonClose.TabIndex = 8;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // numericTextBoxLength
            // 
            this.numericTextBoxLength.AccessibleName = "Length (mm)";
            this.numericTextBoxLength.ErrorProviderControl = null;
            this.numericTextBoxLength.ExpressionErrorString = "Bad Expression";
            this.numericTextBoxLength.LabelText = "Length (mm)";
            this.numericTextBoxLength.Location = new System.Drawing.Point(8, 165);
            this.numericTextBoxLength.MaxValue = 1000000000D;
            this.numericTextBoxLength.MaxValueErrorString = "Value exceeds maximum";
            this.numericTextBoxLength.MinValue = -1000000000D;
            this.numericTextBoxLength.MinValueErrorString = "Value is below minimum";
            this.numericTextBoxLength.Name = "numericTextBoxLength";
            this.numericTextBoxLength.Quantity = ABB.Robotics.RobotStudio.BuiltinQuantity.None;
            this.numericTextBoxLength.ReadOnly = false;
            this.numericTextBoxLength.ShowLabel = true;
            this.numericTextBoxLength.Size = new System.Drawing.Size(200, 34);
            this.numericTextBoxLength.StepSize = 1D;
            this.numericTextBoxLength.TabIndex = 3;
            this.numericTextBoxLength.Text = "numericTextBox1";
            this.numericTextBoxLength.UserEdited = false;
            this.numericTextBoxLength.Value = 0D;
            // 
            // numericTextBoxWidth
            // 
            this.numericTextBoxWidth.AccessibleName = "Width (mm)";
            this.numericTextBoxWidth.ErrorProviderControl = null;
            this.numericTextBoxWidth.ExpressionErrorString = "Bad Expression";
            this.numericTextBoxWidth.LabelText = "Width (mm)";
            this.numericTextBoxWidth.Location = new System.Drawing.Point(8, 205);
            this.numericTextBoxWidth.MaxValue = 1000000000D;
            this.numericTextBoxWidth.MaxValueErrorString = "Value exceeds maximum";
            this.numericTextBoxWidth.MinValue = -1000000000D;
            this.numericTextBoxWidth.MinValueErrorString = "Value is below minimum";
            this.numericTextBoxWidth.Name = "numericTextBoxWidth";
            this.numericTextBoxWidth.Quantity = ABB.Robotics.RobotStudio.BuiltinQuantity.None;
            this.numericTextBoxWidth.ReadOnly = false;
            this.numericTextBoxWidth.ShowLabel = true;
            this.numericTextBoxWidth.Size = new System.Drawing.Size(200, 34);
            this.numericTextBoxWidth.StepSize = 1D;
            this.numericTextBoxWidth.TabIndex = 4;
            this.numericTextBoxWidth.Text = "numericTextBox2";
            this.numericTextBoxWidth.UserEdited = false;
            this.numericTextBoxWidth.Value = 0D;
            // 
            // numericTextBoxHeight
            // 
            this.numericTextBoxHeight.AccessibleName = "Height (mm)";
            this.numericTextBoxHeight.ErrorProviderControl = null;
            this.numericTextBoxHeight.ExpressionErrorString = "Bad Expression";
            this.numericTextBoxHeight.LabelText = "Height (mm)";
            this.numericTextBoxHeight.Location = new System.Drawing.Point(8, 245);
            this.numericTextBoxHeight.MaxValue = 1000000000D;
            this.numericTextBoxHeight.MaxValueErrorString = "Value exceeds maximum";
            this.numericTextBoxHeight.MinValue = -1000000000D;
            this.numericTextBoxHeight.MinValueErrorString = "Value is below minimum";
            this.numericTextBoxHeight.Name = "numericTextBoxHeight";
            this.numericTextBoxHeight.Quantity = ABB.Robotics.RobotStudio.BuiltinQuantity.None;
            this.numericTextBoxHeight.ReadOnly = false;
            this.numericTextBoxHeight.ShowLabel = true;
            this.numericTextBoxHeight.Size = new System.Drawing.Size(200, 34);
            this.numericTextBoxHeight.StepSize = 1D;
            this.numericTextBoxHeight.TabIndex = 5;
            this.numericTextBoxHeight.Text = "numericTextBox3";
            this.numericTextBoxHeight.UserEdited = false;
            this.numericTextBoxHeight.Value = 0D;
            // 
            // frmCreateAluminiumProfile
            // 
            this.AdjustableHeight = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Caption = "Create ABB Box";
            this.Controls.Add(this.pictureBoxModel);
            this.Controls.Add(this.labelReference);
            this.Controls.Add(this.comboBoxReference);
            this.Controls.Add(this.positionControlPC);
            this.Controls.Add(this.orientationControlOC);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.numericTextBoxLength);
            this.Controls.Add(this.numericTextBoxWidth);
            this.Controls.Add(this.numericTextBoxHeight);
            this.Name = "frmCreateAluminiumProfile";
            this.Size = new System.Drawing.Size(228, 339);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxModel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private void comboBoxReference_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxReference.SelectedItem)
            {
                case "20 x 20":
                    pictureBoxModel.Image = Resources.BT_box_65;
                    break;
                case "30 x 30":
                    pictureBoxModel.Image = Resources.BT_box_65;
                    break;
                case "40 x 40":
                    pictureBoxModel.Image = Resources.BT_box_65;
                    break;
                case "50 x 50":
                    pictureBoxModel.Image = Resources.BT_box_65;
                    break;
                case "60 x 60":
                    pictureBoxModel.Image = Resources.BT_box_65;
                    break;
                case "80 x 80":
                    pictureBoxModel.Image = Resources.BT_box_65;
                    break;
                case "90 x 90":
                    pictureBoxModel.Image = Resources.BT_box_65;
                    break;
                case "100 x 100":
                    pictureBoxModel.Image = Resources.BT_box_65;
                    break;
                default:
                    break;
            }
        }
    }
}
