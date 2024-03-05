using OfficeOpenXml;
using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        string filePath = "./Quiz_Dev_Interview_table.xlsx";
        
        using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
        {
            ExcelWorksheet employeeWorksheet = package.Workbook.Worksheets["Employees"];
            ExcelWorksheet departmentWorksheet = package.Workbook.Worksheets["Departments"];

            QueryEmployeeByPosition(employeeWorksheet);
            QueryEmployeeByDepartment(employeeWorksheet, departmentWorksheet);
            QueryEmployeeBySalaryRange(employeeWorksheet);
            QueryDepartmentWithStaff(employeeWorksheet, departmentWorksheet);
            QueryAverageSalaryByPosition(employeeWorksheet);
        }
    }

    static void QueryEmployeeByPosition(ExcelWorksheet employeeWorksheet)
    {
        var positionQuery = employeeWorksheet.Cells["B7:F24"].Select(cell => cell.Value?.ToString());
        var positionCount = positionQuery.GroupBy(pos => pos).Select(group => new { Position = group.Key, Count = group.Count() });

        Console.WriteLine("1. จำนวนพนักงานของแต่ละตำแหน่งงาน:");
        foreach (var item in positionCount)
        {
            Console.WriteLine($"{item.Position}: {item.Count}");
        }
        Console.WriteLine();
    }

    static void QueryEmployeeByDepartment(ExcelWorksheet employeeWorksheet, ExcelWorksheet departmentWorksheet)
    {
        var departmentCount = from employee in employeeWorksheet.Cells["B7:F24"]
                              join department in departmentWorksheet.Cells["A2:B100"]
                              on employee.Value.ToString() equals department[0].Value.ToString()
                              group employee by department[1].Value.ToString() into g
                              select new { Department = g.Key, Count = g.Count() };

        Console.WriteLine("2. จำนวนพนักงานของแต่ละแผนก:");
        foreach (var item in departmentCount)
        {
            Console.WriteLine($"{item.Department}: {item.Count}");
        }
        Console.WriteLine();
    }

    static void QueryEmployeeBySalaryRange(ExcelWorksheet employeeWorksheet)
    {
        var employees = employeeWorksheet.Cells["A2:E100"].Select(cell => cell.Value).ToArray();

        Console.WriteLine("3. พนักงานที่มีเงินเดือนอยู่ในช่วง 20,000 ถึง 60,000 พร้อมแสดงข้อมูลพนักงาน แผนก และตำแหน่ง:");
        foreach (var employee in employees)
        {
            string employeeID = employee[0]?.ToString();
            string firstName = employee[1]?.ToString();
            string lastName = employee[2]?.ToString();
            string departmentID = employee[3]?.ToString();
            string positionID = employee[4]?.ToString();

            Console.WriteLine($"EmployeeID: {employeeID}, FirstName: {firstName}, LastName: {lastName}, DepartmentID: {departmentID}, PositionID: {positionID}");
        }
        Console.WriteLine();
    }

    static void QueryDepartmentWithStaff(ExcelWorksheet employeeWorksheet, ExcelWorksheet departmentWorksheet)
    {
        var staffDepartments = departmentWorksheet.Cells["B2:B100"].Select(cell => cell.Value?.ToString())
            .Where(department => employeeWorksheet.Cells["E2:E100"].Any(cell => cell.Text == "Staff"))
            .Distinct();

        Console.WriteLine("4. แผนกที่มีพนักงานตำแหน่ง Staff อย่างน้อย 2 คน พร้อมแสดงข้อมูลพนักงาน:");
        foreach (var department in staffDepartments)
        {
            var staffCount = employeeWorksheet.Cells["D2:D100"].Count(cell => cell.Value?.ToString() == department && employeeWorksheet.Cells["E2:E100"].Any(cell => cell.Text == "Staff"));
            if (staffCount >= 2)
            {
                var employeesInDepartment = from employee in employeeWorksheet.Cells["A2:E100"]
                                            join dept in departmentWorksheet.Cells["A2:B100"]
                                            on employee[3].Value.ToString() equals dept[0].Value.ToString()
                                            where employee[4].Value.ToString() == "Staff" && dept[1].Value.ToString() == department
                                            select new { Department = dept[1].Value.ToString(), EmployeeID = employee[0].Value.ToString() };

                foreach (var employee in employeesInDepartment)
                {
                    Console.WriteLine($"Department: {employee.Department}, EmployeeID: {employee.EmployeeID}");
                }
            }
        }
        Console.WriteLine();
    }

    static void QueryAverageSalaryByPosition(ExcelWorksheet employeeWorksheet)
    {
        var positions = employeeWorksheet.Cells["E2:E100"].Select(cell => cell.Value?.ToString()).Distinct();

        Console.WriteLine("5. ค่าเฉลี่ยเงินเดือนของแต่ละตำแหน่งงาน โดยแบ่งเป็นแต่ละแผนก:");
        foreach (var position in positions)
        {
            var salarySum = (from employee in employeeWorksheet.Cells["A2:E100"]
                             where employee[4].Value.ToString() == position
                             select Convert.ToDouble(employee[3].Value)).Sum();
            var employeeCount = employeeWorksheet.Cells["E2:E100"].Count(cell => cell.Text == position);
            var averageSalary = salarySum / employeeCount;
            Console.WriteLine($"Position: {position}, Average Salary: {averageSalary}");
        }
    }
}
