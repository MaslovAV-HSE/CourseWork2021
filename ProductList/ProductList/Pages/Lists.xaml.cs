using Microsoft.Maui.Controls.Shapes;
using ProductList;
namespace ProductList.Pages;



public partial class Lists : ContentPage
{

	User _user;
	private int row_counter = 0;
	private List<Label> labels = new List<Label>();

	public Lists()
	{
		InitializeComponent();
        MessagingCenter.Subscribe<Authorization, User>(this, "UserRegestrated", async (sender, arg) =>
        {
            AddUsersLists(arg);
			//FillLists(arg);
			_user = arg;
        });
    }

    private void AddUsersLists(User user)
    {
        MySQLclass bd = new MySQLclass();
        user.ProductLists = bd.GetUserLists(user.id);
		row_counter = user.ProductLists.Count();
    }

	private void FillLists(User user)
	{

		for(int i = 0; i < row_counter; i++)
		{
			Label label = new Label
			{
				Text = user.ProductLists[i].Name,
				TextColor = Colors.White,
				FontSize = 18,
				FontAttributes = FontAttributes.Bold,
				
			
			};

            Border border = new Border
            {
                Stroke = Color.FromArgb("#C49B33"),
                Background = Color.FromArgb("#2B0B98"),
                StrokeThickness = 4,
                Padding = new Thickness(16, 8),
                HorizontalOptions = LayoutOptions.Center,
                StrokeShape = new RoundRectangle
                {
                    CornerRadius = new CornerRadius(40, 0, 0, 40)
                },
                Content = label
                
            };

			
			//labels.Add(label);
            RowDefinition row = new RowDefinition();
            MainGrid.AddRowDefinition(row);
            //MainGrid.Add(border, 0, i);
        }
	}


	private async void Button_Clicked(object sender, EventArgs e)
	{
        string result = await DisplayPromptAsync("Новый список","Введите имя списка");
		
		if (result != null)
		{
			if (_user != null)
			{
				MySQLclass bd = new MySQLclass();
				_user.ProductLists.Add(new ProductListModel()
				{
					Id = bd.CreateUserList(_user.id, result),
					Name = result,
					Products = new List<ProductModel>()
				});					
			}
            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                
            };

            
            Label label = new Label
            {

                Text = result,
                TextColor = Colors.White,
                FontSize = 18,
                FontAttributes = FontAttributes.Bold
            };

            Border border = new Border
            {
                Stroke = Color.FromArgb("#C49B33"),
                Background = Color.FromArgb("#2B0B98"),
                StrokeThickness = 4,
                Padding = new Thickness(16, 8),
                HorizontalOptions = LayoutOptions.Center,
                StrokeShape = new RoundRectangle
                {
                    CornerRadius = new CornerRadius(40, 0, 0, 40)
                },
                Content = label

            };


            labels.Add(label);
            RowDefinition row = new RowDefinition();

            MainGrid.AddRowDefinition(row);
            MainGrid.Add(border, 0, row_counter);
            row_counter++;
		}
		return;
    }
    
}