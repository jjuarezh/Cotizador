using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using ModelLayer;

namespace BusinessLayer
{
    public class SfProductBusiness
    {
        private SfProductData repository;

        public SfProductBusiness()
        {
            repository = new SfProductData();
        }

        public async Task<ResponseService<ProductModel>> GetProducts()
        {
            ResponseService<ProductModel> responseData = new ResponseService<ProductModel>();

            try
            {
                responseData = await repository.GetProducts();

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
