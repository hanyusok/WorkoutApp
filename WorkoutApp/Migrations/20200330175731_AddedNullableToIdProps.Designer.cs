﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkoutApp.Model;

namespace WorkoutApp.Migrations
{
    [DbContext(typeof(ExerciseContext))]
    [Migration("20200330175731_AddedNullableToIdProps")]
    partial class AddedNullableToIdProps
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WorkoutApp.Model.Exercise", b =>
                {
                    b.Property<int?>("ExerciseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExerciseName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExerciseType")
                        .HasColumnType("int");

                    b.Property<int?>("StationId")
                        .HasColumnType("int");

                    b.HasKey("ExerciseId");

                    b.HasIndex("StationId");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("WorkoutApp.Model.ExerciseStation", b =>
                {
                    b.Property<int>("ExerciseId")
                        .HasColumnType("int");

                    b.Property<int>("StationId")
                        .HasColumnType("int");

                    b.HasKey("ExerciseId", "StationId");

                    b.HasIndex("StationId");

                    b.ToTable("ExerciseStations");
                });

            modelBuilder.Entity("WorkoutApp.Model.Station", b =>
                {
                    b.Property<int?>("StationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("StationName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WorkoutId")
                        .HasColumnType("int");

                    b.HasKey("StationId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("Stations");
                });

            modelBuilder.Entity("WorkoutApp.Model.Workout", b =>
                {
                    b.Property<int?>("WorkoutId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RepSeconds")
                        .HasColumnType("int");

                    b.Property<int>("RestSeconds")
                        .HasColumnType("int");

                    b.HasKey("WorkoutId");

                    b.ToTable("Workouts");
                });

            modelBuilder.Entity("WorkoutApp.Model.Exercise", b =>
                {
                    b.HasOne("WorkoutApp.Model.Station", null)
                        .WithMany("Exercises")
                        .HasForeignKey("StationId");
                });

            modelBuilder.Entity("WorkoutApp.Model.ExerciseStation", b =>
                {
                    b.HasOne("WorkoutApp.Model.Exercise", "Exercise")
                        .WithMany()
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkoutApp.Model.Station", "Station")
                        .WithMany()
                        .HasForeignKey("StationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WorkoutApp.Model.Station", b =>
                {
                    b.HasOne("WorkoutApp.Model.Workout", null)
                        .WithMany("Stations")
                        .HasForeignKey("WorkoutId");
                });
#pragma warning restore 612, 618
        }
    }
}
