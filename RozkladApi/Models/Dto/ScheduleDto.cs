public class ScheduleDTO
{
    public int LineId { get; set; }
    public int StopId { get; set; }
    public List<string> Weekdays { get; set; } = new();
    public List<string> Saturdays { get; set; } = new();
    public List<string> Sundays { get; set; } = new();
}
