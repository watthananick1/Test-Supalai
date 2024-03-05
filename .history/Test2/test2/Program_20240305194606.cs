using ClosedXML;

// ปรับแต่งชื่อไฟล์และชื่อ Sheet
var filePath = "./Quiz_Dev_Interview_table.xlsx";
var sheetName = "Sheet2";

// โหลดไฟล์ xlsx
var workbook = new XLWorkbook(filePath);

// เลือก Worksheet ที่ต้องการ
var worksheet = workbook.Worksheet(sheetName);

// ตัวอย่างการ Query

// 1. หาจำนวนพนักงานของแต่ละตำแหน่งงาน
var positions = worksheet.Rows()
    .Where(row => !row.IsEmpty())
    .Select(row => row.Cell(4).Value.ToString())
    .Distinct()
    .ToList();

foreach (var position in positions)
{
    var count = worksheet.Rows()
        .Where(row => !row.IsEmpty())
        .Count(row => row.Cell(4).Value.ToString() == position);

    Console.WriteLine($"{position}: {count}");
}

// 2. หาจำนวนพนักงานของแต่ละแผนก
var departments = worksheet.Rows()
    .Where(row => !row.IsEmpty())
    .Select(row => row.Cell(3).Value.ToString())
    .Distinct()
    .ToList();

foreach (var department in departments)
{
    var count = worksheet.Rows()
        .Where(row => !row.IsEmpty())
        .Count(row => row.Cell(3).Value.ToString() == department);

    Console.WriteLine($"{department}: {count}");
}

// ... เพิ่มเติมสำหรับ Query อื่นๆ

