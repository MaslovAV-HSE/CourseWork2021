using ProductList.ViewModel;

namespace ProductList.Views;

public partial class ProductListView : ContentPage
{
    User user;
    ProductListModel ProductList;

	public ProductListView()
	{
		InitializeComponent();
		this.BindingContext = new ProductListViewModel();
    }
    private void FillProducts()
    {
        
    }
}