namespace OnlineBookstore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Books
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(16)]
        public string Code { get; set; }

        [StringLength(250)]
        public string SeoUrl { get; set; }

        [StringLength(250)]
        public string SeoTitle { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImages { get; set; }

        public decimal? PurchasePrice { get; set; }

        public decimal? SalePrice { get; set; }

        public decimal? PromotionPrice { get; set; }

        public int? Quantity { get; set; }

        public int? Sold { get; set; }

        public bool? IncludedVAT { get; set; }

        [StringLength(250)]
        public string Supplier { get; set; }

        [StringLength(150)]
        public string Publisher { get; set; }

        [StringLength(250)]
        public string Cover { get; set; }

        [StringLength(250)]
        public string Author { get; set; }

        [StringLength(150)]
        public string Translator { get; set; }

        [StringLength(250)]
        public string PageSize { get; set; }

        public int? TotalPage { get; set; }

        public int CategoryId { get; set; }

        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(250)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [StringLength(250)]
        public string MetaKeywords { get; set; }

        [StringLength(250)]
        public string MetaDescriptions { get; set; }

        public int? Status { get; set; }

        public DateTime? TopHot { get; set; }

        public int? ViewCount { get; set; }

        public virtual Categories Categories { get; set; }
    }
}
