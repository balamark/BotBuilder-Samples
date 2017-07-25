using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdaptiveCards;

namespace CarouselCardsBot
{
    public class GetCards
    {
        public static IList<Attachment> GetCardsAttachments()
        {
            return new List<Attachment>()
            {
                GetHeroCard(
                    "Azure Storage",
                    "$4",
                    "Store and help protect your data. Get durable, highly available data storage across the globe and pay only for what you use.",
                    new CardImage(url: "https://docs.microsoft.com/en-us/azure/storage/media/storage-introduction/storage-concepts.png")),
                GetHeroCard(
                    "Jason's golf putter",
                    "$999",
                    "help you improve your putting skill",
                    new CardImage(url: "http://www.antiqueclubs.com/images4/Putters401/LeadtopPutter401.jpg")),
                GetHeroCard(
                    "DocumentDB",
                    "$2.50",
                    "NoSQL service for highly available, globally distributed apps—take full advantage of SQL and JavaScript over document and key-value data without the hassles of on-premises or virtual machine-based cloud database options.",
                    new CardImage(url: "https://docs.microsoft.com/en-us/azure/documentdb/media/documentdb-introduction/json-database-resources1.png")),
                GetHeroCard(
                    "Azure Functions",
                    "$1",
                    "An event-based serverless compute experience to accelerate your development. It can scale based on demand and you pay only for the resources you consume.",
                    new CardImage(url: "https://azurecomcdn.azureedge.net/cvt-5daae9212bb433ad0510fbfbff44121ac7c759adc284d7a43d60dbbf2358a07a/images/page/services/functions/01-develop.png")),
                GetHeroCard(
                    "Cognitive Services",
                    "$7",
                    "Enable natural and contextual interaction with tools that augment users' experiences using the power of machine-based intelligence. Tap into an ever-growing collection of powerful artificial intelligence algorithms for vision, speech, language, and knowledge.",
                    new CardImage(url: "https://azurecomcdn.azureedge.net/cvt-68b530dac63f0ccae8466a2610289af04bdc67ee0bfbc2d5e526b8efd10af05a/images/page/services/cognitive-services/cognitive-services.png")),
            };
        }

        private static Attachment GetHeroCard(string title, string price, string text, CardImage cardImage)
        {
            var cardAction1 = new CardAction() { Type = ActionTypes.ImBack, Title = "1", Value = $"1 {title} @ {price}" };
            var cardAction2 = new CardAction() { Type = ActionTypes.ImBack, Title = "2", Value = $"2 {title} @ {price}" };
            var cardAction3 = new CardAction() { Type = ActionTypes.ImBack, Title = "3", Value = $"3 {title} @ {price}" };

            var heroCard = new HeroCard
            {
                Title = title,
                Subtitle = price,
                Text = text,
                Images = new List<CardImage>() { cardImage },
                Buttons = new List<CardAction>() { cardAction1, cardAction2, cardAction3 },
            };

            return heroCard.ToAttachment();
        }

        //private static Attachment GetThumbnailCard(string title, string subtitle, string text, CardImage cardImage, CardAction cardAction)
        //{
        //    var heroCard = new ThumbnailCard
        //    {
        //        Title = title,
        //        Subtitle = subtitle,
        //        Text = text,
        //        Images = new List<CardImage>() { cardImage },
        //        Buttons = new List<CardAction>() { cardAction },
        //    };

        //    return heroCard.ToAttachment();
        //}
    }
}