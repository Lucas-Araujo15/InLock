USE InLock_Games; 
GO

INSERT INTO Estudios (NomeEstudio)
VALUES	('Blizzard') , ('Rockstar Studios') ,('Square Enix');
GO

INSERT INTO Jogos (NomeJogo, DescricaoJogo, DataLancamento, ValorJogo, IdEstudio) 
VALUES ('Diablo 3', 'É um jogo que contém bastante ação e é viciante, seja você um novato ou um fã', '15/05/2012', 99.00 , 1) ,
		('Red Dead Redemption II', 'Jogo eletrônico de ação-aventura western', '26/10/2018', 120.00, 2);

GO

INSERT INTO TiposUsuarios (NomeTipoUsuario)
VALUES ('ADMINISTRADOR') , ('CLIENTE');
GO

INSERT INTO Usuarios (Email, Senha, IdTipoUsuario)
VALUES ('cliente@cliente.com', '123', 2), ('admin@admin.com', '321', 1)
GO