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
			ModelLayer.ResultDTO resultDTO = new ModelLayer.ResultDTO();
			var result = BusinessLayer.Editorial.GetEditoriales();
			if (result.Item1)
			{
				resultDTO.Success = true;
				resultDTO.Objects = new List<object>();
				foreach (ModelLayer.Editorial item in result.Item4.Editoriales)
				{
					resultDTO.Objects.Add(item);
				}
				return Request.CreateResponse(HttpStatusCode.OK, resultDTO);
			}
			else
			{
				resultDTO.Success = false;
				resultDTO.Message = result.Item2;
				resultDTO.Error = result.Item3;
				return Request.CreateResponse(HttpStatusCode.BadRequest, resultDTO);
			}
		}
	}
}