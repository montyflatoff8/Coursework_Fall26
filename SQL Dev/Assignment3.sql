1)	create table #State (
Id int,
State VARCHAR(30)
);

create table #City (
Id int,
StateId int,
City VARCHAR(50)
);


INSERT INTO #State (Id, State)
VALUES (1, 'Wisconsin'),
(2, 'New York'),
(3, 'Virginia'),
(4, 'Minnesota'),
(5,'Illinois'),
(6,'California'),
(7,'Mississippi'),
(8,'Colorado'),
(9,'Florida'),
(10,'Montana')

INSERT INTO #City (Id, StateID, City)
VALUES (1, 1, 'Stevens Point'),
(2, 2, 'Albany'),
(3, 3, 'Roanoke'),
(4, 4, 'Duluth'),
(5, 5,'Chicago'),
(6, 6,'San Jose'),
(7, 7,'Jackson'),
(8, 8,'Denver'),
(9, 9,'St. Petersburg'),
(10, 10,'Billings')

CREATE TABLE #CityState (
Id int,
City VARCHAR(50),
State VARCHAR(30),
);

INSERT INTO #CityState (Id, City, State)
SELECT c.Id, c.City, s.State
FROM #City c
JOIN #State s ON c.StateId = s.Id

UPDATE #CityState
SET City = CASE 
                WHEN Id = 1 THEN (SELECT City FROM #CityState WHERE Id = 2)
                WHEN Id = 2 THEN (SELECT City FROM #CityState WHERE Id = 1)
            END
WHERE Id IN (1, 2);

select * from #State
select * from #City
Select * from #CityState