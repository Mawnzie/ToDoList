// See https://aka.ms/new-console-template for more information


using System;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading.Tasks;
using ToDoList;

Console.WriteLine("---------------------------");
Console.WriteLine("Welcome to ToDoLy!");
Console.WriteLine("---------------------------\n");
Console.WriteLine("Pick an option:\n");
char key;

do
{

    Console.WriteLine("Load existing to-do list? y/n \n");
    key = Console.ReadKey(intercept: true).KeyChar;
    if (key != 'y' && key != 'n')
    {
        Console.WriteLine("\nWrong input. Please choose one of the following:\n");
    }

} while (key != 'y' && key != 'n');

MyToDoList list= new MyToDoList();
string jsonfile;
string json;

if (key=='y')
{

    enterfile:
    Console.WriteLine("Enter the name of the file to be loaded:\n");
    jsonfile = Console.ReadLine();
    
    if (File.Exists(jsonfile))
    {
        json = File.ReadAllText(jsonfile);
        list = JsonSerializer.Deserialize<MyToDoList>(json);
        Console.WriteLine("\nTo do list loaded.\n");
    } else
    {
        Console.WriteLine("The file does not exist.\n ");
        goto enterfile;
    }
}



while (true)
{

    Console.WriteLine("\nPick an option:\n");
    Console.WriteLine("(1) Show Task List (sort by date or project)");
    Console.WriteLine("(2) Add new task.");
    Console.WriteLine("(3) Edit task (update, mark as done, remove)");
    Console.WriteLine("(4) Save and quit.\n");




    char input = Console.ReadKey().KeyChar;



optionscreen:
    switch (input)
    {
        case '1':
            Console.WriteLine("\nShow the list sorted by:\n");
            Console.WriteLine("(1) Date");
            Console.WriteLine("(2) Project\n");
            char input1 = Console.ReadKey().KeyChar;
            if (input1 == '1')
            {
                list.Sort("DueDate");
            }
            else if (input1 == '2')
            {
                list.Sort("Project");
            }
            list.ShowTaskList();
            Console.WriteLine("\n");
            break;
        case '2':
            list.AddTask();
            break;

        case '3':
        findtask:
            Console.WriteLine("Enter the name of the task to be edited:");
            
            string task = Console.ReadLine();
            if (list.Tasks.Exists(x => x.Name == task)){


                Console.WriteLine("Which field whould you like to edit:\n");
                Console.WriteLine("(1) Name");
                Console.WriteLine("(2) DueDate.");
                Console.WriteLine("(3) Project");
                Console.WriteLine("(4) Status");
                Console.WriteLine("(5) Remove task");
                string? enteredString;

                switch (Console.ReadKey().KeyChar)
                {

                    case '1':
                        Console.WriteLine("Enter the new name you would like to give the task:");
                        list.Tasks.Find(x => x.Name == task).Name = Console.ReadLine();
                        break;
                    case '2':
                        Console.WriteLine("\nEnter the new due date(MM/dd/yyyy):\n");
                        enteredString = Console.ReadLine();
                        DateTime dueDate;
                        while (!DateTime.TryParse(enteredString, out dueDate))
                        {
                            Console.WriteLine("\nWrong input, please try again.\n");
                            enteredString = Console.ReadLine();
                        }
                        list.Tasks.Find(x => x.Name == task).DueDate = dueDate;
                        break;
                    case '3':
                        Console.WriteLine("\nEnter the new Project name:");
                        enteredString = Console.ReadLine();
                        list.Tasks.Find(x => x.Name == task).Project = enteredString;
                        break;
                    case '4':
                        Console.WriteLine("\nSet project as (1) 'Done' or (2) 'NotDone':");
                    setstatus:
                        var statuskey = Console.ReadKey().KeyChar;

                        if (statuskey == '1')
                        {
                            list.Tasks.Find(x => x.Name == task).Status = ToDoListStatus.Done;
                        }
                        else if (statuskey == '2')
                        {
                            list.Tasks.Find(x => x.Name == task).Status = ToDoListStatus.NotDone;
                        }
                        else
                        {
                            Console.WriteLine("Please choose either (1) 'Done' or (2) 'NotDone'.");
                            goto setstatus;
                        }
                        break;
                    
                    case '5':
                        try
                        {
                            list.Tasks.RemoveAll(x => x.Name == task);
                            Console.WriteLine($"Task with name {task} removed.");
                        } catch (Exception e) { Console.WriteLine(e.ToString()); }
                        break;
                }

            } else
            {
                Console.WriteLine($"The task {task} does not exist in the list.");
                goto findtask;
            }

                break;
        case '4':
        
            break;

        default:
            break;
    }

    string savefile;

    if (input < '1' || input > '4')
    {
        Console.WriteLine("Invalid option. (Valid optionas are (1)-(4))");
        input = Console.ReadKey().KeyChar;
        goto optionscreen;
    } else if (input == '4')
    {
        savename:
        Console.WriteLine("Please enter the name for the to-do list:");
        savefile = Console.ReadLine();
        if (File.Exists(savefile))
        {
            Console.WriteLine($"The file {savefile} already exists. Would you like to replace it? y/n");
            if(Console.ReadKey().KeyChar == 'y')
            {
                File.Delete(savefile);

                // Save to file
                File.WriteAllText(savefile, JsonSerializer.Serialize(list));
                Console.WriteLine($"To do list saved successfully as {savefile}.");
                break;
            } else 
            {
                goto savename;
            }
        } else
        {
            File.WriteAllText(savefile, JsonSerializer.Serialize(list));
            Console.WriteLine($"To do list saved successfully as {savefile}.");
            break;
        }
    }

    
}
Console.WriteLine("Thank you for using ToDoLy. Press any key to exit.");
Console.ReadKey();