//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SIPSProyecto.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class InformePractica
    {
        public int inp_iCodigo { get; set; }
        public Nullable<int> cpr_iCodigo { get; set; }
        public Nullable<int> est_iCodigo { get; set; }
        public string inp_vcContenido { get; set; }
        public Nullable<System.DateTime> inp_dtFechaEnvio { get; set; }
    
        public virtual CoordinadorPracticas CoordinadorPracticas { get; set; }
        public virtual Estudiante Estudiante { get; set; }
    }
}
