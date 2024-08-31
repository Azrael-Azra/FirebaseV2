using FirebaseV2.Class;

namespace FirebaseV2;

public partial class Registration : ContentPage
{
    private readonly AuthService _authService;
    private readonly FirestoreService _firestoreService;

    public Registration()
	{
		InitializeComponent();
        _authService = new AuthService();
        _firestoreService = new FirestoreService();

    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        string username = usernameEntry.Text;
        string email = emailEntry.Text;
        string password = passwordEntry.Text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            await DisplayAlert("Error", "Please fill in all fields.", "OK");
            return;
        }

        try
        {
            // Register the user with Firebase Authentication
            string token = await _authService.Register(email, password, username);

            // Save the user information in Firestore
            var userId = "userId"; // Replace this with the actual user ID from Firebase Authentication
            await _firestoreService.SaveUser(userId, username, email);

            await DisplayAlert("Success", "Registered successfully!", "OK");

            // Navigate back to the login page
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}
