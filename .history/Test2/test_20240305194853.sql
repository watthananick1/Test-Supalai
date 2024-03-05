1. หาจำนวนพนักงานของแต่ละตำแหน่งงาน
SELECT
  position.position_name,
  COUNT(DISTINCT employee.employee_id) AS number_of_employees
FROM employee
INNER JOIN position ON employee.position_id = position.position_id
GROUP BY position.position_name
ORDER BY position.position_name;

2. หาจำนวนพนักงานของแต่ละแผนก


