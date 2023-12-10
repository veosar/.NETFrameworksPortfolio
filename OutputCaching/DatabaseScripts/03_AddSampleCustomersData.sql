INSERT INTO Customer (Id, FirstName, LastName, Email, DateOfBirth)
VALUES
(gen_random_uuid(), 'John', 'Doe', 'john.doe@example.com', '1980-01-01'),
(gen_random_uuid(), 'Jane', 'Smith', 'jane.smith@example.com', '1990-02-02'),
(gen_random_uuid(), 'Alice', 'Johnson', 'alice.johnson@example.com', '1985-03-03'),
(gen_random_uuid(), 'Bob', 'Brown', 'bob.brown@example.com', '1975-04-04'),
(gen_random_uuid(), 'Charlie', 'Davis', 'charlie.davis@example.com', '1995-05-05');