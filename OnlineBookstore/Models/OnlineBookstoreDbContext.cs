using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace OnlineBookstore.Models
{
    public partial class OnlineBookstoreDbContext : DbContext
    {
        public OnlineBookstoreDbContext()
            : base("name=OnlineBookstoreDbContext")
        {
        }

        public virtual DbSet<Abouts> Abouts { get; set; }
        public virtual DbSet<Articles> Articles { get; set; }
        public virtual DbSet<ArticlesOrTags> ArticlesOrTags { get; set; }
        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Contacts> Contacts { get; set; }
        public virtual DbSet<FeedBacks> FeedBacks { get; set; }
        public virtual DbSet<Footers> Footers { get; set; }
        public virtual DbSet<Menus> Menus { get; set; }
        public virtual DbSet<MenuTypes> MenuTypes { get; set; }
        public virtual DbSet<Slides> Slides { get; set; }
        public virtual DbSet<SystemConfigs> SystemConfigs { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
        public virtual DbSet<Topics> Topics { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Abouts>()
                .Property(e => e.SeoUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Abouts>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Abouts>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Abouts>()
                .Property(e => e.MetaDescriptions)
                .IsFixedLength();

            modelBuilder.Entity<Articles>()
                .Property(e => e.SeoUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Articles>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Articles>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Articles>()
                .Property(e => e.MetaDescriptions)
                .IsFixedLength();

            modelBuilder.Entity<Articles>()
                .HasMany(e => e.ArticlesOrTags)
                .WithRequired(e => e.Articles)
                .HasForeignKey(e => e.ArticleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ArticlesOrTags>()
                .Property(e => e.TagId)
                .IsUnicode(false);

            modelBuilder.Entity<Books>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Books>()
                .Property(e => e.SeoUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Books>()
                .Property(e => e.PurchasePrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Books>()
                .Property(e => e.SalePrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Books>()
                .Property(e => e.PromotionPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Books>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Books>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Books>()
                .Property(e => e.MetaDescriptions)
                .IsFixedLength();

            modelBuilder.Entity<Categories>()
                .Property(e => e.SeoUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Categories>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Categories>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Categories>()
                .Property(e => e.MetaDescriptions)
                .IsFixedLength();

            modelBuilder.Entity<Categories>()
                .HasMany(e => e.Books)
                .WithRequired(e => e.Categories)
                .HasForeignKey(e => e.CategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Footers>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<MenuTypes>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<MenuTypes>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<MenuTypes>()
                .HasMany(e => e.Menus)
                .WithRequired(e => e.MenuTypes)
                .HasForeignKey(e => e.MenuTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Slides>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Slides>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Slides>()
                .Property(e => e.MetaDescriptions)
                .IsFixedLength();

            modelBuilder.Entity<SystemConfigs>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<SystemConfigs>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<Tags>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<Tags>()
                .HasMany(e => e.ArticlesOrTags)
                .WithRequired(e => e.Tags)
                .HasForeignKey(e => e.TagId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Topics>()
                .Property(e => e.SeoUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Topics>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Topics>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Topics>()
                .Property(e => e.MetaDescriptions)
                .IsFixedLength();

            modelBuilder.Entity<Topics>()
                .HasMany(e => e.Articles)
                .WithRequired(e => e.Topics)
                .HasForeignKey(e => e.TopicId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);
        }
    }
}
