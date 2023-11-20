#pragma warning disable CS8618


using System.ComponentModel.DataAnnotations;

namespace SessionWorkshop.Models
{
    public class User
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        [MinLength(4,ErrorMessage = "El largo mínimo debe ser de 4 caracteres")]
        public string Name { get; set; }

        public string? Logout {get;set;}
    }
}