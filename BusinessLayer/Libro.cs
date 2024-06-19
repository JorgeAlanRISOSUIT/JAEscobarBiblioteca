using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Libro
    {
        public static (bool, string, Exception, ModelLayer.Libro) GetByIdAutor(int idAutor)
        {
            try
            {
                ModelLayer.Libro model = new ModelLayer.Libro { Libros = new List<ModelLayer.Libro>() };
                using (DataLayer.JAEscobarLibrosEntities context = new DataLayer.JAEscobarLibrosEntities())
                {
                    var result = context.ConsultaPorAutor(idAutor).ToList();
                    if (result.Count > 0)
                    {
                        foreach (var info in result)
                        {
                            ModelLayer.Libro libro = new ModelLayer.Libro
                            {
                                ISN = info.ISN,
                                Titulo = info.Titulo,
                                Fecha_Publicacion = info.Fecha_Publicacion,
                                Total_Paginas = info.Total_Paginas,
                                Autor = new ModelLayer.Autor
                                {
                                    IdAutor = info.IdAutor,
                                    Nombre = info.Nombre,
                                },
                                Editorial = new ModelLayer.Editorial
                                {
                                    IdEditorial = info.IdEditorial,
                                    Nombre = info.Editorial,
                                }
                            };
                            model.Libros.Add(libro);
                        }
                        return (true, "", null, model);
                    }
                    else
                    {
                        return (false, "No hay disponibilidad en este momento", null, model);
                    }
                }
            }
            catch (Exception e)
            {
                return (false, e.Message, e, null);
            }
        }

        public static (bool, string, Exception, ModelLayer.Libro) GetByTitulo(string titulo)
        {
            try
            {
                ModelLayer.Libro model = new ModelLayer.Libro { Libros = new List<ModelLayer.Libro>() };
                using (DataLayer.JAEscobarLibrosEntities context = new DataLayer.JAEscobarLibrosEntities())
                {
                    var result = context.ConsultaPorTitulo(titulo).ToList();
                    if (result.Count > 0)
                    {
                        foreach (var item in result)
                        {
                            ModelLayer.Libro objLibro = new ModelLayer.Libro
                            {
                                ISN = item.ISN,
                                Titulo = item.Titulo,
                                Total_Paginas = item.Total_Paginas,
                                Fecha_Publicacion = item.Fecha_Publicacion,
                                Autor = new ModelLayer.Autor
                                {
                                    IdAutor = item.IdAutor,
                                    Nombre = item.Autor,
                                },
                                Editorial = new ModelLayer.Editorial
                                {
                                    IdEditorial = item.IdEditorial,
                                    Nombre = item.Nombre
                                }
                            };
                            model.Libros.Add(objLibro);
                        }
                        return (true, "", null, model);
                    }
                    else
                    {
                        return (false, "", null, null);
                    }
                }
            }
            catch (Exception e)
            {
                return (false, e.Message, e, null);
            }
        }

        public static (bool, string, Exception, ModelLayer.Libro) GetByDate(DateTime date)
        {
            try
            {
                ModelLayer.Libro model = new ModelLayer.Libro { Libros = new List<ModelLayer.Libro>() };
                using (DataLayer.JAEscobarLibrosEntities context = new DataLayer.JAEscobarLibrosEntities())
                {
                    var result = context.ConsultaPorFecha(date).ToList();
                    if(result.Count > 0)
                    {
                        foreach (DataLayer.ConsultaPorFecha_Result item in result)
                        {
                            ModelLayer.Libro objLibro = new ModelLayer.Libro
                            {
                                ISN = item.ISN,
                                Titulo = item.Titulo,
                                Fecha_Publicacion = item.Fecha_Publicacion,
                                Total_Paginas = item.Total_Paginas,
                                Autor = new ModelLayer.Autor
                                {
                                    IdAutor = item.IdAutor,
                                    Nombre = item.Autor
                                },
                                Editorial = new ModelLayer.Editorial
                                {
                                    IdEditorial = item.IdEditorial,
                                    Nombre = item.Nombre
                                },
                            };
                            model.Libros.Add(objLibro);
                        }
                        return (true, "", null, model);
                    }
                    else
                    {
                        return (false, "No existen estos productos", null, null);
                    }
                }
            }
            catch (Exception e)
            {
                return (false, e.Message, e, null);
            }
        }

        public static (bool, string, Exception, ModelLayer.Libro) GetByEditorial(string nombre)
        {
            try
            {
                ModelLayer.Libro model = new ModelLayer.Libro { Libros = new List<ModelLayer.Libro>() };
                using (DataLayer.JAEscobarLibrosEntities context = new DataLayer.JAEscobarLibrosEntities())
                {
                    var result = context.ConsultaPorEditorial(nombre).ToList();
                    if(result.Count > 0)
                    {
                        foreach (DataLayer.ConsultaPorEditorial_Result item in result)
                        {
                            ModelLayer.Libro objLibro = new ModelLayer.Libro
                            {
                                ISN = item.ISN,
                                Titulo = item.Titulo,
                                Fecha_Publicacion = item.Fecha_Publicacion,
                                Total_Paginas = item.Total_Paginas,
                                Autor = new ModelLayer.Autor
                                {
                                    IdAutor = item.IdAutor,
                                    Nombre = item.Autor
                                },
                                Editorial = new ModelLayer.Editorial
                                {
                                    IdEditorial 
                                }
                            }
                        }
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
