﻿// <auto-generated />
using System;
using EventOrganizer.EF.MySql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EventOrganizer.EF.MySql.Migrations
{
    [DbContext(typeof(EventOrganazerMySqlDbContext))]
    [Migration("20240119005836_Update_DateTime_To_DateTimeOffset")]
    partial class Update_DateTime_To_DateTimeOffset
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("EventOrganizer.Domain.Models.DialogueMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("SenderId");

                    b.ToTable("DialogueMessages");
                });

            modelBuilder.Entity("EventOrganizer.Domain.Models.EventInvolvement", b =>
                {
                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("JoiningDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<DateTime?>("LeavingDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("EventId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("EventInvolvement", (string)null);

                    b.HasData(
                        new
                        {
                            EventId = 1,
                            UserId = 1,
                            JoiningDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            EventId = 2,
                            UserId = 2,
                            JoiningDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("EventOrganizer.Domain.Models.EventModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset>("EndDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIME(6)")
                        .HasDefaultValueSql("(DATE_ADD(CURRENT_TIMESTAMP(6), INTERVAL 2 HOUR))");

                    b.Property<bool>("IsMessagingAllowed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<DateTimeOffset?>("LastEndDate")
                        .HasColumnType("DATETIME(6)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<int>("RecurrenceType")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<DateTimeOffset>("StartDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIME(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("EventModels");

                    b.HasDiscriminator<string>("Discriminator").HasValue("EventModel");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("EventOrganizer.Domain.Models.EventResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("EventResults");
                });

            modelBuilder.Entity("EventOrganizer.Domain.Models.EventTag", b =>
                {
                    b.Property<string>("Keyword")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Keyword");

                    b.ToTable("EventTags");

                    b.HasData(
                        new
                        {
                            Keyword = "godel"
                        },
                        new
                        {
                            Keyword = "online"
                        },
                        new
                        {
                            Keyword = "entertainment"
                        });
                });

            modelBuilder.Entity("EventOrganizer.Domain.Models.LogRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AdditionalInfo")
                        .HasColumnType("longtext");

                    b.Property<string>("Application")
                        .HasColumnType("longtext");

                    b.Property<string>("CallerName")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ExceptionMessage")
                        .HasColumnType("longtext");

                    b.Property<string>("LogLevel")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Message")
                        .HasColumnType("longtext");

                    b.Property<string>("StackTrace")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("LogRecords");
                });

            modelBuilder.Entity("EventOrganizer.Domain.Models.Subscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Auth")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Endpoint")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("P256DH")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("EventOrganizer.Domain.Models.TagToEvent", b =>
                {
                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<string>("Keyword")
                        .HasColumnType("varchar(30)");

                    b.HasKey("EventId", "Keyword");

                    b.HasIndex("Keyword");

                    b.ToTable("TagToEvent", (string)null);

                    b.HasData(
                        new
                        {
                            EventId = 1,
                            Keyword = "godel"
                        },
                        new
                        {
                            EventId = 1,
                            Keyword = "online"
                        },
                        new
                        {
                            EventId = 2,
                            Keyword = "godel"
                        });
                });

            modelBuilder.Entity("EventOrganizer.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "mikita.n@godeltech.com",
                            FirstName = "Mikita",
                            LastName = "N",
                            Nickname = "mikita.n"
                        },
                        new
                        {
                            Id = 2,
                            Email = "john.doe@gmail.com",
                            FirstName = "John",
                            LastName = "Doe",
                            Nickname = "john.doe"
                        });
                });

            modelBuilder.Entity("EventOrganizer.Domain.Models.OfflineEvent", b =>
                {
                    b.HasBaseType("EventOrganizer.Domain.Models.EventModel");

                    b.Property<string>("Location")
                        .HasColumnType("longtext");

                    b.HasDiscriminator().HasValue("OfflineEvent");
                });

            modelBuilder.Entity("EventOrganizer.Domain.Models.OnlineEvent", b =>
                {
                    b.HasBaseType("EventOrganizer.Domain.Models.EventModel");

                    b.Property<string>("MeetingLink")
                        .HasColumnType("longtext");

                    b.HasDiscriminator().HasValue("OnlineEvent");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Mastery completion and presentation of the final product",
                            EndDate = new DateTimeOffset(new DateTime(2024, 1, 24, 14, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            IsMessagingAllowed = false,
                            OwnerId = 1,
                            RecurrenceType = 0,
                            StartDate = new DateTimeOffset(new DateTime(2024, 1, 24, 12, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Title = "Event organizer presentation"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Description created by John",
                            EndDate = new DateTimeOffset(new DateTime(2024, 1, 26, 20, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            IsMessagingAllowed = false,
                            OwnerId = 2,
                            RecurrenceType = 0,
                            StartDate = new DateTimeOffset(new DateTime(2024, 1, 26, 18, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Title = "Event created by John"
                        });
                });

            modelBuilder.Entity("EventOrganizer.Domain.Models.DialogueMessage", b =>
                {
                    b.HasOne("EventOrganizer.Domain.Models.EventModel", "Event")
                        .WithMany("DialogueMessages")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventOrganizer.Domain.Models.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("EventOrganizer.Domain.Models.EventInvolvement", b =>
                {
                    b.HasOne("EventOrganizer.Domain.Models.EventModel", "EventModel")
                        .WithMany("EventInvolvements")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventOrganizer.Domain.Models.User", "User")
                        .WithMany("EventInvolvements")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EventModel");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EventOrganizer.Domain.Models.EventModel", b =>
                {
                    b.HasOne("EventOrganizer.Domain.Models.User", "Owner")
                        .WithMany("CreatedEvents")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("EventOrganizer.Domain.Models.EventResult", b =>
                {
                    b.HasOne("EventOrganizer.Domain.Models.EventModel", "Event")
                        .WithMany("EventResults")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("EventOrganizer.Domain.Models.Subscription", b =>
                {
                    b.HasOne("EventOrganizer.Domain.Models.User", null)
                        .WithMany("Subscriptions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EventOrganizer.Domain.Models.TagToEvent", b =>
                {
                    b.HasOne("EventOrganizer.Domain.Models.EventModel", "EventModel")
                        .WithMany("TagToEvents")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventOrganizer.Domain.Models.EventTag", "EventTag")
                        .WithMany("TagToEvents")
                        .HasForeignKey("Keyword")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EventModel");

                    b.Navigation("EventTag");
                });

            modelBuilder.Entity("EventOrganizer.Domain.Models.EventModel", b =>
                {
                    b.Navigation("DialogueMessages");

                    b.Navigation("EventInvolvements");

                    b.Navigation("EventResults");

                    b.Navigation("TagToEvents");
                });

            modelBuilder.Entity("EventOrganizer.Domain.Models.EventTag", b =>
                {
                    b.Navigation("TagToEvents");
                });

            modelBuilder.Entity("EventOrganizer.Domain.Models.User", b =>
                {
                    b.Navigation("CreatedEvents");

                    b.Navigation("EventInvolvements");

                    b.Navigation("Subscriptions");
                });
#pragma warning restore 612, 618
        }
    }
}
