using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Firebase.Auth;
using System.Threading.Tasks;

namespace FirebaseV2.Class
{
//For Acount Creration and Sign In Make Sure the Authentication is on also with the Realtime Database
    public class AuthService
    {
        private FirebaseAuthProvider _authProvider;

        public AuthService()
        {
            _authProvider = new FirebaseAuthProvider(new Firebase.Auth.FirebaseConfig(FirebaseConfig.ApiKey));
        }

        public async Task<string> Register(string email, string password, string username)
        {
            var auth = await _authProvider.CreateUserWithEmailAndPasswordAsync(email, password, username);
            return auth.User.LocalId; // Return the user ID
        }

        public async Task<string> Login(string email, string password)
        {
            var auth = await _authProvider.SignInWithEmailAndPasswordAsync(email, password);
            return auth.User.LocalId; // Return the user ID
        }
    }
}
