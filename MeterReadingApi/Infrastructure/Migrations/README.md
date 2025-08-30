Run the following commands in the terminal from the Infrastructure project folder to manage Entity Framework Core migrations:

## Add a new migration
```
dotnet ef migrations add "my migration" --output-dir ./Infrastructure/Migrations
```

## Script a migration to a file
```
dotnet ef migrations script -i -o ./Infrastructure/Migrations/Migrations.sql
```