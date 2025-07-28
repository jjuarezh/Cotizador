using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using ModelLayer;


namespace ApiSmCotizador.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProductController : ApiController
    {
        private SfProductBusiness repository = new SfProductBusiness();

        [HttpGet]
        //[HttpPost]
        [Route("api/Product")]
        public async Task<IEnumerable<ProductModel>> GetProducts()
        {
            var result = await repository.GetProducts();

            if (result.Error)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, result.Message));
            }

            return result.ResulList;
        }
    }
}
