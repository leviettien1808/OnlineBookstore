namespace OnlineBookstore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Footers
    {
        [StringLength(50)]
        public string Id { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        public int? Status { get; set; }
    }
}
