namespace RozkladApi.Models.Dto
{
public class BusLineDto
    {
    public int Id { get; set; }
    public string LineNumber { get; set; }
    public string Description { get; set; }
    public List<BusLineStopDto> LineStops { get; set; }
    }
}
