using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using BusinessLayer;
using ModelLayer;

namespace ApiSmCotizador.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BusinessRuleController : ApiController
    {
        private SfBusinessRuleBusiness repository = new SfBusinessRuleBusiness();

        [HttpGet]
        //[HttpPost]
        [Route("api/businessrule")]
        public async Task<IEnumerable<BusinessRuleModel>> GetProducts()
        {
            var result = await repository.GetBusinessRules();

            if (result.Error)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, result.Message));
            }

            return result.ResulList;
        }
    }
}
