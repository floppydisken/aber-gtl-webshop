SET IDENTITY_INSERT [dbo].[Product] ON;

INSERT INTO [dbo].[Product] (Id, Name, SKU, Price, Currency, Description, AmountInStock, MinStock) 
VALUES
(1, 'To Kill a Mockingbird', 'SKU001', 1999, 'USD', 'A gripping, heart-wrenching, and wholly remarkable tale of coming-of-age in a South poisoned by virulent prejudice.', 50, 5),
(2, '1984', 'SKU002', 1499, 'USD', 'A startling and haunting vision of the world, 1984 is powerful, convincing depiction of a future dystopia.', 75, 7),
(3, 'Pride and Prejudice', 'SKU003', 999, 'USD', 'An insightful novel of manners that follows the Bennets, a family of five daughters.', 100, 10),
(4, 'Harry Potter and the Sorcerer''s Stone', 'SKU004', 2499, 'USD', 'The first book in the epic Harry Potter saga.', 200, 20),
(5, 'The Hobbit', 'SKU005', 1499, 'USD', 'A timeless classic that takes the reader into a whimsical, fantastical world.', 80, 8),
(6, 'Moby Dick', 'SKU006', 1999, 'USD', 'A thrilling adventure of a whaling ship and its crew chasing a notorious whale.', 65, 6),
(7, 'The Catcher in the Rye', 'SKU007', 1299, 'USD', 'A classic novel originally published for adults, it has since become popular with adolescent readers for its themes of teenage angst and alienation.', 90, 9),
(8, 'The Great Gatsby', 'SKU008', 999, 'USD', 'A classic piece of American fiction, this book is a devastating exploration of the American Dream.', 70, 7),
(9, 'War and Peace', 'SKU009', 1999, 'USD', 'A legendary masterpiece, this book is synonymous with epic literature.', 30, 3),
(10, 'Ulysses', 'SKU010', 1999, 'USD', 'One of the most important works of modernist literature.', 40, 4),
(11, 'Don Quixote', 'SKU011', 2499, 'USD', 'A middle-aged gentleman from the region of La Mancha in central Spain, obsessed with the chivalrous ideals touted in books he has read.', 35, 3),
(12, 'In Search of Lost Time', 'SKU012', 2999, 'USD', 'A novel in seven volumes, it is his most prominent work, known both for its length and its theme of involuntary memory.', 25, 2),
(13, 'The Odyssey', 'SKU013', 1499, 'USD', 'It is, in part, a sequel to the Iliad, the other Homeric epic.', 45, 4),
(14, 'One Hundred Years of Solitude', 'SKU014', 1999, 'USD', 'The story follows 100 years in the life of Macondo, a village founded by José Arcadio Buendía and occupied by descendants all sporting variations on their progenitor''s name.', 55, 5),
(15, 'The Divine Comedy', 'SKU015', 2499, 'USD', 'An Italian long narrative poem by Dante Alighieri, begun c. 1308 and completed in 1320.', 40, 4),
(16, 'The Brothers Karamazov', 'SKU016', 2999, 'USD', 'The final novel by the Russian author Fyodor Dostoevsky.', 30, 3),
(17, 'Madame Bovary', 'SKU017', 1499, 'USD', 'The debut novel of French writer Gustave Flaubert, published in 1856.', 35, 3),
(18, 'The Adventures of Huckleberry Finn', 'SKU018', 999, 'USD', 'A novel by Mark Twain, first published in the United Kingdom in December 1884.', 45, 4),
(19, 'Lolita', 'SKU019', 1999, 'USD', 'A novel written by Russian-American novelist Vladimir Nabokov.', 30, 3),
(20, 'Catch-22', 'SKU020', 1499, 'USD', 'A satirical war novel by American author Joseph Heller.', 35, 3);

SET IDENTITY_INSERT [dbo].[Product] OFF;
