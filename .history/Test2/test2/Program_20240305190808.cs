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
            ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

            QueryEmployeeByPosition(worksheet);
            QueryEmployeeByDepartment(worksheet);
            QueryEmployeeBySalaryRange(worksheet);
            QueryDepartmentWithStaff(worksheet);
            QueryAverageSalaryByPosition(worksheet);
        }
    }

    static void QueryEmployeeByPosition(ExcelWorksheet worksheet)
{
    var positionQuery = worksheet.Cells["B2:B100"].Select(cell => cell.Value?.ToString());
    var positionCount = positionQuery.GroupBy(pos => pos).Select(group => new { Position = group.Key, Count = group.Count() });

    Console.WriteLine("1. จำนวนพนักงานของแต่ละตำแหน่งงาน:");
    foreach (var item in positionCount)
    {
        Console.WriteLine($"{item.Position}: {item.Count}");
    }
    Console.WriteLine();
}

static void QueryEmployeeByDepartment(ExcelWorksheet worksheet)
{
    var departmentQuery = worksheet.Cells["C2:C100"].Select(cell => cell.Value?.ToString());
    var departmentCount = departmentQuery.GroupBy(dept => dept).Select(group => new { Department = group.Key, Count = group.Count() });

    Console.WriteLine("2. จำนวนพนักงานของแต่ละแผนก:");
    foreach (var item in departmentCount)
    {
        Console.WriteLine($"{item.Department}: {item.Count}");
    }
    Console.WriteLine();
}

static void QueryEmployeeBySalaryRange(ExcelWorksheet worksheet)
{
    var employees = worksheet.Cells["B7:F24"].Select(cell => cell.Value).ToArray();

    Console.WriteLine("3. พนักงานที่มีเงินเดือนอยู่ในช่วง 20,000 ถึง 60,000 พร้อมแสดงข้อมูลพนักงาน แผนก และตำแหน่ง:");
    foreach (var employee in employees)
    {
        string name = employee[0]?.ToString();
        string position = employee[1]?.ToString();
        string department = employee[2]?.ToString();
        double salary = Convert.ToDouble(employee[3]);

        if (salary >= 20000 && salary <= 60000)
        {
            Console.WriteLine($"Name: {name}, Position: {position}, Department: {department}, Salary: {salary}");
        }
    }
    Console.WriteLine();
}

static void QueryDepartmentWithStaff(ExcelWorksheet worksheet)
{
    var staffDepartments = worksheet.Cells["C2:C100"].Select(cell => cell.Value?.ToString())
        .Where(department => worksheet.Cells["B2:B100"].Any(cell => cell.Text == "Staff"))
        .Distinct();

    Console.WriteLine("4. แผนกที่มีพนักงานตำแหน่ง Staff อย่างน้อย 2 คน พร้อมแสดงข้อมูลพนักงาน:");
    foreach (var department in staffDepartments)
    {
        var staffCount = worksheet.Cells["C2:C100"].Count(cell => cell.Value?.ToString() == department && worksheet.Cells["B2:B100"].Any(cell => cell.Text == "Staff"));
        if (staffCount >= 2)
        {
            var employeesInDepartment = worksheet.Cells["A2:C100"].Where(cell => cell.Offset(0, 1).Text == department && cell.Offset(0, -1).Text == "Staff").Select(cell => cell.Value);
            foreach (var employee in employeesInDepartment)
            {
                Console.WriteLine($"Department: {department}, Employee: {string.Join(", ", employee)}");
            }
        }
    }
    Console.WriteLine();
}

static void QueryAverageSalaryByPosition(ExcelWorksheet worksheet)
{
    var positions = worksheet.Cells["B2:B100"].Select(cell => cell.Value?.ToString()).Distinct();

    Console.WriteLine("5. ค่าเฉลี่ยเงินเดือนของแต่ละตำแหน่งงาน โดยแบ่งเป็นแต่ละแผนก:");
    foreach (var position in positions)
    {
        var salarySum = worksheet.Cells["D2:D100"].Where(cell => cell.Offset(0, -1).Text == position).Sum(cell => Convert.ToDouble(cell.Value));
        var employeeCount = worksheet.Cells["B2:B100"].Count(cell => cell.Text == position);
        var averageSalary = salarySum / employeeCount;
        Console.WriteLine($"Position: {position}, Average Salary: {averageSalary}");
    }
}

}
