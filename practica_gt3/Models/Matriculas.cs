using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace practica_gt3.Models
{
    public class Matriculas
    {
        public int Id { get; set; }
        [Display(Name = "Grupo")]
        public int GrupoId { get; set; }
        public virtual GrupoClases Grupo { get; set; }
        [Display(Name = "Alumno")]
        public string UsuarioId { get; set; }
        public virtual ApplicationUser Usuario { get; set; }
    }
}