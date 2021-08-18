using System;
using System.Drawing;
using System.Windows.Forms;

using ABB.Robotics.RobotStudio;
using ABB.Robotics.RobotStudio.Environment;
using ABB.Robotics.RobotStudio.Stations;
using ABB.Robotics.RobotStudio.Stations.Forms;
using Puime_s_Addin.Properties;

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
        private static CommandBarButton btnCM; // Create Markups

        public static void AddinMain()
        {
            Logger.AddMessage(new LogMessage("Puime's Addin Loaded ...          2021.08.18 - 13:20", "Puime's Add-in"));

            if (rgPA == null)
            {
                addRibbonGroup();
            }
        }

        public static void addRibbonGroup()
        {
            rgPA = new RibbonGroup("rgPA", "PA");
            galleryPA = new CommandBarGalleryPopup("Puime's addin");
            galleryPA.NumberOfColumns = 3;
            galleryPA.GalleryTextPosition = GalleryTextPosition.Below;
            galleryPA.GalleryItemSize = new Size(96, 96);
            galleryPA.Image = Resources.PA;
            galleryPA.HelpText = "Puime's Addin.";
            
            CommandBarHeader control = new CommandBarHeader("Copy & set position");
            galleryPA.GalleryControls.Add(control);
            
            btnCP = new CommandBarButton("Copy", "Copy position");
            btnCP.Image = Resources.BT_copy;
            galleryPA.GalleryControls.Add(btnCP);
            btnCP.UpdateCommandUI += btnCP_UpdateCommandUI;
            btnCP.ExecuteCommand += btnCP_ExecuteCommand;

            btnSP = new CommandBarButton("Set", "Set position");
            btnSP.Image = Resources.BT_paste;
            galleryPA.GalleryControls.Add(btnSP);
            btnSP.UpdateCommandUI += btnSP_UpdateCommandUI;
            btnSP.ExecuteCommand += btnSP_ExecuteCommand;
            
            CommandBarHeader control2 = new CommandBarHeader("Helpers");
            galleryPA.GalleryControls.Add(control2);

            btnCF = new CommandBarButton("Floor", "Floor creator");
            btnCF.Image = Resources.BT_floor;
            galleryPA.GalleryControls.Add(btnCF);
            btnCF.UpdateCommandUI += btnCF_UpdateCommandUI;
            btnCF.ExecuteCommand += btnCF_ExecuteCommand;

            btnCB = new CommandBarButton("ABBBox", "ABB Box creator");
            btnCB.Image = Resources.BT_box;
            galleryPA.GalleryControls.Add(btnCB);
            btnCB.UpdateCommandUI += btnCB_UpdateCommandUI;
            btnCB.ExecuteCommand += btnCB_ExecuteCommand;
            ToolControlManager.RegisterToolCommand("ABBBox", ToolControlManager.FindToolHost("ElementBrowser"));

            btnCR = new CommandBarButton("ABB Raiser", "ABB Raiser creator");
            btnCR.Image = Resources.BT_raiser;
            galleryPA.GalleryControls.Add(btnCR);
            btnCR.UpdateCommandUI += btnCR_UpdateCommandUI;
            btnCR.ExecuteCommand += btnCR_ExecuteCommand;

            btnCM = new CommandBarButton("Marks", "Auto markup creator");
            btnCM.Image = Resources.BT_marks;
            galleryPA.GalleryControls.Add(btnCM);
            btnCM.UpdateCommandUI += btnCM_UpdateCommandUI;
            btnCM.ExecuteCommand += btnCM_ExecuteCommand;
            ToolControlManager.RegisterToolCommand("Marks", ToolControlManager.FindToolHost("ElementBrowser"));


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

        private static void btnCM_UpdateCommandUI(object sender, UpdateCommandUIEventArgs e)
        {
            e.Enabled = Project.ActiveProject is Station;
        }

        static void btnCM_ExecuteCommand(object sender, ExecuteCommandEventArgs e)
        {
            ToolControlManager.ShowTool(typeof(frmAutoMarkUpBuilder), e.Id);
        }
    }
}
