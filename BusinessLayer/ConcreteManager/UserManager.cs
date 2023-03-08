using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.Dtos;
using Helper;
using DataAccessLayer.UnitOfWork;
using BusinessLayer.AbstractManager;
using System.Linq;

namespace BusinessLayer
{
    public class UserManager: IUserManager
    {

        //mail helper must being interface, too
        MailHelper mailHelper;
        //IRepository<User> userRepository;
        IUnitOfWork unitOfWork;

        public UserManager(IUnitOfWork _unitOfWork, MailHelper _mailHelper)
        {
            unitOfWork = _unitOfWork;
            mailHelper = _mailHelper;
        }

        public async Task<User> GetUser(string id)
        {
            return await unitOfWork.User.GetAsync(x => x.Id == id);
        }

        public async Task<List<UserMiniModel>> GetUsers(string searchKeyword)
        {
            List<UserMiniModel> userMiniModel = new List<UserMiniModel>();
            List<User> userList = await unitOfWork.User.FindListAsync(x => x.UserName == searchKeyword);

            foreach (var user in userList)
            {
                userMiniModel.Add(new UserMiniModel
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Firstname = user.FirstName,
                    Description = user.Description,
                    Email = user.Email,
                    PhotoUrl = user.Photourl
                });
            }
            return userMiniModel;
        }

        public async Task<User> Update(User user)
        {
            return await unitOfWork.User.UpdateUser(user);
        }


        public async Task<User> Login(UserForLoginModel userModel)
        {
                User user = await unitOfWork.User.GetAsync(x => x.UserName == userModel.username.ToLower());

                if (user != null && VerifyPassword(userModel.password, user.PasswordHash, user.PasswordSalt))
                {    
                        return user;       
                }
                else
                {
                    return null;
                }
              
        }

        public async Task<User> Register(UserRegisterModel registerModel)
        {
            byte[] passwordSalt;
            string passwordHash;

            User checkUser = await unitOfWork.User.GetAsync(x => x.UserName == registerModel.username || x.Email == registerModel.email);

            //if (checkUser == null)
            //{
                CreatePasswordHash(registerModel.password, out passwordHash, out passwordSalt);

                var check = unitOfWork.User.Insert(new User
                {
                    UserName = registerModel.username,
                    Email = registerModel.email,
                    ActivatedGuid = Guid.NewGuid(),
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Photourl = "http://www.iconninja.com/files/1024/108/58/users-human-avatar-people-male-account-person-user-man-profile-icon.png"
                });

                if (check.Result > 0)
                {
                    User user = await unitOfWork.User.GetAsync(x => x.UserName == registerModel.username);

                    string activeUri = $"http://localhost:4200/useractivate/{user.ActivatedGuid}";

                    string body = $"Merhaba {user.UserName} <br><br> Hesabýnýzý" + $"aktifleþtirmek için <a href='{activeUri}' target='_blank'>týklayýnýz</a>.";

                    mailHelper.SendMail(body, user.Email, "Hesap Aktifleþtirme");
                    return user;
                }
                return null;

            //}
            //else
            //{
            //    return null;
            //}

          
        }


        public async Task<User> ActivateUser(Guid activateId)
        {
            User user = await unitOfWork.User.GetAsync(x => x.ActivatedGuid == activateId);

            if( user != null)
            {
                if (user.IsActive)
                {
                   
                }

                user.IsActive = true;
                await unitOfWork.User.Update(user);
            }
            return user;
        }
             
        public void CreatePasswordHash(string password, out string passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512()){
                passwordSalt = hmac.Key;
                passwordHash = Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
            }
        }

        //passwordHash is changed from byte[] to string 
        //check out if it works with string
        private bool VerifyPassword(string password, string passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                byte[] passHash = Convert.FromBase64String(passwordHash);
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passHash[i])
                        return false;
                }
                return true;
            }
        }

        public async Task<List<UserMiniModel>> GetSearchedUsers(Dictionary<string, string> searchItems)
        {

            List<UserMiniModel> userMiniModel = new List<UserMiniModel>();
            List<User> searchedUserswithUsername = new List<User>();
            string searchKeyword = searchItems["searchKeyword"];
            //burdan followeeid'ler geliyor
            searchedUserswithUsername = await unitOfWork.User.FindListAsync(x => x.UserName == searchKeyword);
           //followerId
            string followerId = searchItems["followerId"];
            List<KeyValuePair<string, string>> ids = new List<KeyValuePair<string, string>>();

            foreach (var user in searchedUserswithUsername)
            {
                ids.Add(new KeyValuePair<string, string>("followeeId", user.Id));
            }


            ids.Add(new KeyValuePair<string, string>("followerId", followerId));
           
            List<User> userList = await unitOfWork.User.SearchPeopleAsync(ids);
    

            foreach (var user in userList)
            {
                for (int i = 0; i < searchedUserswithUsername.Count; i++)
                {
                    if (user.Id == searchedUserswithUsername[i].Id){
                    searchedUserswithUsername.RemoveAt(i);
                    }
                }
                
                  
                
            }

            foreach (var user in userList)
            {
                userMiniModel.Add(new UserMiniModel
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Firstname = user.FirstName,
                    Description = user.Description,
                    Email = user.Email,
                    PhotoUrl = user.Photourl,
                    isFollowed = user == null ? false : true,
                });
            }

            foreach (var user in searchedUserswithUsername)
            {
                userMiniModel.Add(new UserMiniModel
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Firstname = user.FirstName,
                    Description = user.Description,
                    Email = user.Email,
                    PhotoUrl = user.Photourl,
                    isFollowed = user != null ? false : true,
                });
            }
            //List<User> result =  searchedUserswithUsername.Except(userList as List<User>) as List<User>;
          
            return userMiniModel;
        }
    }
}