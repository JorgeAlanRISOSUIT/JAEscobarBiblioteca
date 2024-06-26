using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.ModelBinding;

namespace LibroAPI.Controllers
{
    [EnableCors("https://localhost:44348/", "*", "*")]
    public class LibroController : ApiController
    {
        [HttpGet]
        [Route("Autor/{idAutor}")]
        public HttpResponseMessage ObtenerPorAutor(int idAutor, [QueryString]int pagina, [QueryString]int seccion)
        {
            ModelLayer.ResultDTO resultDTO = new ModelLayer.ResultDTO();
            var result = BusinessLayer.Libro.GetByIdAutor(idAutor, pagina, seccion);
            if (result.Item1)
            {
                resultDTO.Success = true;
                resultDTO.Message = string.Empty;
                resultDTO.Paginations = new List<List<object>>();
                foreach(var item in result.Item4.TempLibros)
                {
                    List<object> listaTemp = new List<object>();
                    foreach(var item2 in item)
                    {
                        listaTemp.Add(item2);
                    }
                    resultDTO.Paginations.Add(listaTemp);
                }
                return Request.CreateResponse(HttpStatusCode.OK, resultDTO);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, resultDTO);
            }
        }

        [HttpGet]
        [Route("Editorial/{nombre}")]
        public HttpResponseMessage ObtenerPorEditorial(string nombre, [QueryString]int seccion, [QueryString]int pagina)
        {
            ModelLayer.ResultDTO resultDTO = new ModelLayer.ResultDTO();
            var result = BusinessLayer.Libro.GetByEditorial(nombre, seccion, pagina);
            if (result.Item1)
            {
                resultDTO.Success = true;
                resultDTO.Message = string.Empty;
                resultDTO.Paginations = new List<List<object>>();
                foreach(var item in result.Item4.TempLibros)
                {
                    List<object> nombres = new List<object>();
                    foreach(var item2 in item)
                    {
                        nombres.Add(item2);
                    }
                    resultDTO.Paginations.Add(nombres);
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
