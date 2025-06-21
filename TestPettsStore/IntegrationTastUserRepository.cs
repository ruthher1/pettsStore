using Entities;
using Moq;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPettsStore
{
    public class IntegrationTastUserRepository:IClassFixture<DatabaseFixture>
    {
        private readonly PettsStoreContext _dbContext;
        private readonly UserRepository userRepository;

        public IntegrationTastUserRepository(DatabaseFixture databaseFixture)
        {
            this._dbContext = databaseFixture.Context;
            this.userRepository = new UserRepository(this._dbContext);
        }

        [Fact]
        public async Task getUser_ValidCredentials_ReturnsUser()
        {
            var user = new User {FirstName = "Ruth", LastName = "Hermelin", Username = "ruthher1", Password = "8197" };
            var loginuser = new User { Username = "ruthher1", Password = "8197" };

            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            var result = await userRepository.login(loginuser);

            Assert.NotNull(result);
            Assert.Equal(result, user);
        }
        [Fact]
        public async Task Login_InvalidUsername_ReturnsNull()
        {
            var loginuser = new User { Username = "unknownUser", Password = "password" };

            var result = await userRepository.login(loginuser);

            Assert.Null(result);
        }
        [Fact]
        public async Task Login_InvalidPassword_ReturnsNull()
        {
            var user = new User { FirstName = "Ruth", LastName = "Hermelin", Username = "ruthher1", Password = "8197" };
            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            var loginuser = new User { Username = "ruthher1", Password = "wrongPassword" };

            var result = await userRepository.login(loginuser);

            Assert.Null(result);
        }
        [Fact]
        public async Task Login_EmptyCredentials_ReturnsNull()
        {
            var loginuser = new User { Username = "", Password = "" };

            var result = await userRepository.login(loginuser);

            Assert.Null(result);
        }

        [Fact]

        public async Task AddUser_ValidUser_ReturnsAddedUser()

        {

            var user = new User { FirstName = "Ruth", LastName = "Hermelin", Username = "ruthher1", Password = "8197" };

            var result = await userRepository.addUser(user);


            Assert.NotNull(result);

            Assert.Equal(user.Username, result.Username);

            Assert.Equal(user.FirstName, result.FirstName);

        }


        [Fact]

        public async Task UpdateUser_ValidId_ReturnsUpdatedUser()

        {

            var user = new User { FirstName = "Ruth", LastName = "Hermelin", Username = "ruthher1", Password = "8197" };
            await userRepository.addUser(user);


            var userUpdate = new User { FirstName = "NewName", Password = "111", Username = "updateUser", LastName = "New" };

            var result = await userRepository.updateUser(user.Id, userUpdate);


            Assert.NotNull(result);

            Assert.Equal("NewName", result.FirstName);

        }

        [Fact]

        public async Task GetUserById_ValidId_ReturnsUser()

        {

            var user = new User { FirstName = "Ruth", LastName = "Hermelin", Username = "ruthher1", Password = "8197" };
            await userRepository.addUser(user);


            var result = await userRepository.getUserById(user.Id);


            Assert.NotNull(result);

            Assert.Equal(user.Username, result.Username);

        }

        [Fact]
        public async Task GetAllUsers_ReturnsListOfUsers()

        {

            var user = new User { FirstName = "Ruth", LastName = "Hermelin", Username = "ruthher1", Password = "8197" };
            await userRepository.addUser(new User { FirstName = "User2", Username = "user2", Password = "pass" });


            var result = await userRepository.getAllUsers();


            Assert.NotNull(result);

            Assert.True(result.Count >= 2); // Ensure it returns all users added

        }


    }
}
