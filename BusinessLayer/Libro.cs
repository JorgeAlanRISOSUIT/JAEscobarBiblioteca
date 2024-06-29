using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Libro
    {
        public static (bool, string, Exception, ModelLayer.Libro) GetAllLibros()
        {
            try
            {
                ModelLayer.Libro model = new ModelLayer.Libro { Libros = new List<ModelLayer.Libro>() };
                using (DataLayer.JAEscobarLibrosEntities context = new DataLayer.JAEscobarLibrosEntities())
                {
                    var result = context.ObtenerTodos().ToList();
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
                                Editorial = new ModelLayer.Editorial
                                {
                                    IdEditorial = item.IdEditorial,
                                    Nombre = item.Editorial
                                },
                                Autor = new ModelLayer.Autor
                                {
                                    IdAutor = item.IdAutor,
                                    Nombre = item.Autor
                                }
                            };
                            model.Libros.Add(objLibro);
                        }
                        return (true, "", null, model);
                    }
                    else
                    {
                        return (false, "No existen leyes de datos", null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex, null);
            }
        }

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
                        foreach (var item in result)
                        {
                            ModelLayer.Libro objLibro = new ModelLayer.Libro
                            {
                                ISN = item.ISN,
                                Titulo = item.Titulo,
                                Fecha_Publicacion = item.Fecha_Publicacion,
                                Total_Paginas = item.Total_Paginas,
                                Editorial = new ModelLayer.Editorial
                                {
                                    IdEditorial = item.IdEditorial,
                                    Nombre = item.Editorial
                                },
                                Autor = new ModelLayer.Autor
                                {
                                    IdAutor = item.IdAutor,
                                    Nombre = item.Nombre
                                }
                            };
                            model.Libros.Add(objLibro);
                        }

                        return (true, "", null, model);
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

        public static (bool, string, Exception, ModelLayer.Libro) GetByEditorial(string nombre)
        {
            try
            {
                ModelLayer.Libro model = new ModelLayer.Libro { Libros = new List<ModelLayer.Libro>() };
                using (DataLayer.JAEscobarLibrosEntities context = new DataLayer.JAEscobarLibrosEntities())
                {
                    var result = context.ConsultaPorEditorial(nombre).ToList();
                    if (result.Count > 0)
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
                                    IdEditorial = item.IdEditorial,
                                    Nombre = item.Editorial
                                }
                            };
                            model.Libros.Add(objLibro);
                        }
                        return (true, "", null, model);
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

        public static (bool, string, Exception, DateTime) ConversionFecha(string fecha)
        {
            try
            {
                DateTime conversion = new DateTime();
                if (DateTime.TryParse(fecha, CultureInfo.InvariantCulture, DateTimeStyles.None, out conversion))
                {
                    return (true, "", null, Convert.ToDateTime(conversion.ToString("yyyy/MM/dd")));
                }
                else
                {
                    return (false, "", null, new DateTime(0, 0, 0));
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex, new DateTime(0, 0, 0));
                throw;
            }
        }

        public static (bool, string, Exception) UpdateLibro(ModelLayer.Libro libro)
        {
            try
            {
                using (DataLayer.JAEscobarLibrosEntities context = new DataLayer.JAEscobarLibrosEntities())
                {
                    int result = context.ActualizarLibro(libro.ISN, libro.Titulo, libro.Total_Paginas, libro.Fecha_Publicacion, libro.Editorial.IdEditorial, libro.Autor.IdAutor);
                    if (result > 0)
                    {
                        return (true, "Se ha actualizado el libro", null);
                    }
                    else
                    {
                        return (false, "No se encuentra en la lista", null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex);
            }
        }

        public static (bool, string, Exception, ModelLayer.Libro) ObtenerLibro(string ISBN)
        {
            try
            {
                using (DataLayer.JAEscobarLibrosEntities context = new DataLayer.JAEscobarLibrosEntities())
                {
                    DataLayer.ObtenerLibro_Result result =  context.ObtenerLibro(ISBN).SingleOrDefault();
                    if(result != null)
                    {
                        ModelLayer.Libro objLibro = new ModelLayer.Libro
                        {
                            ISN = result.ISN,
                            Titulo = result.Titulo,
                            Fecha_Publicacion = result.Fecha_Publicacion,
                            Total_Paginas = result.Total_Paginas,
                            Editorial = new ModelLayer.Editorial
                            {
                                IdEditorial = result.IdEditorial,
                                Nombre = result.Editorial
                            },
                            Autor = new ModelLayer.Autor
                            {
                                IdAutor = result.IdAutor,
                                Nombre = result.Autor,
                            }
                        };
                        return (true, "", null, objLibro);
                    }
                    else
                    {
                        return (false, "No existe este libro en el catalogo", null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex, null);
            }
        }

        public static (bool, string, Exception) AddLibro(ModelLayer.Libro libro)
        {
            try
            {
                using (DataLayer.JAEscobarLibrosEntities context = new DataLayer.JAEscobarLibrosEntities())
                {
                    int result = context.CrearLibro(libro.ISN, libro.Titulo, libro.Total_Paginas, libro.Fecha_Publicacion, libro.Editorial.IdEditorial, libro.Autor.IdAutor);
                    if(result > 0)
                    {
                        return (true, "Se ha agregado el libro al catalogo", null);
                    }
                    else
                    {
                        return (false, "No se ha agregado el libro al catalogo", null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex);
            }
        }
    }
}
