# 🏢 Booking System REST API

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-2CA5E0?style=for-the-badge&logo=docker&logoColor=white)
![Clean Architecture](https://img.shields.io/badge/Clean_Architecture-Success?style=for-the-badge)

RESTful API для управління конференц-залами, бронюваннями та розрахунку вартості оренди з урахуванням динамічних часових зон. Розроблено як архітектурний проєкт для демонстрації принципів **Clean Architecture**, **SOLID** та **Domain-Driven Design (DDD)**.

## 🛠 Стек технологій
- **Мова та фреймворк:** C#, .NET 10, ASP.NET Core Web API
- **База даних:** PostgreSQL, Entity Framework Core (Code-First)
- **Документація:** OpenAPI, Scalar UI
- **Інфраструктура:** Docker, Docker Compose

---

## 🏗 Чиста Архітектура (Clean Architecture)

Проєкт розділений на 4 незалежні шари для забезпечення слабкої зв'язності та легкості тестування:

- **`.API` (Presentation Layer):** Точка входу. Містить налаштування DI та контролери. Маршрутизація побудована за стандартами REST.
- **`.Application` (Service Layer):** Реалізація бізнес-логіки (`RoomService`, `BookingService`, `AnalyticsService`). Використання відповідних DTO для визначення вхідних/вихідних даних та збереження стану через `IUnitOfWork`.
- **`.Core` (Domain Layer):** Ядро системи. Містить багаті доменні моделі (Rich Domain Model), константи бізнес-правил та контракти інтерфейсів.
- **`.DataAccess.Postgres` (Infrastructure Layer):** Реалізація паттернів `Repository` та `UnitOfWork`. Містить механізм автоматичного наповнення бази початковими даними (Seeding) при першому запуску.

---

## 💎 Архітектурні рішення та Патерни

### 📦 Domain-Driven Design (Rich Domain Model)
Моделі (`Room`, `Service`, `BookingRecord`) повністю інкапсулюють свій стан. Колекції назовні видаються як `IReadOnlyCollection`, а зміна стану відбувається виключно через методи (наприклад, `AddService()`, `UpdateDetails()`). Конструктори моделей містять базову валідацію, захищаючи БД від невалідних даних (наприклад, від'ємної місткості).

### 🛡 Result Pattern & Global Exception Handling
Замість генерації дорогих винятків (`throw new Exception`), використано патерн Result. 
Сервіси повертають `ServiceResponse<T>` з enum `ErrorType` (None (при відсутності помилки), NotFound, Conflict, Validation). У шарі API `BaseController` автоматично мапить ці типи на відповідні HTTP-статуси.

### ⏱ Алгоритмічний розрахунок вартості (Domain Service)
Створено `RentCalculatorService`. Він використовує математичний алгоритм "крокування по часових зонах": розбиває період бронювання на відрізки і точно вираховує ціну на основі `Constants` (ранкова знижка, пікова націнка тощо). Це дозволяє безпомилково рахувати вартість навіть якщо бронювання перетинає кілька різних зон.

### ⚡ Оптимізація взаємодії БД
Математичні обчислення для звітів та аналітики винесено на сторону PostgreSQL. `AnalyticsService` використовує `IBookingRepository`, де EF Core переводить C# операції (таких як `Sum()`) у чистий SQL-запит. Це запобігає завантаженню потенційних тисяч записів у пам'ять сервера шляхом проведення всіх необхідних операцій на рівні Бази Даних.

---

## 📊 Документація API (Endpoints)

Після запуску додатку документація доступна за допомогою **Scalar** (сучасна альтернатива Swagger) за адресою:
- При запуску через Visual Studio: `https://localhost:7027/scalar/v1`
- При запуску через Docker: `http://localhost:8080/scalar/v1`

**Реалізовані методи:**
- `POST /api/Rooms` — Створення конференц-залу.
- `PUT /api/Rooms/{id}` — Оновлення базової ціни та списку послуг.
- `DELETE /api/Rooms/{id}` — Видалення залу.
- `GET /api/Rooms/available` — Пошук доступних залів із врахуванням місткості та перетинів часу.
- `POST /api/Bookings` — Бронювання залу з розрахунком фінальної вартості.
- `GET /api/Analytics/revenue` — Звіт про доходи компанії по залах.
- `GET /api/Analytics/occupancy` — Звіт про завантаженість/популярність залів.

---

## 🚀 Як запустити локально (Через Docker)

1. Клонуйте репозиторій:
   ```bash
   git clone https://github.com/Maksym-0/BookingSystem.git
   ```

2. Створіть файл `.env` у кореневій директорії (поруч із `docker-compose.yml`), скопіювавши його з `.env.example` та відредагуйте `.env`, встановивши пароль для змінної `DB_PASSWORD`.

3. Запустіть контейнери:
   ```bash
   docker-compose up --build -d
   ```

💡 **Важливо:** *При першому запуску додаток автоматично застосує EF Core міграції та наповнить базу початковими даними (3 зали та 3 послуги).*
