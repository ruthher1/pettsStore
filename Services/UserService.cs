using Repositories;
using Entities;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using AutoMapper;
using DTOs;
using System.ComponentModel.DataAnnotations;
namespace Services
{
    public class UserService : IUserService
    {
        IUserRepository userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;


        public UserService(IUserRepository userRepository, IMapper mapper, ILogger<UserService> logger)
        {
            this.userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserDTO> getUserById(int id)
        {
            return _mapper.Map<User, UserDTO>(await userRepository.getUserById(id));
        }
        public async Task<List<UserDTO>> getAllUsers()
        {
            return _mapper.Map<List<User>, List<UserDTO>>(await userRepository.getAllUsers());
        }
        public async Task<UserDTO> addUser(UserRegisterDTO user)
        {
            if(user.FirstName.Length < 2 || user.FirstName.Length > 50)
            {
                _logger.LogWarning("First name is out of range: {FirstName}", user.FirstName);
                throw new ArgumentException("First name must be at least 2 characters long an less than 50 characters long.");
            }
            if (!IsValidEmail(user.Username))
            {
                _logger.LogWarning("Invalid email: {Email}", user.Username);
                throw new ArgumentException("Email is not valid.");
            }

            if (GetPassStrength(user.Password) == false)
            {
                throw new ArgumentException("Password is not strong enough.");
            }
            return _mapper.Map<User, UserDTO>(await userRepository.addUser(_mapper.Map<UserRegisterDTO, User>(user)));
        }

        public async Task<UserDTO> updateUser(int id, UserRegisterDTO userUpdate)
        {
            if (userUpdate.FirstName.Length < 2 || userUpdate.FirstName.Length > 50)
            {
                _logger.LogWarning("First name is out of range: {FirstName}", userUpdate.FirstName);
                throw new ArgumentException("First name must be at least 2 characters long an less than 50 characters long.");
            }
            if (!IsValidEmail(userUpdate.Username))
            {
                _logger.LogWarning("Invalid email: {Email}", userUpdate.Username);
                throw new ArgumentException("Email is not valid.");
            }

            if (GetPassStrength(userUpdate.Password) == false)
            {
                throw new ArgumentException("Password is not strong enough.");
            }

            return _mapper.Map<User, UserDTO>(await userRepository.updateUser(id, _mapper.Map<UserRegisterDTO, User>(userUpdate)));
        }

        public async Task<UserDTO> login(UserLoginDTO newUser)
        {
            User user = await userRepository.login(_mapper.Map<UserLoginDTO, User>(newUser));
            if (user == null)
            {
                _logger.LogWarning("Login failed for user: {Username}", newUser.Username);
                return null;
            }
            _logger.LogInformation("User {Username} logged in successfully.", user.Username);
            return _mapper.Map<User, UserDTO>(user);
        }

        public bool GetPassStrength(string password)
        {
            var zxcvbnResult = Zxcvbn.Core.EvaluatePassword(password).Score;
            return zxcvbnResult >= 3;

        }
        private bool IsValidEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }
    }
}
