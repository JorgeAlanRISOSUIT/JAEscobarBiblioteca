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
				using (DataLayer.JAEscobarLibrosEntities context = new DataLayer.JAEscobarLibrosEntities())
				{
					List<DataLayer.ObtenerAutores_Result> result = context.ObtenerAutores().ToList();
					if(result.Count > 0)
					{

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
