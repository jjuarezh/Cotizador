using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using System.Configuration;
using System.DirectoryServices;
using BusinessLayer.Utils;


namespace BusinessLayer
{
    public class ValidUserBusiness
    {
        public async Task<ResponseService<bool>> ValidUser(UserModel usuario)
        {
            ResponseService<bool> responseBussines = new ResponseService<bool>();

            #region Parameters
            string domain = ConfigurationManager.AppSettings["Domain"];
            string cDomain = ConfigurationManager.AppSettings["Cdomain"];
            string filter = ConfigurationManager.AppSettings["Filter"];
            string server = ConfigurationManager.AppSettings["NameServer"];
            string path = "LDAP://" + server + "/DC=" + domain + ",DC=" + cDomain;
            string strShare = string.Empty;
            string user = Cryptography.DecryptAES(usuario.Username);
            string pwd = Cryptography.DecryptAES(usuario.Password);
            #endregion

            try
            {
                DirectoryEntry pathLdap = new DirectoryEntry();

                pathLdap.Path = path;
                pathLdap.AuthenticationType = AuthenticationTypes.Secure;
                pathLdap.Username = user;
                pathLdap.Password = pwd;

                strShare = filter + "=" + user;

                DirectorySearcher dsSystem = new DirectorySearcher(pathLdap, strShare);
                dsSystem.SearchScope = SearchScope.Subtree;

                SearchResult srSystem = dsSystem.FindOne();
                if (srSystem != null)
                {
                    responseBussines.ResultObject = true;
                }
                
            }
            catch (Exception ex)
            {
                responseBussines.Error = true;
                responseBussines.Message = ex.Message;
            }

            return responseBussines;
        }
    }
}
