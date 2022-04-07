using Lab_8.Models;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Lab_8.ViewModels;
using System;
using System.IO;
using Avalonia.Media.Imaging;
using System.Reactive;




namespace Lab_8.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            List<Tasks> TasksPl = FillTasks(1);
            List<Tasks> TasksIn = FillTasks(2);
            List<Tasks> TasksCo = FillTasks(3);

            ItemsPlanned = new ObservableCollection<Tasks>(TasksPl);
            ItemsInWork = new ObservableCollection<Tasks>(TasksIn);
            ItemsCompleted = new ObservableCollection<Tasks>(TasksCo);

        }

        ObservableCollection<Tasks> itemsPlanned;
        public ObservableCollection<Tasks> ItemsPlanned
        {
            get{return itemsPlanned;}
            set{this.RaiseAndSetIfChanged(ref itemsPlanned, value);}
        }
        ObservableCollection<Tasks> itemsInWork;
        public ObservableCollection<Tasks> ItemsInWork
        {
            get { return itemsInWork; }
            set { this.RaiseAndSetIfChanged(ref itemsInWork, value); }
        }
        ObservableCollection<Tasks> itemsCompleted;
        public ObservableCollection<Tasks> ItemsCompleted
        {
            get { return itemsCompleted; }
            set { this.RaiseAndSetIfChanged(ref itemsCompleted, value); }
        }

        private List<Tasks> FillTasks(int wut)
        {
            switch (wut)
            {
                case 1:
                    return new List<Tasks>
                    {
                        new Tasks("Задача 1", "Описание задачи 1","/Assets/avalonia-logo.ico" ),
                        new Tasks("Задача 2", "Описание задачи 2","/Assets/avalonia-logo.ico" ),
                    };
                case 2:
                    return new List<Tasks>
                    {
                        new Tasks("Задача 1", "Описание задачи 1","/Assets/avalonia-logo.ico" ),
                        new Tasks("Задача 2", "Описание задачи 2","/Assets/avalonia-logo.ico" ),
                    };
                case 3:
                    return new List<Tasks>
                    {
                        new Tasks("Задача 1", "Описание задачи 1","/Assets/avalonia-logo.ico" ),
                        new Tasks("Задача 2", "Описание задачи 2","/Assets/avalonia-logo.ico" ),
                    };
                default:
                    return new List<Tasks>
                    {
                        new Tasks("ERROR", "ERROR","/Assets/avalonia-logo.ico" ),
                    };
            }

        }

        public void Delete(Tasks itm)
        {
            if(ItemsPlanned.Contains(itm)) ItemsPlanned.Remove(itm);
            else if (ItemsInWork.Contains(itm)) ItemsInWork.Remove(itm);
            else if (ItemsCompleted.Contains(itm)) ItemsCompleted.Remove(itm);
        }

        public void AddNewPl()
        {
            int count = ItemsPlanned.Count + 1 ;
            var newtsk = new Tasks("Задача " + count, "", "/Assets/avalonia-logo.ico");
            ItemsPlanned.Add(newtsk);
        }
        public void AddNewIn()
        {
            int count = ItemsInWork.Count + 1;
            var newtsk = new Tasks("Задача " + count, "", "/Assets/avalonia-logo.ico");
            ItemsInWork.Add(newtsk);
        }
        public void AddNewCo()
        {
            int count = ItemsCompleted.Count + 1;
            var newtsk = new Tasks("Задача " + count, "", "/Assets/avalonia-logo.ico");
            ItemsCompleted.Add(newtsk);
        }

        public void ClearAll()
        {
            ItemsPlanned.Clear();
            ItemsInWork.Clear();
            ItemsCompleted.Clear();
        }
        public Window show = null;
        public async void AddImg(Tasks task)
        {

            string? path;
            var taskPath = new OpenFileDialog()
            {
                Title = "Open file",
                Filters = new List<FileDialogFilter>()
            };

            taskPath.Filters.Add(new FileDialogFilter() { Name = "IMG files", Extensions = { "ico", "png", "jpg", "jpeg" } });
            string[]? pathArray = await taskPath.ShowAsync(show);
            
            path = pathArray is null ? null : string.Join(@"\", pathArray);
            if (path != null)
            {
                task.Img = new Bitmap(path);
                task.ImgName = path;
            }
        }
    }
}
