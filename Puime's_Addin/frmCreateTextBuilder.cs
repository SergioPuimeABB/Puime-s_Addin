using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using ABB.Robotics.Math;
using ABB.Robotics.RobotStudio;
using ABB.Robotics.RobotStudio.Environment;
using ABB.Robotics.RobotStudio.Stations;
using ABB.Robotics.RobotStudio.Stations.Forms;
using PuimesAddin.Properties;

namespace Puime_s_Addin
{
    public class frmCreateTextBuilder : ToolControlBase
    {
        private ComboBox comboBox1;
        private TextBox textBox1;
        private TextBox textBox2;

        public frmCreateTextBuilder()
        {
            InitializeComponent();
            base.Activate += frmCreateTex_Activate;
            base.Deactivate += frmCreateTex_Deactivate;


            // Load fonts
            InstalledFontCollection installedFonts = new InstalledFontCollection();
            foreach (FontFamily fontFamily in installedFonts.Families)
            {
                comboBox1.Items.Add(fontFamily.Name);
            }

            comboBox1.SelectedIndexChanged += FontComboBox_SelectedIndexChanged;
            textBox1.TextChanged += InputTextBox_TextChanged;

        }
        private void frmCreateTex_Activate(object sender, EventArgs e)
        {
        }

        private void frmCreateTex_Deactivate(object sender, EventArgs e)
        {
        }
        private void InputTextBox_TextChanged(object sender, EventArgs e)
        {
            //labelResultname.Text = textBoxPrefix.Text + numericUpDownStartWith.Value + textBoxSuffix.Text;
            CreateText();
        }

        private void FontComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //labelResultname.Text = textBoxPrefix.Text + numericUpDownStartWith.Value + textBoxSuffix.Text;
            CreateText();
        }


        private void Btn_create_clicked(object sender, EventArgs e)
        {
        }

        private void Btn_close_clicked(object sender, EventArgs e)
        {
            CloseTool();
        }


        private void CreateText()
        {

            if (comboBox1.SelectedItem == null || string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please enter text and select a font.");
                return;
            }

            //Font DaFont = comboBox1.SelectedItem as Font;
            string selectedFontName = comboBox1.SelectedItem.ToString();

            Font DaFont = new Font(selectedFontName, Convert.ToUInt32(textBox2.Text));
            string text = textBox1.Text;

            // Create a bitmap to measure the text
            using (Bitmap bitmap = new Bitmap(1, 1))
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                // Measure the string
                SizeF size = graphics.MeasureString(text, DaFont);

                // Get the advance width and left side bearing
                float advanceWidth = size.Width;
                float leftSideBearing = graphics.MeasureString(" "+text, DaFont).Width - advanceWidth;

                Logger.AddMessage(new LogMessage(""));
                Logger.AddMessage(new LogMessage($"AdvanceWith: {advanceWidth}", "Puime's Add-in"));
                Logger.AddMessage(new LogMessage($"leftSideBearing: {leftSideBearing}", "Puime's Add-in"));
                Logger.AddMessage(new LogMessage(""));

            }

            FontMetrics fm = g.GetFontMetrics(font);


            Project.UndoContext.BeginUndoStep("CreateText");
            try
            {
                Station station = Station.ActiveStation;


                Part p = new Part() { Name = "Text" };
                station.GraphicComponents.Add(p);

                //string text = "F";

                //using (Font f = new Font("Tahoma", 40f))
                using (Font f = new Font(selectedFontName, Convert.ToUInt32(textBox2.Text)))

                {
                    foreach (char c in text)
                    {
                        using (GraphicsPath path = new GraphicsPath())
                        {
                            path.AddString(c.ToString(), f.FontFamily, (int)f.Style, f.Size, new PointF(0, 0), StringFormat.GenericDefault);
                            PointF[] points = path.PathPoints;
                            byte[] types = path.PathTypes;

                            Logger.AddMessage(new LogMessage("--"));
                            Logger.AddMessage(new LogMessage($"Character: {path.GetBounds().Size}", "Puime's Add-in"));
                            Logger.AddMessage(new LogMessage("--"));

                            bool bFirstJump = true;

                            Vector3 vTempStart = new Vector3();

                            using (Graphics g = CreateGraphics())
                            {
                                for (int i = 0; i < text.Length; i++)
                                {
                                    //char c = text[i];
                                    SizeF charSize = g.MeasureString(c.ToString(), f);
                                    //PointF[] glyphPoints = GetGlyphPoints(g, c);

                                    //detailsListBox.Items.Add($"Character: {c}");
                                    //detailsListBox.Items.Add($"Size: {charSize.Width} x {charSize.Height}");
                                    //detailsListBox.Items.Add($"Glyph Points: {string.Join(", ", points)}");
                                    //detailsListBox.Items.Add($"Separation Distance: {charSize.Width}");
                                    //detailsListBox.Items.Add("");




                                    Logger.AddMessage(new LogMessage($"Character: {c}", "Puime's Add-in"));
                                    Logger.AddMessage(new LogMessage($"Size: {charSize.Width} x {charSize.Height}"));
                                    Logger.AddMessage(new LogMessage($"Glyph Points: {string.Join(", ", points)}"));
                                    Logger.AddMessage(new LogMessage($"Separation Distance: {charSize.Width}"));
                                    Logger.AddMessage(new LogMessage(""));


                                    
                                }
                            }

                            

                            Logger.AddMessage(new LogMessage("Character:" + c.ToString(), "Puime's Add-in"));
                            for (int i = 0; i < points.Length; i++)
                            {
                                Logger.AddMessage(new LogMessage("Point: " + i.ToString() + "Type: " + types[i].ToString() + " " + "X: " + points[i].X.ToString() + "Y: " + points[i].Y.ToString(), "Puime's Add-in"));

                                if (types[i] == 0) // StartPoint
                                {
                                    if (bFirstJump == false)
                                    {
                                        vTempStart = (new Vector3(points[i].X / 1000, points[i].Y / 1000, 0.0));
                                    }
                                }

                                if (types[i] == 1) // Line
                                {
                                    Vector3 vStart = new Vector3(points[i - 1].X / 1000, points[i - 1].Y / 1000, 0.0);
                                    Vector3 vEnd = new Vector3(points[i].X / 1000, points[i].Y / 1001, 0.0);

                                    Body b1 = Body.CreateLine(vStart, vEnd);
                                    b1.Name = "Line" + i.ToString();
                                    b1.Color = Color.Red;
                                    p.Bodies.Add(b1);

                                }

                                if (types[i] == 3) // Bezier curve
                                {
                                    Vector3 vStart = new Vector3(points[i - 1].X / 1000, points[i - 1].Y / 1000, 0.0);
                                    Vector3 vCP1 = new Vector3(points[i].X / 1000, points[i].Y / 1000, 0.0);
                                    Vector3 vCP2 = new Vector3(points[i + 1].X / 1000, points[i + 1].Y / 1000, 0.0);
                                    Vector3 vEnd = new Vector3(points[i + 2].X / 1000, points[i + 2].Y / 1000, 0.0);
                                    i += 2; // Skip the next two points as they are part of the current Bezier segment

                                    Body b1 = Body.CreateLine(vStart, vEnd);
                                    b1.Name = "Line" + i.ToString();
                                    b1.Color = Color.Blue;
                                    p.Bodies.Add(b1);

                                    Matrix4 mCP1 = new Matrix4(vCP1);
                                    Matrix4 mCP2 = new Matrix4(vCP2);

                                    Body b2 = Body.CreateCircle(mCP1, 0.00001);
                                    b2.Name = "Circle" + i.ToString();
                                    b2.Color = Color.Green;
                                    p.Bodies.Add(b2);

                                    Body b3 = Body.CreateCircle(mCP2, 0.00001);
                                    b3.Name = "Circle" + i.ToString();
                                    b3.Color = Color.Green;
                                    p.Bodies.Add(b3);
                                }

                                if (types[i] == 129) // JumpPoint
                                {
                                    Vector3 vStart = new Vector3(points[i - 1].X / 1000, points[i - 1].Y / 1000, 0.0);
                                    Vector3 vEnd = new Vector3(points[i].X / 1000, points[i].Y / 1000, 0.0);

                                    Body b1 = Body.CreateLine(vStart, vEnd);
                                    b1.Name = "Line" + i.ToString();
                                    b1.Color = Color.Red;
                                    p.Bodies.Add(b1);

                                    if (bFirstJump)
                                    {
                                        Vector3 vStart2 = new Vector3(points[i].X / 1000, points[i].Y / 1000, 0.0);
                                        Vector3 vEnd2 = new Vector3(points[0].X / 1000, points[0].Y / 1000, 0.0);

                                        Body b1b = Body.CreateLine(vStart2, vEnd2);
                                        b1b.Name = "Line" + i.ToString() + " 129 First Jump";
                                        b1b.Color = Color.Red;
                                        p.Bodies.Add(b1b);

                                        bFirstJump = false;
                                    }
                                    else
                                    {
                                        Vector3 vStart2 = new Vector3(points[i].X / 1000, points[i].Y / 1000, 0.0);
                                        Body b1c = Body.CreateLine(vStart2, vTempStart);
                                        b1c.Name = "Line" + i.ToString() + " 129 NO First Jump";
                                        b1c.Color = Color.Red;
                                        p.Bodies.Add(b1c);
                                    }
                                }

                                if (types[i] == 161) // EndPoint
                                {

                                    if (bFirstJump == true)
                                    {
                                        Vector3 vStart = new Vector3(points[i - 1].X / 1000, points[i - 1].Y / 1000, 0.0);
                                        Vector3 vEnd = new Vector3(points[i].X / 1000, points[i].Y / 1000, 0.0);

                                        Body b1 = Body.CreateLine(vStart, vEnd);
                                        b1.Name = "Line" + i.ToString() + " End";
                                        b1.Color = Color.Red;
                                        p.Bodies.Add(b1);

                                        Vector3 vStart2 = new Vector3(points[i].X / 1000, points[i].Y / 1000, 0.0);
                                        Vector3 vEnd2 = new Vector3(points[0].X / 1000, points[0].Y / 1000, 0.0);

                                        Body b1b = Body.CreateLine(vStart2, vEnd2);
                                        b1b.Name = "Line" + i.ToString() + "Line Close";
                                        b1b.Color = Color.Red;
                                        p.Bodies.Add(b1b);
                                    }
                                    else
                                    {
                                        Vector3 vStart = new Vector3(points[i - 1].X / 1000, points[i - 1].Y / 1000, 0.0);
                                        Vector3 vEnd = new Vector3(points[i].X / 1000, points[i].Y / 1000, 0.0);

                                        Body b1 = Body.CreateLine(vStart, vEnd);
                                        b1.Name = "Line" + i.ToString() + " End with Jump";
                                        b1.Color = Color.Red;
                                        p.Bodies.Add(b1);

                                        Vector3 vStart2 = new Vector3(points[i].X / 1000, points[i].Y / 1000, 0.0);

                                        Body b1b = Body.CreateLine(vStart2, vTempStart);
                                        b1b.Name = "Line" + i.ToString() + "Line Close with Jump";
                                        b1b.Color = Color.Red;
                                        p.Bodies.Add(b1b);
                                    }
                                }
                            }

                            
                        }
                    }
                }

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


        }


        private void InitializeComponent()
        {

            //comboBox1 = new ComboBox();

            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();

            SuspendLayout();
            // 
            // comboBox1 - Font selection
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(49, 63);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedText = "Arial";
            // 
            // textBox1 - Text to Create
            // 
            this.textBox1.Location = new System.Drawing.Point(49, 126);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.Focus();
            // 
            // textBox2 - Text size
            // 
            this.textBox2.Location = new System.Drawing.Point(49, 156);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 1;
            this.textBox2.Text = "12";

            AutoScroll = true;
            base.AdjustableHeight = true;
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            base.Caption = "Text Creator";
            base.Controls.Add(textBox1);
            base.Controls.Add(comboBox1);
            base.Controls.Add(textBox2);
            base.Name = "frmCreateTextBuilder";
            base.Size = new System.Drawing.Size(242, 340);
            ResumeLayout(false);
            PerformLayout();
        }



    }
}
