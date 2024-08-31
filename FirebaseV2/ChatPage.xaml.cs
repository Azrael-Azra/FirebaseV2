using Firebase.Auth;
using FirebaseV2.Class;

namespace FirebaseV2;

public partial class ChatPage : ContentPage
{
    private readonly FirestoreService _firestoreService;
    private string _chatRoomId = "global_chat"; // For example, a global chat room ID
    private string _userId;
    public ChatPage(string userId)
	{
		InitializeComponent();
        _firestoreService = new FirestoreService();
        _userId = userId;
        LoadChatMessages();
    }

    private async void LoadChatMessages()
    {
        var messages = await _firestoreService.GetChatMessages(_chatRoomId);
        chatListView.ItemsSource = messages;
    }

    private async void OnSendClicked(object sender, EventArgs e)
    {
        string message = messageEntry.Text;

        if (!string.IsNullOrEmpty(message))
        {
            await _firestoreService.SaveChatMessage(_chatRoomId, _userId, message);
            messageEntry.Text = string.Empty;
            LoadChatMessages(); // Reload messages to include the new one
        }
    }

}