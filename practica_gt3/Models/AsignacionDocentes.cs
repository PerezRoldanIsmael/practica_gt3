using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace practica_gt3.Models
{
    public class AsignacionDocentes
    {
        public int Id { get; set; }
        [Display(Name = "Curso")]
        public int CursoId { get; set; }
        public virtual Cursos Curso { get; set; }
        [Display(Name = "Grupo")]
        public int GrupoId { get; set; }
        public virtual GrupoClases Grupo {get; set;}
        [Display(Name = "Profesor")]
        public string UsuarioId { get; set; }
        public virtual ApplicationUser Usuario { get; set; }
    }
}