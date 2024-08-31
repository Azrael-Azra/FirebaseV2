using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirebaseV2.Class
{
    public class FirestoreService
    {
        private FirebaseClient _firebaseClient;

        public FirestoreService()
        {
            _firebaseClient = new FirebaseClient(FirebaseConfig.DatabaseUrl);
        }

        public async Task SaveUser(string userId, string username, string email)
        {
            await _firebaseClient
                .Child("Users")
                .Child(userId)
                .PutAsync(new { Username = username, Email = email });
        }

        public async Task SaveChatMessage(string chatRoomId, string userId, string message)
        {
            await _firebaseClient
                .Child("ChatRooms")
                .Child(chatRoomId)
                .Child("Messages")
                .PostAsync(new { UserId = userId, Message = message, Timestamp = DateTime.UtcNow });
        }

        public async Task<List<ChatMessage>> GetChatMessages(string chatRoomId)
        {
            return (await _firebaseClient
                .Child("ChatRooms")
                .Child(chatRoomId)
                .Child("Messages")
                .OnceAsync<ChatMessage>())
                .Select(item => new ChatMessage
                {
                    UserId = item.Object.UserId,
                    Message = item.Object.Message,
                    Timestamp = item.Object.Timestamp
                }).ToList();
        }
    }
}

public class ChatMessage
{
    public string UserId { get; set; }
    public string Message { get; set; }
    public DateTime Timestamp { get; set; }
}