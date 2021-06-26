using core.Domain.Models;
using core.Domain.Repositories;
using core.Domain.Services;
using core.Domain.Services.Communication;
using core.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await _userRepository.ListAsync();
        }

        public async Task<User> FindByIdAsync(int id)
        {
            return await _userRepository.FindByIdAsync(id);
        }

        public async Task<UserResponse> FindByCredentials(string email, string password)
        {
            var user = await _userRepository.FindByEmailAsync(email);

            if (user == null)
            {
                return new UserResponse("Invalid email.");
            }

            if (user.Password != password)
            {
                return new UserResponse("Invalid password.");
            }

            return new UserResponse(user);
        }

        public async Task<UserResponse> SaveAsync(User user)
        {
            try
            {
                var existingUser = await _userRepository.FindByEmailAsync(user.Email);

                if (existingUser != null)
                {
                    return new UserResponse("Email already in use.");
                }

                existingUser = await _userRepository.FindByCpfAsync(user.Cpf);

                if (existingUser != null)
                {
                    return new UserResponse("CPF already in use.");
                }

                if (!ValidationFunctions.IsValidEmail(user.Email))
                {
                    return new UserResponse("Invalid email.");
                }

                if (!ValidationFunctions.IsValidCpf(user.Cpf))
                {
                    return new UserResponse("Invalid CPF.");
                }

                await _userRepository.AddAsync(user);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error has occurred when saving the user: {ex.Message}");
            }
        }

        public async Task<UserResponse> UpdateAsync(int id, User user)
        {
            var existingUser = await _userRepository.FindByIdAsync(id);

            if (existingUser == null)
            {
                return new UserResponse("User not found.");
            }

            var existingCpfUser = await _userRepository.FindByCpfAsync(user.Cpf);

            if (existingCpfUser != null && existingUser.Id != existingCpfUser.Id)
            {
                return new UserResponse("CPF already in use by another user.");
            }

            var existingEmailUser = await _userRepository.FindByEmailAsync(user.Email);

            if (existingEmailUser != null && existingUser.Id != existingEmailUser.Id)
            {
                return new UserResponse("Email already in use by another user.");
            }

            if (user.Email != existingUser.Email && !ValidationFunctions.IsValidEmail(user.Email))
            {
                return new UserResponse("Invalid email.");
            }

            if (user.Cpf != existingUser.Cpf && !ValidationFunctions.IsValidCpf(user.Cpf))
            {
                return new UserResponse("Invalid CPF.");
            }

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            existingUser.Cpf = user.Cpf;

            try
            {
                _userRepository.Update(existingUser);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error has occurred when updating the user: {ex.Message}");
            }
        }

        public async Task<UserResponse> DeleteAsync(int id)
        {
            var existingUser = await _userRepository.FindByIdAsync(id);

            if (existingUser == null)
            {
                return new UserResponse("User not found.");
            }

            try
            {
                _userRepository.Remove(existingUser);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error has occurred when deleting the user: {ex.Message}");
            }
        }
    }
}
