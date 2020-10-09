
using ABB.Robotics.Math;
using ABB.Robotics.RobotStudio;
using ABB.Robotics.RobotStudio.Stations;
using ABB.Robotics.RobotStudio.Stations.Forms;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Puime_s_Addin
{

    partial class Create_box_v3
    {
        
        
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponents()
        {
            this.components = new System.ComponentModel.Container();
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