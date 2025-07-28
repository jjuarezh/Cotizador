using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Cors;
using ModelLayer;
using BusinessLayer;
using System.Threading.Tasks;
using System.Web.Http.Cors;


namespace ApiSmCotizador.Controllers
{
    [EnableCors(origins:"*", headers:"*", methods:"*")]
    public class ValidUserSmController : ApiController
    {
        private ValidUserBusiness responseBusiness = new ValidUserBusiness();
        // GET: api/ValidUserSm
        
        public void Get()
        {
        }

        // GET: api/ValidUserSm/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ValidUserSm
        [HttpPost]
        [Route("api/ValidUser")]
        public async Task<bool> Post([FromBody]UserModel usuario)
        {
            var response = await responseBusiness.ValidUser(usuario);

            if (response.Error)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, response.Message));
            }

            return response.ResultObject;
        }

        // PUT: api/ValidUserSm/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ValidUserSm/5
        public void Delete(int id)
        {
        }
    }
}
