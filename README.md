# Project Soccer
- Bu proje futbol kulüpleri ve futbolcuların sıralandığı bir ASP.NET MVC projesidir.
- Veri tabanı için MS SQL Server, Veri tabanı ile iletişimi sağlamak için de ADO.NET kullanılmıştır.
- DB First bir yaklaşım izlenmiştir, başlangıç verileri sql komutları ile oluşturulmuştur.
Aşağıdaki komutlar ile Data Seeding işlemini gerçekleştirebilirsiniz.
```sql
CREATE TABLE Clubs (
  Id INT NOT NULL IDENTITY PRIMARY KEY,
  Name VARCHAR(50) NOT NULL,
  ShortName VARCHAR(10) NOT NULL UNIQUE,
  Logo VARCHAR(MAX) NOT NULL
);
CREATE TABLE Players (
  Id INT NOT NULL IDENTITY PRIMARY KEY,
  FirstName VARCHAR(50) NOT NULL,
  LastName VARCHAR(50) NOT NULL,
  DateOfBirth DATE NOT NULL,
  ClubId INT NOT NULL FOREIGN KEY REFERENCES Clubs(Id)
);
INSERT INTO Clubs (Name, ShortName, Logo) VALUES
('Bayern Munich', 'BAY', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSyhqQ8Ca-M0Ne4r6M1XN52KttJAaigoiBU-hC00PI&usqp=CAE&s'),
('Chelsea', 'CHE', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ-uEgJGcg6isat3xz4-AAT8lRZHXCuApnxyohjtXQ&usqp=CAE&s'),
('Real Madrid', 'RMA', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRGrWvc9sC19zMald57ln2fC7u8TqwQyuzG2sOwkTTz&usqp=CAE&s'),
('Manchester City', 'MCI', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTR3iN5iz6-efSL66AMgE3ZF3MN_lTod6SH-Rk82zg&usqp=CAE&s');
INSERT INTO Players (FirstName, LastName, DateOfBirth, ClubId) VALUES
('Erling', 'Haaland', '2002-05-28', 1),
('Kevin', 'De Bruyne', '1992-05-28', 1),
('Karim', 'Benzema', '1988-05-28', 3),
('Luka', 'Modric', '1986-05-28', 3),
('N''Golo', 'Kante', '1992-05-28', 2),
('Joao', 'Felix', '2001-05-28', 2),
('Joshua', 'Kimmich', '1995-05-28', 4),
('Kingsley', 'Coman', '1997-05-28', 4);
```