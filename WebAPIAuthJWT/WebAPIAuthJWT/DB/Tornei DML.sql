INSERT INTO Utenti(Username, Password) VALUES ('filippo.albertini','65Test12')
INSERT INTO Utenti_Ruoli(Username, Ruolo) VALUES ('filippo.albertini','USER')

INSERT INTO Utenti(Username, Password) VALUES ('thomas.casali','386js8l')
INSERT INTO Utenti_Ruoli(Username, Ruolo) VALUES ('thomas.casali','USER')
INSERT INTO Utenti_Ruoli(Username, Ruolo) VALUES ('thomas.casali','ADMIN')

INSERT INTO Comuni(Cod, Citta, Provincia, Regione, SiglaProvincia, SiglaRegione) VALUES('C357','Cattolica','RN','EMILIA-ROMAGNA','RN','ER')
INSERT INTO Societa(Comuni_Cod, NomeSocieta, Indirizzo) VALUES ('C357','Società 1','Indirizzo 1')
INSERT INTO Atleti(Societa_Id, CodiceTessera, Nome, Cognome, CodiceFiscale, DataNascita) VALUES (1,'TESSERA1','Filippo','Albertini','LBRFPP68M23C357N','19680823')
