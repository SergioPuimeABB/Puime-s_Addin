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
    public partial class frmMainWindowSize : ToolControlBase
    {
        private Button buttonCreate;

        public frmMainWindowSize()
        {
            Project.UndoContext.BeginUndoStep("AddToolWindow");

            #region add toolwindow and elements
            try
            {
                InitializeComponent();
                base.Activate += frmMainWindowSize_Activate;
                base.Deactivate += frmMainWindowSize_Desactivate;
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

        private void frmMainWindowSize_Activate(object sender, EventArgs e)
        {
        }

        private void frmMainWindowSize_Desactivate(object sender, EventArgs e)
        {
            CloseTool();
        }


        public void btn_close_clicked(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void btn_create_clicked(object sender, EventArgs e)
        {
                Project.UndoContext.BeginUndoStep();
                try
                {
                    ChangeWindowSize();
                }
                finally
                {
                    Project.UndoContext.EndUndoStep();
                }
        }

        private void ChangeWindowSize()
        {
            //TODO: Buscar información de MainWindowSize

            // Begin UndoStep
            Project.UndoContext.BeginUndoStep("AddDocumentWindow");

            try
            {
                //// Create a new graphic control.
                //GraphicControl gc = new GraphicControl();

                //// Set our station as the object for the graphic control.
                //gc.RootObject = Station.ActiveStation;

                //// Create a camera (our view at the station).
                //Camera cam = new Camera();

                //// Tell it where to look from.
                //cam.LookFrom = new Vector3(10, 10, 10);

                //// Set the camera as the graphic control's camera.
                //gc.Camera = cam;

                ////Size mysize = new Size(12, 12);
                ////gc.ClientSize = mysize;
                ////gc.Height = 12;
                ////gc.Width = 12;
                ////gc.EnableGameControllerNavigation = true;

                //// Create a new window, with our graphic control.
                //DocumentWindow dw = new DocumentWindow("Window ID", gc, "Window Caption");

                //// Add the window to RobotStudio.
                //UIEnvironment.Windows.Add(dw);

                //MainWindow mw = MainWindow();


                //Station stn = Station.ActiveStation;
                //if (stn == null) return;

                //GraphicComponentCollection gc = new GraphicComponentCollection(stn);
                ////Part myPart = sProfile.RootComponent.CopyInstance() as Part;
                //MainWindow[] mw2 = stn.FindGraphicComponentsByType() ;

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

        private void CreateABBBox_Deactivate(object sender, EventArgs e)
        {
            //UpdatePreview(valid: false);
        }

        private void InitializeComponent()
        {
            int tw_width = UIEnvironment.Windows["ObjectBrowser"].Control.Size.Width - 25;

            buttonCreate = new Button();
            SuspendLayout();

            buttonCreate.Text = "Create";
            buttonCreate.Size = new Size(53, 25);
            buttonCreate.Location = new Point(tw_width - 98, 295);
            buttonCreate.FlatStyle = FlatStyle.Flat;
            buttonCreate.UseVisualStyleBackColor = true;
            buttonCreate.TabIndex = 7;
            buttonCreate.Enabled = true;
            buttonCreate.Click += new EventHandler(btn_create_clicked);

            AutoScroll = true;
            base.AdjustableHeight = true;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.Caption = "Create ABB Box";
            base.Controls.Add(buttonCreate);
            base.Name = "frmCreateBoxBuilder";
            base.Size = new Size(tw_width, 330);
            base.Apply += new System.EventHandler(btn_create_clicked);
            base.Deactivate += new System.EventHandler(CreateABBBox_Deactivate);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

