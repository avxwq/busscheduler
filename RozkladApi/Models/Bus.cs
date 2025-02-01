using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RozkladApi.Models;

public class BusLine
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Number { get; set; }
    
    public List<int> Stops { get; set; } = new();
}

public class Route
{
    [Key]
    public string Id { get; set; }
    
    [Required]
    public string Number { get; set; }
    
    [Required]
    public string StartPoint { get; set; }
    
    [Required]
    public string EndPoint { get; set; }
    
    public List<Stop> Stops { get; set; } = new();
}

public class Schedule
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("BusLine")]
    public int LineId { get; set; }
    
    [ForeignKey("Stop")]
    public int StopId { get; set; }
    
    public List<string> Weekdays { get; set; } = new();
    public List<string> Saturdays { get; set; } = new();
    public List<string> Sundays { get; set; } = new();
    public BusLine BusLine { get; set; }  // Navigation property to BusLine
    public Stop Stop { get; set; } 
}

public class Stop
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Location { get; set; }
    
    [Required]
    public string Zone { get; set; }
    
    public Departures Departures { get; set; } = new();
}

public class Departures
{
    public string Id {get; set;}
    public List<string> Weekdays { get; set; } = new();
    public List<string> Weekends { get; set; } = new();
    public List<string> Holidays { get; set; } = new();
}