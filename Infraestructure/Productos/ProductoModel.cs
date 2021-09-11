using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Productos
{
    public class ProductoModel
    {
        private Producto[] productos;

        #region CRUD
        public void Add(Producto p)
        {
            if (p == null)
            {
                throw new ArgumentException("El producto no puede ser null.");
            }
            Add(p, ref productos);
        }

        public int Update(Producto p)
        {
            if(p == null)
            {
                throw new ArgumentException("El producto no puede ser 0");
            }
            int index = GetIndexById(p.Id);
            if (index < 0)
            {
                throw new Exception($"El producto co id:{p.Id} no existe");
            }

            productos[index] = p;

            return index;
        }

        public bool Delete(Producto p)
        {
            if (p == null)
            {
                throw new ArgumentException("El producto no puede ser 0");
            }
            int index = GetIndexById(p.Id);
            if (index < 0)
            {
                throw new Exception($"El producto co id:{p.Id} no existe");
            }
            if(index!=(productos.Length - 1))
            {
                productos[index] = productos[productos.Length - 1];
            }
            Producto[] tmp = new Producto[productos.Length - 1];
            Array.Copy(productos, tmp, tmp.Length);
            productos = tmp;

            return productos.Length == tmp.Length;
        }
        public Producto[] GetAll()
        {
            return productos;
        }

        #endregion

        #region Queries
        public Producto GetProductoById(int id)
        {
            int index = GetIndexById(id);
            return index < 0 ? null : productos[index];
        }

        public Producto[] GetProductosByUnidadMedida(UnidadMedida um)
        {
            Producto[] tmp = null;
            if(productos == null)
            {
                return tmp;
            }
            foreach(Producto p in productos)
            {
                if(p.UnidadMedida == um)
                {
                    Add(p, ref tmp);
                }
            }
            return tmp;
        }

        public Producto[] GetProductoByRangoPrecio(decimal from, decimal to)
        {
            Producto[] tmp = null;
            if (productos == null)
            {
                return tmp;
            }
            foreach (Producto p in productos)
            {
                if (p.Precio >= from && p.Precio <= to)
                {
                    Add(p, ref tmp);
                }
            }
            return tmp;
        }

        public Producto[] GetProductoByVencimiento(DateTime dt)
        {
            Producto[] tmp = null;
            if (productos == null)
            {
                return tmp;
            }
            foreach (Producto p in productos)
            {
                if (p.FechaVencimiento.CompareTo(dt) <= 0)
                {
                    Add(p, ref tmp);
                }
            }
            return tmp;
        }

        public int GetLastProductoId()
        {
            return productos == null ? 0 : productos[productos.Length - 1].Id;
        }

        public string GetProductoAsJason()
        {
            return JsonConvert.
        }
        #endregion

        #region Private method

        private void Add(Producto p, ref Producto[] pds)
        {
            if(pds == null)
            {
                pds = new Producto[1];
                pds[pds.Length - 1] = p;
                return;
            }

            Producto[] tmp = new Producto[pds.Length + 1];
            Array.Copy(pds, tmp, pds.Length);
            tmp[tmp.Length - 1] = p;
            pds = tmp;
        }

        private int GetIndexById(int id)
        {
            int index = int.MinValue, i = 0;
            if(id <= 0)
            {
                throw new ArgumentException($"El id: {id} no puede ser negativo o 0");
            }

            if (productos == null)
            {
                return index;
            }

            foreach(Producto p in productos)
            {
                if(p.Id == id)
                {
                    index = i;
                    break;
                }
                i++;
            }
            return index;
        }
        #endregion
    }
}
