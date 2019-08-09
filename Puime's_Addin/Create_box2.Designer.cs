using ABB.Robotics.RobotStudio.Environment;
using ABB.Robotics.RobotStudio.Stations.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Puime_s_Addin
{
    partial class Create_box2
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pb_createBox = new System.Windows.Forms.PictureBox();
            this.lb_reference = new System.Windows.Forms.Label();
            this.cb_reference = new System.Windows.Forms.ComboBox();
            //this.pos_control = new System.Windows.Forms.ComboBox();
            this.pos_control = new PositionControl();
            //this.orientation_control = new System.Windows.Forms.ComboBox();
            this.orientation_control = new OrientationControl();
            //this.button1 = new System.Windows.Forms.Button();
            //this.button2 = new System.Windows.Forms.Button();
            //this.button3 = new System.Windows.Forms.Button();
            this.btn_clear = new Button();
            this.btn_create = new Button();
            this.btn_close = new Button();
            //this.length_textbox = new System.Windows.Forms.TextBox();
            //this.width_textbox = new System.Windows.Forms.TextBox();
            //this.height_textbox = new System.Windows.Forms.TextBox();
            this.length_textbox = new NumericTextBox();
            this.width_textbox = new NumericTextBox();
            this.height_textbox = new NumericTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb_createBox)).BeginInit();
            //this.SuspendLayout();
            // 
            // pb_createBox
            // 
            this.pb_createBox.Location = new System.Drawing.Point(8, 8);
            this.pb_createBox.Name = "pb_createBox";
            this.pb_createBox.Size = new System.Drawing.Size(65, 65);
            this.pb_createBox.BorderStyle = BorderStyle.FixedSingle;
            this.pb_createBox.Image = Properties.Resources.BT_box;
            this.pb_createBox.TabIndex = 0;
            this.pb_createBox.TabStop = false;
            //tw_CreateBox.Control.Controls.Add(pb_createBox);

            // 
            // lb_reference
            // 
            this.lb_reference.AutoSize = true;
            this.lb_reference.Location = new System.Drawing.Point(79, 30);
            this.lb_reference.Name = "lb_reference";
            this.lb_reference.Size = new System.Drawing.Size(100, 15);
            this.lb_reference.TabIndex = 1;
            this.lb_reference.Text = "Reference";
            //tw_CreateBox.Control.Controls.Add(lb_reference);


            // 
            // cb_reference
            // 
            //int cb_width = UIEnvironment.Windows["ObjectBrowser"].Control.Size.Width - 10; // the ObjectBrowser width less the position of the cb_reference
            //                                                                               // more a margin of 10, to obtain the width size of the cb_reference
            this.cb_reference.FormattingEnabled = true;
            this.cb_reference.Location = new System.Drawing.Point(80, 50);
            this.cb_reference.Name = "cb_reference";
            this.cb_reference.Size = new System.Drawing.Size(105, 21);
            this.cb_reference.FlatStyle = FlatStyle.Flat;
            this.cb_reference.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            this.cb_reference.TabIndex = 2;
            //tw_CreateBox.Control.Controls.Add(cb_reference);
            cb_reference.Items.Add("World");
            //        buttonComboBox.SelectionChanged += new EventHandler(btnComboBox_SelectionChanged);
            cb_reference.Items.Add("UCS");
            cb_reference.SelectedIndex = 0;
            
            // 
            // pos_control
            // 
            int pc_width = UIEnvironment.Windows["ObjectBrowser"].Control.Size.Width - 18; // the ObjectBrowser width less the position of the cb_reference
                                                                                           // more a margin of 10, to obtain the width size of the cb_reference
            //PositionControl pos_control = new PositionControl();
            this.pos_control.ErrorProviderControl = null;
            this.pos_control.ExpressionErrorString = "Bad Expression";
            this.pos_control.LabelQuantity = ABB.Robotics.RobotStudio.BuiltinQuantity.Length;
            this.pos_control.LabelText = "Corner Point";
            this.pos_control.Location = new Point(8, 85);
            this.pos_control.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            this.pos_control.MaxValueErrorString = "Value exceeds maximum";
            this.pos_control.MinValueErrorString = "Value is below minimum";
            this.pos_control.Name = "pos_control";
            this.pos_control.NumTextBoxes = 3;
            this.pos_control.ReadOnly = false;
            this.pos_control.RefCoordSys = null;
            this.pos_control.ShowLabel = true;
            this.pos_control.Size = new Size(55, 105);
            this.pos_control.TabIndex = 4;
            this.pos_control.Text = "positionControl1";
            this.pos_control.VerticalLayout = false;
            this.pos_control.Focus();
            this.pos_control.Select();
            //tw_CreateBox.Control.Controls.Add(pos_control);
            
            // 
            // orientation_control
            // 
            //OrientationControl orientation_control = new OrientationControl();
            this.orientation_control.ErrorProviderControl = null;
            this.orientation_control.ExpressionErrorString = "Bad Expression";
            this.orientation_control.LabelQuantity = ABB.Robotics.RobotStudio.BuiltinQuantity.Length;
            this.orientation_control.LabelText = "Orientation";
            this.orientation_control.Location = new Point(8, 125);
            this.orientation_control.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            this.orientation_control.MaxValueErrorString = "Value exceeds maximum";
            this.orientation_control.MinValueErrorString = "Value is below minimum";
            this.orientation_control.Name = "orientation_control";
            this.orientation_control.NumTextBoxes = 3;
            this.orientation_control.ReadOnly = false;
            //orientation_control.RefCoordSys = null;
            this.orientation_control.ShowLabel = true;
            this.orientation_control.Size = new Size(34, pc_width);
            this.orientation_control.TabIndex = 6;
            this.orientation_control.Text = "positionControl1";
            this.orientation_control.VerticalLayout = false;
            //tw_CreateBox.Control.Controls.Add(orientation_control);

            // numericTextBox - Length
            // 
            //NumericTextBox length_textbox = new NumericTextBox();
            this.length_textbox.ErrorProviderControl = null;
            this.length_textbox.ExpressionErrorString = "Bad Expression";
            this.length_textbox.LabelText = "Length (mm)";
            this.length_textbox.Location = new Point(8, 165);
            this.length_textbox.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            this.length_textbox.MaxValue = 1000000000D;
            this.length_textbox.MaxValueErrorString = "Value exceeds maximum";
            this.length_textbox.MinValue = -1000000000D;
            this.length_textbox.MinValueErrorString = "Value is below minimum";
            this.length_textbox.Name = "length_textbox";
            this.length_textbox.Quantity = ABB.Robotics.RobotStudio.BuiltinQuantity.None;
            this.length_textbox.ReadOnly = false;
            this.length_textbox.ShowLabel = true;
            this.length_textbox.Size = new Size(34, pc_width);
            this.length_textbox.StepSize = 1D;
            this.length_textbox.TabIndex = 10;
            this.length_textbox.Text = "numericTextBox1";
            this.length_textbox.UserEdited = false;
            this.length_textbox.Value = 0D;
            //length_textbox.ValueChanged += new EventHandler(size_TextChanged);
            //tw_CreateBox.Control.Controls.Add(length_textbox);
            
            //
            // numericTextBox - Width
            // 
            //NumericTextBox width_textbox = new NumericTextBox();
            this.width_textbox.ErrorProviderControl = null;
            this.width_textbox.ExpressionErrorString = "Bad Expression";
            this.width_textbox.LabelText = "Width (mm)";
            this.width_textbox.Location = new Point(8, 205);
            this.width_textbox.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            this.width_textbox.MaxValue = 1000000000D;
            this.width_textbox.MaxValueErrorString = "Value exceeds maximum";
            this.width_textbox.MinValue = -1000000000D;
            this.width_textbox.MinValueErrorString = "Value is below minimum";
            this.width_textbox.Name = "width_textbox";
            this.width_textbox.Quantity = ABB.Robotics.RobotStudio.BuiltinQuantity.None;
            this.width_textbox.ReadOnly = false;
            this.width_textbox.ShowLabel = true;
            this.width_textbox.Size = new Size(34, pc_width);
            this.width_textbox.StepSize = 1D;
            this.width_textbox.TabIndex = 11;
            this.width_textbox.Text = "numericTextBox2";
            this.width_textbox.UserEdited = false;
            this.width_textbox.Value = 0D;
            //width_textbox.TextChanged += new EventHandler(size_TextChanged);
            //tw_CreateBox.Control.Controls.Add(width_textbox);
            
            //
            // numericTextBox - Height
            // 
            //NumericTextBox height_textbox = new NumericTextBox();
            this.height_textbox.ErrorProviderControl = null;
            this.height_textbox.ExpressionErrorString = "Bad Expression";
            this.height_textbox.LabelText = "Height (mm)";
            this.height_textbox.Location = new Point(8, 245);
            this.height_textbox.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            this.height_textbox.MaxValue = 1000000000D;
            this.height_textbox.MaxValueErrorString = "Value exceeds maximum";
            this.height_textbox.MinValue = -1000000000D;
            this.height_textbox.MinValueErrorString = "Value is below minimum";
            this.height_textbox.Name = "height_textbox";
            this.height_textbox.Quantity = ABB.Robotics.RobotStudio.BuiltinQuantity.None;
            this.height_textbox.ReadOnly = false;
            this.height_textbox.ShowLabel = true;
            this.height_textbox.Size = new Size(34, pc_width);
            this.height_textbox.StepSize = 1D;
            this.height_textbox.TabIndex = 6;
            this.height_textbox.Text = "numericTextBox3";
            this.height_textbox.UserEdited = false;
            this.height_textbox.Value = 0D;
            //height_textbox.ValueChanged += new EventHandler(size_TextChanged);
            //tw_CreateBox.Control.Controls.Add(height_textbox);

            //
            // Button Clear
            //
            //Button btn_clear = new Button();
            this.btn_clear.Text = "Clear";
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new Size(53, 25);
            this.btn_clear.Location = new Point(pc_width - 165, 290);
            this.btn_clear.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btn_clear.Click += new EventHandler(btn_clear_clicked);
            this.btn_clear.TextAlign = ContentAlignment.MiddleCenter;
            this.btn_clear.FlatStyle = FlatStyle.Flat;
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.TabIndex = 7;
            //tw_CreateBox.Control.Controls.Add(btn_clear);

            //
            // Button Create
            //
            //Button btn_create = new Button();
            this.btn_create.Text = "Create";
            this.btn_create.Name = "btn_create";
            this.btn_create.Size = new Size(53, 25);
            this.btn_create.Location = new Point(pc_width - 105, 290);
            this.btn_create.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btn_create.TextAlign = ContentAlignment.MiddleCenter;
            this.btn_create.FlatStyle = FlatStyle.Flat;
            this.btn_create.UseVisualStyleBackColor = true;
            this.btn_create.TabIndex = 8;
            //if (!string.IsNullOrEmpty(length_textbox.Text))
            //    btn_create.Enabled = true;
            //else
            //    btn_create.Enabled = false;
            btn_create.Enabled = false;
            //tw_CreateBox.Control.Controls.Add(btn_create);
            //btn_create.Click += new EventHandler(btn_create_clicked);
            
            //
            // Button Close
            //
            //Button btn_close = new Button();
            this.btn_close.Text = "Close";
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new Size(53, 25);
            this.btn_close.Location = new Point(pc_width - 45, 290);
            this.btn_close.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            //btn1.Image = Properties.Resources.BT_box;
            //btn1.ImageAlign = ContentAlignment.MiddleLeft;
            this.btn_close.TextAlign = ContentAlignment.MiddleCenter;
            this.btn_close.FlatStyle = FlatStyle.Flat;
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.TabIndex = 9;
            //tw_CreateBox.Control.Controls.Add(btn_close);
            //btn_close.Click += new EventHandler(btn_close_clicked);



            // 
            // Create_box2
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //this.ClientSize = new System.Drawing.Size(438, 440);
            this.Control.Controls.Add(this.height_textbox);
            this.Control.Controls.Add(this.width_textbox);
            this.Control.Controls.Add(this.length_textbox);
            this.Control.Controls.Add(this.btn_clear);
            this.Control.Controls.Add(this.btn_create);
            this.Control.Controls.Add(this.btn_close);
            this.Control.Controls.Add(this.orientation_control);
            this.Control.Controls.Add(this.pos_control);
            this.Control.Controls.Add(this.cb_reference);
            this.Control.Controls.Add(this.lb_reference);
            this.Control.Controls.Add(this.pb_createBox);
            //this.Name = "Create_box2";
            //this.Text = "Create_box2";
            ((System.ComponentModel.ISupportInitialize)(this.pb_createBox)).EndInit();
            //this.ResumeLayout(false);
            //this.PerformLayout();


            



        }

        #endregion

        private System.Windows.Forms.PictureBox pb_createBox;
        private System.Windows.Forms.Label lb_reference;
        private System.Windows.Forms.ComboBox cb_reference;
        private PositionControl pos_control;
        private OrientationControl orientation_control;
        private Button btn_clear;
        private Button btn_create;
        private Button btn_close;
        private NumericTextBox length_textbox;
        private NumericTextBox width_textbox;
        private NumericTextBox height_textbox;

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

    }
}