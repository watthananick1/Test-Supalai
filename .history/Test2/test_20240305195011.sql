--1. หาจำนวนพนักงานของแต่ละตำแหน่งงาน
SELECT
  position.position_name,
  COUNT(DISTINCT employee.employee_id) AS number_of_employees
FROM employee
INNER JOIN position ON employee.position_id = position.position_id
GROUP BY position.position_name
ORDER BY position.position_name;

--2. หาจำนวนพนักงานของแต่ละแผนก

SELECT
  department.department_name,
  COUNT(DISTINCT employee.employee_id) AS number_of_employees
FROM employee
INNER JOIN department ON employee.department_id = department.department_id
GROUP BY department.department_name
ORDER BY department.department_name;

--3. หาพนักงานที่มีเงินเดือนอยู่ในช่วง 20,000 ถึง 60,000 พร้อมแสดงข้อมูลพนักงาน แผนก และตำแหน่ง

SELECT
  employee.employee_name,
  department.department_name,
  position.position_name,
  employee.salary
FROM employee
INNER JOIN department ON employee.department_id = department.department_id
INNER JOIN position ON employee.position_id = position.position_id
WHERE employee.salary BETWEEN 20000 AND 60000
ORDER BY employee.employee_name;
