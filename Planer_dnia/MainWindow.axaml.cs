using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MsBox.Avalonia;

namespace Planer_dnia;

public partial class MainWindow : Window
{
    public ObservableCollection<Zadanie> Zadania { get; set; } = new ObservableCollection<Zadanie>();

    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;
        TasksList.ItemsSource = Zadania;
    }

    private void AddTaskButtonClick(object sender, RoutedEventArgs e)
    {
        string nazwaZadania = TaskTextBox.Text;
        string kategoria = (CategoryComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

      
        var noweZadanie = new Zadanie
        {
            Nazwa = nazwaZadania,
            Kategoria = kategoria,
            CzyUkonczone = false
        };

        Zadania.Add(noweZadanie);
        TaskTextBox.Clear();
        CategoryComboBox.SelectedIndex = 0;
        UpdateSummary();
    }

    private void OnDeleteTaskButtonClick(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var taskToDelete = button?.DataContext as Zadanie;
        if (taskToDelete != null)
        {
            Zadania.Remove(taskToDelete);
            UpdateSummary();
        }
    }

    private void UpdateSummary()
    {
        int totalTasks = Zadania.Count;
        int completedTasks = Zadania.Count(t => t.CzyUkonczone);
        SummaryTextBlock.Text = $"Liczba zadań: {totalTasks}, Ukończonych: {completedTasks}";
    }
}
public class Zadanie
{
    public string Nazwa;
    public string Kategoria;
    public bool CzyUkonczone;
}