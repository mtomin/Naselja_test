using System.ComponentModel.DataAnnotations.Schema;

namespace Naselja_test_api.Models
{
    public class Naselje
    {
        public int ID { get; set; }

        public string Naziv { get; set; }

        public int PostanskiBroj { get; set; }

        [ForeignKey("Drzava")]
        public int DrzavaID { get; set; }

        public virtual Drzava Drzava { get; set; }

    }
}
