using RadoHub.Data.Models;

namespace RadoHub.Services.Constants
{
    public class SupportedCultures
    {
        public static Culture[] GetAll => new[] { Bulgarian, English };

        public static Culture Bulgarian => new Culture("BG", "bg");

        public static Culture English => new Culture("EN", "en-US");


    }
}
