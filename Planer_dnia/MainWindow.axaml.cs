using System;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;

namespace Planer_dnia
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Zadanie> Zadania { get; set; } = new ObservableCollection<Zadanie>();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
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

            UpdateTaskList();
            UpdateCompletedTasksCount();
        }

        private void UpdateTaskList()
        {
            TasksList.Items.Clear();

            foreach (var zadanie in Zadania)
            {
                var stackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Spacing = 10,
                };

                var nazwaTextBlock = new TextBlock
                {
                    Text = zadanie.Nazwa,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                var kategoriaComboBox = new ComboBox
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    SelectedItem = zadanie.Kategoria
                };
                kategoriaComboBox.Items.Add("Praca");
                kategoriaComboBox.Items.Add("Relaks");
                kategoriaComboBox.Items.Add("Zakupy");

                var czyUkonczoneCheckBox = new CheckBox
                {
                    IsChecked = zadanie.CzyUkonczone,
                    Content = "Ukończone",
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                
                czyUkonczoneCheckBox.Checked += (sender, args) =>
                {
                    zadanie.CzyUkonczone = true;
                    UpdateCompletedTasksCount();
                };
                czyUkonczoneCheckBox.Unchecked += (sender, args) =>
                {
                    zadanie.CzyUkonczone = false;
                    UpdateCompletedTasksCount();
                };

                var deleteButton = new Button
                {
                    Content = "Usuń",
                    VerticalAlignment = VerticalAlignment.Center
                };
                deleteButton.Click += (sender, args) =>
                {
                    Zadania.Remove(zadanie);
                    UpdateTaskList();
                    UpdateCompletedTasksCount();
                };

                stackPanel.Children.Add(nazwaTextBlock);
                stackPanel.Children.Add(kategoriaComboBox);
                stackPanel.Children.Add(czyUkonczoneCheckBox);
                stackPanel.Children.Add(deleteButton);

                TasksList.Items.Add(stackPanel);
            }
        }

        private void UpdateCompletedTasksCount()
        {
            int completedTasks = 0;

            foreach (var zadanie in Zadania)
            {
                if (zadanie.CzyUkonczone)
                {
                    completedTasks++;
                }
            }

            SummaryTextBlock.Text = $"Liczba zadań: {Zadania.Count}, Ukończonych: {completedTasks}";
        }

        public class Zadanie
        {
            public string Nazwa { get; set; }
            public string Kategoria { get; set; }
            public bool CzyUkonczone { get; set; }
        }
    }
}
