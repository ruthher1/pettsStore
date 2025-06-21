using Repositories;
using Entities;
using System.Text.Json;
using AutoMapper;
using DTOs;
namespace Services
{
    public class UserService : IUserService
    {
        IUserRepository userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            _mapper = mapper;
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
            return _mapper.Map<User, UserDTO>(await userRepository.addUser(_mapper.Map<UserRegisterDTO, User>(user)));
        }

        public async Task<UserDTO> updateUser(int id, UserRegisterDTO userUpdate)
        {
            return _mapper.Map<User, UserDTO>(await userRepository.updateUser(id, _mapper.Map<UserRegisterDTO, User>(userUpdate)));
        }

        public async Task<UserDTO> login(UserLoginDTO newUser)
        {
            return _mapper.Map<User, UserDTO>(await userRepository.login(_mapper.Map<UserLoginDTO, User>(newUser)));
        }

        public int GetPassStrength(string password)
        {
            return Zxcvbn.Core.EvaluatePassword(password).Score;
        }
    }
}
