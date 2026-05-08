using StardewModdingAPI.Utilities;
using StardewValley;
using StardewWebApi.Server;
using StardewWebApi.Server.Routing;

namespace StardewWebApi.Api.V1;

[RequireLoadedGame]
[Route("/api/v1/calendar")]
public class CalendarController : ApiControllerBase
{
    [Route("/")]
    public void GetCalendar()
    {
        var date = SDate.Now();

        var birthdays = Utility.getAllCharacters()
            .Where(n => n.Birthday_Season == date.Season && n.Birthday_Day == date.Day)
            .Select(n => new
            {
                name = n.Name,
                displayName = n.displayName
            });

        var isFestival = Game1.isFestival();
        var festivalName = isFestival ? Game1.CurrentEvent?.festivalName : null;

        Response.Ok(new
        {
            season = date.Season,
            day = date.Day,
            year = date.Year,
            isFestival,
            festivalName,
            birthdays
        });
    }
}
