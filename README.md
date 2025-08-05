# 🛍️ So2Baladna E-Commerce API

A modular, clean-architecture ASP.NET Core Web API for a fully functional e-commerce system. This project is structured for scalability and maintainability using best practices in software architecture.

---

## 📂 Project Structure

This solution is divided into **three main layers**:

- **API** – The presentation layer (controllers, endpoints, DTOs).
- **Core** – The domain layer (entities, interfaces, services).
- **Infrastructure** – The data access layer (EF Core, repositories, Stripe, email, etc).

---

## 🚀 Technologies Used

- ASP.NET Core 7 Web API
- Entity Framework Core
- Microsoft Identity
- AutoMapper
- Stripe API (for payments)
- JWT Authentication with Cookies
- MSSQL Server
- Redis (for basket caching)
- Angular (for frontend – optional integration)
- SendGrid (for email services)

---

## 🔐 Authentication & Authorization

- User Registration with Email Confirmation
- JWT Token stored as HttpOnly cookies
- Reset Password functionality
- Protected routes using `[Authorize]`

---

## 🧾 API Endpoints Overview

### 🧑 Account
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/Account/Register` | Register a new user |
| POST | `/api/Account/Login` | User login |
| POST | `/api/Account/active-account` | Confirm email |
| POST | `/api/Account/reset-password` | Reset password |
| GET  | `/api/Account/send-email-forget-password` | Send reset password email |
| PUT  | `/api/Account/update-address` | Update user address |
| GET  | `/api/Account/get-address-for-user` | Get user address |
| GET  | `/api/Account/get-user-name` | Get username |
| GET  | `/api/Account/Logout` | Logout |
| GET  | `/api/Account/IsUserAuth` | Check auth status |

---

### 🛒 Baskets
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Baskets/get-basket-item/{id}` | Get basket by ID |
| POST | `/api/Baskets/update-basket` | Create/update basket |
| DELETE | `/api/Baskets/delete-basket-item/{id}` | Delete item from basket |

---

### 📦 Orders
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/Orders/create-order` | Place an order |
| GET | `/api/Orders/get-orders-for-user` | Get all orders for user |
| GET | `/api/Orders/get-order-by-id/{id}` | Get order details |
| GET | `/api/Orders/get-delivery` | Get delivery options |

---

### 💳 Payment
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/Payment/Create` | Create or update payment intent |

---

### 📁 Products & Categories
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Products/get-all` | Get all products |
| GET | `/api/Products/get-by-id/{id}` | Get product by ID |
| POST | `/api/Products/add` | Add product |
| PUT | `/api/Products/update` | Update product |
| DELETE | `/api/Products/delete/{id}` | Delete product |
| GET | `/api/Categories/get-all` | Get all categories |
| GET | `/api/Categories/get-by-id/{id}` | Get category by ID |
| POST | `/api/Categories/add-category` | Add category |
| PUT | `/api/Categories/update-category` | Update category |
| DELETE | `/api/Categories/delete-category/{id}` | Delete category |

---

### 🧪 Errors & Debugging
- `/api/Bugs/not-found`
- `/api/Bugs/server-error`
- `/api/Bugs/bad-request/{id}`

---

## 🧠 Clean Architecture Layers

