using System.ComponentModel.DataAnnotations;

namespace Naselja_test.Models
{
    public class Naselje
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Naziv naselja je obavezan!")]
        public string Naziv { get; set; }

        [Required(ErrorMessage = "Poštanski broj naselja je obavezan!")]
        [Display(Name = "Poštanski broj")]
        public int PostanskiBroj { get; set; }

        [Required(ErrorMessage = "Morate odabrati državu!")]
        [Display(Name = "Država")]

        public Drzava Drzava { get; set; }
    }
}
