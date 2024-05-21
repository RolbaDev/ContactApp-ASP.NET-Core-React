# ContactsApp

## Opis

Repozytorium zawiera aplikację do zarządzania kontaktami, która składa się z backendu napisanego w .NET Core (.NET 6) z wykorzystaniem Entity Framework oraz frontendu napisanego w React.

## Wymagania

### Backend (.NET Core)

1. **.NET 6 SDK**: Aby zbudować i uruchomić backend, wymagane jest zainstalowanie [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0).
   
2. **Baza danych PostgreSQL**: Projekt korzysta z bazy danych PostgreSQL hostowanej na NeonTech. Upewnij się, że masz dostęp do hostowanej bazy danych PostgreSQL i ustawione połączenie w pliku `appsettings.json`.

### Frontend (React)

1. **Node.js i npm**: Aby uruchomić frontend, wymagane jest zainstalowanie [Node.js](https://nodejs.org/) wraz z menedżerem pakietów npm.

## Instalacja i uruchomienie

### Backend (.NET Core)

1. Sklonuj to repozytorium za pomocą polecenia:

   ```bash
   git clone https://github.com/RolbaDev/ContactApp-ASP.NET-Core-React.git


2. Przejdź do katalogu `backend/ContactsApp`.
3. Uruchom aplikację za pomocą następujących poleceń:
    ```bash
    dotnet restore
    dotnet run
    ```
4. Backend zostanie uruchomiony na domyślnym porcie `5000`.

### Frontend (React)

1. Przejdź do katalogu `frontend/contact_list`.
2. Zainstaluj zależności za pomocą komendy:
    ```bash
    npm install
    ```
3. Uruchom aplikację za pomocą komendy:
    ```bash
    npm start
    ```
4. Frontend zostanie uruchomiony i dostępny będzie pod adresem `http://localhost:3000`.

## Dodatkowe informacje

- Aby korzystać z aplikacji, upewnij się, że backend został uruchomiony i podłączony do hostowanej bazy danych PostgreSQL.
- Po uruchomieniu, interfejs użytkownika frontendu będzie dostępny pod adresem `http://localhost:3000`, a dokumentacja API backendu pod adresem `http://localhost:5000/swagger/index.html`.
