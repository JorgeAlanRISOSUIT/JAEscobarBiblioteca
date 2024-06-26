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
        public static (bool, string, Exception, ModelLayer.PaginationDTO) GetByIdAutor(int idAutor, int pagina, int seccion)
        {
            try
            {
                ModelLayer.PaginationDTO pagination = new ModelLayer.PaginationDTO();
                using (DataLayer.JAEscobarLibrosEntities context = new DataLayer.JAEscobarLibrosEntities())
                {
                    var result = context.ConsultaPorAutor(idAutor).ToList();
                    if (result.Count > 0)
                    {
                        pagination.CantidadElementos = result.Count;
                        pagination.SeleccionElementos = seccion;
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
                            pagination.Libros.Add(libro);
                        }
                        return (true, "", null, pagination);
                    }
                    else
                    {
                        return (false, "No hay disponibilidad en este momento", null, null);
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
                    if (result.Count > 0)
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

        public static (bool, string, Exception, ModelLayer.PaginationDTO) GetByEditorial(string nombre, int seccion, int pagina)
        {
            try
            {
                ModelLayer.PaginationDTO pagination = new ModelLayer.PaginationDTO();
                using (DataLayer.JAEscobarLibrosEntities context = new DataLayer.JAEscobarLibrosEntities())
                {
                    var result = context.ConsultaPorEditorial(nombre).ToList();
                    if (result.Count > 0)
                    {
                        pagination.CantidadElementos = result.Count;
                        pagination.SeleccionElementos = seccion;
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
                                    IdEditorial = item.IdEditorial,
                                    Nombre = item.Editorial
                                }
                            };
                            pagination.Libros.Add(objLibro);
                        }
                        return (true, "", null, pagination);
                    }
                    else
                    {
                        return (false, "No se puede encontrar el resultado", null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex, null);
            }
        }

        public static (bool, string, Exception, ModelLayer.Libro) GetByAutorEditorial(int idAutor, int idEditorial)
        {
            try
            {
                ModelLayer.Libro model = new ModelLayer.Libro { Libros = new List<ModelLayer.Libro>() };
                using (DataLayer.JAEscobarLibrosEntities context = new DataLayer.JAEscobarLibrosEntities())
                {
                    var result = context.ConsultaPorAutorEditorial(idAutor, idEditorial).ToList();
                    if (result.Count > 0)
                    {
                        foreach (DataLayer.ConsultaPorAutorEditorial_Result item in result)
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
                                    Nombre = item.Editorial
                                }
                            };
                        }
                        return (true, "", null, model);
                    }
                    else
                    {
                        return (false, "No existen libros a mostrar", null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex, null);
            }
        }

        public static (bool, string, Exception) BorrarLibroPorAutor(int idAutor)
        {
            try
            {
                using (DataLayer.JAEscobarLibrosEntities context = new DataLayer.JAEscobarLibrosEntities())
                {
                    var result = context.BorrarLibroPorAutor(idAutor);
                    if (result > 0)
                    {
                        return (true, "Se ha eliminado el libro a referencia del autor", null);
                    }
                    else
                    {
                        return (false, "No se puede eliminar este libro", null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex);
            }
        }

        public static (bool, string, Exception) BorrarLibroPorEditorial(int idEditorial)
        {
            try
            {
                using (DataLayer.JAEscobarLibrosEntities context = new DataLayer.JAEscobarLibrosEntities())
                {
                    int result = context.BorrarLibroEditorial(idEditorial);
                    if (result > 0)
                    {
                        return (true, "Se a elimnado el libro por la editorial", null);
                    }
                    else
                    {
                        return (false, "No se pudo eliminar por editorial", null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public static (bool, string, Exception, ModelLayer.Libro) AcomodarLibro(int pagina, int seccion)
        {
            try
            {
                ModelLayer.Libro libro = new ModelLayer.Libro() { Libros = new List<ModelLayer.Libro>() };
                using (DataLayer.JAEscobarLibrosEntities context = new DataLayer.JAEscobarLibrosEntities())
                {
                    var result = context.ObtenerLibros().ToList();
                    if (result.Count > 0)
                    {
                        ModelLayer.PaginationDTO pagination = new ModelLayer.PaginationDTO();
                        pagination.CantidadElementos = result.Count;
                        pagination.SeleccionElementos = seccion;
                        foreach (var item in result)
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
                                    Nombre = item.Editorial
                                }
                            };
                            pagination.Libros.Add(objLibro);
                        }
                        libro.Libros = pagination.TempLibros.ElementAt(pagina - 1 < 0 ? 0 : pagina - 1);
                        return (true, "", null, libro);
                    }
                    else
                    {
                        return (false, "", null, null);
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
