using System.Net;
using Ical.Net;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using Ical.Net.Serialization.iCalendar.Serializers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{

    var calendar = new Calendar();
    calendar.AddProperty("X-WR-CALNAME", "The calendar Title"); // sets the calendar title
    calendar.AddProperty("X-ORIGINAL-URL", "http://www.yourdomain.com");
    calendar.AddProperty("METHOD", "PUBLISH");
  
    var icalevent = new Event()
        {
            DtStart = new CalDateTime(new DateTime(2017, 07, 06, 18, 0, 0, DateTimeKind.Utc)), //start date
            DtEnd = new CalDateTime(new DateTime(2017, 07, 06, 19, 0, 0, DateTimeKind.Utc)), // end date
            Created = new CalDateTime(DateTime.Now), //created time
            Location = "Your Location",
            Summary = "Your Summary",
            Url = new Uri("http://yoururl.com")
        };

    string description = "Your description";

    icalevent.AddProperty("X-ALT-DESC;FMTTYPE=text/html", description); // creates an HTML description
    calendar.Events.Add(icalevent);

    var serializer = new CalendarSerializer(new SerializationContext());

    var serializedCalendar = serializer.SerializeToString(calendar);
    var bytesCalendar = Encoding.UTF8.GetBytes(serializedCalendar);
    var result = new HttpResponseMessage(HttpStatusCode.OK)
    {
        Content = new ByteArrayContent(bytesCalendar)
    };

    result.Content.Headers.ContentDisposition =
        new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
    {
        FileName = "invite.ics" // Name of your ICS file
    };

    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

    return result;
}
