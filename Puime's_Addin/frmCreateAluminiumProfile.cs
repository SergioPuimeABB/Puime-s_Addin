using ABB.Robotics.Controllers.RapidDomain;
using ABB.Robotics.Math;
using ABB.Robotics.RobotStudio;
using ABB.Robotics.RobotStudio.Environment;
using ABB.Robotics.RobotStudio.Stations;
using ABB.Robotics.RobotStudio.Stations.Forms;
using PuimesAddin.Properties;
using RobotStudio.API.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace Puime_s_Addin
{
    public partial class FrmCreateAluminiumProfile : ToolControlBase
    {
        private PictureBox pictureBoxModel;
        private Label labelReference;
        private Label labelDetails;
        private ComboBox comboBoxReference;
        private PositionControl positionControlPC;
        private OrientationControl orientationControlOC;
        private Button buttonClear;
        private Button buttonCreate;
        private Button buttonClose;
        private NumericTextBox numericTextBoxLength;
        private TemporaryGraphic previewBox;


        private IContainer componentsB;
        private ErrorProvider errorProviderNumTextBoxValidation;

        public FrmCreateAluminiumProfile()
        {
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


        private static Transform GetRefCoordSysTransforms(Part part)
        {
            Transform refTrf = null;

            //Get Parent of part object and store in base type(ProjectObject) variable.
            ProjectObject poPartParent = part.Parent;

            if (poPartParent != null && poPartParent.Parent is IHasTransform)
            {
                IHasTransform parent = poPartParent.Parent as IHasTransform;
                refTrf = parent.Transform;
            }
            else
                refTrf = null;

            return refTrf;
        }

        private static void ApplyLocal(Part part, Vector3 position, Vector3 orientation)
        {
            Project.UndoContext.BeginUndoStep();
            try
            {
                //Checks if Part variable refers to the instance Part object
                if (part != null)
                {
                    // Save old matrix for the part 
                    Matrix4 oldGlobalMatrix = part.Transform.GlobalMatrix;

                    //Get reference coordinate system transform value
                    Transform refTrf = GetRefCoordSysTransforms(part);

                    //Create new Identity matrix and apply position argument to matrix translation and 
                    //orientation argument to the matrix EulerZYX
                    Matrix4 mat = Matrix4.Identity;
                    mat.Translation = position;
                    mat.EulerZYX = orientation;

                    part.Transform.GlobalMatrix = (refTrf == null) ? mat : (refTrf.GlobalMatrix * mat);

                    // New matrix for the part       
                    Matrix4 newGlobalMatrix = part.Transform.GlobalMatrix;

                    // Calculate difference for moving back bodies  
                    newGlobalMatrix.InvertRigid();
                    Matrix4 diffTrans = newGlobalMatrix * oldGlobalMatrix;

                    // Move all the bodies back
                    if (part.HasGeometry)
                    {
                        foreach (Body bd in part.Bodies)
                        {
                            bd.Transform.Matrix = diffTrans * bd.Transform.Matrix;
                        }
                    }
                    else
                    {
                        // CQ5356
                        part.Mesh.Transform(diffTrans);
                        part.Mesh.Rebuild();
                    }
                }
            }
            catch
            {
                Project.UndoContext.CancelUndoStep(CancelUndoStepType.Rollback);
            }
            finally
            {
                Project.UndoContext.EndUndoStep();
            }
        }


        private void frmCreateAluminiumProfile_Activate(object sender, EventArgs e)
        {
        }

        private void frmCreateAluminiumProfile_Desactivate(object sender, EventArgs e)
        {
            CloseTool();
        }

        //Enable the create button if the length values different from 0 and don't exceed the max value from the selected profile size
        private void size_TextChanged(object sender, EventArgs e)
        {
            switch(comboBoxReference.SelectedItem)
                {
                    case "20 x 20":
                    var bl2 = (numericTextBoxLength.Value);
                    if (bl2 != 0 & bl2 < 3001 & bl2 > 0)
                        {
                            buttonCreate.Enabled = true;
                        }
                        else
                        {
                            buttonCreate.Enabled = false;
                        }
                    break;
                case "30 x 30":
                    var bl3 = (numericTextBoxLength.Value);
                    if (bl3 != 0 & bl3 < 5601 & bl3 > 0)
                        {
                            buttonCreate.Enabled = true;
                        }
                        else
                        {
                            buttonCreate.Enabled = false;
                        }
                    break;
                case "40 x 40":
                    var bl4 = (numericTextBoxLength.Value);
                    if (bl4 != 0 & bl4 < 6001 & bl4 > 0)
                        {
                            buttonCreate.Enabled = true;
                        }
                        else
                        {
                            buttonCreate.Enabled = false;
                        }
                    break;
                case "50 x 50":
                    var bl5 = (numericTextBoxLength.Value);
                    if (bl5 != 0 & bl5 < 6001 & bl5 > 0)
                        {
                            buttonCreate.Enabled = true;
                        }
                        else
                        {
                            buttonCreate.Enabled = false;
                        }
                    break;
                case "60 x 60":
                    var bl6 = (numericTextBoxLength.Value);
                    if (bl6 != 0 & bl6 < 6001 & bl6 > 0)
                        {
                            buttonCreate.Enabled = true;
                        }
                        else
                        {
                            buttonCreate.Enabled = false;
                        }
                    break;
                case "80 x 80":
                    var bl8 = (numericTextBoxLength.Value);
                    if (bl8 != 0 & bl8 < 6001 & bl8 > 0)
                        {
                            buttonCreate.Enabled = true;
                        }
                        else
                        {
                            buttonCreate.Enabled = false;
                        }
                    break;
                case "90 x 90":
                    var bl9 = (numericTextBoxLength.Value);
                    if (bl9 != 0 & bl9 < 5601 & bl9 > 0)
                    {
                        buttonCreate.Enabled = true;
                    }
                    else
                    {
                        buttonCreate.Enabled = false;
                    }
                    break;
                default:
                    break;
                }


                

            ValidateToolControl();
        }

        private void btn_clear_clicked(object sender, EventArgs e)
        {
            CleanValues();
        }

        private void CleanValues()
        {
            positionControlPC.Value = new Vector3(0, 0, 0);
            orientationControlOC.Value = new Vector3(0, 0, 0);
            numericTextBoxLength.Value = 0;
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
                    CreateAluminiumProfile(preview: false);
                }
                finally
                {
                    Project.UndoContext.EndUndoStep();
                }
            }
        }


        private void CreateAluminiumProfile(bool preview)
        {
            #region try
            Project.UndoContext.BeginUndoStep("CreateAluminiumProfile");
            try
            {
                //Station stn = Station.ActiveStation;
                //if (stn == null) return;

                string WorkingDirectory;
                string DirectoryPath1 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string DirectoryPath2 = "\\ABB\\DistributionPackages2\\PuimesAddin-4.0\\RobotStudio\\Add-In\\Library\\AlumProfiles\\";
                string currentDirectoryPath = DirectoryPath1 + DirectoryPath2;

                if (Directory.Exists(currentDirectoryPath))
                {
                    WorkingDirectory = currentDirectoryPath;
                }
                else
                {
                    WorkingDirectory = "C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-4.0\\RobotStudio\\Add-In\\Library\\AlumProfiles\\";
                }

                double Xvalue = 0;
                double Yvalue = 0;
                double Zvalue = numericTextBoxLength.Value;

                switch (comboBoxReference.SelectedItem)
                {
                    case "20 x 20":
                        Xvalue = 20; Yvalue = 20;
                        break;
                    case "30 x 30":
                        Xvalue = 30; Yvalue = 30;
                        break;
                    case "40 x 40":
                        Xvalue = 40; Yvalue = 40;
                        break;
                    case "50 x 50":
                        Xvalue = 50; Yvalue = 50;
                        break;
                    case "60 x 60":
                        Xvalue = 60; Yvalue = 60;
                        break;
                    case "80 x 80":
                        Xvalue = 80; Yvalue = 80;
                        break;
                    case "90 x 90":
                        Xvalue = 90; Yvalue = 90;
                        break;
                    default:
                        break;
                }

                
                Vector3 valueCorner = new Vector3 (positionControlPC.Value.x - ((Xvalue/2)/1000), positionControlPC.Value.y - ((Yvalue / 2) / 1000), positionControlPC.Value.z);
                Vector3 value = new Vector3 (positionControlPC.Value.x, positionControlPC.Value.y, positionControlPC.Value.z);
                Vector3 value2 = orientationControlOC.Value;
                Vector3 projection = new Vector3(0.0, 0.0, Zvalue / 1000);
                Matrix4 PosOrientCorner = new Matrix4(valueCorner, value2);
                Matrix4 PosOrient = new Matrix4(value, value2);

                Vector3 size = new Vector3(Xvalue / 1000, Yvalue / 1000, Zvalue / 1000);

                Station station = Project.ActiveProject as Station;
                if (station == null) return;

                if (preview)
                {
                    previewBox = station.TemporaryGraphics.DrawBox(PosOrient, size, Color.FromArgb(128, Color.Gray)); 
                    return;
                }

                //
                // Import the Profile library
                //

                GraphicComponentLibrary ProfileLib = new GraphicComponentLibrary(); //First part
                GraphicComponentLibrary ProfileLib2 = new GraphicComponentLibrary(); //Second part
                GraphicComponentLibrary ProfileLib3 = new GraphicComponentLibrary(); //Third part

                string sProfileName="";
                int nProfiles = 1;

                switch (comboBoxReference.SelectedItem)
                {
                    case "20 x 20":
                        ProfileLib = GraphicComponentLibrary.Load( WorkingDirectory + "BaseProfile_20x20.rslib", true, null, true);
                        sProfileName = "Aluminum profile 20x20";
                        nProfiles = 1;
                        break;
                    case "30 x 30":
                        ProfileLib = GraphicComponentLibrary.Load(WorkingDirectory + "BaseProfile_30x30A.rslib", true, null, true);
                        ProfileLib2 = GraphicComponentLibrary.Load(WorkingDirectory + "BaseProfile_30x30B.rslib", true, null, true);
                        sProfileName = "Aluminum profile 30x30";
                        nProfiles = 2;
                        break;
                    case "40 x 40":
                        ProfileLib = GraphicComponentLibrary.Load(WorkingDirectory + "BaseProfile_40x40A.rslib", true, null, true);
                        ProfileLib2 = GraphicComponentLibrary.Load(WorkingDirectory + "BaseProfile_40x40B.rslib", true, null, true);
                        sProfileName = "Aluminum profile 40x40";
                        nProfiles = 2;
                        break;
                    case "50 x 50":
                        ProfileLib = GraphicComponentLibrary.Load(WorkingDirectory + "BaseProfile_50x50A.rslib", true, null, true);
                        ProfileLib2 = GraphicComponentLibrary.Load(WorkingDirectory + "BaseProfile_50x50B.rslib", true, null, true);
                        sProfileName = "Aluminum profile 50x50";
                        nProfiles = 2;
                        break;
                    case "60 x 60":
                        ProfileLib = GraphicComponentLibrary.Load(WorkingDirectory + "BaseProfile_60x60A.rslib", true, null, true);
                        ProfileLib2 = GraphicComponentLibrary.Load(WorkingDirectory + "BaseProfile_60x60B.rslib", true, null, true);
                        ProfileLib3 = GraphicComponentLibrary.Load(WorkingDirectory + "BaseProfile_60x60C.rslib", true, null, true);
                        sProfileName = "Aluminum profile 60x60";
                        nProfiles = 3;
                        break;
                    case "80 x 80":
                        ProfileLib = GraphicComponentLibrary.Load(WorkingDirectory + "BaseProfile_80x80A.rslib", true, null, true);
                        ProfileLib2 = GraphicComponentLibrary.Load(WorkingDirectory + "BaseProfile_80x80B.rslib", true, null, true);
                        ProfileLib3 = GraphicComponentLibrary.Load(WorkingDirectory + "BaseProfile_80x80C.rslib", true, null, true);
                        sProfileName = "Aluminum profile 80x80";
                        nProfiles = 3;
                        break;
                    case "90 x 90":
                        ProfileLib = GraphicComponentLibrary.Load(WorkingDirectory + "BaseProfile_90x90A.rslib", true, null, true);
                        ProfileLib2 = GraphicComponentLibrary.Load(WorkingDirectory + "BaseProfile_90x90B.rslib", true, null, true);
                        ProfileLib3 = GraphicComponentLibrary.Load(WorkingDirectory + "BaseProfile_90x90C.rslib", true, null, true);
                        sProfileName = "Aluminum profile 90x90";
                        nProfiles = 3;
                        break;
                    default:
                        break;
                }

                GraphicComponentLibrary sProfile = new GraphicComponentLibrary();

                Part part = new Part(); //First step
                Part part2 = new Part(); //Second step
                Part part3 = new Part(); //Final step
                Part part4 = new Part(); //First cut
                Part part5 = new Part(); //Second cut
                
                for (int i = 0; i < 3; i++)
                {
                    switch (i)
                    {
                        case 0:
                            sProfile = ProfileLib;
                            if (nProfiles == 1) i = 3; 
                            break;
                        case 1:
                            sProfile = ProfileLib2;
                            if (nProfiles == 2) i = 3;
                            break;
                        case 2:
                            sProfile = ProfileLib3;
                            if (nProfiles == 3) i = 3;
                            break;
                        default:
                            break;
                    }
                    
                    Part myPart = sProfile.RootComponent.CopyInstance() as Part;
                    myPart.DisconnectFromLibrary();
                    sProfile.Close();

                    Wire MyWire2 = null;

                    foreach (var item in myPart.Bodies)
                    {
                        Body MyBody = item as Body;
                        if (MyBody != null)
                        {
                            MyWire2 = MyBody.Shells[0].Wires[0];
                        }
                    }

                    Wire alongWire = null;

                    SweepOptions sweepOptions = new SweepOptions();
                    sweepOptions.MakeSolid = true;
                    
                    Body[] bodyarray = Body.Extrude(MyWire2, projection, alongWire, sweepOptions);
                    if (bodyarray.Length != 0)
                    {
                        //Fist step
                        foreach (Body bbody in bodyarray)
                        {
                            bbody.Name = "Body1";
                            part.Bodies.Add(bbody);

                            //Copy the body 3 times and rotate it to make a square.
                            Body bbodycopy2 = (Body)bbody.Copy();
                            bbodycopy2.Name = "Body2";
                            bbodycopy2.Transform.RZ = Globals.DegToRad(90);
                            part.Bodies.Add(bbodycopy2);

                            Body bbodycopy3 = (Body)bbody.Copy();
                            bbodycopy3.Name = "Body3";
                            bbodycopy3.Transform.RZ = Globals.DegToRad(180);
                            part.Bodies.Add(bbodycopy3);

                            Body bbodycopy4 = (Body)bbody.Copy();
                            bbodycopy4.Name = "Body4";
                            bbodycopy4.Transform.RZ = Globals.DegToRad(270);
                            part.Bodies.Add(bbodycopy4);

                            // Second step
                            // Join all the four rotated parts in one
                            Body[] b1 = bbody.Join(bbodycopy2, false);
                            foreach (Body b11 in b1)
                            {
                                b11.Name = "Body1";
                                part2.Bodies.Add(b11);
                            }

                            Body[] b2 = bbodycopy3.Join(bbodycopy4, false);
                            foreach (Body b12 in b2)
                            {
                                b12.Name = "Body";
                                part2.Bodies.Add(b12);
                            }
                            
                            Body[] b7 = b1[0].Join(b2[0], false);
                            foreach (Body b in b7)
                            {
                                b.Name = "Body";
                                b.Color = Color.FromArgb(224, 224, 224);
                                part3.Bodies.Add(b);
                            }

                        }

                    }

                }

                switch (nProfiles)
                {
                    case 1:

                        part3.Name = sProfileName + "_h" + Zvalue;
                        station.GraphicComponents.Add(part3);

                        Vector3 vecPosi = new Vector3(-(Xvalue / 2) / 1000, -(Yvalue / 2) / 1000, 0);
                        Vector3 vecOri = new Vector3(0, 0, 0);

                        ApplyLocal(part3, vecPosi, vecOri);

                        part3.Transform.X = positionControlPC.Value.x;
                        part3.Transform.Y = positionControlPC.Value.y;
                        part3.Transform.Z = positionControlPC.Value.z;

                        part3.Transform.RX = orientationControlOC.Value.x;
                        part3.Transform.RY = orientationControlOC.Value.y;
                        part3.Transform.RZ = orientationControlOC.Value.z;

                        station.GraphicComponents.Remove(part);
                        station.GraphicComponents.Remove(part2);
                        
                        CleanValues();
                        break;

                    case 2:
                        // cut bodies to generate the final body
                        Body[] b8 = part3.Bodies[0].Cut2(part3.Bodies[1]);
                        foreach (Body b in b8)
                        {
                            b.Name = "Body";
                            b.Color = Color.FromArgb(224, 224, 224);
                            part4.Bodies.Add(b);
                        }

                
                        part4.Name = sProfileName + "_h" + Zvalue;
                        station.GraphicComponents.Add(part4);

                        Vector3 vecPosi2 = new Vector3(-(Xvalue / 2) / 1000, -(Yvalue / 2) / 1000, 0);
                        Vector3 vecOri2 = new Vector3(0, 0, 0);

                        ApplyLocal(part4, vecPosi2, vecOri2);

                        part4.Transform.X = positionControlPC.Value.x;
                        part4.Transform.Y = positionControlPC.Value.y;
                        part4.Transform.Z = positionControlPC.Value.z;

                        part4.Transform.RX = orientationControlOC.Value.x;
                        part4.Transform.RY = orientationControlOC.Value.y;
                        part4.Transform.RZ = orientationControlOC.Value.z;

                        station.GraphicComponents.Remove(part);
                        station.GraphicComponents.Remove(part2);
                        station.GraphicComponents.Remove(part3);

                        CleanValues();
                        break;

                    case 3:
                        // cut bodies to generate the final body
                        Body[] b9 = part3.Bodies[0].Cut2(part3.Bodies[1]);
                        foreach (Body b in b9)
                        {
                            b.Name = "Body";
                            b.Color = Color.FromArgb(224, 224, 224);
                            part4.Bodies.Add(b);
                        }

                        Body[] b10 = part4.Bodies[0].Cut2(part3.Bodies[2]);
                        foreach (Body b in b10)
                        {
                            b.Name = "Body";
                            b.Color = Color.FromArgb(224, 224, 224);
                            part5.Bodies.Add(b); 
                        }

                        part5.Name = sProfileName + "_h" + Zvalue;
                        station.GraphicComponents.Add(part5);

                        Vector3 vecPosi3 = new Vector3(-(Xvalue / 2) / 1000, -(Yvalue / 2) / 1000, 0);
                        Vector3 vecOri3 = new Vector3(0, 0, 0);

                        ApplyLocal(part5, vecPosi3, vecOri3);

                        part5.Transform.X = positionControlPC.Value.x;
                        part5.Transform.Y = positionControlPC.Value.y;
                        part5.Transform.Z = positionControlPC.Value.z;

                        part5.Transform.RX = orientationControlOC.Value.x;
                        part5.Transform.RY = orientationControlOC.Value.y;
                        part5.Transform.RZ = orientationControlOC.Value.z;

                        //part4.Transform.Matrix = PosOrientCorner;

                        station.GraphicComponents.Remove(part);
                        station.GraphicComponents.Remove(part2);
                        station.GraphicComponents.Remove(part3);
                        station.GraphicComponents.Remove(part4);

                        CleanValues();
                        break;

                    default:
                        break;
                }

            }// End try

            catch (Exception execption)
            {
                Project.UndoContext.CancelUndoStep(CancelUndoStepType.Rollback);
                Logger.AddMessage(new LogMessage(execption.Message.ToString()));
                throw;
            }
            finally
            {
                Project.UndoContext.EndUndoStep();
            }
            #endregion try
        }

        protected override bool ValidateToolControl()
        {
            double value = numericTextBoxLength.Value;
            bool flag = value > 0.0 && value <= 10000.0;
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
                CreateAluminiumProfile(preview: true);
            }
        }

        private void CreateAluminiumProfile_Desactivate(object sender, EventArgs e)
        {
            UpdatePreview(valid: false);
        }



    





        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            int tw_width = UIEnvironment.Windows["ObjectBrowser"].Control.Size.Width - 25;

            this.componentsB = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager componentResourceManager = new System.ComponentModel.ComponentResourceManager(typeof(Puime_s_Addin.FrmCreateAluminiumProfile));

            this.pictureBoxModel = new System.Windows.Forms.PictureBox();
            this.labelReference = new System.Windows.Forms.Label();
            this.labelDetails = new System.Windows.Forms.Label();
            this.comboBoxReference = new System.Windows.Forms.ComboBox();
            this.positionControlPC = new ABB.Robotics.RobotStudio.Stations.Forms.PositionControl();
            this.orientationControlOC = new ABB.Robotics.RobotStudio.Stations.Forms.OrientationControl();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.numericTextBoxLength = new ABB.Robotics.RobotStudio.Stations.Forms.NumericTextBox();

            this.errorProviderNumTextBoxValidation = new System.Windows.Forms.ErrorProvider(this.componentsB);
            //((System.ComponentModel.ISupportInitialize)this.pictureBoxModel).BeginInit();
            base.SuspendLayout();


            // 
            // pictureBoxModel
            // 
            componentResourceManager.ApplyResources(this.pictureBoxModel, "pictureBox");
            this.pictureBoxModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxModel.Image = global::PuimesAddin.Properties.Resources.Alum_Prof_20_65;
            this.pictureBoxModel.Location = new System.Drawing.Point(8, 8);
            this.pictureBoxModel.Name = "pictureBoxModel";
            this.pictureBoxModel.Size = new System.Drawing.Size(65, 65);
            //this.pictureBoxModel.TabIndex = 0;
            this.pictureBoxModel.TabStop = false;
            // 
            // labelReference
            // 
            componentResourceManager.ApplyResources(this.labelReference, "labelReference");
            this.labelReference.Location = new System.Drawing.Point(79, 30);
            this.labelReference.Name = "labelReference";
            this.labelReference.Size = new System.Drawing.Size(100, 15);
            this.labelReference.TabIndex = 1;
            this.labelReference.Text = "Model";
            // 
            // comboBoxReference
            // 
            componentResourceManager.ApplyResources(this.comboBoxReference, "comboBoxReference");
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
            this.comboBoxReference.Size = new System.Drawing.Size(tw_width - 65, 21);
            this.comboBoxReference.Sorted = true;
            this.comboBoxReference.TabIndex = 0;
            this.comboBoxReference.SelectedIndex = 0;
            this.comboBoxReference.SelectedIndexChanged += new System.EventHandler(this.comboBoxReference_SelectedIndexChanged);

            // 
            // positionControlPC
            // 
            componentResourceManager.ApplyResources(this.positionControlPC, "positionControlPC");
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
            this.positionControlPC.Size = new System.Drawing.Size(tw_width +7, 34);
            this.positionControlPC.TabIndex = 1;
            this.positionControlPC.Text = "Position Control";
            this.positionControlPC.VerticalLayout = false;
            // 
            // orientationControlOC
            // 
            componentResourceManager.ApplyResources(this.orientationControlOC, "orientationControlOC");
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
            this.orientationControlOC.Size = new System.Drawing.Size(tw_width + 7, 34);
            this.orientationControlOC.TabIndex = 2;
            this.orientationControlOC.Text = "positionControl1";
            this.orientationControlOC.VerticalLayout = false;
            // 
            // numericTextBoxLength
            //
            componentResourceManager.ApplyResources(this.numericTextBoxLength, "numericTextBoxLength");
            this.numericTextBoxLength.AccessibleName = "Length (mm)";
            this.numericTextBoxLength.ErrorProviderControl = null;
            this.numericTextBoxLength.ExpressionErrorString = "Bad Expression";
            this.numericTextBoxLength.LabelText = "Length (mm)";
            this.numericTextBoxLength.Location = new System.Drawing.Point(8, 165);
            this.numericTextBoxLength.MaxValue = 1000000.0;
            this.numericTextBoxLength.MaxValueErrorString = "Value exceeds maximum";
            this.numericTextBoxLength.MinValue = 0;
            this.numericTextBoxLength.MinValueErrorString = "Value is below minimum";
            this.numericTextBoxLength.Name = "numericTextBoxLength";
            this.numericTextBoxLength.Quantity = ABB.Robotics.RobotStudio.BuiltinQuantity.None;
            this.numericTextBoxLength.ReadOnly = false;
            this.numericTextBoxLength.ShowLabel = true;
            this.numericTextBoxLength.Size = new System.Drawing.Size(tw_width + 7, 34);
            this.numericTextBoxLength.StepSize = 1D;
            this.numericTextBoxLength.TabIndex = 3;
            this.numericTextBoxLength.Text = "numericTextBox1";
            this.numericTextBoxLength.UserEdited = false;
            this.numericTextBoxLength.Value = 0D;
            this.numericTextBoxLength.ValueChanged += new System.EventHandler(this.size_TextChanged);
            // 
            // labelDetails
            // 
            componentResourceManager.ApplyResources(this.labelDetails, "labelDetails");
            this.labelDetails.Location = new System.Drawing.Point(8, 245);
            this.labelDetails.Name = "labelDetails";
            this.labelDetails.Size = new System.Drawing.Size(300, 15);
            //this.labelDetails.TabIndex = 1;
            this.labelDetails.Text = "Aluminum profile 20x20 - Max length = 3000 mm";
            // 
            // buttonClear
            // 
            componentResourceManager.ApplyResources(this.buttonClear, "buttonClear");
            this.buttonClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClear.Location = new System.Drawing.Point(tw_width - 158, 295);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(53, 25);
            this.buttonClear.TabIndex = 6;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.btn_clear_clicked);
            // 
            // buttonCreate
            // 
            componentResourceManager.ApplyResources(this.buttonCreate, "buttonCreate");
            this.buttonCreate.Enabled = false;
            this.buttonCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCreate.Location = new System.Drawing.Point(tw_width - 98, 295);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(53, 25);
            this.buttonCreate.TabIndex = 7;
            this.buttonCreate.Text = "Create";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.btn_create_clicked);
            // 
            // buttonClose
            // 
            componentResourceManager.ApplyResources(this.buttonClose, "buttonClose");
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Location = new System.Drawing.Point(tw_width - 38, 295);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(53, 25);
            this.buttonClose.TabIndex = 8;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.btn_close_clicked);

            // 
            // FrmCreateAluminiumProfile
            // 
            componentResourceManager.ApplyResources(this, "$this");
            //base.Padding = new Padding(DpiSupport.Adjust(2));
            //base.FlowDirection = FlowDirection.RightToLeft;
            base.AutoScroll = true;
            base.AutoSize = true;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //base.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            base.AdjustableHeight = true;
            base.Caption = "Create Aluminum Profile";
            base.Controls.Add(this.pictureBoxModel);
            base.Controls.Add(this.labelReference);
            base.Controls.Add(this.comboBoxReference);
            base.Controls.Add(this.positionControlPC);
            base.Controls.Add(this.orientationControlOC);
            base.Controls.Add(this.buttonClear);
            base.Controls.Add(this.buttonCreate);
            base.Controls.Add(this.buttonClose);
            base.Controls.Add(this.numericTextBoxLength);
            base.Controls.Add(this.labelDetails);
            base.Name = "FrmCreateAluminumProfile";
            //base.Size = new System.Drawing.Size(330, 339);
            //base.Size = new System.Drawing.Size(tw_width, 600);
            //base.ShowClearButton = true;
            //base.ShowCloseButton = true;
            //base.ShowCreateButton = true;
            base.Apply += new System.EventHandler(this.btn_create_clicked);
            base.Deactivate += new System.EventHandler(this.CreateAluminiumProfile_Desactivate);
            ((System.ComponentModel.ISupportInitialize)this.errorProviderNumTextBoxValidation).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxModel)).EndInit();
            //pictureBoxModel.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();


        }

        #endregion

        private void comboBoxReference_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxReference.SelectedItem)
            {
                case "20 x 20":
                    pictureBoxModel.Image = Resources.Alum_Prof_20_65;
                    labelDetails.Text = "Aluminum profile 20x20 - Max length = 3000 mm";
                    break;
                case "30 x 30":
                    pictureBoxModel.Image = Resources.Alum_Prof_30_65;
                    labelDetails.Text = "Aluminum profile 30x30 - Max length = 5600 mm";
                    break;
                case "40 x 40":
                    pictureBoxModel.Image = Resources.Alum_Prof_40_65;
                    labelDetails.Text = "Aluminum profile 40x40 - Max length = 6000 mm";
                    break;
                case "50 x 50":
                    pictureBoxModel.Image = Resources.Alum_Prof_50_65;
                    labelDetails.Text = "Aluminum profile 50x50 - Max length = 6000 mm";
                    break;
                case "60 x 60":
                    pictureBoxModel.Image = Resources.Alum_Prof_60_65;
                    labelDetails.Text = "Aluminum profile 60x60 - Max length = 6000 mm";
                    break;
                case "80 x 80":
                    pictureBoxModel.Image = Resources.Alum_Prof_80_65;
                    labelDetails.Text = "Aluminum profile 80x80 - Max length = 6000 mm";
                    break;
                case "90 x 90":
                    pictureBoxModel.Image = Resources.Alum_Prof_90_65;
                    labelDetails.Text = "Aluminum profile 90x90 - Max length = 5600 mm";
                    break;
                default:
                    break;
            }
        }
    }
}
