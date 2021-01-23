using OnlineBookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBookstore.DAL
{
    public class UsersAccess
    {
        OnlineBookstoreDbContext context = new OnlineBookstoreDbContext();
        
        public int CreateUser(Users users)
        {
            // kiểm tra username đã tồn tại chưa
            var user = GetUserByUsername(users.Username);
            if (user != null)
            {
                return 0;
            }
            context.Users.Add(users);
            context.SaveChanges();
            return users.Id;
        }

        public List<Users> ReadUsers()
        {
            List<Users> users = context.Users.Where(x => x.Status != 0).ToList();
            return users;
        }

        public Users ReadUser(int id)
        {
            Users users = context.Users.FirstOrDefault(x => x.Id == id);
            return users;
        }

        public bool UpdateUser(Users users)
        {
            Users updateUser = context.Users.FirstOrDefault(x => x.Id == users.Id);
            if (updateUser != null)
            {
                updateUser.FullName = users.FullName;
                if (!string.IsNullOrEmpty(users.Password))
                {
                    updateUser.Password = users.Password;
                }
                updateUser.Email = users.Email;
                updateUser.Gender = users.Gender;
                updateUser.Address = users.Address;
                updateUser.PhoneNumber = users.PhoneNumber;
                updateUser.Authorization = users.Authorization;
                updateUser.ModifiedDate = DateTime.Now;
                updateUser.ModifiedBy = users.ModifiedBy;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteUser(int id)
        {
            Users users = context.Users.FirstOrDefault(x => x.Id == id);
            if (users != null)
            {
                // Move this user to trash
                users.ModifiedDate = DateTime.Now;
                users.Status = 0;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public int SignIn(string username, string password)
        {
            Users users = context.Users.FirstOrDefault(x => x.Username == username && x.Status != 0);
            if (users != null)
            {
                // chưa hoạt động
                if (users.Status == 2)
                {
                    return 2;
                }
                else
                {
                    if (users.Password == password)
                    {
                        return 1;
                    }
                    // sai mật khẩu
                    else
                    {
                        return -1;
                    }
                }
            }
            // bị xóa
            return 0;
        }

        public Users GetUserByUsername(string username)
        {
            return context.Users.FirstOrDefault(x => x.Username == username && x.Status != 0);
        }

        public Users GetUserById(int id)
        {
            return context.Users.FirstOrDefault(x => x.Id == id);
        }

        public List<Users> GetUsersInTrash()
        {
            List<Users> users = context.Users.Where(x => x.Status == 0).ToList();
            return users;
        }

        public bool ReTrash(int id)
        {
            Users users = context.Users.FirstOrDefault(x => x.Id == id);
            if (users != null)
            {
                // Move this user out of the trash
                users.ModifiedDate = DateTime.Now;
                users.Status = 1;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public void Status(int id, string modifiedBy)
        {
            var user = GetUserById(id);
            user.Status = (user.Status == 1) ? 2 : 1;
            user.ModifiedBy = modifiedBy;
            user.ModifiedDate = DateTime.Now;
            context.SaveChanges();
        }
    }
}