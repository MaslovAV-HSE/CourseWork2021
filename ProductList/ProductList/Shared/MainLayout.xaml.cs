using ProductList;
using ProductList.Pages;

namespace ProductList;

 
public partial class MainLyout : Shell
{
    public MainLyout()
	{
		InitializeComponent();
        Rec.IsVisible = false;
        Account.IsVisible = false;

		MessagingCenter.Subscribe<Authorization,User>(this, "UserRegestrated", async (sender, arg) =>
        {
            CurrentItem = lists;
            Reg.IsVisible = false;
            Rec.IsVisible = true;
            Auth.IsVisible = false;
            Account.IsVisible = true;
            UserName.Text = arg.name;
            Usermail.Text = arg.email;
        }
        );

    }

}
