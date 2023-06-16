using ABB.Robotics.RobotStudio.Stations;
using ABB.Robotics.RobotStudio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ABB.Robotics.Math;
using RobotStudio.API.Internal;

namespace PuimesAddin
{
    public partial class frmAutoMoveParam : Form
    {
        public frmAutoMoveParam()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Get the selected Move instructions
            #region SelectMoveInstructions

            //List<Part> listParts = new List<Part>();

            foreach (var item in Selection.SelectedObjects)
            {
                RsMoveInstruction SelectedMove = item as RsMoveInstruction;
                if (SelectedMove != null)
                {
                    Logger.AddMessage(new LogMessage("Move selected", "Puime's Add-in"));
                }
            }

            #endregion SelectMoveInstructions
        }
    }
}
