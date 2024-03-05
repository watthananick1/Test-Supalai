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
            ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet2"];

            QueryEmployeeByPosition(worksheet);
            QueryEmployeeByDepartment(worksheet);
            // QueryEmployeeBySalaryRange(worksheet);
            // QueryDepartmentWithStaff(worksheet);
            // QueryAverageSalaryByPosition(worksheet);
        }
    }

    static void QueryEmployeeByPosition(ExcelWorksheet worksheet)
    {
        var positionQuery = worksheet.Cells["B7:F24"].Select(cell => cell.Value?.ToString());
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
        var departmentCount = worksheet.Cells["H7:I14"].Select(cell => cell.Value?.ToString())
                              .GroupBy(dep => dep)
                              .Select(group => new { Department = group.Key, Count = group.Count() });

        Console.WriteLine("2. จำนวนพนักงานของแต่ละแผนก:");
        foreach (var item in departmentCount)
        {
            Console.WriteLine($"{item.Department}: {item.Count}");
        }
        Console.WriteLine();
    }

    // static void QueryEmployeeBySalaryRange(ExcelWorksheet worksheet)
    // {
    //     var employees = worksheet.Cells["B7:F24"].Select(cell => cell.Value).ToArray();

    //     Console.WriteLine("3. พนักงานที่มีเงินเดือนอยู่ในช่วง 20,000 ถึง 60,000 พร้อมแสดงข้อมูลพนักงาน แผนก และตำแหน่ง:");
    //     foreach (var employee in employees)
    //     {
    //         string employeeID = employee[0]?.ToString();
    //         string firstName = employee[1]?.ToString();
    //         string lastName = employee[2]?.ToString();
    //         string departmentID = employee[3]?.ToString();
    //         string positionID = employee[4]?.ToString();

    //         Console.WriteLine($"EmployeeID: {employeeID}, FirstName: {firstName}, LastName: {lastName}, DepartmentID: {departmentID}, PositionID: {positionID}");
    //     }
    //     Console.WriteLine();
    // }

    // static void QueryDepartmentWithStaff(ExcelWorksheet worksheet)
    // {
    //     var staffDepartments = worksheet.Cells["G7:H24"].Select(cell => cell.Value?.ToString())
    //         .Where(department => worksheet.Cells["F7:F24"].Any(cell => cell.Text == "Staff"))
    //         .Distinct();

    //     Console.WriteLine("4. แผนกที่มีพนักงานตำแหน่ง Staff อย่างน้อย 2 คน พร้อมแสดงข้อมูลพนักงาน:");
    //     foreach (var department in staffDepartments)
    //     {
    //         var staffCount = worksheet.Cells["G7:H24"].Count(cell => cell.Value?.ToString() == department && worksheet.Cells["F7:F24"].Any(cell => cell.Text == "Staff"));
    //         if (staffCount >= 2)
    //         {
    //             var employeesInDepartment = from employee in worksheet.Cells["B7:F24"]
    //                                         where employee[3].Value.ToString() == department && employee[4].Value.ToString() == "Staff"
    //                                         select new { Department = department, EmployeeID = employee[0].Value.ToString() };

    //             foreach (var employee in employeesInDepartment)
    //             {
    //                 Console.WriteLine($"Department: {employee.Department}, EmployeeID: {employee.EmployeeID}");
    //             }
    //         }
    //     }
    //     Console.WriteLine();
    // }

    // static void QueryAverageSalaryByPosition(ExcelWorksheet worksheet)
    // {
    //     var positions = worksheet.Cells["E7:F24"].Select(cell => cell.Value?.ToString()).Distinct();

    //     Console.WriteLine("5. ค่าเฉลี่ยเงินเดือนของแต่ละตำแหน่งงาน โดยแบ่งเป็นแต่ละแผนก:");
    //     foreach (var position in positions)
    //     {
    //         var salarySum = (from employee in worksheet.Cells["B7:F24"]
    //                          where employee[4].Value.ToString() == position
    //                          select Convert.ToDouble(employee[3].Value)).Sum();
    //         var employeeCount = worksheet.Cells["E7:F24"].Count(cell => cell.Text == position);
    //         var averageSalary = salarySum / employeeCount;
    //         Console.WriteLine($"Position: {position}, Average Salary: {averageSalary}");
    //     }
    // }
}
