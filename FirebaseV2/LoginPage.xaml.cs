using FirebaseV2.Class;

namespace FirebaseV2;

public partial class LoginPage : ContentPage
{
    private readonly AuthService _authService;
    private readonly FirestoreService _firestoreService;

    public LoginPage() 
    {
       InitializeComponent();
        _authService = new AuthService();
        _firestoreService = new FirestoreService();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string email = emailEntry.Text;
        string password = passwordEntry.Text;

        try
        {
            string token = await _authService.Login(email, password);
            await DisplayAlert("Success", "Logged in successfully!", "OK");

            // Navigate to the chat page
            var userId = "userId"; // Replace with the actual user ID obtained from Firebase Authentication
            await Navigation.PushAsync(new ChatPage(userId));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Registration());
    }
}