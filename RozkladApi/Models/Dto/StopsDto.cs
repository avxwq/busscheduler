public class StopDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string Zone { get; set; }
    public DeparturesDto Departures { get; set; }
}

public class DeparturesDto
{
    public List<string> Weekdays { get; set; }
    public List<string> Weekends { get; set; }
    public List<string> Holidays { get; set; }
}