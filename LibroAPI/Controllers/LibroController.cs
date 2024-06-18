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
    public class LibroController : ApiController
    {
    }
}
