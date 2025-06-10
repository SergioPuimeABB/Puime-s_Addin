using ABB.Robotics.RobotStudio;
using ABB.Robotics.RobotStudio.Environment;
using ABB.Robotics.RobotStudio.Stations;
using ABB.Robotics.RobotStudio.Stations.Forms;
using PuimesAddin;
using PuimesAddin.Properties;
using System.Drawing;

namespace Puime_s_Addin
{
    public class main
    {

        private static RibbonGroup rgPA; 
        private static CommandBarGalleryPopup galleryPA;
        private static CommandBarButton btnCP; // Copy Position
        private static CommandBarButton btnSP; // Set Position
        private static CommandBarButton btnCF; // Create Floor
        private static CommandBarButton btnCB; // Create ABB Box
        private static CommandBarButton btnCR; // Create ABB Raiser
        private static CommandBarButton btnCBP; // Create ABB Base plate
        private static CommandBarButton btnCM; // Create Markups
        private static CommandBarButton btnRT; // Rename Targets
        private static CommandBarButton btnJP; // Join Parts
        private static CommandBarButton btnTC; // Text Creator
        private static CommandBarButton btnMP; // Auto move parameters
        private static CommandBarButton btnCZ; // Camera zoom
        private static CommandBarButton btnCD; // CAD Export 
        private static CommandBarButton btnAP; // Create Aluminum Profile
        private static CommandBarButton btnWS; // Window Size

        public static void AddinMain()
        {
            Logger.AddMessage(new LogMessage("Puime's addin v4b loaded 10/06/2025 - 10:06 ...", "Puime's Add-in"));

            if (rgPA == null)
            {
                addRibbonGroup();
            }

            //
            //Mouse Right Click//
            //
            // Solo funciona con los targets, ver para añadirlo a otros objetos.
            // Ver cómo hacer el menú desplegable.
            // Ver cómo posicionarlo en el lugar correcto.
            //

            // Add context menu button to target datatype
            CommandBarContextPopup ctxPopup = UIEnvironment.GetContextMenu(typeof(RsTarget));
            CommandBarControlCollection cbcc = ctxPopup.Controls;
            // Create button object
            CommandBarButton BtnSyncTarget = new CommandBarButton("TwRobotstudioSyncTarget.SyncTarget", "Sync without path");
            BtnSyncTarget.DefaultEnabled = true;
            BtnSyncTarget.Image = Resources.PA;
            //BtnSyncTarget.LargeImage = Properties.Resources.icon_32;
            //BtnSyncTarget.ExecuteCommand += BtnSyncTarget_ExecuteCommand;
            //Add button to top. 
            cbcc.Insert(0, BtnSyncTarget);
            //


        }

        public static void addRibbonGroup()
        {
            rgPA = new RibbonGroup("rgPA", "PA");
            galleryPA = new CommandBarGalleryPopup("Puime's addin");
            galleryPA.NumberOfColumns = 5;
            galleryPA.GalleryTextPosition = GalleryTextPosition.Below;
            galleryPA.GalleryItemSize = new Size(96, 96);
            galleryPA.Image = Resources.PA;
            galleryPA.HelpText = "Puime's Addin.";
            
            CommandBarHeader control = new CommandBarHeader("Copy & set position");
            galleryPA.GalleryControls.Add(control);
            
            btnCP = new CommandBarButton("PuimesAddin Copy position", "Copy position");
            btnCP.Image = Resources.BT_copy;
            galleryPA.GalleryControls.Add(btnCP);
            btnCP.UpdateCommandUI += btnCP_UpdateCommandUI;
            btnCP.ExecuteCommand += btnCP_ExecuteCommand;

            btnSP = new CommandBarButton("PuimesAddin Set position", "Set position");
            btnSP.Image = Resources.BT_paste;
            galleryPA.GalleryControls.Add(btnSP);
            btnSP.UpdateCommandUI += btnSP_UpdateCommandUI;
            btnSP.ExecuteCommand += btnSP_ExecuteCommand;
            
            CommandBarHeader control2 = new CommandBarHeader("Creators");
            galleryPA.GalleryControls.Add(control2);

            btnCF = new CommandBarButton("PuimesAddin Floor creator", "Floor creator");
            btnCF.Image = Resources.BT_floor;
            galleryPA.GalleryControls.Add(btnCF);
            btnCF.UpdateCommandUI += btnCF_UpdateCommandUI;
            btnCF.ExecuteCommand += btnCF_ExecuteCommand;

            btnCB = new CommandBarButton("PuimesAddin ABB box creator", "ABB box creator");
            btnCB.Image = Resources.BT_box;
            galleryPA.GalleryControls.Add(btnCB);
            btnCB.UpdateCommandUI += btnCB_UpdateCommandUI;
            btnCB.ExecuteCommand += btnCB_ExecuteCommand;
            //ToolControlManager.RegisterToolCommand("ABBBox", ToolControlManager.FindToolHost("ElementBrowser"));

            btnCR = new CommandBarButton("PuimesAddin ABB raiser creator", "ABB raiser creator");
            btnCR.Image = Resources.BT_raiser;
            galleryPA.GalleryControls.Add(btnCR);
            btnCR.UpdateCommandUI += btnCR_UpdateCommandUI;
            btnCR.ExecuteCommand += btnCR_ExecuteCommand;

            btnCBP = new CommandBarButton("PuimesAddin ABB baseplate creator", "ABB base plate creator");
            btnCBP.Image = Resources.BT_baseplate;
            galleryPA.GalleryControls.Add(btnCBP);
            btnCBP.UpdateCommandUI += btnCBP_UpdateCommandUI;
            btnCBP.ExecuteCommand += btnCBP_ExecuteCommand;

            btnCM = new CommandBarButton("PuimesAddin Auto markup creator", "Auto markup creator");
            btnCM.Image = Resources.BT_marks;
            galleryPA.GalleryControls.Add(btnCM);
            btnCM.UpdateCommandUI += btnCM_UpdateCommandUI;
            btnCM.ExecuteCommand += btnCM_ExecuteCommand;
            //ToolControlManager.RegisterToolCommand("Marks", ToolControlManager.FindToolHost("ElementBrowser"));

            btnAP = new CommandBarButton("PuimesAddin Aluminum profile creator", "Aluminum profile creator");
            btnAP.Image = Resources.Alum_Prof_20_65_b;
            galleryPA.GalleryControls.Add(btnAP);
            btnAP.UpdateCommandUI += btnAP_UpdateCommandUI;
            btnAP.ExecuteCommand += btnAP_ExecuteCommand;


            CommandBarHeader control3 = new CommandBarHeader("Helpers");
            galleryPA.GalleryControls.Add(control3);


            btnRT = new CommandBarButton("PuimesAddin Auto rename targets", "Auto rename targets");
            btnRT.Image = Resources.BT_rename;
            galleryPA.GalleryControls.Add(btnRT);
            btnRT.UpdateCommandUI += btnRT_UpdateCommandUI;
            btnRT.ExecuteCommand += btnRT_ExecuteCommand;

            btnJP = new CommandBarButton("PuimesAddin Join parts", "Join parts");
            btnJP.Image = Resources.BT_joinparts;
            galleryPA.GalleryControls.Add(btnJP);
            btnJP.UpdateCommandUI += btnJP_UpdateCommandUI;
            btnJP.ExecuteCommand += btnJP_ExecuteCommand;

            btnMP = new CommandBarButton("Auto move parameters", "Auto move parameters");
            btnMP.Image = Resources.BT_move;
            galleryPA.GalleryControls.Add(btnMP);
            btnMP.UpdateCommandUI += btnMP_UpdateCommandUI;
            btnMP.ExecuteCommand += btnMP_ExecuteCommand;

            btnCZ = new CommandBarButton("PuimesAddin Zoom view", "Zoom view");
            btnCZ.Image = Resources.BT_camera;
            galleryPA.GalleryControls.Add(btnCZ);
            btnCZ.UpdateCommandUI += btnCZ_UpdateCommandUI;
            btnCZ.ExecuteCommand += btnCZ_ExecuteCommand;

            btnWS = new CommandBarButton("PuimesAddin Main window size", "Main window Size");
            //btnWS.Image = Resources.BT_camera;
            galleryPA.GalleryControls.Add(btnWS);
            btnWS.UpdateCommandUI += btnWS_UpdateCommandUI;
            btnWS.ExecuteCommand += btnWS_ExecuteCommand;

            //
            // TO DO
            //
            //btnTC = new CommandBarButton("Text creator", "Text creator");
            //btnTC.Image = Resources.BT_textcreator;
            //galleryPA.GalleryControls.Add(btnTC);
            //btnTC.UpdateCommandUI += btnTC_UpdateCommandUI;
            //btnTC.ExecuteCommand += btnTC_ExecuteCommand;

            //btnCD = new CommandBarButton("CAD Export", "CAD Export");
            //btnCD.Image = Resources.BT_cadexport;
            //galleryPA.GalleryControls.Add(btnCD);
            //btnCD.UpdateCommandUI += btnCD_UpdateCommandUI;
            //btnCD.ExecuteCommand += btnCD_ExecuteCommand;



            UIEnvironment.RibbonTabs["Modeling"].Groups[0].Controls.Insert(7, galleryPA);
        }

        private static void btnCP_UpdateCommandUI(object sender, UpdateCommandUIEventArgs e)
        {
            e.Enabled = Project.ActiveProject is Station;
        }

        private static void btnCP_ExecuteCommand(object sender, ExecuteCommandEventArgs e)
        {
            Copy_Position.ObtainPosition();
        }

        private static void btnSP_UpdateCommandUI(object sender, UpdateCommandUIEventArgs e)
        {
            e.Enabled = Project.ActiveProject is Station;
        }

        static void btnSP_ExecuteCommand(object sender, ExecuteCommandEventArgs e)
        {
            Copy_Position.SetPosition();
        }

        private static void btnCF_UpdateCommandUI(object sender, UpdateCommandUIEventArgs e)
        {
            e.Enabled = Project.ActiveProject is Station;
        }

        static void btnCF_ExecuteCommand(object sender, ExecuteCommandEventArgs e)
        {
            Make_Floor.ObtenerObjetosEstacion();
            Hide_CS.Hide_Objects();
            Hide_CS.ResetFloor();
            Logger.AddMessage(new LogMessage("Floor created.", "Puime's Add-in"));
        }

        private static void btnCB_UpdateCommandUI(object sender, UpdateCommandUIEventArgs e)
        {
            e.Enabled = Project.ActiveProject is Station;
        }

        static void btnCB_ExecuteCommand(object sender, ExecuteCommandEventArgs e)
        {
            ToolControlManager.ShowTool(typeof(frmCreateBoxBuilder), e.Id);
        }
        private static void btnCR_UpdateCommandUI(object sender, UpdateCommandUIEventArgs e)
        {
            e.Enabled = Project.ActiveProject is Station;
        }
        static void btnCR_ExecuteCommand(object sender, ExecuteCommandEventArgs e)
        {
            Create_Raiser.Create_ABB_Raiser();
        }

        private static void btnCBP_UpdateCommandUI(object sender, UpdateCommandUIEventArgs e)
        {
            e.Enabled = Project.ActiveProject is Station;
        }

        static void btnCBP_ExecuteCommand(object sender, ExecuteCommandEventArgs e)
        {
            Create_BasePlate.Create_ABB_BasePlate();
        }

        private static void btnCM_UpdateCommandUI(object sender, UpdateCommandUIEventArgs e)
        {
            e.Enabled = Project.ActiveProject is Station;
        }

        static void btnCM_ExecuteCommand(object sender, ExecuteCommandEventArgs e)
        {
            ToolControlManager.ShowTool(typeof(frmAutoMarkUpBuilder), e.Id);
        }

        private static void btnRT_UpdateCommandUI(object sender, UpdateCommandUIEventArgs e)
        {
            e.Enabled = Project.ActiveProject is Station;
        }
        static void btnRT_ExecuteCommand(object sender, ExecuteCommandEventArgs e)
        {
            RenameMoveTargets.CheckPathSelected();
        }

        private static void btnJP_UpdateCommandUI(object sender, UpdateCommandUIEventArgs e)
        {
            e.Enabled = Project.ActiveProject is Station;
        }
        static void btnJP_ExecuteCommand(object sender, ExecuteCommandEventArgs e)
        {
            JoinParts.JointBodies();
        }

        private static void btnTC_UpdateCommandUI(object sender, UpdateCommandUIEventArgs e)
        {
            e.Enabled = Project.ActiveProject is Station;
        }
        static void btnTC_ExecuteCommand(object sender, ExecuteCommandEventArgs e)
        {
            //TextCreator.CreateText();
            ToolControlManager.ShowTool(typeof(frmCreateTextBuilder), e.Id);
        }

        private static void btnMP_UpdateCommandUI(object sender, UpdateCommandUIEventArgs e)
        {
            e.Enabled = Project.ActiveProject is Station;
        }
        static void btnMP_ExecuteCommand(object sender, ExecuteCommandEventArgs e)
        {
            frmAutoMoveParam frm1 = new frmAutoMoveParam();
            frm1.Show();
        }

        private static void btnCZ_UpdateCommandUI(object sender, UpdateCommandUIEventArgs e)
        {
            e.Enabled = Project.ActiveProject is Station;
        }
        static void btnCZ_ExecuteCommand(object sender, ExecuteCommandEventArgs e)
        {
            //Camera_Zoom.Create_Zoom();
            frmZoom frm1 = new frmZoom();
            frm1.Show();
        }

        private static void btnCD_UpdateCommandUI(object sender, UpdateCommandUIEventArgs e)
        {
            e.Enabled = Project.ActiveProject is Station;
        }
        static void btnCD_ExecuteCommand(object sender, ExecuteCommandEventArgs e)
        {
            CADExport.ExportCADmain();
        }

        private static void btnAP_UpdateCommandUI(object sender, UpdateCommandUIEventArgs e)
        {
            e.Enabled = Project.ActiveProject is Station;
        }
        static void btnAP_ExecuteCommand(object sender, ExecuteCommandEventArgs e)
        {
            ToolControlManager.ShowTool(typeof(FrmCreateAluminiumProfile), e.Id);
        }

        private static void btnWS_UpdateCommandUI(object sender, UpdateCommandUIEventArgs e)
        {
            e.Enabled = Project.ActiveProject is Station;
        }
        static void btnWS_ExecuteCommand(object sender, ExecuteCommandEventArgs e)
        {
            ToolControlManager.ShowTool(typeof(frmMainWindowSize), e.Id);
        }
    }
}
