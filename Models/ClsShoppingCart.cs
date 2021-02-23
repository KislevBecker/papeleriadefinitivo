using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramaDeRequisas.Models
{
    public class ClsShoppingCart
    {
        //public int id { get; set; }
        //public string nombre { get; set; }
        //public int cantidad { get; set; }


        private TBL_Materiales_X_Sociedad _producto;
        public TBL_Materiales_X_Sociedad tBL_Materiales_X_Sociedad
        {
            get { return _producto; }
            set { _producto = value; }
        }


        //private TBL_Carrito _producto;
        //public TBL_Carrito tBL_Carrito
        //{
        //    get { return _producto; }
        //    set { _producto = value; }
        //}


        private int _cantidad;

        public int Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }

        public ClsShoppingCart() { }
        public ClsShoppingCart(TBL_Materiales_X_Sociedad tBL_Materiales_X_Sociedad, int cantidad)
        {
            this._producto = tBL_Materiales_X_Sociedad;
            this._cantidad = cantidad;
        }

        //public ClsShoppingCart(TBL_Materiales_X_Sociedad tBL_Materiales_X_Sociedad)
        //{
        //    this.tBL_Materiales_X_Sociedad = tBL_Materiales_X_Sociedad;
        //}
    }
}