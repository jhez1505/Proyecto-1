using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto2_API.Models
{
    public class OperacionModel
    {
        public int Id { get; set; }
        public string Operacion { get; set; }
        public double Resultado { get; set; }
        public string Fecha { get; set; }
    }

}