namespace Prueba.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("gavAnimal")]
    public partial class gavAnimal
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long aniId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(30)]
        public string aniCodigo { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int empId { get; set; }

        public long? camId { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long razId { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long catId { get; set; }

        public long? troId { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(1)]
        public string aniEstado { get; set; }

        [Key]
        [Column(Order = 6)]
        public decimal aniPeso { get; set; }

        [StringLength(1)]
        public string aniSexo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? aniFechaNacimiento { get; set; }

        public int? aniMeses { get; set; }

        public long? odeId { get; set; }

        public long? proId { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(100)]
        public string empNombre { get; set; }

        [StringLength(100)]
        public string camNombre { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(100)]
        public string razNombre { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(100)]
        public string catNombre { get; set; }

        [StringLength(100)]
        public string troNombre { get; set; }

        public decimal? PesoEntrada { get; set; }

        public int? DiasEnCampo { get; set; }

        public decimal? GDPV { get; set; }
    }
}
