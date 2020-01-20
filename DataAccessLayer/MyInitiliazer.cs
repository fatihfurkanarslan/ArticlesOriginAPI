using Entities;
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
            byte[] passwordHash, passwordSalt;
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
                Photourl = "StandartUser.png",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                OnCreated = DateTime.Now,
                OnModified = DateTime.Now.AddHours(1),
                ModifiedUsername = "fatiharslan"

            };

            for (int i = 0; i < 10; i++)
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
                Photourl = "StandartUser.png",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                OnCreated = DateTime.Now,
                OnModified = DateTime.Now.AddHours(1),
                ModifiedUsername = "fatiharslan"

            };
            context.Users.Add(user1);
            }

            for (int i = 0; i < 10; i++)
            {
                Category cat = new Category
                {
                    Categoryname = "categoryname",   
                    Description = "description",               
                    OnModified = DateTime.Now.AddHours(1),
                    OnModifiedUsername = "fatiharslan",
                    PhotoUrl = "urltest"

                };

                context.Categories.Add(cat);

                for (int j = 0; j < 10; j++)
                {
                    Note note = new Note
                    {
                        Title = "testtitle",
                        Text = "testtext",
                        LikeCount = 4,
                        OnCreated = DateTime.Now,
                        OnModified = DateTime.Now.AddHours(1),
                        OnModifiedUsername = "fatiharslan",
                        User = user,
                        Category = cat
                                          
                    };
                    context.Notes.Add(note);

                    for (int k = 0; k < 10; k++)
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

                    for (int l = 0; l < 10; l++)
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

                    for (int l = 0; l < 10; l++)
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

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }
    }

}
