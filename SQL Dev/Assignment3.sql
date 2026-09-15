create table #State (
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
SET State = CASE 
                WHEN Id = 1 THEN (SELECT State FROM #CityState WHERE Id = 6)
                WHEN Id = 6 THEN (SELECT State FROM #CityState WHERE Id = 1)
            END
WHERE Id IN (1, 6);

CREATE TABLE #CityChanges (
    City VARCHAR(50),
    NewState VARCHAR(30)
);

INSERT INTO #CityChanges (City, NewState)
VALUES ('San Jose', 'Montana');

UPDATE cs
SET cs.State = cc.NewState
FROM #CityState cs
JOIN #CityChanges cc ON cs.City = cc.City;

ALTER TABLE #City
ADD DateAdded DATETIME DEFAULT '1/1/18';

INSERT INTO #City (Id, StateId, City)
VALUES (11, 1, 'Madison');

INSERT INTO #City (Id, StateId, City, DateAdded)
VALUES (12, 6, 'Los Angeles', '6/1/18');
