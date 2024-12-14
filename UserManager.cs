using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class UserManager
{
    private readonly IUserRepository _userRepository;

    public UserManager(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    // Kullanıcı Login Kontrolü
    public bool ValidateUser(User user)
    {
        return _userRepository.ValidateUser(user.Username, user.Password);
    }

}

