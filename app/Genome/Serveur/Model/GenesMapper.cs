using System.Data.Linq.Mapping;

namespace Serveur.Model
{
    [Table(Name = "Genes")]
    class GenesMapper
    {

        [Column(Name = "acideA1", IsPrimaryKey = true, CanBeNull = false)]
        public char acideA1 { get; set; }

        [Column(Name = "acideA2", IsPrimaryKey = true, CanBeNull = false)]
        public char acideA2 { get; set; }

        [Column(Name = "acideA3", IsPrimaryKey = true, CanBeNull = false)]
        public char acideA3 { get; set; }

        [Column(Name = "gene", CanBeNull = false)]
        public string gene { get; set; }

    }
}
