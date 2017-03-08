namespace Domain.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HtmlElements
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Content { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Html { get; set; }

        public long TypeId { get; set; }
    }
}
