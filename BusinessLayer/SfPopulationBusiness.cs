using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using BusinessLayer;
using BusinessLayer.Utils;
using DataLayer;

namespace BusinessLayer
{
    public class SfPopulationBusiness
    {
        private SfPopulation repository;

        public SfPopulationBusiness()
        {
            repository = new SfPopulation();
        }
        
        public async Task<ResponseService<SfAccountModel>> GetPopulation(UserModel infoUserSf)
        {
            ResponseService<SfAccountModel> ResponseData = new ResponseService<SfAccountModel>();

            try
            {
                ResponseData = await repository.GetPopulation(infoUserSf);

                if (ResponseData.Error)
                {
                    throw new Exception(ResponseData.Message);
                }
            }
            catch (Exception ex)
            {
                ResponseData.Error = true;
                ResponseData.Message = ex.Message;
            }

            return ResponseData;
        }
    }
}
