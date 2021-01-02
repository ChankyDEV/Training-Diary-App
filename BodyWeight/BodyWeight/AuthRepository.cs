using Firebase.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace BodyWeight
{
    public class AuthRepository : IAuth
    {
        private string webApiKey = "AIzaSyDjGLLGY1sWENpq0S07OGvkDm6WyetxyJA";

        public async Task LoginWithEmailAndPassword(string email, string password)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
            var auth = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
            var content = await auth.GetFreshAuthAsync();
            var serializedContent = JsonConvert.SerializeObject(content);
            Preferences.Set("MyFirebaseRefreshToken", serializedContent);
        }

        public async Task<string> RegisterWithEmailAndPassword(string email, string password)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
            var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
            return auth.User.LocalId;
        }
    }
}
