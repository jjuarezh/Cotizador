using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Utils;
using DataLayer.Functions;
using ModelLayer;

namespace DataLayer
{
    public class SfPopulation : Salesforce
    {
        public async Task<ResponseService<SfAccountModel>> GetPopulation(UserModel infoUserSf)
        {
            ResponseService<SfAccountModel> Response = new ResponseService<SfAccountModel>();

            try
            {
                List<SfAccountModel> allAccountPopulation = Accounts.GetAccount(infoUserSf.UserNameSf);

                Response.ResulList = allAccountPopulation;
            }
            catch (Exception ex)
            {
                Response.Error = true;
                Response.Message = ex.Message;
            }

            return Response;
        }
    }
}
