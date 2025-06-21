using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Repositories;
using Moq;
using Moq.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;

namespace TestPettsStore
{
  
    public class TestUserRepository
    {

        [Fact]
        public async Task GetAllUsers_ReturnAllUsers()
        {
            var user = new User { Id = 1,FirstName = "Ruth", LastName = "Hermelin", Username = "ruthher1", Password = "8197" };

            var mockContext = new Mock<PettsStoreContext>();
            var users =new List<User>() { user };
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);
            var userRepository = new UserRepository(mockContext.Object);

            var result = await userRepository.getAllUsers();

            Assert.Equal(1, result.Count());

        }

        [Fact]
        public async Task CreateUser_AddsUserToContext()
        {
            var user1 = new User { Id = 1, FirstName = "Ruth", LastName = "Hermelin", Username = "ruthher1", Password = "8197" };
            var user2 = new User { Id = 2, FirstName = "Ruthhhh", LastName = "Hermelinnnn", Username = "ruthher1111", Password = "819777" };

            var mockContext = new Mock<PettsStoreContext>();
            var users = new List<User>() { user1 }.AsQueryable();
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);
            var userRepository = new UserRepository(mockContext.Object);

            await userRepository.addUser(user1);


            mockContext.Verify(x => x.Users.AddAsync(user1, It.IsAny<CancellationToken>()), Times.Once);
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetUser_ById_ReturnsUser()
        {
            var user = new User { Id = 1, FirstName = "Ruth", LastName = "Hermelin", Username = "ruthher1", Password = "8197" };

            var mockContext = new Mock<PettsStoreContext>();
            var users = new List<User> { user }.AsQueryable();
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);
            var userRepository = new UserRepository(mockContext.Object);

            var result = await userRepository.getUserById(1);

            Assert.NotNull(result);
            Assert.Equal(user.Username, result.Username);
        }


        [Fact]
        public async Task UpdateUser_UpdatesUserInContext()
        {
            var user = new User { Id = 1, FirstName = "Ruth", LastName = "Hermelin", Username = "ruthher1", Password = "8197" };
            var newuser = new User { Id = 1, FirstName = "Ruth", LastName = "Levi", Username = "ruthher1", Password = "8197" };

            var users = new List<User>() { user };
            var mockContext = new Mock<PettsStoreContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);
            var userRepository = new UserRepository(mockContext.Object);

            await userRepository.updateUser(user.Id, newuser);


            mockContext.Verify(x => x.Users.Update(newuser), Times.Once);
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
        

        [Fact]
        public async Task Login_ValidUser()
        {
            var user = new User { Id = 1, FirstName = "Ruth", LastName = "Hermelin", Username = "ruthher1", Password = "8197" };
            var loginuser = new User {Username = "ruthher1", Password = "8197" };

            var users = new List<User>() { user };
            var mockContext = new Mock<PettsStoreContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);
            var userRepository = new UserRepository(mockContext.Object);

            var result=await userRepository.login(loginuser);

            Assert.Equal(user, result);
        }

        [Fact]
        public async Task Login_NotValidUser()
        {
            var user = new User { Id = 1, FirstName = "Ruth", LastName = "Hermelin", Username = "ruthher1", Password = "8197" };
            var loginuser = new User { Username = "ruthher1", Password = "1111" };

            var users = new List<User>() { user };
            var mockContext = new Mock<PettsStoreContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);
            var userRepository = new UserRepository(mockContext.Object);

            var result = await userRepository.login(loginuser);

            Assert.Null(result);

        }

    }
}