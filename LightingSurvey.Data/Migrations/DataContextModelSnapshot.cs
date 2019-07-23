﻿// <auto-generated />
using System;
using LightingSurvey.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LightingSurvey.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LightingSurvey.Data.Models.SurveyResponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("HappyWithLighting");

                    b.Property<string>("IdExternal");

                    b.Property<int?>("PerceivedBrightnessLevel");

                    b.HasKey("Id");

                    b.ToTable("Responces");
                });

            modelBuilder.Entity("LightingSurvey.Data.Models.SurveyResponse", b =>
                {
                    b.OwnsOne("LightingSurvey.Data.Models.SurveyDates", "Dates", b1 =>
                        {
                            b1.Property<int>("SurveyResponseId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<DateTime?>("Completed");

                            b1.Property<DateTime>("Created");

                            b1.Property<DateTime>("Modified");

                            b1.HasKey("SurveyResponseId");

                            b1.ToTable("Responces");

                            b1.HasOne("LightingSurvey.Data.Models.SurveyResponse")
                                .WithOne("Dates")
                                .HasForeignKey("LightingSurvey.Data.Models.SurveyDates", "SurveyResponseId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("LightingSurvey.Data.Models.SurveyRespondent", "Respondent", b1 =>
                        {
                            b1.Property<int>("SurveyResponseId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("EmailAddress")
                                .HasMaxLength(254);

                            b1.Property<string>("Name")
                                .HasMaxLength(50);

                            b1.HasKey("SurveyResponseId");

                            b1.ToTable("Responces");

                            b1.HasOne("LightingSurvey.Data.Models.SurveyResponse")
                                .WithOne("Respondent")
                                .HasForeignKey("LightingSurvey.Data.Models.SurveyRespondent", "SurveyResponseId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.OwnsOne("LightingSurvey.Data.Models.Address", "Address", b2 =>
                                {
                                    b2.Property<int>("SurveyRespondentSurveyResponseId")
                                        .ValueGeneratedOnAdd()
                                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                                    b2.Property<decimal>("Latitude");

                                    b2.Property<string>("Line1")
                                        .HasMaxLength(128);

                                    b2.Property<string>("Line2")
                                        .HasMaxLength(128);

                                    b2.Property<decimal>("Longitude");

                                    b2.Property<string>("PostCode")
                                        .HasMaxLength(9);

                                    b2.Property<string>("Town")
                                        .HasMaxLength(64);

                                    b2.HasKey("SurveyRespondentSurveyResponseId");

                                    b2.ToTable("Responces");

                                    b2.HasOne("LightingSurvey.Data.Models.SurveyRespondent")
                                        .WithOne("Address")
                                        .HasForeignKey("LightingSurvey.Data.Models.Address", "SurveyRespondentSurveyResponseId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                });
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
