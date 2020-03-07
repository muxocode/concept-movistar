using Microsoft.EntityFrameworkCore;
using entities;
using System.Collections.Generic;
using System;
using System.Linq;

namespace canalonline.data
{
    public partial class movistarContext : DbContext
    {
        public movistarContext()
        {
            this.Init();
        }

        public movistarContext(DbContextOptions<movistarContext> options)
            : base(options)
        {
            this.Init();
        }

        public void Init()
        {
            for (int i = 0; i < 5; i++)
            {
                aIds.Add(Guid.NewGuid());
            }

            if (!this.Set<Offer>().Any())
            {
                CreateMockOfferType().ToList().ForEach(x =>
                {
                    this.Add(x);
                });

                CreateMockOffer().ToList().ForEach(x =>
                {
                    this.Add(x);
                });


            }

            if (!this.Set<Client>().Any())
            {
                for (int i = 0; i < 4; i++)
                {
                    this.Add(new Client()
                    {
                        Email = "xxxxx@xx.com",
                        Password = "*******",
                        Name = $"Client {i}",
                        Id = Guid.NewGuid()
                    });
                }
            }

            this.SaveChanges();


            var n = 1;
            if(!this.Set<OffersClients>().Any())
            {
                foreach (var client in this.Set<Client>().ToList())
                {
                    foreach (var offer in this.Set<Offer>().ToList())
                    {
                        n++;
                        this.Add(new OffersClients()
                        {
                            Date = DateTime.Now.AddDays((n % 2 == 0) ? n : -1 * n),
                            ClientId = client.Id,
                            Id = Guid.NewGuid(),
                            OfferId = offer.Id,
                            Searched= (n % 2 == 0),
                            Showed= (n % 3 == 0),
                            Visited= (n % 5 == 0)
                        });
                    }
                }
            }

            this.SaveChanges();

        }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Para esta demo no necesitamos BBDD real
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        }

        List<Guid> aIds = new List<Guid>();
        protected IEnumerable<OfferType> CreateMockOfferType()
        {

            var i = 0;
            foreach (var item in aIds)
            {
                i++;
                yield return new OfferType()
                {
                    Description = $"Oferta tipo {i}",
                    Id = item,
                    Name = $"Tipo {i}"
                };
            };


        }


        protected IEnumerable<Offer> CreateMockOffer()
        {
            for (int i = 0; i < 100; i++)
            {
                var MockId = i % 5;


                yield return new Offer
                {
                    Description = $"Esto es la descripción de la oferta {i}",
                    Finish = (i % 4 == 0) ? null : (DateTime?)DateTime.Now.AddDays(i),
                    Id = Guid.NewGuid(),
                    Name = $"Oferta sensacional {i}",
                    Price = 30 * (i + 1) / ((i % 5) + 1),
                    Start = DateTime.Now.AddDays((i % 4 == 0) ? i : -1 * i),
                    OfferTypeId = aIds[MockId]
                };
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.ToTable("Articles");
            });

            modelBuilder.Entity<ChangeLog>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.EntityType)
                    .WithMany(p => p.ChangeLog)
                    .HasForeignKey(d => d.EntityTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChangeLog_EntityType");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ChangeLog)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChangeLog_User");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<DataType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(5);
            });

            modelBuilder.Entity<DetailTypeId>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.DataType)
                    .WithMany(p => p.DetailTypeId)
                    .HasForeignKey(d => d.DataTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DetailTypeId_DataType");
            });

            modelBuilder.Entity<EntityType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });


            modelBuilder.Entity<OfferDetails>(entity =>
            {
                entity.ToTable("Offer_Details");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.OfferDetails)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("FK_Offer_Details_Items");

                entity.HasOne(d => d.DetailType)
                    .WithMany(p => p.OfferDetails)
                    .HasForeignKey(d => d.DetailTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Offer_Details_DetailTypeId");

                entity.HasOne(d => d.Offer)
                    .WithMany(p => p.OfferDetails)
                    .HasForeignKey(d => d.OfferId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Offer_Details_Offers");
            });

            modelBuilder.Entity<OfferType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Offer>(entity =>
            {
                entity.ToTable("Offers");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Finish).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.Start).HasColumnType("datetime");

                entity.HasOne(d => d.OfferType)
                    .WithMany(p => p.Offers)
                    .HasForeignKey(d => d.OfferTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Offers_OfferType");
            });

            modelBuilder.Entity<OffersClients>(entity =>
            {
                entity.ToTable("Offers_Clients");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Offers_Clients)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Offer_Client_Client");

                entity.HasOne(d => d.Offer)
                    .WithMany(p => p.Offers_Clients)
                    .HasForeignKey(d => d.OfferId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Offer_Client_Offers");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UserRol>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.UserRol)
                    .HasForeignKey(d => d.RolId)
                    .HasConstraintName("FK_UserRol_Rol");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRol)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRol_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
