using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class Libro
    {
        public string ISN { get; set; }
        public string Titulo { get; set; }
        public int Total_Paginas { get; set; }
        public DateTime Fecha_Publicacion { get; set; }
        public Editorial Editorial { get; set; }
        public Autor Autor { get; set; }
        public List<Libro> Libros { get; set; }

    }
}
