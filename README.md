# 📌 Smart Productivity Manager

## 🧠 Overview

**Smart Productivity Manager** is a desktop application built using .NET that helps users efficiently manage their daily tasks, track productivity, and stay organized. The application provides a structured way to add, update, delete, and monitor tasks with a user-friendly interface.

---

## 🚀 Features

* ✅ Add new tasks with details
* ✏️ Update existing tasks
* ❌ Delete tasks
* 🔍 Search and filter tasks
* 📅 Track task status (Pending / Completed)
* 💾 Persistent data storage using database (SQLite)
* 🖥️ Clean and interactive UI

---

## 🛠️ Technologies Used

* **.NET (C#)**
* **WPF (Windows Presentation Foundation)** for UI
* **SQLite Database** for storing tasks
* **MVVM / Layered Architecture** (Models, Services, UI)

---

## 📂 Project Structure

```
SmartProductivityManager/
│
├── Data/          → Database files and configurations  
├── Models/        → Data models (Task structure)  
├── Services/      → Business logic and database operations  
├── UI/            → User interface components  
├── Events/        → Event handling logic  
├── App.xaml       → Application entry point  
├── MainWindow     → Main dashboard UI  
├── tasks.db       → SQLite database file  
```

---

## ⚙️ Installation & Setup

1. Clone the repository:

   ```bash
   (https://github.com/PratikshaSingh04/Smart-Productivity-Manager)
   ```

2. Open the project in:

   * Visual Studio

3. Restore dependencies (if needed)

4. Run the application:

   * Press **F5** or click **Start**

---

## 💡 How It Works

* Users can create tasks which are stored in the SQLite database.
* The application retrieves and displays tasks dynamically.
* Users can perform CRUD operations (Create, Read, Update, Delete).
* Task status helps in tracking productivity.
* Give reminder every 60 sec for high priority tasks.

---

## 📜 License

This project is for educational purposes.
