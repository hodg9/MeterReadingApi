Run the following commands in the terminal from the Infrastructure project folder to manage Entity Framework Core migrations:

## Add a new migration
```
dotnet ef migrations add "my migration"
```

## Script a migration to a file
```
dotnet ef migrations script -i -o ./Migrations/Migrations.sql
```