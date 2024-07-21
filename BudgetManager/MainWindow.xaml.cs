using System.Windows;

namespace BudgetManager
{
    public partial class MainWindow : Window
    {
        private Dictionary<string, string> users = new Dictionary<string, string>();
        private List<string> categories = new List<string>();
        private List<Transaction> transactions = new List<Transaction>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = Username.Text;
            string password = Password.Password;

            if (users.ContainsKey(username) && users[username] == password)
            {
                MessageBox.Show("Login successful!");
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string username = Username.Text;
            string password = Password.Password;

            if (!users.ContainsKey(username))
            {
                users.Add(username, password);
                MessageBox.Show("Registration successful!");
            }
            else
            {
                MessageBox.Show("Username already exists.");
            }
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            string categoryName = CategoryName.Text;

            if (!categories.Contains(categoryName))
            {
                categories.Add(categoryName);
                Categories.Items.Add(categoryName);
                CategorySelector.Items.Add(categoryName);
                MessageBox.Show("Category added successfully!");
            }
            else
            {
                MessageBox.Show("Category already exists.");
            }
        }

        private void AddTransaction_Click(object sender, RoutedEventArgs e)
        {
            string category = (string)CategorySelector.SelectedItem;
            string description = Description.Text;
            double amount;

            if (double.TryParse(Amount.Text, out amount))
            {
                Transaction transaction = new Transaction(category, description, amount);
                transactions.Add(transaction);
                Transactions.Items.Add(transaction);
                UpdateTotal();
                MessageBox.Show("Transaction added successfully!");
            }
            else
            {
                MessageBox.Show("Invalid amount.");
            }
        }
        private void GenerateReport_Click(object sender, RoutedEventArgs e)
        {
            string report = "Category\tDescription\tAmount\n";

            foreach (var transaction in transactions)
            {
                report += $"{transaction.Category}\t{transaction.Description}\t{transaction.Amount:C}\n";
            }

            // Visa rapporten i en MessageBox för enkel visning
            MessageBox.Show(report, "Transaction Report");

            // Alternativt, spara rapporten till en fil
            // System.IO.File.WriteAllText("TransactionReport.txt", report);
        }


        private void UpdateTotal()
        {
            double total = 0;
            foreach (Transaction transaction in transactions)
            {
                total += transaction.Amount;
            }
            Total.Text = $"Total: {total:C}";
        }
    }

    public class Transaction
    {
        public string Category { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }

        public Transaction(string category, string description, double amount)
        {
            Category = category;
            Description = description;
            Amount = amount;
        }

        public override string ToString()
        {
            return $"{Category}: {Description} - {Amount:C}";
        }
    }
}