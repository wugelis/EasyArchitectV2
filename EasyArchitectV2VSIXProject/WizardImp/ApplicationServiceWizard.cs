using EasyArchitectV2VSIXProject.ClassesDef;
using EasyArchitectV2VSIXProject.Common;
using EasyArchitectV2VSIXProject.Forms;
using EnvDTE;
using Microsoft.Internal.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.TemplateWizard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyArchitectV2VSIXProject.WizardImp
{
    /// <summary>
    /// 這是建構 API Framework V2 所需要的 UseCase 應用層 Application Services 所需要的相關程式碼的 Wizard
    /// </summary>
    public class ApplicationServiceWizard : IWizard
    {
        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            if(frmAddApplicationService.GetSelectedTargetName == "radioRentalCar")
            {
                Utils.CreateApplicationInterfaceOrClass(project, null, "IRentalCarUseCase", ApplicationClassDef.GetApplicationUseCaseInterfaceTemplate, "port", "In");

                Utils.CreateApplicationInterfaceOrClass(project, null, "IRentalCarRepository", ApplicationClassDef.GetApplicationRepositoryInterfaceTemplate, "port", "Out");

                Utils.CreateApplicationInterfaceOrClass(project, "RentalCarSerAppServices", "IRentalCarUseCase", ApplicationClassDef.GetApplicationServiceTemplate, null, null);
            }
            else if(frmAddApplicationService.GetSelectedTargetName == "radioConcertTicket")
            {

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
            frmAddApplicationService appForm = new frmAddApplicationService();
            GlobalVar.GetSetMyApplicationServiceMappingWindowForm = appForm;
            System.Windows.Forms.DialogResult result = appForm.ShowDialog();

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
            return true;
        }
    }
}
