//using Microsoft.AspNetCore.Mvc;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {

        PettsStoreContext _pettsStoreContext;
        public UserRepository(PettsStoreContext pettsStoreContext)
        {
            _pettsStoreContext = pettsStoreContext;
        }

        public async Task<User> getUserById(int id)// GetUserById
        {
            return await _pettsStoreContext.Users.FirstOrDefaultAsync(user=>user.Id==id);
            // return user;
        }

        public async Task<List<User>> getAllUsers()// GetAllUsers
        {
            return await _pettsStoreContext.Users.ToListAsync();
        }
        public async Task<User> addUser(User user) // AddUser
        {
            await _pettsStoreContext.Users.AddAsync(user);
            await _pettsStoreContext.SaveChangesAsync();
            return user;
        }
        public async Task<User> updateUser(int id, User updateUser) //UpdateUser
        {
            _pettsStoreContext.Users.Update(updateUser);
            await _pettsStoreContext.SaveChangesAsync();
            return updateUser;
        }

        public async Task<User> login(User newUser)//Login
        {
            return await _pettsStoreContext.Users.FirstOrDefaultAsync(user => user.Username == newUser.Username && user.Password == newUser.Password);
            //return user;
        }

    }
}
