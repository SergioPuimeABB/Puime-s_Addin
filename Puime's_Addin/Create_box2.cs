using ABB.Robotics.RobotStudio.Environment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puime_s_Addin
{
    //public partial class Create_box2 : Form
    public partial class Create_box2 : ToolWindow
    {
        public Create_box2()
        {
            InitializeComponent();


            int tw_width = UIEnvironment.Windows["ObjectBrowser"].Control.Size.Width - 30;
            //ToolWindow tw_CreateBox = new ToolWindow("Create_ABB_Box");
            this.Caption = "Create ABB Box";
            this.PreferredSize = new Size(tw_width, 330);
            //this.Control.Size = new Size(330, 200);
            //UIEnvironment.Windows.AddDocked(tw_CreateBox, System.Windows.Forms.DockStyle.Top, UIEnvironment.Windows["ObjectBrowser"] as ToolWindow);
            UIEnvironment.Windows.AddDocked(this, System.Windows.Forms.DockStyle.Top, UIEnvironment.Windows["ObjectBrowser"] as ToolWindow);

        }

        private void btn_clear_clicked(object sender, EventArgs e)
        {
            //UIEnvironment.Windows["Create ABB Box"].Close();
            this.Close();
            new Create_box2();
        }
        private void btn_close_clicked(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
