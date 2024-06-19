using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class Autor
    {
        public int IdAutor { get; set; }
        public string Nombre { get; set; }
        public List<Autor> Autores { get; set; }
    }
}
