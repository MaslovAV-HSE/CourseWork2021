namespace ProductList.Pages;

public partial class Authorization : ContentPage
{
	
    public Authorization()
	{
		InitializeComponent();
	}

	private void OnButtonEnter_Clicked(object sender, EventArgs e)
	{
		User currentUser;
        MySQLclass bd = new MySQLclass();

		if (CheckData())
		{
            currentUser = bd.GetUserByLogin(login.Text);
            MessagingCenter.Send<Authorization, User>(this, "UserRegestrated", currentUser);
        }
		
    }
	private bool CheckData()
	{
		if (!String.IsNullOrWhiteSpace(login.Text) && !String.IsNullOrWhiteSpace(password.Text)) 
			return true; 
		else
			return false;
	}
}