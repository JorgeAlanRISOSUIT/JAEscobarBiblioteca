using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Security;

namespace LibroAPI.Controllers
{
	[EnableCors("https://localhost:44348/", "*", "*")]
	public class AutorController : ApiController
	{
		[HttpGet]
		[Route("Autores")]
		public HttpResponseMessage ObtenerAutores()
		{
			ModelLayer.ResultDTO modelDTO = new ModelLayer.ResultDTO();
			var result = BusinessLayer.Autor.GetAutores();
			if (result.Item1)
			{
				modelDTO.Success = true;
				modelDTO.Objects = new List<object>();
				foreach (ModelLayer.Autor item in result.Item4.Autores)
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