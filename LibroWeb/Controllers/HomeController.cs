using LibroWeb.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace LibroWeb.Controllers
{
    [AllowEnableCors]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ModelLayer.ResultDTO modelDTO = new ModelLayer.ResultDTO();
            ModelLayer.Libro modelLibro = GetLibros(null);
            ModelLayer.Autor modelAutor = GetAutores();
            ModelLayer.Editorial modelEditorial = GetEditoriales();
            modelLibro.Autor = modelAutor;
            modelLibro.Editorial = modelEditorial;
            modelDTO.Success = modelLibro.Libros.Count > 0;
            modelDTO.Object = modelLibro;
            return View(modelDTO);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult ObtenerLibro(string ISBN)
        {
            return Json(GetLibros(ISBN), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerPorTitulo(string titulo)
        {
            ModelLayer.Libro libro = GetLibroPorTitulo(titulo);
            return Json(libro, JsonRequestBehavior.AllowGet);
        }

        private ModelLayer.Libro GetLibros(string ISBN)
        {
            ModelLayer.Libro libro = new ModelLayer.Libro { Libros = new List<ModelLayer.Libro>() };
            if (!string.IsNullOrEmpty(ISBN))
            {
                using (HttpClient cliente = new HttpClient())
                {
                    HttpResponseMessage mensaje = cliente.GetAsync($"{ConfigurationManager.AppSettings["ObtenerLibro"]}/{ISBN}").GetAwaiter().GetResult();
                    if (mensaje.IsSuccessStatusCode)
                    {
                        string body = mensaje.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        ModelLayer.ResultDTO resultDTO = JsonConvert.DeserializeObject<ModelLayer.ResultDTO>(body);
                        libro = JsonConvert.DeserializeObject<ModelLayer.Libro>(resultDTO.Object.ToString());
                        return libro;
                    }
                    else
                    {
                        return libro;
                    }
                }
            }
            else
            {
                using (HttpClient cliente = new HttpClient())
                {
                    HttpResponseMessage mensaje = cliente.GetAsync($"{ConfigurationManager.AppSettings["LibrosApi"]}").GetAwaiter().GetResult();
                    if (mensaje.IsSuccessStatusCode)
                    {
                        string body = mensaje.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        ModelLayer.ResultDTO modelDTO = JsonConvert.DeserializeObject<ModelLayer.ResultDTO>(body);
                        foreach (object item in modelDTO.Objects)
                        {
                            libro.Libros.Add(JsonConvert.DeserializeObject<ModelLayer.Libro>(item.ToString()));
                        }
                        return libro;
                    }
                    else
                    {
                        return libro;
                    }
                }
            }
        }

        private ModelLayer.Editorial GetEditoriales()
        {
            ModelLayer.Editorial model = new ModelLayer.Editorial() { Editoriales = new List<ModelLayer.Editorial>() };
            using (HttpClient cliente = new HttpClient())
            {
                HttpResponseMessage mensaje = cliente.GetAsync(ConfigurationManager.AppSettings["Editoriales"]).GetAwaiter().GetResult();
                if (mensaje.IsSuccessStatusCode)
                {
                    string body = mensaje.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    ModelLayer.ResultDTO modelDTO = JsonConvert.DeserializeObject<ModelLayer.ResultDTO>(body);
                    foreach (object item in modelDTO.Objects)
                    {
                        model.Editoriales.Add(JsonConvert.DeserializeObject<ModelLayer.Editorial>(item.ToString()));
                    }
                    return model;
                }
                else
                {
                    return model;
                }
            }
        }

        private ModelLayer.Libro GetLibroPorTitulo(string titulo)
        {
            ModelLayer.Libro objLibro = new ModelLayer.Libro { Libros = new List<ModelLayer.Libro>() };
            using (HttpClient cliente = new HttpClient())
            {
                HttpResponseMessage mensaje = cliente.GetAsync(ConfigurationManager.AppSettings["Titulo"]).GetAwaiter().GetResult();
                if (mensaje.IsSuccessStatusCode)
                {
                    string body = mensaje.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    ModelLayer.ResultDTO modelDTO = JsonConvert.DeserializeObject<ModelLayer.ResultDTO>(body);
                    foreach (object item in modelDTO.Objects)
                    {
                        objLibro.Libros.Add(JsonConvert.DeserializeObject<ModelLayer.Libro>(item.ToString()));
                    }
                    return objLibro;
                }
                return objLibro;
            }
        }

        private ModelLayer.Autor GetAutores()
        {
            ModelLayer.Autor autor = new ModelLayer.Autor { Autores = new List<ModelLayer.Autor>() };
            using (HttpClient cliente = new HttpClient())
            {
                HttpResponseMessage mensaje = cliente.GetAsync(ConfigurationManager.AppSettings["Autores"]).GetAwaiter().GetResult();
                if (mensaje.IsSuccessStatusCode)
                {
                    string body = mensaje.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    ModelLayer.ResultDTO modelDTO = JsonConvert.DeserializeObject<ModelLayer.ResultDTO>(body);
                    foreach (object item in modelDTO.Objects)
                    {
                        autor.Autores.Add(JsonConvert.DeserializeObject<ModelLayer.Autor>(item.ToString()));
                    }
                    return autor;
                }
                else
                {
                    return autor;
                }
            }
        }

        [HttpPost]
        public JsonResult AgregarLibro(ModelLayer.Libro libro)
        {
            using (HttpClient cliente = new HttpClient())
            {
                StringContent value = new StringContent(JsonConvert.SerializeObject(libro), Encoding.UTF8, "application/json");
                HttpResponseMessage mensaje = cliente.PostAsync(ConfigurationManager.AppSettings["NuevoLibro"], value).GetAwaiter().GetResult();
                if (mensaje.IsSuccessStatusCode)
                {
                    string body = mensaje.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    ModelLayer.ResultDTO resultDTO = JsonConvert.DeserializeObject<ModelLayer.ResultDTO>(body);
                    return Json(resultDTO);
                }
                else
                {
                    ModelLayer.ResultDTO modelDTO = new ModelLayer.ResultDTO
                    {
                        Success = false
                    };
                    return Json(modelDTO);
                }
            }
        }

        [HttpDelete]
        public async Task<JsonResult> EliminarInformacion(string ISBN)
        {
            using (HttpClient cliente = new HttpClient())
            {
                var result = await cliente.DeleteAsync($"{ConfigurationManager.AppSettings["EliminarLibro"]}/{ISBN}");
                if (result.IsSuccessStatusCode)
                {
                    string body = await result.Content.ReadAsStringAsync();
                    ModelLayer.ResultDTO resultDTO = JsonConvert.DeserializeObject<ModelLayer.ResultDTO>(body);
                    return Json(resultDTO, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ModelLayer.ResultDTO resultDTO = new ModelLayer.ResultDTO() { Success = false, Message = "No se encontro la API correctamente" };
                    return Json(resultDTO);
                }
            }
        }

        [HttpPut]
        public async Task<JsonResult> ActualizarLibro(ModelLayer.Libro libro)
        {
            using (HttpClient cliente = new HttpClient())
            {
                StringContent jsonContent = new StringContent(JsonConvert.SerializeObject(libro), Encoding.UTF8, "application/json");
                HttpResponseMessage body = await cliente.PutAsync(ConfigurationManager.AppSettings["ActualizarLibro"], jsonContent);
                if (body.IsSuccessStatusCode)
                {
                    string result = await body.Content.ReadAsStringAsync();
                    ModelLayer.ResultDTO modelDTO = JsonConvert.DeserializeObject<ModelLayer.ResultDTO>(result);
                    return Json(modelDTO);
                }
                else
                {
                    ModelLayer.ResultDTO resultDTO = new ModelLayer.ResultDTO() { Success = false, Message = "No se encontro la api correctamente" };
                    return Json(resultDTO);
                }
            }
        }
    }
}