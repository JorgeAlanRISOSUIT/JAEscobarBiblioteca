using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
	public class PaginationDTO
	{
		public int CantidadElementos { get; set; }
		public int SeleccionElementos { get; set; }
		public int CantidadTempPaginas
		{
			get
			{
				if (CantidadElementos > 0)
				{
					decimal valor = CantidadElementos / SeleccionElementos;
					return (int)Math.Floor(valor);
				}
				else return 0;
			}
			set
			{
				CantidadTempPaginas = (int)Math.Floor(Convert.ToDecimal(CantidadElementos / value));
			}
		}
		public List<Libro> Libros { get; set; } = new List<Libro>();

		public List<List<Libro>> TempLibros
		{
			get
			{
				if (Libros.Count == 0)
				{
					return new List<List<Libro>>();
				}
				else
				{
					List<List<Libro>> temp = new List<List<Libro>>();
					if (SeleccionElementos < CantidadElementos)
					{
						for (int i = (SeleccionElementos - 1); i < CantidadElementos; i += SeleccionElementos)
						{
							List<Libro> temp2 = new List<Libro>();
							for (int j = (i - (SeleccionElementos - 1)); j <= i; j++)
							{
								temp2.Add(Libros[j]);
								if (j == (i))
								{
									temp.Add(temp2);
								}
							}
						}
					}
					else
					{
						for (int i = (CantidadElementos - 1); i < CantidadElementos; i++)
						{
							List<Libro> temp2 = new List<Libro>();
							for (int j = i - CantidadElementos; j <= i; j++)
							{
								temp2.Add(Libros[j]);
								if (j == (i))
								{
									temp.Add(temp2);
								}
							}
						}
					}
					return temp;
				}
			}
			set { TempLibros = value; }
		}

	}
}
