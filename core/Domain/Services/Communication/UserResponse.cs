using core.Domain.Models;

namespace core.Domain.Services.Communication
{
    public class UserResponse : BaseResponse
    {
        public User User { get; set; }

        private UserResponse(bool success, string message, User user) : base(success, message)
        {
            User = user;
        }

        public UserResponse(User user) : this(true, string.Empty, user)
        {

        }

        public UserResponse(string message) : this(false, message, null)
        {

        }
    }
}
