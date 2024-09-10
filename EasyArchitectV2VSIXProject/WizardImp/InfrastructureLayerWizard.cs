using EasyArchitectV2VSIXProject.ClassesDef;
using EasyArchitectV2VSIXProject.Common;
using EasyArchitectV2VSIXProject.Forms;
using EnvDTE;
using Microsoft.Build.Construction;
using Microsoft.VisualStudio.TemplateWizard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyArchitectV2VSIXProject.WizardImp
{
    /// <summary>
    /// 這是建構 API Framework V2 所需要的 基礎建設 Infrastructure 所需要的相關程式碼
    /// </summary>
    public class InfrastructureLayerWizard : IWizard
    {
        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
            string PERSISTANCE_FOLDER = "Persistance";
            ProjectItem modelsFolder = Utils.GetProjectItemFolder(project, PERSISTANCE_FOLDER)
                .FirstOrDefault();

            List<string> projectNames = Utils.GetProjectNameFromDTE(project);
            string selectedProjectName = string.Empty;

            if (projectNames.Count() > 0)
            {
                frmAddProjectsRef addPrjRef = new frmAddProjectsRef(projectNames);

                System.Windows.Forms.DialogResult settingResult = addPrjRef.ShowDialog();

                IEnumerable<string> selectedProjectNames = frmAddProjectsRef.GetSelectCheckListBox.SelectedItems.OfType<string>().ToArray();

                ProjectRootElement projectRoot = ProjectRootElement.Open(project.FullName);

                foreach (var selectedPrjName in selectedProjectNames)
                {
                    if (selectedPrjName != project.Name)
                    {
                        ProjectItemGroupElement projectGroup01 = projectRoot.AddItemGroup();
                        projectGroup01.AddItem("ProjectReference", $"..\\{selectedPrjName}\\{selectedPrjName}.csproj");
                        selectedProjectName = selectedPrjName;
                    }
                }

                projectRoot.Save();
            }

            // 2020/10/12 後修正為最後才產生 ApplicationDbContext 物件
            Utils.CreateDbContextFromSourceTables(project, modelsFolder, projectNames, selectedProjectName);

            string currentEndNamsSpace = "Persistance";
            //string entityFolderName = "Entities";

            foreach (string node in frmMyORMappingWindow.SelectedTables)
            {
                Utils.CreateEnitiesFromSourceTables(
                    project,
                    currentEndNamsSpace,
                    modelsFolder,
                    node,
                    node,
                    true,
                    CLASS_TYPE.ENTITY);
            }

            Utils.CreateEasyArchitectV2InfrastructureManager(project, "EasyArchitectV2InfrastructureManager");
        }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
        }

        public void RunFinished()
        {
        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            frmMyORMappingWindow infraForm = new frmMyORMappingWindow();
            GlobalVar.GetSetMyORMMappingWindowForm = infraForm;
            System.Windows.Forms.DialogResult result = infraForm.ShowDialog();

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
