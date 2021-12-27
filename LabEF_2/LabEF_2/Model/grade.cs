namespace LabEF_2.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class grade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int sid { get; set; }

        [StringLength(50)]
        public string sname { get; set; }

        [Column("grade")]
        public int? grade1 { get; set; }

        [StringLength(50)]
        public string cname { get; set; }
    }
}
