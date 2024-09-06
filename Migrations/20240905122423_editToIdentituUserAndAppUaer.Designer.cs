﻿// <auto-generated />
using System;
using Banha_UniverCity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Banha_UniverCity.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240905122423_editToIdentituUserAndAppUaer")]
    partial class editToIdentituUserAndAppUaer
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Banha_UniverCity.Models.AcademicYear", b =>
                {
                    b.Property<int>("AcademicYearID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AcademicYearID"));

                    b.Property<string>("YearName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AcademicYearID");

                    b.ToTable("AcademicYear");
                });

            modelBuilder.Entity("Banha_UniverCity.Models.ClassSchedule", b =>
                {
                    b.Property<int>("ClassScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClassScheduleId"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("DayOfWeek")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("ClassScheduleId");

                    b.HasIndex("CourseId");

                    b.ToTable("ClassSchedule");

                    b.HasData(
                        new
                        {
                            ClassScheduleId = 1,
                            CourseId = 1,
                            DayOfWeek = "Monday",
                            EndTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StartTime = new DateTime(2024, 10, 1, 10, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ClassScheduleId = 2,
                            CourseId = 2,
                            DayOfWeek = "Monday",
                            EndTime = new DateTime(2024, 10, 1, 12, 1, 1, 0, DateTimeKind.Unspecified),
                            StartTime = new DateTime(2024, 10, 1, 12, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Banha_UniverCity.Models.Course", b =>
                {
                    b.Property<int>("CourseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseID"));

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Credits")
                        .HasColumnType("int");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InstructorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CourseID");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("InstructorId");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            CourseID = 1,
                            CourseName = "Introduction to Programming",
                            Credits = 0,
                            DepartmentId = 1,
                            Description = "",
                            InstructorId = "2"
                        },
                        new
                        {
                            CourseID = 2,
                            CourseName = "Digital Circuits",
                            Credits = 0,
                            DepartmentId = 2,
                            Description = "",
                            InstructorId = "2"
                        });
                });

            modelBuilder.Entity("Banha_UniverCity.Models.CourseCurriculum", b =>
                {
                    b.Property<int>("CourseCurriculumID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseCurriculumID"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CourseID")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CourseCurriculumID");

                    b.HasIndex("CourseID");

                    b.ToTable("CourseCurricula");

                    b.HasData(
                        new
                        {
                            CourseCurriculumID = 1,
                            Content = "Intro Of Cs",
                            CourseID = 1,
                            Title = "Introduction"
                        },
                        new
                        {
                            CourseCurriculumID = 2,
                            Content = "Intro Of C++",
                            CourseID = 2,
                            Title = "Basic Programming Concepts"
                        });
                });

            modelBuilder.Entity("Banha_UniverCity.Models.CourseVideo", b =>
                {
                    b.Property<int>("CourseVideoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseVideoID"));

                    b.Property<int>("CourseID")
                        .HasColumnType("int");

                    b.Property<string>("VideoTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CourseVideoID");

                    b.HasIndex("CourseID");

                    b.ToTable("CourseVideos");

                    b.HasData(
                        new
                        {
                            CourseVideoID = 1,
                            CourseID = 1,
                            VideoTitle = "Course Overview",
                            VideoURL = "http://example.com/video1"
                        },
                        new
                        {
                            CourseVideoID = 2,
                            CourseID = 1,
                            VideoTitle = "Getting Started",
                            VideoURL = "http://example.com/video2"
                        });
                });

            modelBuilder.Entity("Banha_UniverCity.Models.Department", b =>
                {
                    b.Property<int>("DepartmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepartmentID"));

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DepartmentID");

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            DepartmentID = 1,
                            DepartmentName = "Computer Science"
                        },
                        new
                        {
                            DepartmentID = 2,
                            DepartmentName = "Electrical Engineering"
                        });
                });

            modelBuilder.Entity("Banha_UniverCity.Models.Enrollment", b =>
                {
                    b.Property<int>("EnrollmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EnrollmentID"));

                    b.Property<int>("CourseID")
                        .HasColumnType("int");

                    b.Property<decimal?>("Grade")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("StudentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("EnrollmentID");

                    b.HasIndex("CourseID");

                    b.HasIndex("StudentId");

                    b.ToTable("Enrollments");

                    b.HasData(
                        new
                        {
                            EnrollmentID = 1,
                            CourseID = 1,
                            StudentId = "1"
                        },
                        new
                        {
                            EnrollmentID = 2,
                            CourseID = 2,
                            StudentId = "1"
                        });
                });

            modelBuilder.Entity("Banha_UniverCity.Models.Feedback", b =>
                {
                    b.Property<int>("FeedbackID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FeedbackID"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FeedbackDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProviderUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TargetStudentUserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("FeedbackID");

                    b.HasIndex("CourseId");

                    b.HasIndex("ProviderUserId");

                    b.HasIndex("TargetStudentUserId");

                    b.ToTable("Feedback");

                    b.HasData(
                        new
                        {
                            FeedbackID = 1,
                            Content = "Great course!",
                            CourseId = 1,
                            FeedbackDate = new DateTime(2024, 9, 5, 15, 24, 19, 945, DateTimeKind.Local).AddTicks(9839),
                            ProviderUserId = "1",
                            TargetStudentUserId = "1"
                        },
                        new
                        {
                            FeedbackID = 2,
                            Content = "Need improvement on some topics.",
                            CourseId = 2,
                            FeedbackDate = new DateTime(2024, 9, 5, 15, 24, 19, 945, DateTimeKind.Local).AddTicks(9905),
                            ProviderUserId = "2",
                            TargetStudentUserId = "1"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator().HasValue("IdentityUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Banha_UniverCity.Models.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<int?>("AcademicYearID")
                        .HasColumnType("int");

                    b.Property<int?>("AvailableCreditHours")
                        .HasColumnType("int");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("AcademicYearID");

                    b.HasDiscriminator().HasValue("ApplicationUser");

                    b.HasData(
                        new
                        {
                            Id = "1",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "80526a8e-9e0e-433e-b500-606a8c6f43f0",
                            Email = "student1@example.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "STUDENT1@EXAMPLE.COM",
                            NormalizedUserName = "STUDENT1",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "8cb305f1-e7ba-40c8-8361-d56b27cb30d9",
                            TwoFactorEnabled = false,
                            UserName = "student1",
                            AvailableCreditHours = 0,
                            FullName = "Student One",
                            UserType = "Student"
                        },
                        new
                        {
                            Id = "2",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "c48770fe-857a-45df-8a1d-da27acdbcd8e",
                            Email = "instructor1@example.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "INSTRUCTOR1@EXAMPLE.COM",
                            NormalizedUserName = "INSTRUCTOR1",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "f2e21d4b-8985-4c84-9414-7d0df48d0247",
                            TwoFactorEnabled = false,
                            UserName = "instructor1",
                            AvailableCreditHours = 0,
                            FullName = "Instructor One",
                            UserType = "Instructor"
                        });
                });

            modelBuilder.Entity("Banha_UniverCity.Models.ClassSchedule", b =>
                {
                    b.HasOne("Banha_UniverCity.Models.Course", "Course")
                        .WithMany("ClassSchedules")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Banha_UniverCity.Models.Course", b =>
                {
                    b.HasOne("Banha_UniverCity.Models.Department", "Department")
                        .WithMany("Courses")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Banha_UniverCity.Models.ApplicationUser", "Instructor")
                        .WithMany("CoursesTaught")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Instructor");
                });

            modelBuilder.Entity("Banha_UniverCity.Models.CourseCurriculum", b =>
                {
                    b.HasOne("Banha_UniverCity.Models.Course", "Course")
                        .WithMany("CourseCurricula")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Banha_UniverCity.Models.CourseVideo", b =>
                {
                    b.HasOne("Banha_UniverCity.Models.Course", "Course")
                        .WithMany("CourseVideos")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Banha_UniverCity.Models.Enrollment", b =>
                {
                    b.HasOne("Banha_UniverCity.Models.Course", "Course")
                        .WithMany("Enrollments")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Banha_UniverCity.Models.ApplicationUser", "Student")
                        .WithMany("Enrollments")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Banha_UniverCity.Models.Feedback", b =>
                {
                    b.HasOne("Banha_UniverCity.Models.Course", "Course")
                        .WithMany("Feedbacks")
                        .HasForeignKey("CourseId");

                    b.HasOne("Banha_UniverCity.Models.ApplicationUser", "ProviderUser")
                        .WithMany("FeedbacksGiven")
                        .HasForeignKey("ProviderUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Banha_UniverCity.Models.ApplicationUser", "TargetStudentUser")
                        .WithMany("FeedbacksReceived")
                        .HasForeignKey("TargetStudentUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Course");

                    b.Navigation("ProviderUser");

                    b.Navigation("TargetStudentUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Banha_UniverCity.Models.ApplicationUser", b =>
                {
                    b.HasOne("Banha_UniverCity.Models.AcademicYear", "AcademicYear")
                        .WithMany("Students")
                        .HasForeignKey("AcademicYearID");

                    b.Navigation("AcademicYear");
                });

            modelBuilder.Entity("Banha_UniverCity.Models.AcademicYear", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("Banha_UniverCity.Models.Course", b =>
                {
                    b.Navigation("ClassSchedules");

                    b.Navigation("CourseCurricula");

                    b.Navigation("CourseVideos");

                    b.Navigation("Enrollments");

                    b.Navigation("Feedbacks");
                });

            modelBuilder.Entity("Banha_UniverCity.Models.Department", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("Banha_UniverCity.Models.ApplicationUser", b =>
                {
                    b.Navigation("CoursesTaught");

                    b.Navigation("Enrollments");

                    b.Navigation("FeedbacksGiven");

                    b.Navigation("FeedbacksReceived");
                });
#pragma warning restore 612, 618
        }
    }
}
