using System;
using System.Collections.Generic;
using FullstackOpdracht.Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FullstackOpdracht.Domains.Data;

public partial class VoetbalDbContext : DbContext
{
    public VoetbalDbContext()
    {
    }

    public VoetbalDbContext(DbContextOptions<VoetbalDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<BookingMembership> BookingMemberships { get; set; }

    public virtual DbSet<BookingTicket> BookingTickets { get; set; }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<Membership> Memberships { get; set; }

    public virtual DbSet<Ring> Rings { get; set; }

    public virtual DbSet<Seat> Seats { get; set; }

    public virtual DbSet<Section> Sections { get; set; }

    public virtual DbSet<Stadium> Stadia { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                      .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                      .AddJsonFile("appsettings.json")
                      .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Booking__3214EC07AA3B0E9F");

            entity.ToTable("Booking");

            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Booking_AspNetUsers");
        });

        modelBuilder.Entity<BookingMembership>(entity =>
        {
            entity.HasKey(e => new { e.BookingId, e.MembershipId }).HasName("PK__BookingM__FABF628A282413AB");

            entity.ToTable("BookingMembership");

            entity.HasOne(d => d.Booking).WithMany(p => p.BookingMemberships)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookingMe__Booki__571DF1D5");

            entity.HasOne(d => d.Membership).WithMany(p => p.BookingMemberships)
                .HasForeignKey(d => d.MembershipId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookingMe__Membe__5812160E");
        });

        modelBuilder.Entity<BookingTicket>(entity =>
        {
            entity.HasKey(e => new { e.BookingId, e.TicketId }).HasName("PK__BookingT__1487D68DBA49C2A0");

            entity.ToTable("BookingTicket");

            entity.HasOne(d => d.Booking).WithMany(p => p.BookingTickets)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookingTi__Booki__52593CB8");

            entity.HasOne(d => d.Ticket).WithMany(p => p.BookingTickets)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookingTi__Ticke__534D60F1");
        });

        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Match__3214EC07539D63F5");

            entity.ToTable("Match");

            entity.Property(e => e.MatchDate).HasColumnType("datetime");

            entity.HasOne(d => d.AwayTeam).WithMany(p => p.MatchAwayTeams)
                .HasForeignKey(d => d.AwayTeamId)
                .HasConstraintName("FK__Match__AwayTeamI__45F365D3");

            entity.HasOne(d => d.HomeTeam).WithMany(p => p.MatchHomeTeams)
                .HasForeignKey(d => d.HomeTeamId)
                .HasConstraintName("FK__Match__HomeTeamI__44FF419A");
        });

        modelBuilder.Entity<Membership>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Membersh__3214EC077F0F5BF8");

            entity.ToTable("Membership");

            entity.HasOne(d => d.Seat).WithMany(p => p.Memberships)
                .HasForeignKey(d => d.SeatId)
                .HasConstraintName("FK__Membershi__SeatI__49C3F6B7");

            entity.HasOne(d => d.Team).WithMany(p => p.Memberships)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK__Membershi__TeamI__48CFD27E");
        });

        modelBuilder.Entity<Ring>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ring__3214EC07D26D9993");

            entity.ToTable("Ring");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Stadium).WithMany(p => p.Rings)
                .HasForeignKey(d => d.StadiumId)
                .HasConstraintName("FK__Ring__StadiumId__3C69FB99");
        });

        modelBuilder.Entity<Seat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Seat__3214EC07A2BB2113");

            entity.ToTable("Seat");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Section).WithMany(p => p.Seats)
                .HasForeignKey(d => d.SectionId)
                .HasConstraintName("FK__Seat__SectionId__4222D4EF");
        });

        modelBuilder.Entity<Section>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Section__3214EC07214BE8A7");

            entity.ToTable("Section");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Ring).WithMany(p => p.Sections)
                .HasForeignKey(d => d.RingId)
                .HasConstraintName("FK__Section__RingId__3F466844");
        });

        modelBuilder.Entity<Stadium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Stadium__3214EC079F298A3F");

            entity.ToTable("Stadium");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Team__3214EC07BF978F8E");

            entity.ToTable("Team");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Stadium).WithMany(p => p.Teams)
                .HasForeignKey(d => d.StadiumId)
                .HasConstraintName("FK__Team__StadiumId__398D8EEE");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ticket__3214EC0745A19CA7");

            entity.ToTable("Ticket");

            entity.HasOne(d => d.Match).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.MatchId)
                .HasConstraintName("FK__Ticket__MatchId__4D94879B");

            entity.HasOne(d => d.Seat).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.SeatId)
                .HasConstraintName("FK__Ticket__SeatId__4CA06362");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
