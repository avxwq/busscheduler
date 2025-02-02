using System;
using System.Collections.Generic;

namespace RozkladApi.Models
{
    public class RouteDTO
    {
        public string Id { get; set; }          // The unique identifier of the route
        public string Number { get; set; }      // The route number (e.g., "101")
        public string StartPoint { get; set; }  // Starting point of the route
        public string EndPoint { get; set; }    // Ending point of the route
        public List<StopDto> Stops { get; set; } = new();  // A list of stop DTOs for this route
    }

}
