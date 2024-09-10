using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyArchitectV2VSIXProject.Forms
{
    public partial class frmAddDomainLayer : Form
    {
        private static string _selectedTargetName = string.Empty;

        public static string GetSelectedTargetName => _selectedTargetName;

        public frmAddDomainLayer()
        {
            InitializeComponent();
        }

        private void radioRentalCar_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton target = sender as RadioButton;
            // 讀取頁面資料
            LoadUI(target.Name);

            _selectedTargetName = target.Name;
        }

        private void LoadUI(string targetName)
        {
            switch (targetName)
            {
                case "radioRentalCar":
                    picClassDiagram.Image = global::EasyArchitectV2VSIXProject.Properties.Resources.RentalCar_ClassDiagram;
                    break;
                case "radioConcertTicket":
                    picClassDiagram.Image = global::EasyArchitectV2VSIXProject.Properties.Resources.ConcertTicket_ClassDiagram;
                    break;
            }

            _selectedTargetName = targetName;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmAddDomainLayer_Load(object sender, EventArgs e)
        {
            LoadUI("radioRentalCar");
        }
    }
}
