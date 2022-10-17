using Google.Protobuf.Collections;
using MySql.Data.MySqlClient;



namespace ProductList
{
    public class MySQLclass
    {
        private string connStr = "server=localhost;user=root;database=coursework2021;password=PassforSQL01;";
        private MySqlConnection conn;
        private MySqlCommand cmd;  
        private MySqlDataReader reader;

        
        public MySQLclass()
        {
            conn = new MySqlConnection(connStr);
            conn.Open();
        }

        public void NewUser(string login, string password, string phone, string email, string name) 
        {
            CreateUser(login, password);
            CreateUserData(login, phone, email, name);
        }

        private void CreateUser(string login, string password)
        {
            string comand = $"insert coursework2021.user(login, password, level) values(\"{login}\", \"{password}\", 0)";
            cmd = new MySqlCommand(comand, conn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Ошибка Созздания пользователя {ex}");
            }
            
        }

        private void CreateUserData(string login, string phone, string email, string name)
        {
            string comand = $"SELECT id FROM coursework2021.user where login = \"{login}\"";
            cmd = new MySqlCommand(comand, conn);
            try
            {
                string id = cmd.ExecuteScalar().ToString();
                comand = $"insert coursework2021.userdata(user_id,phone,email,name) values ({id},\"{phone}\", \"{email}\",\"{name}\")";
                cmd = new MySqlCommand(comand, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка Созздания пользователя {ex}");
            }
            
        }

        public void DeleteUserById(int id)
        {
            string comand = $"delete from coursework2021.user where id = {id}";
            cmd = new MySqlCommand(comand, conn);
            cmd.ExecuteNonQuery();
        }

        public string[] FindUser(int id)
        {

            string comand = $"SELECT coursework2021.user.login, coursework2021.user.password, coursework2021.userdata.email, coursework2021.userdata.name, coursework2021.userdata.phone from coursework2021.user join coursework2021.userdata on coursework2021.user.Id = coursework2021.userdata.user_id where id = {id}";

            string[] userdata = new string[5];
            MySqlCommand command = new MySqlCommand(comand, conn);
            // объект для чтения ответа сервера
            MySqlDataReader reader = command.ExecuteReader();
            // читаем результат
            while (reader.Read())
            {
                for (int i = 0; i < userdata.Length; i++)
                {
                    userdata[i] = reader[i].ToString();
                }

            }
            if (userdata[0] == null)
                userdata = new string[] { "NoDataFound" };
            reader.Close();
            return userdata;

        }
        public User GetUserByLogin(string login)
        {
            string command = $"SELECT coursework2021.user.Id, coursework2021.user.login, coursework2021.user.password, coursework2021.user_data.email, " +
                $"coursework2021.user_data.name, coursework2021.user_data.phone " +
                $"FROM coursework2021.user join coursework2021.user_data " +
                $"where coursework2021.user.login = \"{login}\"";
            cmd = new MySqlCommand(command, conn);
            // объект для чтения ответа сервера
            reader = cmd.ExecuteReader();
            User user = new User();
            // читаем результат
            while (reader.Read())
            {
                user = new User()
                {
                    id = Int32.Parse(reader[0].ToString()),
                    login = login,
                    password = reader[2].ToString(),
                    email = reader[3].ToString(),
                    name = reader[4].ToString(),
                    phone = reader[5].ToString(),
                };
            }
            reader.Close();
            return user;
        }
        
        //-------------------------------------------------------------------------------------------------------

        public int CreateUserList(int user_id, string list_name)
        {
            
            try
            {
                string comand = $"insert coursework2021.list(name) values(\"{list_name}\");";
                cmd = new MySqlCommand(comand, conn);
                cmd.ExecuteNonQuery();
                string id = cmd.LastInsertedId.ToString();
                comand = $"insert into coursework2021.user_list(user, list) values ({user_id},{id})";
                cmd = new MySqlCommand(comand, conn);
                cmd.ExecuteNonQuery();
                return Int32.Parse(id); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка Созздания списка{ex}");
            }
            return 0;
        }

        public List<ProductModel> GetAllProducts()
        {
            List<ProductModel> products = new List<ProductModel>();
            string command = "select coursework2021.product.Id, coursework2021.product.Product_name, coursework2021.category.Category_name " +
                "from coursework2021.product " +
                "left join coursework2021.product_category " +
                "on coursework2021.product.Id = coursework2021.product_category.product " +
                "left join coursework2021.category " +
                "on coursework2021.category.Id = coursework2021.product_category.category " +
                "order by coursework2021.product.Id";

            cmd = new MySqlCommand(command, conn);
            // объект для чтения ответа сервера
            MySqlDataReader reader = cmd.ExecuteReader();
            // читаем результат
            while (reader.Read())
            {

                    ProductModel model = new ProductModel()
                    {
                        Id = Int32.Parse(reader[0].ToString()),
                        Name = reader[1].ToString(),
                        Category = reader[2].ToString()
                    };
                    products.Add(model);
            }
            reader.Close();
            return products;

        }

        public List<ProductListModel> GetUserLists(int user_id)
        {
            List<ProductListModel> lists = new List<ProductListModel>();
            string command = $"SELECT coursework2021.user_list.list, coursework2021.list.name FROM " +
                $"coursework2021.user_list join coursework2021.list where coursework2021.user_list.user = {user_id};";
            cmd = new MySqlCommand(command, conn);
            
            
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {                               
                ProductListModel model = new ProductListModel() {
                    Id = Int32.Parse(reader[0].ToString()),
                    Name = reader[1].ToString()                    
                };
                lists.Add(model);
            }
            reader.Close();


            List<ProductModel> AllProducts = GetAllProducts();

            foreach (var productlist in lists)
            {
                List<int> prods = GetAllProductsFromList(productlist.Id);
                List<ProductModel> productLists = AllProducts.Where(p => prods.Contains(p.Id)).ToList();
                productlist.Products = productLists;
                
            }


            return lists;
        }

        public void AddProductToList(int list_id, int product_id)
        {
            try
            {
                string comand = $"insert into coursework2021.product_list(list_id, product_id) values ({list_id},{product_id})";
                cmd = new MySqlCommand(comand, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка Созздания списка{ex}");
            }
        }

        public List<int> GetAllProductsFromList(int list_id)
        {
            List<int> products = new List<int>();
            string command = $"SELECT product_id FROM coursework2021.product_list where coursework2021.product_list.list_id = {list_id}";

            cmd = new MySqlCommand(command, conn);
            // объект для чтения ответа сервера
            MySqlDataReader reader = cmd.ExecuteReader();
            // читаем результат
            while (reader.Read())
            {
                products.Add((int)reader[0]);
            }
            reader.Close();
            return products;
        }
    }
}
