namespace OnlineBookstore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ArticlesOrTags
    {
        public int Id { get; set; }

        public int ArticleId { get; set; }

        [Required]
        [StringLength(50)]
        public string TagId { get; set; }

        public virtual Articles Articles { get; set; }

        public virtual Tags Tags { get; set; }
    }
}
