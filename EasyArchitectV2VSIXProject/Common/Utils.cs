﻿using EasyArchitectV2VSIXProject.ClassesDef;
using EasyArchitectV2VSIXProject.Data;
using EasyArchitectV2VSIXProject.Forms;
using EnvDTE;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyArchitectV2VSIXProject.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class Utils
    {
        #region 建立供 Model 使用的 Entity 檔案
        /// <summary>
        /// 建立供 Model 使用的 Entity 檔案
        /// </summary>
        /// <param name="ClassDef">Class 內容定義</param>
        /// <param name="ClassFillPath">要建立的 Class 檔案的完整路徑</param>
        /// <returns>傳回建立的 CS 檔案路徑</returns>
        public static void CreateModelCSFile(string ClassDef, string ClassFullPath)
        {
            FileStream fs = new FileStream(ClassFullPath, FileMode.Create);
            try
            {
                StreamWriter sw = new StreamWriter(fs);
                try
                {
                    sw.WriteLine(ClassDef);
                }
                finally
                {
                    sw.Close();
                }
            }
            finally
            {
                fs.Close();
            }
        }
        #endregion

        #region 取得 Project 內的資料夾 (ProjectItem 物件)
        /// <summary>
        /// 取得 Project 內的資料夾 (ProjectItem 物件)
        /// </summary>
        /// <param name="project"></param>
        /// <param name="QUERY_COMMAND_FOLDER"></param>
        /// <returns></returns>
        public static IEnumerable<ProjectItem> GetProjectItemFolder(Project project, string QUERY_COMMAND_FOLDER)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            return project.ProjectItems.OfType<ProjectItem>().Where(c => c.Name.ToLower() == QUERY_COMMAND_FOLDER.ToLower());
        }
        #endregion

        #region 透過資料夾名稱取得專案的 ProjectItem 物件執行個體
        /// <summary>
        /// 取得資料夾 Folder 的 ProjectItem 執行個體
        /// </summary>
        /// <param name="CQRSFolder"></param>
        /// <param name="queryFolderName"></param>
        /// <returns></returns>
        public static ProjectItem GetOrCreateCurrentFolder(ProjectItem CQRSFolder, string queryFolderName)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            ProjectItem currentFolder;
            try
            {
                var queryFolder = CQRSFolder.ProjectItems.OfType<ProjectItem>()
#pragma warning disable VSTHRD010 // 在主執行緒叫用單一執行緒類型
                    .Where(c => c.Name == queryFolderName)
#pragma warning restore VSTHRD010 // 在主執行緒叫用單一執行緒類型
                    .FirstOrDefault();

                if(queryFolder == null)
                {
                    currentFolder = CQRSFolder.ProjectItems.AddFolder(queryFolderName);
                }
                else
                {
                    currentFolder = queryFolder;
                }
            }
            catch (NullReferenceException nex)
            {
                Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

                var tmpQueryFolder = CQRSFolder.ProjectItems.OfType<ProjectItem>()
#pragma warning disable VSTHRD010 // 在主執行緒叫用單一執行緒類型
                    .Where(c => c.Name == queryFolderName)
#pragma warning restore VSTHRD010 // 在主執行緒叫用單一執行緒類型
                    .FirstOrDefault();

                currentFolder = tmpQueryFolder;
            }

            return currentFolder;
        }
        #endregion

        #region 建立 CQRS 使用的 Command 資料夾
        /// <summary>
        /// 建立 CQRS 使用的 Command 資料夾
        /// </summary>
        /// <param name="project"></param>
        /// <param name="targetFolder"></param>
        /// <param name="CQRSFolder"></param>
        /// <returns></returns>
        internal static ProjectItem CreateProjectFolder(Project project, string targetFolder, ProjectItem CQRSFolder)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            IEnumerable<ProjectItem> resultEntity = Utils.GetProjectItemFolder(project, targetFolder);

            if (resultEntity.FirstOrDefault() == null)
            {
#pragma warning disable VSTHRD108 // 無條件判斷提示執行緒親和性
                Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();
#pragma warning restore VSTHRD108 // 無條件判斷提示執行緒親和性

                CQRSFolder = CreateRootFolder(project, targetFolder, CQRSFolder);
            }
            else
            {
                //若 Entities 這個目錄已經存在，則直接取得這個目錄.
                CQRSFolder = resultEntity.FirstOrDefault();
            }

            CQRSFolder = project.ProjectItems.OfType<ProjectItem>()
                        .Where(c => c.Name.ToLower() == targetFolder.ToLower())
                        .FirstOrDefault();

            return CQRSFolder;
        }
        /*
        /// <summary>
        /// 建立 與 初始化 Update Command Handler for (CQRS)
        /// </summary>
        /// <param name="project"></param>
        /// <param name="currentFolder"></param>
        /// <param name="commandClassName"></param>
        /// <param name="dtoClassName"></param>
        internal static void UpdateCQRSCreateCommandClassFromSource(Project project, ProjectItem currentFolder, string commandClassName, string dtoClassName)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            SQLStore store = new SQLStore();
            StringBuilder sb = new StringBuilder();
            int columnOrder = 0;

            string commandHandlerDefined = UpdateCommandDef.GetClassTemplate;

            commandHandlerDefined = commandHandlerDefined.Replace("$(NAMESPACE_DEF)$", string.Format("{0}", project.Name));
            commandHandlerDefined = commandHandlerDefined.Replace("$(CREATE_COMMAND_NAME)$", commandClassName);

            ClassDef.GetClassProperties(store.GetNoDataDataTableByName(dtoClassName), new string[] { }, sb, columnOrder);
            commandHandlerDefined = commandHandlerDefined.Replace("$(CLASS_PROPERTIES_DEF)$", sb.ToString());

            string CommandHandlerFileName = $"Update{commandClassName}Command.cs";

            //產生 CQRS Create Command 檔案，並先暫放在 Temp 資料夾下.
            string TempPath = AddFile2ProjectItem(currentFolder, commandHandlerDefined, CommandHandlerFileName);
            //刪除掉暫存檔案
            try
            {
                File.Delete(TempPath);
            }
            catch (Exception ex) { } //非系統服務刪除暫存檔案若失敗不處理任何訊息（請手動清除 Temp 底下檔案）.
        }
        */

        /*
        /// <summary>
        /// 建立 與 初始化 Delete Command Handler for (CQRS)
        /// </summary>
        /// <param name="project"></param>
        /// <param name="currentFolder"></param>
        /// <param name="commandClassName"></param>
        /// <param name="dtoClassName"></param>
        internal static void DeleteCQRSCreateCommandClassFromSource(Project project, ProjectItem currentFolder, string commandClassName, string dtoClassName)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            SQLStore store = new SQLStore();
            StringBuilder sb = new StringBuilder();
            int columnOrder = 0;

            string commandHandlerDefined = DeleteCommandDef.GetClassTemplate;

            commandHandlerDefined = commandHandlerDefined.Replace("$(NAMESPACE_DEF)$", string.Format("{0}", project.Name));
            commandHandlerDefined = commandHandlerDefined.Replace("$(CREATE_COMMAND_NAME)$", commandClassName);

            ClassDef.GetClassProperties(store.GetNoDataDataTableByName(dtoClassName), new string[] { }, sb, columnOrder);
            commandHandlerDefined = commandHandlerDefined.Replace("$(CLASS_PROPERTIES_DEF)$", sb.ToString());

            string CommandHandlerFileName = $"Delete{commandClassName}Command.cs";

            //產生 CQRS Create Command 檔案，並先暫放在 Temp 資料夾下.
            string TempPath = AddFile2ProjectItem(currentFolder, commandHandlerDefined, CommandHandlerFileName);
            //刪除掉暫存檔案
            try
            {
                File.Delete(TempPath);
            }
            catch (Exception ex) { } //非系統服務刪除暫存檔案若失敗不處理任何訊息（請手動清除 Temp 底下檔案）.
        }
        */

        /// <summary>
        /// 建立專案 Root 資料夾
        /// </summary>
        /// <param name="project"></param>
        /// <param name="ROOT_FOLDER_NAME"></param>
        /// <param name="newFolder"></param>
        /// <returns></returns>
        public static ProjectItem CreateRootFolder(Project project, string ROOT_FOLDER_NAME, ProjectItem newFolder)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            try
            {
                newFolder = project.ProjectItems.AddFolder(ROOT_FOLDER_NAME);
            }
            catch (NullReferenceException nex) //指攔下 NullReferenceException
            {
                // VS2019 的非同步機制會在這裡取不到剛建立的資料夾，所以暫時忽略此錯誤 NullReferenceException 訊息
                // WriteLog
            }
            catch(InvalidOperationException ioex) 
            {
                // 當資料夾存在時，回傳 InvalidOperationEException 因此重新取得存在的資料夾即可
                newFolder = project.ProjectItems
                    .OfType<ProjectItem>()
#pragma warning disable VSTHRD010 // 在主執行緒叫用單一執行緒類型
                    .Where(c => c.Name.ToLower() == ROOT_FOLDER_NAME.ToLower())
#pragma warning restore VSTHRD010 // 在主執行緒叫用單一執行緒類型
                    .FirstOrDefault();
            }

            return newFolder;
        }
        #endregion

        /// <summary>
        /// 產生 Domain Interface 類別定義（有實作繼承 interface）
        /// </summary>
        /// <param name="project"></param>
        /// <param name="className"></param>
        /// <param name="inheritanceInterfaceName"></param>
        /// <param name="classOrInterfaceDef"></param>
        public static void CreateDomainInterfaceOrClass(Project project, string className, string inheritanceInterfaceName, string classOrInterfaceDef, string currentFolder, string InOutFolder)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            string domainInterfaceDefined = classOrInterfaceDef;
            string usingStatement = $"using {project.Name}.port.In;\r\nusing {project.Name}.port.Out;";

            domainInterfaceDefined = domainInterfaceDefined.Replace("$(USING)$", usingStatement);

            domainInterfaceDefined = domainInterfaceDefined.Replace("$(NAMESPACE_DEF)$", string.Format("{0}", project.Name));

            if(!string.IsNullOrEmpty(inheritanceInterfaceName))
            {
                domainInterfaceDefined = domainInterfaceDefined.Replace("$(INTERFACE_NAME)$", inheritanceInterfaceName);
            }
            
            if(!string.IsNullOrEmpty(className))
            {
                domainInterfaceDefined = domainInterfaceDefined.Replace("$(DOMAIN_CLASS)$", className);
            }

            //產生等會使用的暫存檔名
            string TempCSPath = Path.Combine(
                Environment.GetEnvironmentVariable("temp"),
                $"{className ?? inheritanceInterfaceName.ToUpperFirstWord()}.cs");

            if (currentFolder == null)
            {
                if (!Directory.Exists(Path.GetDirectoryName(TempCSPath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(TempCSPath));
                }
                //建立暫存的 Class 檔案
                CreateModelCSFile(domainInterfaceDefined, TempCSPath);
                //加入暫存的 Class 檔案
                project.ProjectItems.AddFromFileCopy(TempCSPath);
            }
            else
            {
                ProjectItem portInOutFolder = null;
                portInOutFolder = Utils.CreateProjectFolder(project, currentFolder, portInOutFolder);

                ProjectItem inFolder = Utils.GetOrCreateCurrentFolder(portInOutFolder, InOutFolder??"In");

                //ProjectItem modelsFolder = Utils.GetProjectItemFolder(project, currentFolder)
                //    .FirstOrDefault();

                AddFile2ProjectItem(inFolder, domainInterfaceDefined, Path.GetFileName(TempCSPath));
            }
            

            //刪除掉暫存檔案
            try
            {
                File.Delete(TempCSPath);
            }
            catch (Exception ex) { } //刪除暫存檔案若失敗不處理任何訊息.
            //CreateDomainInterfaceOrClass(project, className, string.Empty, classOrInterfaceDef);
        }
        /// <summary>
        /// 產生 Domain Interface 類別定義
        /// </summary>
        /// <param name="project"></param>
        /// <param name="modelsFolder"></param>
        public static void CreateDomainInterfaceOrClass(Project project, string className, string classOrInterfaceDef)
        {
            CreateDomainInterfaceOrClass(project, className, null, classOrInterfaceDef, null, null);
        }

        public static void CreateApplicationInterfaceOrClass(Project project, string className, string classOrInterfaceDef)
        {
            CreateDomainInterfaceOrClass(project, className, null, classOrInterfaceDef, null, null);
        }

        public static void CreateApplicationInterfaceOrClass(Project project, string className, string inheritanceInterfaceName, string classOrInterfaceDef, string currentFolder, string inOutFolder)
        {
            CreateDomainInterfaceOrClass(project, className, inheritanceInterfaceName, classOrInterfaceDef, currentFolder, inOutFolder);
        }

        #region 產生 DbContext 定義
        /// <summary>
        /// 產生 DbContext 定義
        /// </summary>
        /// <param name="project"></param>
        /// <param name="INTERFACE_FOLDER"></param>
        /// <param name="modelsFolder"></param>
        /// <param name="projectNames"></param>
        public static void CreateDbContextFromSourceTables(
            Project project, 
            ProjectItem modelsFolder, 
            List<string> projectNames,
            string selectProjectName)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            string DbContextDefined = DbContextDef.GetClassTemplate;
            string EntitiesName = "ApplicationDbContext"; // string.Format("{0}Model", ConnectionServices.ConnectionInfo.Initial_Catalog).ToUpperFirstWord().Replace(" ", "");
            string DbSetDefined = string.Empty;

            bool haveRefInfra = projectNames.Count() > 0; // 是否有參照 Infrastructure 專案
            bool completeUsing = false;
            string appProjectName = selectProjectName; //haveRefInfra ? projectNames[0] : "";

            DbContextDefined = DbContextDefined.Replace("$(NAMESPACE_DEF)$", string.Format("{0}", project.Name));
            DbContextDefined = DbContextDefined.Replace("$(ENTITIES_DEF)$", EntitiesName);

            DbContextDefined = DbContextDefined.Replace("$(OTHER_NAMESPACE)$", $"\r\nusing {project.Name}.Persistance;");

            if (haveRefInfra)
            {
                string domainProjectName = GetProjectNameFromDTE(project)
                    .Where(c => c != appProjectName)
                    .FirstOrDefault();

                completeUsing = !string.IsNullOrEmpty(domainProjectName);

                if(completeUsing)
                {
                    // 顯示 Confirm Windows 提示開發人員輸入 Domain Layer 專案名稱 提供 ApplicationDbContext 程式碼內容的 using 參考使用
                    DbContextDefined = DbContextDefined.Replace("$(OTHER_NAMESPACE)$", $"\r\nusing {domainProjectName}.Entities;\r\nusing {appProjectName}.Common.Interfaces;");
                }
                else
                {
                    // 顯示 Confirm Windows 提示開發人員輸入 Domain Layer 專案名稱 提供 ApplicationDbContext 程式碼內容的 using 參考使用
                    DbContextDefined = DbContextDefined.Replace("$(OTHER_NAMESPACE)$", $""); // 若目前方案中無法找到 domain 專案就清空該標籤
                }

                DbContextDefined = DbContextDefined.Replace("$(MARK_CODE)$", "");
            }
            else
            {
                DbContextDefined = DbContextDefined.Replace("$(OTHER_NAMESPACE)$", "");
                DbContextDefined = DbContextDefined.Replace("$(MARK_CODE)$", "//");
            }
            

            string DbContextFileName = $"{EntitiesName}.cs"; //string.Format("{0}Context.cs", EntitiesName);

            SQLStore store = new SQLStore();

            //if(!completeUsing)
            //{
            //    DbSetDefined += "/* 請參考 Domain 專案後再將之取消註解\r\n";
            //}

            int count = 0;
            foreach (string node in frmMyORMappingWindow.SelectedTables)
            {
                string entityName = node.ToUpperFirstWord().Replace(" ", "_");
                DbSetDefined += count > 0 ? $"\t\tpublic virtual DbSet<{entityName}Ent> {entityName} {{ get; set; }}\r\n" : $"public virtual DbSet<{entityName}Ent> {entityName} {{ get; set; }}\r\n";
                //DbSetDefined += string.Format("{1}public virtual DbSet<{0}Ent> {0} {{ get; set; }}\r\n", node.ToUpperFirstWord().Replace(" ", "_"), "\t\t");
                count++;
            }

            //if(!completeUsing)
            //{
            //    DbSetDefined += "\t\t*/";
            //}```

            DbContextDefined = DbContextDefined.Replace("$(DB_SET_DEF)$", DbSetDefined);

            //產生 DbContext 檔案，使用檔名 InitialCalog+Model+Context.cs 並先暫放在 Temp 資料夾下.
            string TempInterfacePath = AddFile2ProjectItem(modelsFolder, DbContextDefined, DbContextFileName);

            //刪除掉暫存檔案
            try
            {
                File.Delete(TempInterfacePath);
            }
            catch (Exception ex) { } //刪除暫存檔案若失敗不處理任何訊息.
        }
        #endregion

        #region 初始化專案 Project 內容
        /*
        /// <summary>
        /// 建立 與 初始化 Create Command Handler for (CQRS)
        /// </summary>
        /// <param name="project"></param>
        /// <param name="currentFolder"></param>
        /// <param name="commandClassName"></param>
        /// <param name="dtoClassName"></param>
        public static void CreateCQRSCreateCommandClassFromSource(Project project, ProjectItem currentFolder, string commandClassName, string dtoClassName)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            SQLStore store = new SQLStore();
            StringBuilder sb = new StringBuilder();
            int columnOrder = 0;

            string commandHandlerDefined = CreateCommandDef.GetClassTemplate;

            commandHandlerDefined = commandHandlerDefined.Replace("$(NAMESPACE_DEF)$", string.Format("{0}", project.Name));
            commandHandlerDefined = commandHandlerDefined.Replace("$(CREATE_COMMAND_NAME)$", commandClassName);
            
            ClassDef.GetClassProperties(store.GetNoDataDataTableByName(dtoClassName), new string[] { }, sb, columnOrder);
            commandHandlerDefined = commandHandlerDefined.Replace("$(CLASS_PROPERTIES_DEF)$", sb.ToString());

            string CommandHandlerFileName = $"Create{commandClassName}Command.cs";

            //產生 CQRS Create Command 檔案，並先暫放在 Temp 資料夾下.
            string TempPath = AddFile2ProjectItem(currentFolder, commandHandlerDefined, CommandHandlerFileName);
            //刪除掉暫存檔案
            try
            {
                File.Delete(TempPath);
            }
            catch (Exception ex) { } //刪除暫存檔案若失敗不處理任何訊息.
        }
        */

        /*
        /// <summary>
        /// 建立 CQRS 的 Query Command Handler 定義
        /// </summary>
        /// <param name="project"></param>
        /// <param name="MODEL_FOLDER"></param>
        /// <param name="currentFolder"></param>
        /// <param name="commandClassName"></param>
        /// <param name="dtoClassName"></param>
        public static void CreateCQRSQueryCommandClassFromSource(Project project, ProjectItem currentFolder, string commandClassName, string dtoClassName)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            string commandHandlerDefined = QueriesDef.GetCQRSQueryCommandTemplate;
            //string DbContextName = string.Format("{0}Model", ConnectionServices.ConnectionInfo.Initial_Catalog).ToUpperFirstWord().Replace(" ", "");

            commandHandlerDefined = commandHandlerDefined.Replace("$(NAMESPACE_DEF)$", string.Format("{0}", project.Name));
            commandHandlerDefined = commandHandlerDefined.Replace("$(QUERY_COMMAND_NAME)$", commandClassName);
            commandHandlerDefined = commandHandlerDefined.Replace("$(QUERY_DTO)$", $"{dtoClassName.ToUpperFirstWord()}Dto");

            string CommandHandlerFileName = $"Get{commandClassName}Query.cs";

            //產生 GenericRepository 檔案，使用檔名 GenericRepository.cs 並先暫放在 Temp 資料夾下.
            string TempPath = AddFile2ProjectItem(currentFolder, commandHandlerDefined, CommandHandlerFileName);
            //刪除掉暫存檔案
            try
            {
                File.Delete(TempPath);
            }
            catch (Exception ex) { } //刪除暫存檔案若失敗不處理任何訊息.
        }
        */
        /// <summary>
        /// 在目前的 ProjectItem 的資料夾產生一個 CS 檔案
        /// </summary>
        /// <param name="currentFolder"></param>
        /// <param name="contentDefined"></param>
        /// <param name="csFileName"></param>
        /// <returns></returns>
        private static string AddFile2ProjectItem(ProjectItem currentFolder, string contentDefined, string csFileName)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            string TempPath = Path.Combine(
                Environment.GetEnvironmentVariable("temp"),
                csFileName);

            //建立暫存的 Class 檔案
            Common.Utils.CreateModelCSFile(contentDefined, TempPath);
            //加入暫存的 Class 檔案
            currentFolder.ProjectItems.AddFromFileCopy(TempPath);
            return TempPath;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="project"></param>
        /// <param name="className"></param>
        public static void CreateEasyArchitectV2InfrastructureManager(Project project, string className)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            string infrastructureDefined = DbContextDef.GetEasyArchitectV2InfrastructureManager;
            //string usingStatement = $"using {project.Name}.port.In;\r\nusing {project.Name}.port.Out;";

            //domainInterfaceDefined = domainInterfaceDefined.Replace("$(USING)$", usingStatement);

            infrastructureDefined = infrastructureDefined.Replace("$(NAMESPACE_DEF)$", string.Format("{0}", project.Name));

            infrastructureDefined = infrastructureDefined.Replace("$(DOMAIN_CLASS)$", className);

            //產生等會使用的暫存檔名
            string TempCSPath = Path.Combine(
                Environment.GetEnvironmentVariable("temp"),
                $"{className.ToUpperFirstWord()}.cs");

            if (!Directory.Exists(Path.GetDirectoryName(TempCSPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(TempCSPath));
            }
            //建立暫存的 Class 檔案
            CreateModelCSFile(infrastructureDefined, TempCSPath);
            //加入暫存的 Class 檔案
            project.ProjectItems.AddFromFileCopy(TempCSPath);
        }
        #region 產生 Entities 的 Class 定義.
        /// <summary>
        /// 產生 Entities 的 Class 定義
        /// </summary>
        /// <param name="project"></param>
        /// <param name="currentFolder"></param>
        /// <param name="entitiesFolder"></param>
        /// <param name="classType">需要產生的類別類型 (DTO 物件／Entity 物件)</param>
        public static void CreateEnitiesFromSourceTables(
            Project project, 
            string currentFolder, 
            ProjectItem entitiesFolder, 
            string classNameOrTableName,
            string commandName,
            bool createKeyAttribute,
            CLASS_TYPE classType)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            string ClassName = classNameOrTableName.Replace(" ", "_");
            ClassDef clsDef = new ClassDef();
            SQLStore store = new SQLStore();
            string ClassDefined = ClassDef.GetClassTemplate;
            string classEndWord = classType == CLASS_TYPE.DTO ? "Dto" : "Ent";
            string usingStatement = classType == CLASS_TYPE.ENTITY ? $"\nusing {project.Name}.Common;" : "";
            //string commandOrFolder = classType == CLASS_TYPE.ENTITY ? "Entities" : commandName;

            //ClassDefined = ClassDefined.Replace("$(USING)$", usingStatement);
            ClassDefined = ClassDefined.Replace("$(USING)$", "");
            if(string.IsNullOrEmpty(currentFolder))
            { 
                ClassDefined = ClassDefined.Replace(".$(FOLDER_NAME)$", currentFolder);
            }
            else
            {
                ClassDefined = ClassDefined.Replace("$(FOLDER_NAME)$", currentFolder);
            }
            ClassDefined = ClassDefined.Replace("$(FOLDER_NAME)$", currentFolder);
            ClassDefined = ClassDefined.Replace("$(NAMESPACE_DEF)$", $"{project.Name}");
            //ClassDefined = ClassDefined.Replace("$(QUERY_COMMAND_NAME)$", $"{commandOrFolder}");

            ClassDefined = ClassDefined.Replace(
                    "$(CLASS_DEF)$",
                    clsDef.GetClassDef(
                        store.GetNoDataDataTableByName(classNameOrTableName), 
                        $"{ClassName.ToUpperFirstWord()}{classEndWord}", 
                        createKeyAttribute ? store.GetTableKeyByName(string.Format("{0}", ClassName)) : new string[] { }, 
                        classType));

            //產生等會使用的暫存檔名
            string TempCSPath = Path.Combine(
                Environment.GetEnvironmentVariable("temp"),
                $"{classNameOrTableName.ToUpperFirstWord()}{classEndWord}.cs");

            if (!Directory.Exists(Path.GetDirectoryName(TempCSPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(TempCSPath));
            }
            //建立暫存的 Class 檔案
            CreateModelCSFile(ClassDefined, TempCSPath);
            //加入暫存的 Class 檔案
            entitiesFolder.ProjectItems.AddFromFileCopy(TempCSPath);
            //刪除掉暫存檔案
            try
            {
                File.Delete(TempCSPath);
            }
            catch (Exception ex) { } //刪除暫存檔案若失敗不處理任何訊息.
            //}
            
        }
        #endregion

        /// <summary>
        /// 將匿名型別轉換為 T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static T Cast<T>(object obj, T type)
        {
            return (T)obj;
        }

        /// <summary>
        /// 從 DTE 服務中取得目前 Solution 中的所有專案名稱
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public static List<string> GetProjectNameFromDTE(Project project)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            List<string> projectNames = new List<string>();

            foreach (Project prj in project.DTE.Solution.Projects)
            {
                Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

                //Trace.WriteLine($"Sub Project '{prj.Name}' Count:{prj.Collection.Count}");
                Trace.WriteLine($"the folder {prj.Name} Kind is {prj.Kind}.");

                if(prj.Kind == "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}")
                {
                    projectNames = RecursiveSubProject(prj, project, ref projectNames);

                    if(prj.Collection != null)
                    {
                        Trace.WriteLine($"方案資料夾下面有 {prj.Collection.Count} 個專案");
                    }
                }
                else
                {
                    // 只要 方案內有專案的 名稱 不等於 自身的 Project.Name 時，就將其它的 Project.Name 加入到 ListBox 中
                    if (prj.Name != project.Name)
                    {
                        projectNames.Add(prj.Name);
                    }
                }
            }

            return projectNames;
        }

        //private static List<string> projectNames; // = new List<string>();
        /// <summary>
        /// 不斷的往下尋找 SubProject 是否為『方案資料夾』，如果是，就繼續往下尋找，如果不是就直接回傳.
        /// </summary>
        /// <param name="currentNode"></param>
        /// <returns></returns>
        private static List<string> RecursiveSubProject(Project currentNode, Project parentNode, ref List<string> noneSlnFolders)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            Project lastSubNode = null;

            if(currentNode == null)
            {
                return noneSlnFolders;
            }

            foreach (ProjectItem p in currentNode.ProjectItems)
            {
                if(p.SubProject != null)
                {
                    if(p.SubProject.Kind == "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}")
                    {
                        noneSlnFolders = RecursiveSubProject((Project)p.SubProject, parentNode, ref noneSlnFolders);
                        Trace.WriteLine($"{p.Name} 有子專案!!");
                        Trace.WriteLine($"在 {p.Name} 底下有 {p.SubProject.Name}");
                    }
                    else
                    {
                        // 只要 方案內有專案的 名稱 不等於 自身的 Project.Name 時，就將其它的 Project.Name 加入到 ListBox 中
                        if(parentNode.Name != p.SubProject.Name)
                        {
                            noneSlnFolders.Add(p.SubProject.Name);
                        }
                        
                        Trace.WriteLine($"在 SubProject 下有不是方案資料夾的專案，名稱為 {p.SubProject.Name}");
                        lastSubNode = p.SubProject;
                    }
                }
            }

            return noneSlnFolders;
        }
    }
}
