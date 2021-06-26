using ProjetoDFS.Domain.Models;

namespace ProjetoDFS.Tests.Helpers
{
    public class UserBuilder
    {
        private string _name { get; set; }
        private string _cpf { get; set; }
        private string _email { get; set; }
        private string _password { get; set; }

        public UserBuilder()
        {

        }

        public UserBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public UserBuilder WithCpf(string cpf)
        {
            _cpf = cpf;
            return this;
        }

        public UserBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public UserBuilder WithPassword(string password)
        {
            _password = password;
            return this;
        }

        public User Build()
        {
            return new User
            {
                Name = _name,
                Cpf = _cpf,
                Email = _email,
                Password = _password
            };
        }

        public UserBuilder DefaultUser()
        {
            _name = "User";
            _cpf = "73960145063";
            _email = "username@domain.com";
            _password = "teste1234";

            return this;
        }
    }
}