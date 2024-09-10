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
    public partial class frmAddApplicationService : Form
    {
        private static string _selectedTargetName = string.Empty;

        public static string GetSelectedTargetName => _selectedTargetName;

        public frmAddApplicationService()
        {
            InitializeComponent();

            _selectedTargetName = radioRentalCar.Name;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void radioRentalCar_CheckedChanged(object sender, EventArgs e)
        {
            _selectedTargetName = (sender as RadioButton).Name;
        }
    }
}
