using DynamicSunTestTask.Data;
using DynamicSunTestTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace DynamicSunTestTask.Controllers;

public class WeatherController : Controller
{
    private readonly AppDbContext _db = new AppDbContext();
    private readonly IWebHostEnvironment _environment;

    public WeatherController(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    /// <summary>
    /// Просмотр данных, записанных в БД
    /// </summary>
    /// <param name="month">Месяц [int], при выборе которого будут показываться только те строки данных,
    /// в которых в дате стоит выбранный месяц. Если равен -1, то всё зависит от значения параметра `year`,
    /// иначе страница будет пуста</param>
    /// <param name="year">Год [int], при выборе которого будут показываться только те строки данных,
    /// в которых в дате стоит выбранный месяц. Если равен -1, то всё зависит от значения параметра `month`,
    /// иначе страница будет пуста</param>
    /// <returns>Вывод на экран таблицы данных из БД</returns>
    public async Task<IActionResult> Index(int month = -1, int year = -1)
    {
        var weather = new List<Weather>();

        if (month == -1 && year == -1)
        {
            return View(weather);
        }

        if (month != -1 && year != -1)
        {
            weather = await _db.Weathers
                .Where(item => item.Month == month && item.Year == year)
                .ToListAsync();
        }
        else if (month != -1)
        {
            weather = await _db.Weathers
                .Where(item => item.Month == month)
                .ToListAsync();
        }
        else if (year != -1)
        {
            weather = await _db.Weathers
                .Where(item => item.Year == year)
                .ToListAsync();
        }

        return View(weather);
    }

    [HttpGet]
    public IActionResult Import()
    {
        return View();
    }

    /// <summary>
    /// Обработка и запись в БД полученных файлов
    /// </summary>
    /// <param name="files">Полученные от пользователя файлы</param>
    /// <returns>Перенаправляет на страницу просмотра архивов данных</returns>
    [HttpPost]
    public async Task<IActionResult> Import(List<IFormFile> files)
    {
        foreach (var file in files)
        {
            if (file.FileName.Split(".")[1] != "xlsx")
            {
                return RedirectToAction("IncorrectFileType");
            }

            await using var fileStream = new FileStream
            (
                Path.Combine(_environment.ContentRootPath, file.FileName),
                FileMode.Create,
                FileAccess.Write
            );
            await file.CopyToAsync(fileStream);
            var fileName = fileStream.Name;
            fileStream.Close();

            IWorkbook workbook;
            await using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                workbook = new XSSFWorkbook(fs);
            }

            for (var sheetIndex = 0; sheetIndex < workbook.NumberOfSheets; sheetIndex++)
            {
                var sheet = workbook.GetSheetAt(sheetIndex);

                var month = new List<Weather>();
                for (var rowIndex = 4; rowIndex < sheet.LastRowNum + 1; rowIndex++)
                {
                    var row = sheet.GetRow(rowIndex);

                    int dayAtr;
                    int monthAtr;
                    int yearAtr;
                    string timeAtr;
                    double temperatureAtr;
                    double rhAtr;
                    double tdAtr;
                    int atmosphericPressureAtr;
                    string? windDirectionAtr;
                    int? windSpeedAtr;
                    int? cloudCoverAtr;
                    int? hAtr;
                    int? vvAtr;
                    string? weatherPhenomenaAtr;

                    try
                    {
                        var tempDate = row.GetCell(0).StringCellValue.Split(".");
                        var tempTime = row.GetCell(1).StringCellValue.Split(":");

                        var dateAndTime =
                            new DateTimeOffset(int.Parse(tempDate[2]), int.Parse(tempDate[1]), int.Parse(tempDate[0]),
                                int.Parse(tempTime[0]), int.Parse(tempTime[1]), 00, TimeSpan.Zero);
                        dayAtr = int.Parse(tempDate[0]);
                        monthAtr = int.Parse(tempDate[1]);
                        yearAtr = int.Parse(tempDate[2]);
                        timeAtr = dateAndTime.TimeOfDay.ToString();
                        temperatureAtr = row.GetCell(2).NumericCellValue;
                        rhAtr = row.GetCell(3).NumericCellValue;
                        tdAtr = row.GetCell(4).NumericCellValue;
                        atmosphericPressureAtr = int.Parse($"{row.GetCell(5).NumericCellValue}");
                    }
                    catch (Exception)
                    {
                        return RedirectToAction("IncorrectFileType");
                    }

                    try
                    {
                        var value = row.GetCell(6).StringCellValue;
                        windDirectionAtr = value == "" ? null : value;
                    }
                    catch (Exception)
                    {
                        windDirectionAtr = null;
                    }

                    try
                    {
                        var value = row.GetCell(7).NumericCellValue;
                        windSpeedAtr = int.Parse(value.ToString());
                    }
                    catch (Exception)
                    {
                        windSpeedAtr = null;
                    }

                    try
                    {
                        var value = row.GetCell(8).NumericCellValue;
                        cloudCoverAtr = int.Parse(value.ToString());
                    }
                    catch (Exception)
                    {
                        cloudCoverAtr = null;
                    }

                    try
                    {
                        var value = row.GetCell(9).NumericCellValue;
                        hAtr = int.Parse(value.ToString());
                    }
                    catch (Exception)
                    {
                        hAtr = null;
                    }

                    try
                    {
                        var value = row.GetCell(10).NumericCellValue;
                        vvAtr = int.Parse(value.ToString());
                    }
                    catch (Exception)
                    {
                        vvAtr = null;
                    }

                    try
                    {
                        var value = row.GetCell(11).StringCellValue;
                        weatherPhenomenaAtr = value == "" ? null : value;
                    }
                    catch (Exception)
                    {
                        weatherPhenomenaAtr = null;
                    }

                    var weatherRow = new Weather
                    {
                        Day = dayAtr,
                        Month = monthAtr,
                        Year = yearAtr,
                        Time = timeAtr,
                        Temperature = temperatureAtr,
                        Rh = rhAtr,
                        Td = tdAtr,
                        AtmosphericPressure = atmosphericPressureAtr,
                        WindDirection = windDirectionAtr,
                        WindSpeed = windSpeedAtr,
                        CloudCover = cloudCoverAtr,
                        H = hAtr,
                        Vv = vvAtr,
                        WeatherPhenomena = weatherPhenomenaAtr,
                    };

                    /*
                    опционально можно было бы добавлять каждую отдельную строку в бд тут таким образом:

                    await _db.Weathers.AddAsync(weatherRow);
                    await _db.SaveChangesAsync()

                    но я выбрал атомарный вариант, при котором у нас добавляются либо полные данные из файла, либо ничего,
                    потому что это работает в разы быстрее
                    */
                    month.Add(weatherRow);
                }

                await _db.Weathers.AddRangeAsync(month);
            }

            await _db.SaveChangesAsync();
        }

        return RedirectToAction("Index");
    }

    /// <summary>
    /// Отображение страницы, выпадающей при неправильном типе загружаемого файла (не .xlsx)
    /// </summary>
    public IActionResult IncorrectFileType()
    {
        return View("IncorrectFileType");
    }

    /// <summary>
    /// Отображение страницы, выпадающей при некорректных данных в загруженном файле
    /// </summary>
    public IActionResult IncorrectFileData()
    {
        return View("IncorrectFileData");
    }
}