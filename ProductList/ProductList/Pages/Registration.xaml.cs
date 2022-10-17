
using ProductList;
namespace ProductList.Pages;

public partial class Registration : ContentPage
{
	public static User currentUser;
	public Registration()
	{
		InitializeComponent();
	}

	private void OnButtonEnter_Clicked(object sender, EventArgs e)
	{
		if(CheckData() && CheckLogin())
		{
			MySQLclass bd = new MySQLclass();
			bd.NewUser(login.Text, password.Text, phone.Text, email.Text, name.Text);
			currentUser = bd.GetUserByLogin(login.Text);
			MessagingCenter.Send<Registration, User>(this, "UserRegestrated", currentUser);
		}
	}

	private bool CheckData()
	{
		if (!String.IsNullOrWhiteSpace(name.Text) && !String.IsNullOrWhiteSpace(phone.Text) && !String.IsNullOrWhiteSpace(email.Text) &&
            !String.IsNullOrWhiteSpace(login.Text) && !String.IsNullOrWhiteSpace(password.Text)) return true;
		return false;
	}

	private bool CheckLogin()
	{
		return true;
	}
}