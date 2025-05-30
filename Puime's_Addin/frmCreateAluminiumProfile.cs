﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ABB.Robotics.Controllers.RapidDomain;
using ABB.Robotics.Math;
using ABB.Robotics.RobotStudio;
using ABB.Robotics.RobotStudio.Environment;
using ABB.Robotics.RobotStudio.Stations;
using ABB.Robotics.RobotStudio.Stations.Forms;
using PuimesAddin.Properties;
using static System.Collections.Specialized.BitVector32;
using System.ComponentModel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Diagnostics;
using System.Collections.Generic;

namespace Puime_s_Addin
{
    public partial class FrmCreateAluminiumProfile : ToolControlBase
    {
        private Container components;
        private PictureBox pictureBoxModel;
        private Label labelReference;
        private ComboBox comboBoxReference;
        private PositionControl positionControlPC;
        private OrientationControl orientationControlOC;
        private Button buttonClear;
        private Button buttonCreate;
        private Button buttonClose;
        private NumericTextBox numericTextBoxLength;
        private TemporaryGraphic previewBox;

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


        // Modify Local Origin - SDK Help->Code Sampes->Geometry->Modifying Local Origin

        //  Implement with ApplyLocal

        //private static List<Part> GetCADFromStation()
        //{
        //    List<Part> partCollection = new List<Part>();
        //    Station station = Project.ActiveProject as Station;
        //    foreach (GraphicComponent component in station.GraphicComponents)
        //    {
        //        if (component is Part)
        //        {
        //            partCollection.Add((Part)component);
        //        }
        //    }
        //    return partCollection;
        //}

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

        //Enable the create button if the lenght values diferent from 0
        private void size_TextChanged(object sender, EventArgs e)
        {
            var bl = (numericTextBoxLength.Value != 0);
            buttonCreate.Enabled = bl;

            //CreateAluminiumProfile(preview: true);

            ValidateToolControl();
        }

        private void btn_clear_clicked(object sender, EventArgs e)
        {
            CleanValues();
        }

        private void CleanValues()
        {
            //comboBoxReference.SelectedIndex = 0;
            positionControlPC.Value = new Vector3(0, 0, 0);
            orientationControlOC.Value = new Vector3(0, 0, 0);
            numericTextBoxLength.Value = 0;
            //numericTextBoxWidth.Value = 0;
            //numericTextBoxHeight.Value = 0;
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
                    //CreateABBBox(preview: false);
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

                //Vector3 value3 = new Vector3(positionControlPC.Value.x - (Zvalue / 2), positionControlPC.Value.y, positionControlPC.Value.z);

                Vector3 size = new Vector3(Xvalue / 1000, Yvalue / 1000, Zvalue / 1000);


                Station station = Project.ActiveProject as Station;
                if (station == null) return;

                
                // TODO: Ver una forma de crear el preview en el medio del tubo. Ahora lo crea en la esquina.
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
                        ProfileLib = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\AlumProfiles\\BaseProfile_20x20.rslib", true, null, true);
                        sProfileName = "Aluminum profile 20x20";
                        nProfiles = 1;
                        //Matrix4 PosOrient = new Matrix4(value.x +10,value.y +10,value.z, value2);
                        break;
                    case "30 x 30":
                        ProfileLib = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\AlumProfiles\\BaseProfile_30x30A.rslib", true, null, true);
                        ProfileLib2 = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\AlumProfiles\\BaseProfile_30x30B.rslib", true, null, true);
                        sProfileName = "Aluminum profile 30x30";
                        nProfiles = 2;
                        break;
                    case "40 x 40":
                        ProfileLib = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\AlumProfiles\\BaseProfile_40x40A.rslib", true, null, true);
                        ProfileLib2 = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\AlumProfiles\\BaseProfile_40x40B.rslib", true, null, true);
                        sProfileName = "Aluminum profile 40x40";
                        nProfiles = 2;
                        break;
                    case "50 x 50":
                        ProfileLib = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\AlumProfiles\\BaseProfile_50x50A.rslib", true, null, true);
                        ProfileLib2 = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\AlumProfiles\\BaseProfile_50x50B.rslib", true, null, true);
                        sProfileName = "Aluminum profile 50x50";
                        nProfiles = 2;
                        break;
                    case "60 x 60":
                        ProfileLib = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\AlumProfiles\\BaseProfile_60x60A.rslib", true, null, true);
                        ProfileLib2 = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\AlumProfiles\\BaseProfile_60x60B.rslib", true, null, true);
                        ProfileLib3 = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\AlumProfiles\\BaseProfile_60x60C.rslib", true, null, true);
                        sProfileName = "Aluminum profile 60x60";
                        nProfiles = 3;
                        break;
                    case "80 x 80":
                        ProfileLib = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\AlumProfiles\\BaseProfile_80x80A.rslib", true, null, true);
                        ProfileLib2 = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\AlumProfiles\\BaseProfile_80x80B.rslib", true, null, true);
                        ProfileLib3 = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\AlumProfiles\\BaseProfile_80x80C.rslib", true, null, true);
                        sProfileName = "Aluminum profile 80x80";
                        nProfiles = 3;
                        break;
                    case "90 x 90":
                        ProfileLib = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\AlumProfiles\\BaseProfile_90x90A.rslib", true, null, true);
                        ProfileLib2 = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\AlumProfiles\\BaseProfile_90x90B.rslib", true, null, true);
                        ProfileLib3 = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\AlumProfiles\\BaseProfile_90x90C.rslib", true, null, true);
                        sProfileName = "Aluminum profile 90x90";
                        nProfiles = 3;
                        break;
                    default:
                        break;
                }


                GraphicComponentLibrary sProfile = new GraphicComponentLibrary();

                string st_count;

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
                            st_count = "A";
                            if (nProfiles == 1) i = 3; 
                            break;
                        case 1:
                            sProfile = ProfileLib2;
                            st_count = "B";
                            if (nProfiles == 2) i = 3;
                            break;
                        case 2:
                            sProfile = ProfileLib3;
                            st_count = "C";
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

                        //CleanValues();

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

                        //part4.Transform.Matrix = PosOrientCorner;

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
            //double value2 = numericTextBoxWidth.Value;
            //double value3 = numericTextBoxHeight.Value;
            bool flag = value > 0.0 && value <= 100000000.0;
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
                //CreateABBBox(preview: true);
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
            //components = new System.ComponentModel.Container();
            pictureBoxModel = new System.Windows.Forms.PictureBox();
            labelReference = new System.Windows.Forms.Label();
            comboBoxReference = new System.Windows.Forms.ComboBox();
            positionControlPC = new ABB.Robotics.RobotStudio.Stations.Forms.PositionControl();
            orientationControlOC = new ABB.Robotics.RobotStudio.Stations.Forms.OrientationControl();
            buttonClear = new System.Windows.Forms.Button();
            buttonCreate = new System.Windows.Forms.Button();
            buttonClose = new System.Windows.Forms.Button();
            numericTextBoxLength = new ABB.Robotics.RobotStudio.Stations.Forms.NumericTextBox();
            ((System.ComponentModel.ISupportInitialize)(pictureBoxModel)).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxModel
            // 
            pictureBoxModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pictureBoxModel.Image = global::PuimesAddin.Properties.Resources.Alum_Prof_20_65;
            pictureBoxModel.Location = new System.Drawing.Point(8, 8);
            pictureBoxModel.Name = "pictureBoxModel";
            pictureBoxModel.Size = new System.Drawing.Size(65, 65);
            pictureBoxModel.TabIndex = 0;
            pictureBoxModel.TabStop = false;
            // 
            // labelReference
            // 
            labelReference.Location = new System.Drawing.Point(79, 30);
            labelReference.Name = "labelReference";
            labelReference.Size = new System.Drawing.Size(100, 15);
            labelReference.TabIndex = 1;
            labelReference.Text = "Model";
            // 
            // comboBoxReference
            // 
            comboBoxReference.Items.AddRange(new object[] {
            "20 x 20",
            "30 x 30",
            "40 x 40",
            "50 x 50",
            "60 x 60",
            "80 x 80",
            "90 x 90",});
            comboBoxReference.Location = new System.Drawing.Point(80, 52);
            comboBoxReference.Name = "comboBoxReference";
            comboBoxReference.Size = new System.Drawing.Size(133, 21);
            comboBoxReference.TabIndex = 0;
            comboBoxReference.SelectedIndex = 0;
            comboBoxReference.SelectedIndexChanged += new System.EventHandler(comboBoxReference_SelectedIndexChanged);
            // 
            // positionControlPC
            // 
            positionControlPC.AccessibleName = "Corner Point";
            positionControlPC.ErrorProviderControl = null;
            positionControlPC.ExpressionErrorString = "Bad Expression";
            positionControlPC.LabelQuantity = ABB.Robotics.RobotStudio.BuiltinQuantity.Length;
            positionControlPC.LabelText = "Corner Point";
            positionControlPC.Location = new System.Drawing.Point(8, 85);
            positionControlPC.MaxValueErrorString = "Value exceeds maximum";
            positionControlPC.MinValueErrorString = "Value is below minimum";
            positionControlPC.Name = "positionControlPC";
            positionControlPC.NumTextBoxes = 3;
            positionControlPC.ReadOnly = false;
            positionControlPC.RefCoordSys = null;
            positionControlPC.ShowLabel = true;
            positionControlPC.Size = new System.Drawing.Size(200, 34);
            positionControlPC.TabIndex = 1;
            positionControlPC.Text = "Position Control";
            positionControlPC.VerticalLayout = false;
            // 
            // orientationControlOC
            // 
            orientationControlOC.AccessibleName = "Orientation";
            orientationControlOC.EnableModeChange = true;
            orientationControlOC.ErrorProviderControl = null;
            orientationControlOC.ExpressionErrorString = "Bad Expression";
            orientationControlOC.LabelQuantity = ABB.Robotics.RobotStudio.BuiltinQuantity.Angle;
            orientationControlOC.LabelText = "Orientation";
            orientationControlOC.Location = new System.Drawing.Point(8, 125);
            orientationControlOC.MaxValueErrorString = "Value exceeds maximum";
            orientationControlOC.MinValueErrorString = "Value is below minimum";
            orientationControlOC.Name = "orientationControlOC";
            orientationControlOC.NumTextBoxes = 3;
            orientationControlOC.QuaternionMode = false;
            orientationControlOC.ReadOnly = false;
            orientationControlOC.ShowLabel = true;
            orientationControlOC.Size = new System.Drawing.Size(200, 34);
            orientationControlOC.TabIndex = 2;
            orientationControlOC.Text = "positionControl1";
            orientationControlOC.VerticalLayout = false;
            // 
            // buttonClear
            // 
            buttonClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonClear.Location = new System.Drawing.Point(40, 295);
            buttonClear.Name = "buttonClear";
            buttonClear.Size = new System.Drawing.Size(53, 25);
            buttonClear.TabIndex = 6;
            buttonClear.Text = "Clear";
            buttonClear.UseVisualStyleBackColor = true;
            buttonClear.Click += new EventHandler(btn_clear_clicked);
            // 
            // buttonCreate
            // 
            buttonCreate.Enabled = false;
            buttonCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonCreate.Location = new System.Drawing.Point(100, 295);
            buttonCreate.Name = "buttonCreate";
            buttonCreate.Size = new System.Drawing.Size(53, 25);
            buttonCreate.TabIndex = 7;
            buttonCreate.Text = "Create";
            buttonCreate.UseVisualStyleBackColor = true;
            buttonCreate.Click += new EventHandler(btn_create_clicked);
            // 
            // buttonClose
            // 
            buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonClose.Location = new System.Drawing.Point(160, 295);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new System.Drawing.Size(53, 25);
            buttonClose.TabIndex = 8;
            buttonClose.Text = "Close";
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.Click += new EventHandler(btn_close_clicked);
            // 
            // numericTextBoxLength
            // 
            numericTextBoxLength.AccessibleName = "Length (mm)";
            numericTextBoxLength.ErrorProviderControl = null;
            numericTextBoxLength.ExpressionErrorString = "Bad Expression";
            numericTextBoxLength.LabelText = "Length (mm)";
            numericTextBoxLength.Location = new System.Drawing.Point(8, 165);
            numericTextBoxLength.MaxValue = 1000000000D;
            numericTextBoxLength.MaxValueErrorString = "Value exceeds maximum";
            numericTextBoxLength.MinValue = -1000000000D;
            numericTextBoxLength.MinValueErrorString = "Value is below minimum";
            numericTextBoxLength.Name = "numericTextBoxLength";
            numericTextBoxLength.Quantity = ABB.Robotics.RobotStudio.BuiltinQuantity.None;
            numericTextBoxLength.ReadOnly = false;
            numericTextBoxLength.ShowLabel = true;
            numericTextBoxLength.Size = new System.Drawing.Size(200, 34);
            numericTextBoxLength.StepSize = 1D;
            numericTextBoxLength.TabIndex = 3;
            numericTextBoxLength.Text = "numericTextBox1";
            numericTextBoxLength.UserEdited = false;
            numericTextBoxLength.Value = 0D;
            numericTextBoxLength.ValueChanged += new EventHandler(size_TextChanged);
            // 
            // frmCreateAluminiumProfile
            // 
            AdjustableHeight = true;
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoScroll = true;
            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            Caption = "Create Aluminum Profile";
            Controls.Add(pictureBoxModel);
            Controls.Add(labelReference);
            Controls.Add(comboBoxReference);
            Controls.Add(positionControlPC);
            Controls.Add(orientationControlOC);
            Controls.Add(buttonClear);
            Controls.Add(buttonCreate);
            Controls.Add(buttonClose);
            Controls.Add(numericTextBoxLength);
            Name = "frmCreateAluminiumProfile";
            Size = new System.Drawing.Size(228, 339);


            //base.Size = new Size(tw_width, 330);
            base.Apply += new System.EventHandler(btn_create_clicked);
            base.Deactivate += new System.EventHandler(CreateAluminiumProfile_Desactivate);
            pictureBoxModel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

            ((System.ComponentModel.ISupportInitialize)(pictureBoxModel)).EndInit();
            //ResumeLayout(false);

        }

        #endregion

        private void comboBoxReference_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxReference.SelectedItem)
            {
                case "20 x 20":
                    pictureBoxModel.Image = Resources.Alum_Prof_20_65;
                    break;
                case "30 x 30":
                    pictureBoxModel.Image = Resources.Alum_Prof_30_65;
                    break;
                case "40 x 40":
                    pictureBoxModel.Image = Resources.Alum_Prof_40_65;
                    break;
                case "50 x 50":
                    pictureBoxModel.Image = Resources.Alum_Prof_50_65;
                    break;
                case "60 x 60":
                    pictureBoxModel.Image = Resources.Alum_Prof_60_65;
                    break;
                case "80 x 80":
                    pictureBoxModel.Image = Resources.Alum_Prof_80_65;
                    break;
                case "90 x 90":
                    pictureBoxModel.Image = Resources.Alum_Prof_90_65;
                    break;
                default:
                    break;
            }
        }
    }
}
