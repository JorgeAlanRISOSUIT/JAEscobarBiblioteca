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
            ModelLayer.ResultDTO modelDTO = getInformation(null);
            List<object> lista = new List<object>();
            foreach (var item in modelDTO.Objects)
            {
                lista.Add(JsonConvert.DeserializeObject<ModelLayer.Libro>(item.ToString()));
            }
            modelDTO.Objects = lista;
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
            return Json(getInformation(ISBN).Object, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CargarLibros()
        {
            ModelLayer.ResultDTO modelDTO = getInformation(null);
            List<object> lista = new List<object>();
            foreach (var item in modelDTO.Objects)
            {
                lista.Add(JsonConvert.DeserializeObject<ModelLayer.Libro>(item.ToString()));
            }
            modelDTO.Objects = lista;
            return Json(modelDTO, JsonRequestBehavior.AllowGet);
        }

        private ModelLayer.ResultDTO getInformation(string ISBN)
        {
            if (string.IsNullOrEmpty(ISBN))
            {
                using (HttpClient cliente = new HttpClient())
                {
                    HttpResponseMessage mensaje = cliente.GetAsync(ConfigurationManager.AppSettings["LibrosApi"]).GetAwaiter().GetResult();
                    if (mensaje.IsSuccessStatusCode)
                    {
                        string body = mensaje.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        ModelLayer.ResultDTO resultDTO = JsonConvert.DeserializeObject<ModelLayer.ResultDTO>(body);
                        return resultDTO;
                    }
                    else
                    {
                        ModelLayer.ResultDTO result = new ModelLayer.ResultDTO();
                        result.Message = "No existe el resultado";
                        return result;
                    }
                }
            }
            else
            {
                using (HttpClient cliente = new HttpClient())
                {
                    HttpResponseMessage mensaje = cliente.GetAsync($"{ConfigurationManager.AppSettings["ObtenerLibro"]}/{ISBN}").GetAwaiter().GetResult();
                    if (mensaje.IsSuccessStatusCode)
                    {
                        string body = mensaje.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        ModelLayer.ResultDTO modelDTO = JsonConvert.DeserializeObject<ModelLayer.ResultDTO>(body);
                        return modelDTO;
                    }
                    else
                    {
                        ModelLayer.ResultDTO modelDTO = new ModelLayer.ResultDTO();
                        modelDTO.Success = false;
                        modelDTO.Message = "No existe el valor";
                        return modelDTO;
                    }
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
                    ModelLayer.ResultDTO modelDTO = new ModelLayer.ResultDTO();
                    modelDTO.Success = false;
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
                    ModelLayer.ResultDTO resultDTO = new ModelLayer.ResultDTO();
                    resultDTO.Message = "No se encontro la api correctamente";
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
                    ModelLayer.ResultDTO resultDTO = new ModelLayer.ResultDTO();
                    resultDTO.Message = "No se encontro la api correctamente";
                    return Json(resultDTO);
                }
            }
        }
    }
}