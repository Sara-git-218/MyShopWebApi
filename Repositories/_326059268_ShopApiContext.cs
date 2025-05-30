﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Entities;
namespace Repositories;

public partial class _326059268_ShopApiContext : DbContext
{
    public _326059268_ShopApiContext()
    {
        
    }
    public _326059268_ShopApiContext(DbContextOptions<_326059268_ShopApiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Catgory> Catgories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Orderitem> Orderitems { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK_Orderss");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrdersUser");
        });

        modelBuilder.Entity<Orderitem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK_OrderItem");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderitems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrdersitemOrderNew");

            entity.HasOne(d => d.Product).WithMany(p => p.Orderitems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrdersitemOrder");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasOne(d => d.Catgory).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRoductCatgory");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.FirstName).IsFixedLength();
            entity.Property(e => e.LastName).IsFixedLength();
            entity.Property(e => e.Password).IsFixedLength();
            entity.Property(e => e.UserName).IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}