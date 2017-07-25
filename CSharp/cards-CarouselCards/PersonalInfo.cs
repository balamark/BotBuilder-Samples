using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarouselCardsBot
{
    using System;

    [Serializable]
    public class PersonalInfo
    {
        public string Name { get; set; }

        public int Rating { get; set; }

        public int NumberOfReviews { get; set; }

        public int PriceStarting { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }
    }
}