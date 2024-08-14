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
    public class frmAutoMarkUpBuilder : ToolControlBase
    {
        public bool _firstClick = true;

        public int _markNumber = 0;

        private PositionControl positionControlPos;

        private TextBox textBoxPrefix;
        private TextBox textBoxSuffix;
        
        private ComboBox comboBoxIncrementSteps;
        private ComboBox comboBoxClipStandard;

        private NumericUpDown numericUpDownStartWith;

        private Label labelResultname;
        private Label labelPrefix;
        private Label labelSuffix;
        private Label labelNumIncrements;
        private Label labelStartnumber;
        private Label labelColor;
        private Label labelClipStandard;
        private Label labelFirstlabelname;
        private Label labelAllwaysOnTop;

        private Button buttonClear;
        private Button buttonCreate;
        private Button buttonClose;
        private Button buttonColor;
        
        private ColorDialog colorDialogColor;

        private PictureBox pictureboxNutImage;

        private CheckBox checkboxAllwaysOnTop;

        //private Markup markupWText;

        public frmAutoMarkUpBuilder()
        {
            InitializeComponent();
            base.Activate += frmAutoMarkUp_Activate;
            base.Deactivate += frmAutoMarkUp_Deactivate;
        }
        private void frmAutoMarkUp_Activate(object sender, EventArgs e)
        {
            if (_firstClick | positionControlPos.Focused)
            {
                Logger.AddMessage(new LogMessage("Pick targets", "Puime's Addin - Auto markup creator"));
                GraphicPicker.GraphicPick += new GraphicPickEventHandler(GraphicPicker_GraphicPick);
            }
        }

        private void frmAutoMarkUp_Deactivate(object sender, EventArgs e)
        {
            GraphicPicker.GraphicPick -= GraphicPicker_GraphicPick;
        }

        private void GraphicPicker_GraphicPick(object sender, GraphicPickEventArgs e)
        {
            //Logger.AddMessage(new LogMessage("GraphicPicker_GraphicPick ."));

            CreateMarkUp(e.PickedPosition);
            _firstClick = false;
            positionControlPos.SetFocus();
        }

        private void CreateMarkUp(Vector3 position)
        {
            Station station = Project.ActiveProject as Station;
            Markup markupWText = new Markup();
            markupWText.Transform.Translation = position;
            markupWText.Text = GenerateName();
            markupWText.Name = markupWText.Text;
            markupWText.BackgroundColor = buttonColor.BackColor;
            markupWText.TextColor = Color.Black;

            if (checkboxAllwaysOnTop.Checked)
            {
                markupWText.Topmost = true;
            }
            else
            {
                markupWText.Topmost = false;
            }
            
            switch (comboBoxClipStandard.SelectedItem)
            {
                case "Grommet":
                    markupWText.TextColor = Color.White;
                    break;
                case "Big MetNut":
                    markupWText.TextColor = Color.White;
                    break;
                case "Small Snap Clip":
                    markupWText.TextColor = Color.White;
                    break;
                case "S-Clip":
                    markupWText.TextColor = Color.White;
                    break;
                case "Klammer":
                    markupWText.TextColor = Color.White;
                    break;
                case "":
                    markupWText.TextColor = Color.Black;
                    break;
                default:
                    markupWText.TextColor = Color.Black;
                    break;
            }
            station.Markups.Add(markupWText);
            _markNumber += 1 * Increments();
        }

        private string GenerateName()
        {
            string pref = textBoxPrefix.Text;
            int name = Int16.Parse(numericUpDownStartWith.Text);
            string suff = textBoxSuffix.Text;
            int resultname = name + _markNumber;
            string generatedName = pref + resultname + suff;
            return generatedName;
        }

        private int Increments()
        {
            int inc = Int16.Parse(comboBoxIncrementSteps.SelectedItem.ToString());
            return inc;
        }

        private void ComboboxIncrementSteps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxIncrementSteps.SelectedIndex == -1)
            {
                numericUpDownStartWith.Value = decimal.Zero;
            }
            else
            {
                numericUpDownStartWith.Value = Convert.ToDecimal(comboBoxIncrementSteps.SelectedItem.ToString());
            }
        }

        private void TextValueChanged(object sender, EventArgs e)
        {
            labelResultname.Text = textBoxPrefix.Text + numericUpDownStartWith.Value + textBoxSuffix.Text;
        }


        private void buttonColor_Click(object sender, EventArgs e)
        {
            colorDialogColor.ShowDialog();
            buttonColor.BackColor = colorDialogColor.Color;
        }

        private void ClipValueChanged(object sender, EventArgs e)
        {
            switch (comboBoxClipStandard.SelectedItem)
            {
                case "Panzer":
                    buttonColor.BackColor = Color.FromArgb(128, 128, 0);
                    pictureboxNutImage.BorderStyle = BorderStyle.FixedSingle;
                    pictureboxNutImage.Image = Resources.Panzer;
                    textBoxPrefix.Text = "Panzer";
                    break;
                case "Snap":
                    buttonColor.BackColor = Color.FromArgb(255, 127, 0);
                    pictureboxNutImage.BorderStyle = BorderStyle.FixedSingle;
                    pictureboxNutImage.Image = Resources.Snap;
                    textBoxPrefix.Text = "Snap";
                    break;
                case "Grommet":
                    buttonColor.BackColor = Color.FromArgb(0, 0, 255);
                    pictureboxNutImage.Image = Resources.Grommet;
                    textBoxPrefix.Text = "Grommet";
                    break;
                case "Metnut/C_Nut":
                    buttonColor.BackColor = Color.FromArgb(255, 0, 255);
                    pictureboxNutImage.Image = Resources.Metnut;
                    textBoxPrefix.Text = "Metnut";
                    break;
                case "Nut":
                    buttonColor.BackColor = Color.FromArgb(247, 191, 190);
                    pictureboxNutImage.Image = Resources.Nut;
                    textBoxPrefix.Text = "Nut";
                    break;
                case "Big MetNut":
                    buttonColor.BackColor = Color.FromArgb(104, 36, 109);
                    pictureboxNutImage.Image = Resources.BigMetNut;
                    textBoxPrefix.Text = "BigNetNut";
                    break;
                case "Trim Fastener":
                    buttonColor.BackColor = Color.FromArgb(255, 215, 0);
                    pictureboxNutImage.Image = Resources.TrimFastener;
                    textBoxPrefix.Text = "TrimFastener";
                    break;
                case "Small Snap Clip":
                    buttonColor.BackColor = Color.FromArgb(0, 0, 0);
                    pictureboxNutImage.Image = Resources.SmallSnapClip;
                    textBoxPrefix.Text = "SmallSnapClip";
                    break;
                case "Ret Clip":
                    buttonColor.BackColor = Color.FromArgb(245, 245, 220);
                    pictureboxNutImage.Image = Resources.RetClip;
                    textBoxPrefix.Text = "RetClip";
                    break;
                case "S-Clip":
                    buttonColor.BackColor = Color.FromArgb(150, 75, 0);
                    pictureboxNutImage.Image = Resources.SClip;
                    textBoxPrefix.Text = "SClip";
                    break;
                case "Klammer":
                    buttonColor.BackColor = Color.FromArgb(128, 0, 32);
                    pictureboxNutImage.Image = Resources.Klammer;
                    textBoxPrefix.Text = "Klammer";
                    break;
                case "TrimClip Plastic":
                    buttonColor.BackColor = Color.FromArgb(195, 176, 145);
                    pictureboxNutImage.Image = Resources.TrimClipPlastic;
                    textBoxPrefix.Text = "TrimClipPlastic";
                    break;
                case "Plastic Nut":
                    buttonColor.BackColor = Color.FromArgb(208, 255, 20);
                    pictureboxNutImage.Image = Resources.PlasticNut;
                    textBoxPrefix.Text = "PlasticNut";
                    break;
                default:
                    break;
            }
        }

        private void btn_reset_clicked(object sender, EventArgs e)
        {
            positionControlPos.Value = new Vector3(0, 0, 0);
            textBoxPrefix.Text = "";
            textBoxSuffix.Text = "";
            comboBoxIncrementSteps.SelectedItem = "1";
            numericUpDownStartWith.Value = 1;
            buttonColor.BackColor = Color.FromArgb(255, 255, 192);
            comboBoxClipStandard.SelectedItem = null;
            pictureboxNutImage.Image = null;
            pictureboxNutImage.BorderStyle = BorderStyle.None;
            _markNumber = 0;
        }

        private void btn_create_clicked(object sender, EventArgs e)
        {
            CreateMarkUp(positionControlPos.Value);
            _firstClick = false;
            positionControlPos.SetFocus();
        }

        private void btn_close_clicked(object sender, EventArgs e)
        {
            GraphicPicker.GraphicPick -= GraphicPicker_GraphicPick;
            CloseTool();
        }


        private void InitializeComponent()
        {
            int tw_width = UIEnvironment.Windows["ObjectBrowser"].Control.Size.Width - 15;

            positionControlPos = new PositionControl();
            textBoxPrefix = new TextBox();
            textBoxSuffix = new TextBox();
            labelPrefix = new Label();
            labelSuffix = new Label();
            labelNumIncrements = new Label();
            labelStartnumber = new Label();
            labelStartnumber = new Label();
            labelFirstlabelname = new Label();
            labelColor = new Label();
            labelAllwaysOnTop = new Label();
            checkboxAllwaysOnTop = new CheckBox();
            buttonColor = new Button();
            colorDialogColor = new ColorDialog();
            labelClipStandard = new Label();
            labelResultname = new Label();
            comboBoxIncrementSteps = new ComboBox();
            comboBoxClipStandard = new ComboBox();
            pictureboxNutImage = new PictureBox();
            numericUpDownStartWith = new NumericUpDown();
            buttonClear = new Button();
            buttonCreate = new Button();
            buttonClose = new Button();

            positionControlPos.SuspendLayout();
            SuspendLayout();

            positionControlPos.ErrorProviderControl = null;
            positionControlPos.ExpressionErrorString = "Bad Expression";
            positionControlPos.LabelQuantity = ABB.Robotics.RobotStudio.BuiltinQuantity.Length;
            positionControlPos.LabelText = "Position";
            positionControlPos.Location = new Point(8, 18);
            positionControlPos.MaxValueErrorString = "Value exceeds maximum";
            positionControlPos.MinValueErrorString = "Value is below minimum";
            positionControlPos.Name = "pos_control";
            positionControlPos.NumTextBoxes = 3;
            positionControlPos.ReadOnly = false;
            positionControlPos.RefCoordSys = null;
            positionControlPos.ShowLabel = true;
            positionControlPos.Size = new Size(tw_width, 34);
            positionControlPos.TabIndex = 1;
            positionControlPos.Text = "positionControl1";
            positionControlPos.VerticalLayout = false;

            labelPrefix.Text = "Name Prefix";
            labelPrefix.Location = new Point(8, 60);
            labelPrefix.Size = new Size(100, 14);

            textBoxPrefix.Location = new Point(8, 75);
            textBoxPrefix.Size = new Size((positionControlPos.Width / 2) - 15, 34);
            textBoxPrefix.TabIndex = 2;
            textBoxPrefix.TextChanged += new EventHandler(TextValueChanged);

            labelSuffix.Text = "Name Suffix";
            labelSuffix.Location = new Point((positionControlPos.Width / 2) + 23, 60);
            labelSuffix.Size = new Size(100, 14);

            textBoxSuffix.Location = new Point((positionControlPos.Width / 2) + 23, 75);
            textBoxSuffix.Size = textBoxPrefix.Size;
            textBoxSuffix.TabIndex = 3;
            textBoxSuffix.TextChanged += new EventHandler(TextValueChanged);

            labelNumIncrements.Text = "Increment";
            labelNumIncrements.Location = new Point(8, 105);
            labelNumIncrements.Size = new Size(60, 14);

            comboBoxIncrementSteps.Location = new Point(8, 120);
            comboBoxIncrementSteps.Size = new Size(70, 44);
            comboBoxIncrementSteps.DropDownWidth = 70;
            comboBoxIncrementSteps.Items.AddRange(new object[]
                    {"1",
                     "10",
                     "100",
                     "1000"});
            comboBoxIncrementSteps.SelectedItem = "1";
            comboBoxIncrementSteps.TabIndex = 4;
            comboBoxIncrementSteps.SelectedIndexChanged += ComboboxIncrementSteps_SelectedIndexChanged;
            comboBoxIncrementSteps.SelectedIndexChanged += new EventHandler(TextValueChanged);

            labelStartnumber.Text = "Start number";
            labelStartnumber.Location = new Point((positionControlPos.Width / 2) - 26, 105);
            labelStartnumber.Size = new Size(70, 14);

            numericUpDownStartWith.Location = new Point((positionControlPos.Width / 2) - 26, 120);
            numericUpDownStartWith.Size = new Size(70, 85);
            numericUpDownStartWith.Minimum = 1;
            numericUpDownStartWith.Maximum = 1000;
            numericUpDownStartWith.Increment = 1;
            numericUpDownStartWith.DecimalPlaces = 0;
            numericUpDownStartWith.Value = 1;
            numericUpDownStartWith.DecimalPlaces = 0;
            numericUpDownStartWith.TabIndex = 5;
            numericUpDownStartWith.ValueChanged += new EventHandler(TextValueChanged);

            labelColor.Text = "Color";
            labelColor.Location = new Point(positionControlPos.Width - 62, 105);
            labelColor.Size = new Size(70, 14);

            buttonColor.Location = new Point(positionControlPos.Width - 62, 119);
            buttonColor.Name = "buttonnColor";
            buttonColor.Size = new Size(70, 23);
            buttonColor.TabIndex = 6;
            buttonColor.UseVisualStyleBackColor = true;
            buttonColor.BackColor = Color.FromArgb(255, 255, 192);
            buttonColor.Click += new System.EventHandler(buttonColor_Click);

            checkboxAllwaysOnTop.Location = new Point(8, 150);
            checkboxAllwaysOnTop.TabIndex = 7;
            checkboxAllwaysOnTop.Size = new Size(14, 14);
            checkboxAllwaysOnTop.Checked = true;

            labelAllwaysOnTop.Text = "Allways on top";
            labelAllwaysOnTop.Location = new Point(25, 150);
            labelAllwaysOnTop.Size = new Size(120, 14);

            labelClipStandard.Text = "Clipping Standard Color";
            labelClipStandard.Location = new Point(8, 180);
            labelClipStandard.Size = new Size(150, 14);

            comboBoxClipStandard.Location = new Point(8, 196);
            comboBoxClipStandard.Size = new Size(150, 44);
            comboBoxClipStandard.DropDownWidth = 70;
            comboBoxClipStandard.Items.AddRange(new object[]
                    {"Panzer",
                     "Snap",
                     "Grommet",
                     "Metnut/C_Nut",
                     "Nut",
                     "Big MetNut",
                     "Trim Fastener",
                     "Small Snap Clip",
                     "Ret Clip",
                     "S-Clip",
                     "Klammer",
                     "TrimClip Plastic",
                     "Plastic Nut"});
            comboBoxClipStandard.SelectedItem = "0";
            comboBoxClipStandard.TabIndex = 7;
            comboBoxClipStandard.SelectedIndexChanged += new EventHandler(ClipValueChanged);

            pictureboxNutImage.Location = new Point(172, 150);
            pictureboxNutImage.Name = "pictureboxNutImage";
            pictureboxNutImage.Size = new Size(68, 68);
            pictureboxNutImage.BorderStyle = BorderStyle.None;
            pictureboxNutImage.Image = null;
            pictureboxNutImage.TabStop = false;

            labelFirstlabelname.Text = "First label name: ";
            labelFirstlabelname.Location = new Point(8, 255);
            labelFirstlabelname.Size = new Size(90, 14);

            labelResultname.Location = new Point(95, 255);
            labelResultname.Size = new Size(80, 14);
            labelResultname.Text = textBoxPrefix.Text + numericUpDownStartWith.Value + textBoxSuffix.Text;

            buttonClear.Location = new Point(tw_width - 170, 300);
            buttonClear.Size = new Size(53, 25);
            buttonClear.Text = "Reset";
            buttonClear.FlatStyle = FlatStyle.Flat;
            buttonClear.UseVisualStyleBackColor = true;
            buttonClear.TabIndex = 8;
            buttonClear.Click += new EventHandler(btn_reset_clicked);

            buttonCreate.Location = new Point(tw_width - 110, 300);
            buttonCreate.Size = new Size(53, 25);
            buttonCreate.Text = "Create";
            buttonCreate.FlatStyle = FlatStyle.Flat;
            buttonCreate.UseVisualStyleBackColor = true;
            buttonCreate.TabIndex = 9;
            buttonCreate.Click += new EventHandler(btn_create_clicked);

            buttonClose.Location = new Point(tw_width - 50, 300);
            buttonClose.Size = new Size(53, 25);
            buttonClose.Text = "Close";
            buttonClose.FlatStyle = FlatStyle.Flat;
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.TabIndex = 10;
            buttonClose.Click += new EventHandler(btn_close_clicked);

            AutoScroll = true;
            base.AdjustableHeight = true;
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            base.Caption = "Auto MarkUp Tool";
            base.Controls.Add(positionControlPos);
            base.Controls.Add(labelPrefix);
            base.Controls.Add(textBoxPrefix);
            base.Controls.Add(labelSuffix);
            base.Controls.Add(textBoxSuffix);
            base.Controls.Add(labelNumIncrements);
            base.Controls.Add(comboBoxIncrementSteps);
            base.Controls.Add(labelStartnumber);
            base.Controls.Add(labelColor);
            base.Controls.Add(buttonColor);
            base.Controls.Add(numericUpDownStartWith);
            base.Controls.Add(checkboxAllwaysOnTop);
            base.Controls.Add(labelAllwaysOnTop);
            base.Controls.Add(labelClipStandard);
            base.Controls.Add(comboBoxClipStandard);
            base.Controls.Add(pictureboxNutImage);
            base.Controls.Add(labelFirstlabelname);
            base.Controls.Add(labelResultname);
            base.Controls.Add(buttonClear);
            base.Controls.Add(buttonCreate);
            base.Controls.Add(buttonClose);
            base.Name = "frmAutoMarkUpBuilder";
            base.Size = new System.Drawing.Size(242, 340);
            positionControlPos.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }



    }
}
