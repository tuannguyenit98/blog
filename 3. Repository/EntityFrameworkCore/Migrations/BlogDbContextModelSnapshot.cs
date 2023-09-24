﻿// <auto-generated />
using System;
using EntityFrameworkCore.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EntityFrameworkCore.Migrations
{
    [DbContext(typeof(BlogDbContext))]
    partial class BlogDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.Blog.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int")
                        .HasColumnName("created_by");

                    b.Property<DateTime?>("DeleteAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("delete_at");

                    b.Property<string>("MetaTitle")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("meta_title");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int")
                        .HasColumnName("parent_id");

                    b.Property<string>("Slug")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("slug");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("title");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int")
                        .HasColumnName("updated_by");

                    b.HasKey("Id");

                    b.ToTable("categories", (string)null);
                });

            modelBuilder.Entity("Entities.Blog.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int")
                        .HasColumnName("created_by");

                    b.Property<DateTime?>("DeleteAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("delete_at");

                    b.Property<int>("FK_PostId")
                        .HasColumnType("int")
                        .HasColumnName("fk_post_id");

                    b.Property<int>("FK_UserId")
                        .HasColumnType("int")
                        .HasColumnName("fk_user_id");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int")
                        .HasColumnName("parent_id");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int")
                        .HasColumnName("updated_by");

                    b.HasKey("Id");

                    b.HasIndex("FK_PostId");

                    b.HasIndex("FK_UserId");

                    b.ToTable("comments", (string)null);
                });

            modelBuilder.Entity("Entities.Blog.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int")
                        .HasColumnName("created_by");

                    b.Property<DateTime?>("DeleteAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("delete_at");

                    b.Property<int>("FK_CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("fk_category_id");

                    b.Property<int>("FK_UserId")
                        .HasColumnType("int")
                        .HasColumnName("fk_user_id");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("image");

                    b.Property<string>("MetaTitle")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("meta_title");

                    b.Property<string>("Slug")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("slug");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("status");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("title");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int")
                        .HasColumnName("updated_by");

                    b.HasKey("Id");

                    b.HasIndex("FK_CategoryId");

                    b.HasIndex("FK_UserId");

                    b.ToTable("posts", (string)null);
                });

            modelBuilder.Entity("Entities.Blog.PostTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int")
                        .HasColumnName("created_by");

                    b.Property<DateTime?>("DeleteAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("delete_at");

                    b.Property<int>("FK_PostId")
                        .HasColumnType("int")
                        .HasColumnName("fk_post_id");

                    b.Property<int>("FK_TagId")
                        .HasColumnType("int")
                        .HasColumnName("fk_tag_id");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int")
                        .HasColumnName("updated_by");

                    b.HasKey("Id");

                    b.HasIndex("FK_PostId");

                    b.HasIndex("FK_TagId");

                    b.ToTable("post_tags", (string)null);
                });

            modelBuilder.Entity("Entities.Blog.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int")
                        .HasColumnName("created_by");

                    b.Property<DateTime?>("DeleteAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("delete_at");

                    b.Property<string>("MetaTitle")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("meta_title");

                    b.Property<string>("Slug")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("slug");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("title");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int")
                        .HasColumnName("updated_by");

                    b.HasKey("Id");

                    b.ToTable("tags", (string)null);
                });

            modelBuilder.Entity("Entities.Blog.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int")
                        .HasColumnName("created_by");

                    b.Property<DateTime?>("DeleteAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("delete_at");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("email");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("full_name");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("password");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("refresh_token");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("role");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int")
                        .HasColumnName("updated_by");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("user_name");

                    b.HasKey("Id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Entities.Blog.Comment", b =>
                {
                    b.HasOne("Entities.Blog.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("FK_PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Blog.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("FK_UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Entities.Blog.Post", b =>
                {
                    b.HasOne("Entities.Blog.Category", "Category")
                        .WithMany("Posts")
                        .HasForeignKey("FK_CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Blog.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("FK_UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Entities.Blog.PostTag", b =>
                {
                    b.HasOne("Entities.Blog.Post", "Post")
                        .WithMany("PostTags")
                        .HasForeignKey("FK_PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Blog.Tag", "Tag")
                        .WithMany("PostTags")
                        .HasForeignKey("FK_TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("Entities.Blog.Category", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("Entities.Blog.Post", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("PostTags");
                });

            modelBuilder.Entity("Entities.Blog.Tag", b =>
                {
                    b.Navigation("PostTags");
                });

            modelBuilder.Entity("Entities.Blog.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
