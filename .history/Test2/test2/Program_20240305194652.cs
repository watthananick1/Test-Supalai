using OfficeOpenXml;

// ปรับแต่งชื่อไฟล์และชื่อ Sheet
var filePath = "./Quiz_Dev_Interview_table.xlsx";
var sheetName = "Sheet2";

// โหลดไฟล์ xlsx
using (var package = new ExcelPackage(new FileInfo(filePath)))
{
    // เลือก Worksheet ที่ต้องการ
    var worksheet = package.Workbook.Worksheets[sheetName];

    // ตัวอย่างการ Query

    // 1. หาจำนวนพนักงานของแต่ละตำแหน่งงาน
    var positions = worksheet.Cells.Where(cell => !cell.IsEmpty()).Select(cell => cell.Value.ToString()).Distinct().ToList();

    foreach (var position in positions)
    {
        var count = worksheet.Cells.Where(cell => !cell.IsEmpty()).Count(cell => cell.Value.ToString() == position);

        Console.WriteLine($"{position}: {count}");
    }

    // 2. หาจำนวนพนักงานของแต่ละแผนก
    var departments = worksheet.Cells.Where(cell => !cell.IsEmpty()).Select(cell => cell.Offset(0, 1).Value.ToString()).Distinct().ToList();

    foreach (var department in departments)
    {
        var count = worksheet.Cells.Where(cell => !cell.IsEmpty()).Count(cell => cell.Offset(0, 1).Value.ToString() == department);

        Console.WriteLine($"{department}: {count}");
    }

    // ... เพิ่มเติมสำหรับ Query อื่นๆ
}
