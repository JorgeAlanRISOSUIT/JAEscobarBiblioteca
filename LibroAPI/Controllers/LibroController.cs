using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
        public HttpResponseMessage ObtenerPorAutor(int idAutor)
        {
            ModelLayer.ResultDTO resultDTO = new ModelLayer.ResultDTO();
            var result = BusinessLayer.Libro.GetByIdAutor(idAutor);
            if (result.Item1)
            {
                resultDTO.Success = true;
                resultDTO.Message = string.Empty;
                resultDTO.Objects = new List<object>();
                foreach (var item in result.Item4.Libros)
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

        [HttpGet]
        [Route("Editorial/{nombre}")]
        public HttpResponseMessage ObtenerPorEditorial(string nombre)
        {
            ModelLayer.ResultDTO resultDTO = new ModelLayer.ResultDTO();
            var result = BusinessLayer.Libro.GetByEditorial(nombre);
            if (result.Item1)
            {
                resultDTO.Success = true;
                resultDTO.Message = string.Empty;
                resultDTO.Objects = new List<object>();
                foreach (var item in result.Item4.Libros)
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

        [HttpGet]
        [Route("Editorial/{fecha}")]
        public HttpResponseMessage ObtenerPorFecha(string fecha)
        {
            ModelLayer.ResultDTO resultDTO = new ModelLayer.ResultDTO();
            var resultDate = BusinessLayer.Libro.ConversionFecha(fecha);
            if (resultDate.Item1)
            {
                var resultGet = BusinessLayer.Libro.GetByDate(resultDate.Item4);
                if (resultGet.Item1)
                {
                    resultDTO.Success = true;
                    resultDTO.Message = string.Empty;
                    resultDTO.Objects = new List<object>();
                    foreach(var item in resultGet.Item4.Libros)
                    {
                        resultDTO.Objects.Add(item);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, resultDTO);
                }
                else
                {
                    resultDTO.Success = false;
                    resultDTO.Message = resultGet.Item2;
                    resultDTO.Error = resultGet.Item3;
                    return Request.CreateResponse(HttpStatusCode.BadRequest, resultDTO);
                }
            }
            else
            {
                resultDTO.Success = false;
                resultDTO.Message = resultDate.Item2;
                resultDTO.Error = resultDate.Item3;
                return Request.CreateResponse(HttpStatusCode.BadRequest, resultDTO);
            }
        }

        [HttpGet]
        [Route("Libro")]
        public HttpResponseMessage ObtenerPorTitulo([QueryString] string titulo)
        {
            ModelLayer.ResultDTO resultDTO = new ModelLayer.ResultDTO();
            var result = BusinessLayer.Libro.GetByTitulo(titulo);
            if (result.Item1)
            {
                resultDTO.Success = true;
                resultDTO.Message = string.Empty;
                resultDTO.Objects = new List<object>();
                foreach(var item in result.Item4.Libros)
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

        [HttpGet]
        [Route("Editorial/{idEditorial}")]
        public HttpResponseMessage ObtenerPorAutorEditorial([QueryString] int idAutor, int idEditorial)
        {
            ModelLayer.ResultDTO resultDTO = new ModelLayer.ResultDTO();
            var result = BusinessLayer.Libro.GetByAutorEditorial(idAutor, idEditorial);
            if (result.Item1)
            {
                resultDTO.Success = true;
                resultDTO.Message = string.Empty;
                resultDTO.Objects = new List<object>();
                foreach (var item in result.Item4.Libros)
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

        [HttpDelete]
        [Route("Autor/{idAutor}")]
        public HttpResponseMessage EliminarPorAutor(int idAutor)
        {
            ModelLayer.ResultDTO resultDTO = new ModelLayer.ResultDTO();
            var result = BusinessLayer.Libro.BorrarLibroPorAutor(idAutor);
            if (result.Item1)
            {
                resultDTO.Success = true;
                resultDTO.Message = result.Item2;
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

        [HttpDelete]
        [Route("Editorial/{idEditorial}")]
        public HttpResponseMessage EliminarPorEditorial(int idEditorial)
        {
            ModelLayer.ResultDTO resultDTO = new ModelLayer.ResultDTO();
            var result = BusinessLayer.Libro.BorrarLibroPorEditorial(idEditorial);
            if (result.Item1)
            {
                resultDTO.Success = true;
                resultDTO.Message = string.Empty;
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
