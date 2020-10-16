using FoodApp.Models.ViewModels;
using FoodApp.Resources;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodApp.Models
{
    public class MyContext : DbContext
    {
        private ICurrentUserService currentUserService;
        public MyContext(DbContextOptions<MyContext> options, ICurrentUserService currentUserService): base(options)
        {
            this.currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuPackage> MenuPackages { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Menu>()
                .HasOne(m => m.User)
                .WithMany(u => u.Menus)
                .HasForeignKey(m => m.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MenuPackage>()
                .HasOne(mp => mp.Menu)
                .WithMany(m => m.MenuPackages)
                .HasForeignKey(mp => mp.MenuId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MenuItem>()
                 .HasOne(mi => mi.Menu)
                 .WithMany(m => m.MenuItems)
                 .HasForeignKey(mi => mi.MenuId)
                 .IsRequired(false)
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MenuItem>()
                 .HasOne(mi => mi.MenuPackage)
                 .WithMany(mp => mp.MenuItems)
                 .HasForeignKey(mi => mi.MenuPackagedId)
                 .IsRequired(false)
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MenuPackage>()
                 .HasOne(mp => mp.Cart)
                 .WithMany(c => c.MenuPackages)
                 .HasForeignKey(mp => mp.CartId)
                 .IsRequired(false)
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MenuItem>()
                 .HasOne(mi => mi.Cart)
                 .WithMany(c => c.MenuItems)
                 .HasForeignKey(mi => mi.CartId)
                 .IsRequired(false)
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(c => c.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Source>()
                .HasOne(s => s.User)
                .WithMany(u => u.Sources)
                .HasForeignKey(s => s.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Subscription>()
                .HasOne(su => su.User)
                .WithMany(u => u.Subscriptions)
                .HasForeignKey(su => su.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Source>()
               .HasOne(s => s.Card)
               .WithOne(c => c.Source)
               .HasForeignKey<Card>(c => c.SourceId)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.Cascade);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ProcessSave();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ProcessSave()
        {
            var currentTime = DateTimeOffset.UtcNow;
            foreach (var item in ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added && e.Entity is Entity))
            {
                var entity = item.Entity as Entity;
                entity.CreatedDate = currentTime;
                entity.CreatedByUser = currentUserService.GetCurrentUsername();
            }
        }
    }
}
