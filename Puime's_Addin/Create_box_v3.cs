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
	public partial class Create_box_v3 : ToolControlBase
	{

		private TemporaryGraphic previewGfx;
		private IContainer components_v3;
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
	}
}
