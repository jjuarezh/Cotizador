using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using BusinessLayer.Sfdc.Enterprise;
using BusinessLayer.Utils;
using ModelLayer;
using User = ApiSmCotizador.Sfdc.Enterprise.User;

namespace ApiSmCotizador.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PopulationController : ApiController
    {
        private SfPopulationBusiness repository = new SfPopulationBusiness();

        // GET: api/Popilation
        public void Get()
        {
            
        }

        // GET: api/Popilation/5
        public void Get(int value)
        {
            
        }

        // POST: api/Popilation
        [HttpPost]
        [Route("api/Population")]
        //public async Task<IEnumerable<PopulationModel>> Post([FromBody]UserModel infoUserSf)
        public async Task<IEnumerable<SfAccountModel>> Post([FromBody]UserModel infoUserSf)
        {
            var responPopulation = await repository.GetPopulation(infoUserSf);

            if (responPopulation.Error)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, responPopulation.Message));
            }

            return responPopulation.ResulList;
        }

        // PUT: api/Popilation/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Popilation/5
        public void Delete(int id)
        {
        }
    }
}
