

namespace ProductList.Pages;

public partial class Account : ContentPage
{
    User _user;
	public Account()
	{
		InitializeComponent();
        MessagingCenter.Subscribe<Authorization, User>(this, "UserRegestrated", async (sender, arg) =>
        {
            _user = arg;
        }
        ); 
    }

    private async void OnButtonLogin_Clicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("»зменить логин", "¬ведите новый логин");
        _user.login = result;
    }

    private async void OnButtonEmail_Clicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("»зменить почту", "¬ведите новую почту");
        _user.email = result;
    }

    private async void OnButtonPassword_Clicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("»зменить пароль", "¬ведите новый пароль");
        _user.password = result;
    }
    

}