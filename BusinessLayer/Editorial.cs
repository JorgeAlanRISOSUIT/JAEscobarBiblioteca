using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
	public class Editorial
	{
		public static (bool, string, Exception, ModelLayer.Editorial) GetEditoriales()
		{
			try
			{
				ModelLayer.Editorial model = new ModelLayer.Editorial { Editoriales = new List<ModelLayer.Editorial>() };
				using (DataLayer.JAEscobarLibrosEntities context = new DataLayer.JAEscobarLibrosEntities())
				{
					List<DataLayer.ObtenerEditoriales_Result> editorial =  context.ObtenerEditoriales().ToList();
					if(editorial.Count > 0)
					{
						foreach (DataLayer.ObtenerEditoriales_Result item in editorial)
						{
							ModelLayer.Editorial objEditorial = new ModelLayer.Editorial
							{
								IdEditorial = item.IdEditorial,
								Nombre = item.Nombre
							};
							model.Editoriales.Add(objEditorial);
						}
						return (true, string.Empty, null, model);
					}
					else
					{
						return (false, "No hay Editoriales Disponibles", null, null);
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
