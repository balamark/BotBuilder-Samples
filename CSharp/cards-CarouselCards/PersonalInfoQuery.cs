using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarouselCardsBot
{
    using System;
    using Microsoft.Bot.Builder.FormFlow;

    [Serializable]
    public class PersonalInfoQuery
    {
        [Prompt("Please enter your delivery {&}")]
        public string Address { get; set; }

        [Prompt("What is your phone number {&}?")]
        public string Phone { get; set; }

        [Prompt("What is your email address {&}?")]
        public int Email { get; set; }
    }
}