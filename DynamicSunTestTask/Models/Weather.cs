using System.ComponentModel.DataAnnotations;

namespace DynamicSunTestTask.Models;

public class Weather
{
    [Key]
    public int Id { get; set; }
    
    // дата
    [Required]
    public int Day { get; set; }
    
    [Required]
    public int Month { get; set; }
    
    [Required]
    public int Year { get; set; }
    
    // время
    [Required]
    public string Time { get; set; }
    
    // температура воздуха
    [Required]
    public double Temperature { get; set; }
    
    // относительная влажность
    [Required]
    public double Rh { get; set; }
    
    // точка росы
    [Required]
    public double Td { get; set; }
    
    // атмосферное давление
    [Required]
    public int AtmosphericPressure { get; set; }
    
    // направление ветра
    public string? WindDirection { get; set; }
    
    // скорость ветра
    public int? WindSpeed { get; set; }
    
    // облачность
    public int? CloudCover { get; set; }
    
    // нижняя граница облачности
    public int? H { get; set; }
    
    // горизонтальная видимость
    public int? Vv { get; set; }
    
    // погодные явления
    public string? WeatherPhenomena { get; set; }
}