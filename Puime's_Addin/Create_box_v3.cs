using ABB.Robotics.Math;
using ABB.Robotics.RobotStudio;
using ABB.Robotics.RobotStudio.Environment;
using ABB.Robotics.RobotStudio.Stations;
using ABB.Robotics.RobotStudio.Stations.Forms;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Puime_s_Addin
{
	class Create_box_v3 : ToolControlBase
	{

		private TemporaryGraphic previewGfx;
		private IContainer components;
		private OrientationControl orientationControl;
		private ReferenceComboBox referenceComboBox;
		private NumericTextBox numericTextBoxHeight;
		private NumericTextBox numericTextBoxWidth;
		private NumericTextBox numericTextBoxLength;
		private PositionControl positionControlCornerPoint;
		private Label labelReference;
		private PictureBox pictureBox;
		private RefCoordSys refCoordSys;
		private WorldRefCoordSys worldRefCoordSys;
		private UcsRefCoordSys ucsRefCoordSys;
		private ErrorProvider errorProviderNumTextBoxValidation;

		public Create_box_v3()
		{
            InitializeComponents();
        }

		protected override bool ValidateToolControl()
		{
			double value = numericTextBoxLength.Value;
			double value2 = numericTextBoxWidth.Value;
			double value3 = numericTextBoxHeight.Value;
			bool flag = value > 0.0 && value <= 100000000.0 && value2 >= 0.0 && value2 <= 100000000.0 && value3 >= 0.0 && value3 <= 100000000.0;
			UpdatePreview(flag);
			return flag;
		}

		private void UpdatePreview(bool valid)
		{
			if (previewGfx != null)
			{
				previewGfx.Delete();
				previewGfx = null;
			}
			if (valid)
			{
				create_box_v3(preview: true);
			}
		}

		private void create_box_v3(bool preview)
		{
			Vector3 value = positionControlCornerPoint.Value;
			Vector3 value2 = orientationControl.Value;
			double value3 = numericTextBoxLength.Value;
			double num = numericTextBoxWidth.Value;
			double num2 = numericTextBoxHeight.Value;
			if (num == 0.0)
			{
				num = value3;
			}
			if (num2 == 0.0)
			{
				num2 = value3;
			}
			Vector3 size = new Vector3(value3, num, num2);
			Matrix4 globalMatrix = refCoordSys.Current.Transform.GlobalMatrix;
			Matrix4 matrix = globalMatrix * new Matrix4(value, value2);
			Station activeStation = Station.ActiveStation;
			if (preview)
			{
				previewGfx = activeStation.TemporaryGraphics.DrawBox(matrix, size, Color.FromArgb(128, Color.Gray));
				return;
			}
			Part part = new Part();
			Body body = Body.CreateSolidBox(Matrix4.Identity, size);
			part.Bodies.Add(body);
			activeStation.GraphicComponents.Add(part);
			body.Transform.Matrix = matrix;
			//Logger.AddMessage(new LogMessage(string.Format(arg0: part.Name = NamingManager.GetUniqueNameForType(typeof(Part), activeStation), format: ModelingToolResource.SOLID_CREATED), LogMessageSeverity.Information));
			ABB.Robotics.RobotStudio.Selection.SelectedObjects.Clear();
			ABB.Robotics.RobotStudio.Selection.SelectedObjects.Add(part, SelectionReason.ObjectCreated);
		}

		private void Create_box_v3_Apply(object sender, EventArgs e)
		{
			if (ValidateToolControl())
			{
				Project.UndoContext.BeginUndoStep();
				try
				{
					create_box_v3(preview: false);
				}
				finally
				{
					Project.UndoContext.EndUndoStep();
				}
			}
		}

		private void Create_box_v3_Deactivate(object sender, EventArgs e)
		{
			UpdatePreview(valid: false);
		}


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            //{
                this.components.Dispose();
            //}
            base.Dispose(disposing);
        }

        private void InitializeComponents()
        {
            //this.components_v3 = new System.ComponentModel.Container();
            this.components = (IContainer) new Container();
            //System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RobotStudio.UI.Modeling.Create3DBox));
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Create_box_v3));
            this.orientationControl = new ABB.Robotics.RobotStudio.Stations.Forms.OrientationControl();
            this.referenceComboBox = new ABB.Robotics.RobotStudio.Stations.Forms.ReferenceComboBox();
            this.refCoordSys = new ABB.Robotics.RobotStudio.Stations.Forms.RefCoordSys();
            this.worldRefCoordSys = new ABB.Robotics.RobotStudio.Stations.Forms.WorldRefCoordSys("World");
            this.ucsRefCoordSys = new ABB.Robotics.RobotStudio.Stations.Forms.UcsRefCoordSys("UCS");
            this.numericTextBoxHeight = new ABB.Robotics.RobotStudio.Stations.Forms.NumericTextBox();
            this.errorProviderNumTextBoxValidation = new System.Windows.Forms.ErrorProvider(components);
            this.numericTextBoxWidth = new ABB.Robotics.RobotStudio.Stations.Forms.NumericTextBox();
            this.numericTextBoxLength = new ABB.Robotics.RobotStudio.Stations.Forms.NumericTextBox();
            this.positionControlCornerPoint = new ABB.Robotics.RobotStudio.Stations.Forms.PositionControl();
            this.labelReference = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)errorProviderNumTextBoxValidation).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            base.SuspendLayout();
            resources.ApplyResources(orientationControl, "orientationControl");
            this.orientationControl.ErrorProviderControl = null;
            this.orientationControl.LabelQuantity = ABB.Robotics.RobotStudio.BuiltinQuantity.Angle;
            this.orientationControl.Name = "orientationControl";
            this.orientationControl.NumTextBoxes = 3;
            this.orientationControl.ReadOnly = false;
            this.orientationControl.ShowLabel = true;
            this.orientationControl.VerticalLayout = false;
            resources.ApplyResources(referenceComboBox, "referenceComboBox");
            this.referenceComboBox.GraphicalFrameSize = 0.25;
            this.referenceComboBox.GraphicalFrameWidth = 5;
            this.referenceComboBox.Name = "referenceComboBox";
            this.referenceComboBox.RefCoordSys = refCoordSys;
            this.referenceComboBox.ShowGraphicalFrame = true;
            this.refCoordSys.Current = worldRefCoordSys;
            this.refCoordSys.Items.Add(worldRefCoordSys);
            this.refCoordSys.Items.Add(ucsRefCoordSys);
            resources.ApplyResources(worldRefCoordSys, "worldRefCoordSys");
            resources.ApplyResources(ucsRefCoordSys, "ucsRefCoordSys");
            resources.ApplyResources(numericTextBoxHeight, "numericTextBoxHeight");
            this.numericTextBoxHeight.ErrorProviderControl = errorProviderNumTextBoxValidation;
            this.numericTextBoxHeight.MaxValue = 1000000000.0;
            this.numericTextBoxHeight.MinValue = -1000000000.0;
            this.numericTextBoxHeight.Name = "numericTextBoxHeight";
            this.numericTextBoxHeight.Quantity = ABB.Robotics.RobotStudio.BuiltinQuantity.Length;
            this.numericTextBoxHeight.ReadOnly = false;
            this.numericTextBoxHeight.ShowLabel = true;
            this.numericTextBoxHeight.StepSize = 1.0;
            this.numericTextBoxHeight.UserEdited = false;
            this.numericTextBoxHeight.Value = 0.0;
            this.errorProviderNumTextBoxValidation.ContainerControl = this;
            resources.ApplyResources(numericTextBoxWidth, "numericTextBoxWidth");
            this.numericTextBoxWidth.ErrorProviderControl = errorProviderNumTextBoxValidation;
            this.numericTextBoxWidth.MaxValue = 1000000000.0;
            this.numericTextBoxWidth.MinValue = -1000000000.0;
            this.numericTextBoxWidth.Name = "numericTextBoxWidth";
            this.numericTextBoxWidth.Quantity = ABB.Robotics.RobotStudio.BuiltinQuantity.Length;
            this.numericTextBoxWidth.ReadOnly = false;
            this.numericTextBoxWidth.ShowLabel = true;
            this.numericTextBoxWidth.StepSize = 1.0;
            this.numericTextBoxWidth.UserEdited = false;
            this.numericTextBoxWidth.Value = 0.0;
            resources.ApplyResources(numericTextBoxLength, "numericTextBoxLength");
            this.numericTextBoxLength.ErrorProviderControl = errorProviderNumTextBoxValidation;
            this.numericTextBoxLength.MaxValue = 1000000000.0;
            this.numericTextBoxLength.MinValue = -1000000000.0;
            this.numericTextBoxLength.Name = "numericTextBoxLength";
            this.numericTextBoxLength.Quantity = ABB.Robotics.RobotStudio.BuiltinQuantity.Length;
            this.numericTextBoxLength.ReadOnly = false;
            this.numericTextBoxLength.ShowLabel = true;
            this.numericTextBoxLength.StepSize = 1.0;
            this.numericTextBoxLength.UserEdited = false;
            this.numericTextBoxLength.Value = 0.0;
            resources.ApplyResources(positionControlCornerPoint, "positionControlCornerPoint");
            this.positionControlCornerPoint.ErrorProviderControl = null;
            this.positionControlCornerPoint.LabelQuantity = ABB.Robotics.RobotStudio.BuiltinQuantity.Length;
            this.positionControlCornerPoint.Name = "positionControlCornerPoint";
            this.positionControlCornerPoint.NumTextBoxes = 3;
            this.positionControlCornerPoint.ReadOnly = false;
            this.positionControlCornerPoint.RefCoordSys = refCoordSys;
            this.positionControlCornerPoint.ShowLabel = true;
            this.positionControlCornerPoint.VerticalLayout = false;
            resources.ApplyResources(labelReference, "labelReference");
            this.labelReference.Name = "labelReference";
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(pictureBox, "pictureBox");
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.TabStop = false;
            resources.ApplyResources(this, "$this");
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(orientationControl);
            base.Controls.Add(referenceComboBox);
            base.Controls.Add(numericTextBoxHeight);
            base.Controls.Add(numericTextBoxWidth);
            base.Controls.Add(numericTextBoxLength);
            base.Controls.Add(positionControlCornerPoint);
            base.Controls.Add(labelReference);
            base.Controls.Add(pictureBox);
            base.Name = "Create ABB Box";
            base.ShowClearButton = true;
            base.ShowCloseButton = true;
            base.ShowCreateButton = true;
            base.Apply += new System.EventHandler(Create_box_v3_Apply);
            base.Deactivate += new System.EventHandler(Create_box_v3_Deactivate);
            ((System.ComponentModel.ISupportInitialize)errorProviderNumTextBoxValidation).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }


    }
}
