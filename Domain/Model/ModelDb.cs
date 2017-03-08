namespace Domain.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelDb : DbContext
    {
        public ModelDb()
            : base("name=ModelDb")
        {
        }

        public virtual DbSet<HtmlElements> HtmlElements { get; set; }
        public virtual DbSet<Types> Types { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
