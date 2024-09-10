using EasyArchitectV2VSIXProject.ClassesDef;
using EasyArchitectV2VSIXProject.Common;
using EasyArchitectV2VSIXProject.Forms;
using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyArchitectV2VSIXProject.WizardImp
{
    /// <summary>
    /// 這是建構 API Framework V2 所需要的 基礎建設 Infrastructure 所需要的相關程式碼的 Wizard
    /// </summary>
    public class DomainLayerWizard : IWizard
    {
        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            if(frmAddDomainLayer.GetSelectedTargetName == "radioRentalCar")
            {
                // 在目標 Project Template 產生 Domain Interface 檔案
                Utils.CreateDomainInterfaceOrClass(project, null, "IVehicle", DomainClassDef.GetDomainInterfaceTemplate, null, null);

                // 在目標 Project Template 產生 Domain Class 檔案
                Utils.CreateDomainInterfaceOrClass(project, "Car", "IVehicle", DomainClassDef.GetDomainClassTemplate, null, null);
            }
            else if(frmAddDomainLayer.GetSelectedTargetName == "radioConcertTicket")
            {
                Utils.CreateDomainInterfaceOrClass(project, "IAggregateRoot", DomainClassDef.GetAggregateRootTemplate);

                Utils.CreateDomainInterfaceOrClass(project, "ValueObject", DomainClassDef.GetValueObjectTemplate);

                Utils.CreateDomainInterfaceOrClass(project, "Entity", DomainClassDef.GetEntityTemplate);

                Utils.CreateDomainInterfaceOrClass(project, "Ticket", DomainClassDef.GetTicketTemplate);
                // 
                Utils.CreateDomainInterfaceOrClass(project, "ShowTime", DomainClassDef.GetShowTimeTemplate);

                Utils.CreateDomainInterfaceOrClass(project, "SeatReservation", DomainClassDef.GetSeatReservationTemplate);

                Utils.CreateDomainInterfaceOrClass(project, "ShowTimeNotDefinedException", DomainClassDef.GetShowTimeNotDefinedExceptionTemplate);

                Utils.CreateDomainInterfaceOrClass(project, "ConcertVenue", DomainClassDef.GetConcertVenueTemplate);
            }
        }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
        }

        public void RunFinished()
        {
        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            frmAddDomainLayer domainForm = new frmAddDomainLayer();
            GlobalVar.GetSetMyDomainLayerMappingWindowForm = domainForm;
            System.Windows.Forms.DialogResult result = domainForm.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
            }
            else
            {
                throw new WizardCancelledException("使用者取消！");
            }
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
