namespace OnlineBookstore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Contacts
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string Content { get; set; }

        public int? Status { get; set; }
    }
}
