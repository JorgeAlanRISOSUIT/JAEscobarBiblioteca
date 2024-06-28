using LibroWeb.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LibroWeb.Controllers
{
    [AllowEnableCors]
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            using (HttpClient cliente = new HttpClient())
            {
                var result = await cliente.GetAsync("https://localhost:44367/Libros");
                if (result.IsSuccessStatusCode)
                {
                    var body = await result.Content.ReadAsStringAsync();
                    ModelLayer.ResultDTO resultDTO = JsonConvert.DeserializeObject<ModelLayer.ResultDTO>(body);
                    List<object> lista = new List<object>();
                    foreach (var item in resultDTO.Objects)
                    {
                        lista.Add(JsonConvert.DeserializeObject<ModelLayer.Libro>(item.ToString()));
                    }
                    resultDTO.Objects = lista;
                    return View(resultDTO);
                }
                else
                {
                    var body = await result.Content.ReadAsStringAsync();
                    ModelLayer.ResultDTO resultDTO = JsonConvert.DeserializeObject<ModelLayer.ResultDTO>(body);
                    return View(resultDTO);
                }
            }
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
    }
}