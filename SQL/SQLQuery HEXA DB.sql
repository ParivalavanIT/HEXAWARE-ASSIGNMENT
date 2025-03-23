create database sample1db


CREATE TABLE Employee (
    empid INT PRIMARY KEY,
    empname VARCHAR(50),
    deptno INT,
    salary DECIMAL(10,2)
);

INSERT INTO Employee (empid, empname, deptno, salary) VALUES
(1, 'John', 10, 25000),
(2, 'Alice', 20, 30000),
(3, 'Bob', 10, 15000),
(4, 'Charlie', 30, 28000),
(5, 'David', 20, 45000),
(6, 'Emma', 30, 32000);

select deptno ,sum(salary) as salary_sum from employee group by deptno;