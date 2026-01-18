using System;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class StoreContext(DbContextOptions options) : IdentityDbContext<User>(options)
{
    public required DbSet<Product> Products { get; set; }

    public required DbSet<Cart> Carts { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);

        builder.Entity<IdentityRole>()
        .HasData(
            new IdentityRole
            {
                Id = "4720f56b-c0b5-4ceb-9d12-369d3446baa4",
                Name = "Member",
                NormalizedName = "MEMBER",
                ConcurrencyStamp = "member"
            },
            new IdentityRole
            {
                Id = "b23f113e-2c9c-4531-b07f-5679f90819f8",
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "admin"
            }
        );
    }

}
