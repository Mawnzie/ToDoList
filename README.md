# 📝 ToDoLy - Console To-Do List Application

ToDoLy is a simple C# console application that lets you manage tasks with a friendly user interface and JSON-based file storage.

---

## Features

- ✅ Add new tasks (name, due date, project)
- 📋 View tasks sorted by due date or project
- ✏️ Edit tasks (name, date, project, status)
- ✅ Mark tasks as done or not done
- 🗑️ Delete tasks
- 💾 Save and load task lists to/from JSON files

---

## Project Structure

```bash
.
├── Program.cs       # Entry point & UI logic
├── MyTask.cs        # Task models and ToDo list logic
└── README.md        # This file

##UML diagram

+------------------------+
|      MyToDoList        |
+------------------------+
| - Tasks: List<MyTask>  |
| - SortStyle: string    |
+------------------------+
| + ShowTaskList()       |
| + AddTask()            |
| + Sort(style: string)  |
+------------------------+

           ▲
           |
           |
+------------------------+
|        MyTask          |
+------------------------+
| - Name: string         |
| - DueDate: DateTime    |
| - Project: string      |
| - Status: ToDoListStatus |
+------------------------+
| + MyTask(...)          |
| + ToString(): string   |
+------------------------+

+------------------------+
|   <<enum>> ToDoListStatus |
+------------------------+
| - Done                 |
| - NotDone             |
+------------------------+

+------------------------+
|        Program         |
+------------------------+
| + Main()               |
| (Handles menu/UI)      |
+------------------------+

