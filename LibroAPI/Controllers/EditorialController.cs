using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace LibroAPI.Controllers
{
	[EnableCors("https://localhost:44348/", "*", "*")]
	public class EditorialController : ApiController
    {
        [HttpGet]
        [Route("Editoriales")]
        public HttpResponseMessage ObtenerEditoriales()
        {
            ModelLayer.ResultDTO modelDTO = new ModelLayer.ResultDTO();
            var result = BusinessLayer.Editorial.GetEditoriales();
            if (result.Item1)
            {
                modelDTO.Success = true;
                modelDTO.Objects = new List<object>();
                foreach (ModelLayer.Editorial item in result.Item4.Editoriales)
                {
                    modelDTO.Objects.Add(item);
                }
                return Request.CreateResponse(HttpStatusCode.OK, modelDTO);
            }
            else
            {
                modelDTO.Success = false;
                modelDTO.Message = result.Item2;
                modelDTO.Error = result.Item3;
                return Request.CreateResponse(HttpStatusCode.BadRequest, modelDTO);
            }
        }
    }
}
