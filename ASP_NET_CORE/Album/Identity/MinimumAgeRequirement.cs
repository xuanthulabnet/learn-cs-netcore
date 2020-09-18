using Microsoft.AspNetCore.Authorization;

namespace Album.Identity
{
    public class MinimumAgeRequirement : IAuthorizationRequirement
    {
            public int MinimumAge { get; }

            public int CloseTime {set; get;}
            public int OpenTime {set; get;}

            public MinimumAgeRequirement(int minimumAge)
            {
                MinimumAge = minimumAge;
            }

    }
}