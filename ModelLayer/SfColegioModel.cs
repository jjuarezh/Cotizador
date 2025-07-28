using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class SfColegioModel
    {
        private string nescuela;
        private string ncontacto;
        public List<ubicacion> localizacion { get; set; }

        public string NombreEscuela
        {
            get
            {
                return nescuela;
            }
            set
            {
                nescuela = value;
            }
        }

        public string NombreContacto
        {
            get
            {
                return ncontacto;
            }
            set
            {
                ncontacto = value;
            }
        }

        public class ubicacion
        {
            public double latitud { get; set; }
            public double longitud { get; set; }
        }
    }
}
