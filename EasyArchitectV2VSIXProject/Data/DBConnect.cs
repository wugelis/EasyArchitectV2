using EasyArchitectV2VSIXProject.Common;

namespace EasyArchitectV2VSIXProject
{
    public class DBConnect
    {
        public static string Connect()
        {
            return ConnectionServices.ConnectionInfo.ConnectionString; //ConfigurationManager.AppSettings["CDCDDbConfig"];
        }
    }
}