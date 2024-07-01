using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Autor
    {
        public static (bool, string, Exception, ModelLayer.Autor) GetAutores()
        {
			try
			{
				ModelLayer.Autor model = new ModelLayer.Autor { Autores = new List<ModelLayer.Autor>() };
				using (DataLayer.JAEscobarLibrosEntities context = new DataLayer.JAEscobarLibrosEntities())
				{
					List<DataLayer.ObtenerAutores_Result> result = context.ObtenerAutores().ToList();
					if(result.Count > 0)
					{
						foreach (DataLayer.ObtenerAutores_Result item in result)
						{
							ModelLayer.Autor autores = new ModelLayer.Autor
							{
								IdAutor = item.IdAutor,
								Nombre = item.Nombre,
							};
							model.Autores.Add(autores);
						}
						return (true, string.Empty, null, model);
					}
					else
					{
						return (false, "No existen autores", null, null);
					}
				}
			}
			catch (Exception ex)
			{
				return (false, ex.Message, ex, null);
			}
        }
    }
}
