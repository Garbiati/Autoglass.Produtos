select * from dbo.Produtos

select * from dbo.Fornecedores

/*

DELETE FROM Fornecedores
DBCC CHECKIDENT ('dbAutoglass.dbo.Fornecedores', RESEED, 0)
*/

INSERT INTO dbo.Fornecedores
VALUES
('FIAT - Fabbrica Italiana Automobili Torino', '99.999.999/0001-99')
,('HMC - Honda Motor Company, Limited', '88.888.888/0001-88')
,('Volkswagen - Gesellschaft zur Vorbereitung des Deutschen Volkswagens GmbH', '77.777.777/0001-77')
,('TMC - Toyota Motor Corporation', '66.666.666/0001-66')
,('BMW - Bayerische Motoren Werke', '55.555.555/0001-55')
,('Audi AG', '44.444.444/0001-44')
,('Chevrolet', '33.333.333/0001-33')
,('Ford', '22.222.222/0001-22')



INSERT INTO dbo.Produtos
VALUES
('Parachoque Dianteiro Uno 2015', 1, '2019-01-01', '2025-12-31', 1)
,('Parachoque Trazeiro Uno 2015', 1, '2018-03-01', '2025-12-31', 1)
,('Lanterna Traseira Argo 2018', 1, '2020-05-25', '2025-12-31', 1)
,('Lanterna Dianteira Argo 2018', 1, '2020-02-14', '2025-12-31', 1)

,('Parachoque Dianteiro Civic 2013', 1, '2019-01-01', '2025-12-31', 2)
,('Parachoque Trazeiro Civic 2013', 1, '2018-03-01', '2025-12-31', 2)
,('Lanterna Traseira City 2015', 1, '2020-05-25', '2025-12-31', 2)
,('Lanterna Dianteira City 2015', 1, '2020-02-14', '2025-12-31', 2)

,('Parachoque Dianteiro Jetta 2022', 1, '2023-01-01', '2025-12-31', 3)
,('Parachoque Trazeiro Jetta 2022', 1, '2023-03-01', '2025-12-31', 3)
,('Lanterna Traseira Gol 2016', 1, '2020-05-25', '2025-12-31', 3)
,('Lanterna Dianteira Gol 2016', 1, '2020-02-14', '2025-12-31', 3)

,('Parachoque Dianteiro Corolla 2021', 1, '2023-01-01', '2025-12-31', 4)
,('Parachoque Trazeiro Corolla 2021', 1, '2023-03-01', '2025-12-31', 4)
,('Lanterna Traseira Yaris 2019', 1, '2020-05-25', '2025-12-31', 4)
,('Lanterna Dianteira Yaris 2019', 1, '2020-02-14', '2025-12-31', 4)

,('Parachoque Dianteiro BMW X1 2018', 1, '2023-01-01', '2025-12-31', 5)
,('Parachoque Trazeiro BMW X1 2018', 1, '2023-03-01', '2025-12-31', 5)
,('Lanterna Traseira BMW Série 5 2014', 1, '2016-05-25', '2025-12-31', 5)
,('Lanterna Dianteira BMW Série 5 2014', 1, '2016-02-14', '2025-12-31', 5)

,('Parachoque Dianteiro Audi A3 2012', 1, '2023-01-01', '2025-12-31', 6)
,('Parachoque Trazeiro Audi A3 2012', 1, '2023-03-01', '2025-12-31', 6)
,('Lanterna Traseira Audi Q5 2019', 1, '2016-05-25', '2025-12-31', 6)
,('Lanterna Dianteira Audi Q5 2019', 1, '2016-02-14', '2025-12-31', 6)

,('Parachoque Dianteiro Onix 2018', 1, '2023-01-01', '2025-12-31', 7)
,('Parachoque Trazeiro Onix 2018', 1, '2023-03-01', '2025-12-31', 7)
,('Lanterna Traseira Cruze 2013', 1, '2016-05-25', '2025-12-31', 7)
,('Lanterna Dianteira Cruze 2013', 1, '2016-02-14', '2025-12-31', 7)

,('Parachoque Dianteiro Mustang 2022', 1, '2023-01-01', '2025-12-31', 8)
,('Parachoque Trazeiro Mustang  2022', 1, '2023-03-01', '2025-12-31', 8)
,('Lanterna Traseira Fusion 2017', 1, '2016-05-25', '2025-12-31', 8)
,('Lanterna Dianteira Fusion 2017', 1, '2016-02-14', '2025-12-31', 8)