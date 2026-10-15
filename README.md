# 📋 Task Manager CLI (Console To-Do Manager)

> ⚠️ **Учебный пет-проект.** Это консольное приложение создано **исключительно в целях обучения C#**. Оно не претендует на продакшен-решение, но демонстрирует понимание ключевых концепций языка.

---

## 📖 О проекте

**Task Manager CLI** — это консольное приложение для управления списком задач с поддержкой:
- Регистрации и входа пользователей
- Добавления, удаления и отметки задач как выполненных
- Приоритетов задач (Low / Medium / High)
- Поиска задач по ключевому слову

Проект написан на **C# (.NET)** и работает полностью в консоли.

---

## 🎯 Цель проекта

Этот проект был создан как **практическое закрепление** следующих тем C#:

| Тема | Где используется в проекте |
|------|----------------------------|
| **Singleton Pattern** | `TaskManager`, `AuthManager` |
| **Generics** | `Dictionary<int, Task>`, `List<Task>` |
| **IEnumerable + yield return** | `TaskManager.GetEnumerator()` |
| **Events** | `TaskAdded`, `TaskRemoved`, `TaskCompleted` |
| **Delegates (Predicate)** | Поиск задач через `FindTasks` |
| **Exceptions** | `ArgumentException`, `InvalidOperationException` |
| **Struct vs Class** | `User` (struct), `Task` (class) |
| **Enums** | `TaskPriority` (Low, Medium, High) |
| **Properties** | `Id`, `Title`, `IsCompleted` |
| **readonly** | `Username` в `User` |
| **Constants** | `MaxTasks` |
| **params** | `PrintTasks(params TaskPriority[] priorities)` |
| **Optional Parameters** | `ReadInt(prompt, min = 0, max = int.MaxValue)` |
| **Local Functions** | `AddTask()`, `SearchTasks()` внутри `Program` |
| **Interfaces** | `IEnumerable<Task>` |

---

## 🛠️ Технологии

- **Язык:** C#
- **Платформа:** .NET (Console Application)
- **Библиотеки:** только стандартные (`System`, `System.Collections.Generic`)

---

## 🚀 Как запустить

1. Установи [.NET SDK](https://dotnet.microsoft.com/download) (6.0 или выше)
2. Клонируй репозиторий:
   ```bash
   git clone https://github.com/your-username/task-manager-cli.git
   ```
3. Перейди в папку проекта:
   ```bash
   cd task-manager-cli
   ```
4. Запусти:
   ```bash
   dotnet run
   ```

---

## 📂 Структура проекта

```
TaskManagerApp/
│
├── Enums/
│   └── TaskPriority.cs        # Приоритеты задач (Low, Medium, High)
│
├── Models/
│   ├── Task.cs                # Класс задачи
│   └── User.cs                # Структура пользователя
│
├── Services/
│   ├── TaskManager.cs         # Singleton + IEnumerable + Events
│   └── AuthManager.cs         # Singleton для авторизации
│
├── Helpers/
│   └── ConsoleHelper.cs       # Вспомогательные методы ввода/вывода
│
└── Program.cs                 # Главное меню и логика UI
```

---

## 🎮 Пример использования

```
═══════════════════════════════════════════════
  ДОБРО ПОЖАЛОВАТЬ В МЕНЕДЖЕР ЗАДАЧ
═══════════════════════════════════════════════
1. 🔐 Войти
2. 📝 Зарегистрироваться
3. 📋 Работа с задачами (требуется вход)
4. 🚪 Выход
Выберите действие: 1

Введите логин: admin
Введите пароль: 1234
🔓 Добро пожаловать, admin!

═══════════════════════════════════════════════
  ЗАДАЧИ | Пользователь: admin
═══════════════════════════════════════════════
1. ➕ Добавить задачу
2. 📋 Показать все задачи
3. 🔍 Найти задачи
4. ✅ Отметить задачу выполненной
5. 🗑️ Удалить задачу
6. 🔄 Выход в главное меню
```

---

## 📌 Что можно улучшить (TODO)

Это учебный проект, поэтому здесь есть что доработать:

- [ ] Сохранение задач в файл (JSON / TXT)
- [ ] Добавление дедлайнов
- [ ] Категории / теги задач
- [ ] Юнит-тесты (xUnit / NUnit)
- [ ] Более надёжное хеширование паролей (BCrypt)
- [ ] Разделение на слои (UI / Business Logic / Data)

---

## ⚠️ Дисклеймер

Это **учебный проект**, созданный в процессе изучения C#.  
Код **не является** примером продакшен-архитектуры и может содержать упрощения.

Цель проекта — **показать понимание ключевых концепций языка**, а не создать идеальное приложение.

---

## 📜 Лицензия

Проект распространяется под лицензией **MIT**.  
Можешь свободно использовать, изменять и учиться на нём.

---

## 👤 Автор

GitHub: [@LuongTrungHieu7](https://github.com/LuongTrungHieu7)

---

⭐ Если проект был полезен — поставь звезду!
