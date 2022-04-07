using Avalonia.Controls;
using Avalonia.Interactivity;
using Lab_8.Models;
using Lab_8.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using Avalonia.Media.Imaging;
using System.Reactive;

namespace Lab_8.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.FindControl<MenuItem>("Exit").Click += ClickEventExit;
            this.FindControl<MenuItem>("About").Click += ClickEventAbout;
            this.FindControl<MenuItem>("Save").Click += ClickEventSave;
            this.FindControl<MenuItem>("Load").Click += ClickEventLoad;
        }

        private async void ClickEventExit(object? sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private async void ClickEventAbout(object? sender, RoutedEventArgs e)
        {
            await new About().ShowDialog((Window)this.VisualRoot);
            
        }

        private async void ClickEventSave(object? sender, RoutedEventArgs e)
        {
            string? path;
            var taskPath = new SaveFileDialog()
            {
                Title = "Save file",
                Filters = new List<FileDialogFilter>()
            };
            taskPath.Filters.Add(new FileDialogFilter() { Name = "Текстовый файл (.txt)", Extensions = { "txt" } });

            path = await taskPath.ShowAsync((Window)this.VisualRoot);
            if (path is not null)
            {

                var Plan = (this.DataContext as MainWindowViewModel).ItemsPlanned;
                var Work = (this.DataContext as MainWindowViewModel).ItemsInWork;
                var Complete = (this.DataContext as MainWindowViewModel).ItemsCompleted;
                using (StreamWriter wr = File.CreateText(path))
                {
                    wr.WriteLine(Plan.Count.ToString());
                    foreach (var item in Plan)
                    {
                        wr.WriteLine(item.Title);
                        wr.WriteLine(item.Notes);
                        wr.WriteLine(item.ImgName);
                    }
                    wr.WriteLine(Work.Count.ToString());
                    foreach (var item in Work)
                    {
                        wr.WriteLine(item.Title);
                        wr.WriteLine(item.Notes);
                        wr.WriteLine(item.ImgName);
                    }
                    wr.WriteLine(Complete.Count.ToString());
                    foreach (var item in Complete)
                    {
                        wr.WriteLine(item.Title);
                        wr.WriteLine(item.Notes);
                        wr.WriteLine(item.ImgName);
                    }

                }
                
            }
        }

        private async void ClickEventLoad(object? sender, RoutedEventArgs e)
        {
            string? path;
            var taskPath = new OpenFileDialog()
            {
                Title = "Open file",
                Filters = new List<FileDialogFilter>()
            };
            taskPath.Filters.Add(new FileDialogFilter() { Name = "TXT files", Extensions = { "txt" } });

            string[]? pathArray = await taskPath.ShowAsync((Window)this.VisualRoot);
            path = pathArray is null ? null : string.Join(@"\", pathArray);

            if (path != null)
            {
                var Plan = (this.DataContext as MainWindowViewModel).ItemsPlanned;
                var Work = (this.DataContext as MainWindowViewModel).ItemsInWork;
                var Complete = (this.DataContext as MainWindowViewModel).ItemsCompleted;
                Plan.Clear(); Work.Clear(); Complete.Clear();
                using (StreamReader sr = File.OpenText(path))
                {
                    int count;
                    if (!Int32.TryParse(sr.ReadLine(), out count)) return; // В первой строке должно быть количество студентов

                    for (int i = 0; i < count; i++)
                    {
                        Plan.Add(new Tasks(sr.ReadLine(), sr.ReadLine(), sr.ReadLine()));
                    }
                    count = 0;
                    if (!Int32.TryParse(sr.ReadLine(), out count)) return; // В первой строке должно быть количество студентов

                    for (int i = 0; i < count; i++)
                    {
                        Work.Add(new Tasks(sr.ReadLine(), sr.ReadLine(), sr.ReadLine()));
                    }
                    count = 0;
                    if (!Int32.TryParse(sr.ReadLine(), out count)) return; // В первой строке должно быть количество студентов

                    for (int i = 0; i < count; i++)
                    {
                        Complete.Add(new Tasks(sr.ReadLine(), sr.ReadLine(), sr.ReadLine()));
                    }

                }
            }
        }



    }
}
