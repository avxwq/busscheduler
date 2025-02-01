namespace RozkladApi.Models
{
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
    public class BusStop
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
    }

    public class BusLine
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string LineNumber { get; set; }
        public string Description { get; set; }
        
        public List<BusLineStop> LineStops { get; set; }
    }

    public class BusLineStop
    {
        [Key]
        public int Id { get; set; }
        
        public int BusLineId { get; set; }
        public BusLine BusLine { get; set; }

        public int BusDepartureStopId { get; set; }
        public BusStop BusDepartureStop { get; set; }
        
        public int BusStopId { get; set; }
        public BusStop BusStop { get; set; }
        
        [Required]
        public TimeSpan TravelTime { get; set; }
        
    }
}
