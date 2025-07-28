using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using ModelLayer;

namespace BusinessLayer
{
    public class SfBusinessRuleBusiness
    {
        private SfBusinessRuleData repository;

        public SfBusinessRuleBusiness()
        {
            repository = new SfBusinessRuleData();
        }

        public async Task<ResponseService<BusinessRuleModel>> GetBusinessRules()
        {
            ResponseService<BusinessRuleModel> responseData = new ResponseService<BusinessRuleModel>();

            try
            {
                responseData = await repository.GetBusinessRules();

                if (responseData.Error)
                {
                    throw new Exception(responseData.Message);
                }


            }
            catch (Exception ex)
            {
                responseData.Error = true;
                responseData.Message = ex.Message;
            }

            return responseData;
        }
    }
}
