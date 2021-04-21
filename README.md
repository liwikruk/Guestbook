# Guestbook

GuestBook - vue.js, send comments.

InventoryService - get comments.

1. In InventoryService create database
```bash
Update-Database
```

2. In GuestBook, GuestBookDBContext.cs
```bash
change optionsBuilder.UseSqlServer for your database location 
```

# Some information

* In GuestBook, Controllers, CommentController.cs
```bash
method POST - used masstransit and rabbitMq for sending information
method GET - used for receiving data from database
```

* In InventoryService, CommentConsumer.cs 
```bash
method Consume - used masstransit and rabbitMq for receiving information, add information to database
```
