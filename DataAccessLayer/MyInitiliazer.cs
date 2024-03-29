﻿using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class MyInitiliazer
    {
        private readonly BlogContext context;

        public MyInitiliazer(BlogContext _context)
        {
            context = _context;
        }

        public void Seed()
        {
            string passwordHash;
            byte[] passwordSalt;
            CreatePasswordHash("password", out passwordHash, out passwordSalt);

            User user = new User
            {
                FirstName = "fatih",
                LastName = "arslan",
                UserName = "fatiharslan",
                Email = "fatih@gmail.com",
                ActivatedGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                Photourl = "https://cdn.icon-icons.com/icons2/2506/PNG/512/user_icon_150670.png",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                OnCreated = DateTime.Now,
                OnModified = DateTime.Now.AddHours(1),
                ModifiedUsername = "fatiharslan"

            };

            for (int i = 0; i < 5; i++)
            {
                   User user1 = new User
            {
                FirstName = "user",
                LastName = "userlastname",
                UserName = "username" + i,
                Email = "fatih@gmail.com",
                ActivatedGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                Photourl = "https://cdn.icon-icons.com/icons2/2506/PNG/512/user_icon_150670.png",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                OnCreated = DateTime.Now,
                OnModified = DateTime.Now.AddHours(1),
                ModifiedUsername = "fatiharslan"

            };
            context.Users.Add(user1);
            }

            for (int i = 0; i < 5; i++)
            {
                Category cat = new Category
                {
                    Categoryname = "categoryname",   
                    Description = "description",               
                    OnModified = DateTime.Now.AddHours(1),
                    OnModifiedUsername = "fatiharslan",
                    PhotoUrl = "https://image.shutterstock.com/image-vector/copy-file-icon-trendy-modern-260nw-1675417978.jpg"
                };

                context.Categories.Add(cat);

                for (int j = 0; j < 5; j++)
                {
                    Note note = new Note
                    {
                        Title = "What is Lorem Ipsum?",
                        Text = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        LikeCount = 4,
                        OnCreated = DateTime.Now,
                        OnModified = DateTime.Now.AddHours(1),
                        OnModifiedUsername = "fatiharslan",
                        MainPhotourl = "https://media.istockphoto.com/photos/top-view-of-words-new-blog-post-written-on-spiralbound-notepad-on-picture-id1157749043?k=20&m=1157749043&s=612x612&w=0&h=NVF5OPduCotPMlVE-jQH5FdnJQicST8-55cBreuVyuc=",
                        User = user,
                        Category = cat
                                          
                    };
                    context.Notes.Add(note);

                    for (int k = 0; k < 5; k++)
                    {
                        Comment comment = new Comment
                        {
                            Text = "testtext",                   
                            OnCreated = DateTime.Now,
                            OnModified = DateTime.Now.AddHours(1),
                            OnModifiedUsername = "fatiharslan",
                            User = user,
                            Note = note
                        };
                        context.Comments.Add(comment);

                    }

                    for (int l = 0; l < 5; l++)
                    {
                        Like like = new Like
                        {
                            OnCreated = DateTime.Now,
                            OnModifiedUsername = "fatiharslan",
                            User = user,
                            Note = note
                        };
                        context.Likes.Add(like);

                    }

                    for (int l = 0; l < 5; l++)
                    {
                        Photo photo = new Photo
                        {
                            OnCreated = DateTime.Now,
                            OnModifiedUsername = "fatiharslan",                      
                            Note = note
                        };
                        context.Photos.Add(photo);

                    }

                }
             
            }

            context.Users.Add(user);
            context.SaveChanges();
        }

        public void CreatePasswordHash(string password, out string passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = Convert.ToBase64String((hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password))));
            }

        }
    }

}
