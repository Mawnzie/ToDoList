using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ToDoList
{
    public class MyToDoList
    {
        public List<MyTask> Tasks { get; set; } = new List<MyTask>();
 
        public void ShowTaskList()//Shows to-do list
        {
            if (!Tasks.Any())
            {
                Console.WriteLine("There are currently no tasks on the list.");
            }
            else
            {
                Console.WriteLine("\nName:".PadRight(21) + "Date:".PadRight(21) + "Project:".PadRight(20) + "Status:".PadRight(20));
                Console.WriteLine("-----------------------------------------------------------------------");
                foreach (var task in Tasks)
                {
                    Console.WriteLine(task);
                }
            }
        }
        public void AddTask()
        {   
            Console.WriteLine("\nPlease enter the name of the task:\n");
        AddTask:
            string? name = Console.ReadLine();
            if (!Tasks.Exists(x => x.Name == name)){
                Console.WriteLine("\nEnter the due date(MM/dd/yyyy):\n");
                string? enteredString = Console.ReadLine();
                DateTime dueDate;
                while (!DateTime.TryParse(enteredString, out dueDate))
                {
                    Console.WriteLine("\nWrong input, please try again.\n");
                    enteredString = Console.ReadLine();
                }


                Console.WriteLine("\nPlease enter the name of the Project:\n");
                string? project = Console.ReadLine();

                Tasks.Add(new MyTask(name, dueDate, project));
                Console.WriteLine("\nTask succesfully added to To-do list\n");
            } else
            {
                Console.WriteLine($"\nThe task {name} already exists. Please choose another name for the task:\n");
                goto AddTask;
            }

        }
     
        
        
        public void Sort(string style)
        {
            if (style == "DueDate")
            {
                Tasks.Sort((a, b) => DateTime.Compare(a.DueDate, b.DueDate));
            }
            else if(style == "Project")
            {
                Tasks.Sort((a, b) => string.Compare(a.Project, b.Project));
            }
            else
            {
                Console.WriteLine("Please choose an appropriate sorting style.");
            }
        }

      
    }

    public class MyTask
    {
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public string Project { get; set; } = string.Empty;
        public ToDoListStatus Status { get; set; } = ToDoListStatus.NotDone;
      


        public MyTask(string name, DateTime dueDate, string project, ToDoListStatus status = ToDoListStatus.NotDone)
        {
            Name = name;
            DueDate = dueDate;
            Status = status;
            Project = project;
        }
        
        public override string ToString()
        {
           
            return $"{Name}".PadRight(20) + $"{DueDate.ToString("yyyy-MM-dd")}".PadRight(20) + $"{Project}".PadRight(20) + $"{Status}".PadRight(20);
        }


    }

    public enum ToDoListStatus
    {
        Done,
        NotDone,
    }

    



}
