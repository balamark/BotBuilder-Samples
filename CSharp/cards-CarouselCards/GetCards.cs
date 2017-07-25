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
                    "Offload the heavy lifting of data center management",
                    "Store and help protect your data. Get durable, highly available data storage across the globe and pay only for what you use.",
                    new CardImage(url: "https://docs.microsoft.com/en-us/azure/storage/media/storage-introduction/storage-concepts.png")),
                GetHeroCard(
                    "Jason's golf putter",
                    "worth your money",
                    "help you improve your putting skill",
                    new CardImage(url: "http://www.antiqueclubs.com/images4/Putters401/LeadtopPutter401.jpg")),
                GetHeroCard(
                    "DocumentDB",
                    "Blazing fast, planet-scale NoSQL",
                    "NoSQL service for highly available, globally distributed apps—take full advantage of SQL and JavaScript over document and key-value data without the hassles of on-premises or virtual machine-based cloud database options.",
                    new CardImage(url: "https://docs.microsoft.com/en-us/azure/documentdb/media/documentdb-introduction/json-database-resources1.png")),
                GetHeroCard(
                    "Azure Functions",
                    "Process events with a serverless code architecture",
                    "An event-based serverless compute experience to accelerate your development. It can scale based on demand and you pay only for the resources you consume.",
                    new CardImage(url: "https://azurecomcdn.azureedge.net/cvt-5daae9212bb433ad0510fbfbff44121ac7c759adc284d7a43d60dbbf2358a07a/images/page/services/functions/01-develop.png")),
                GetHeroCard(
                    "Cognitive Services",
                    "Build powerful intelligence into your applications to enable natural and contextual interactions",
                    "Enable natural and contextual interaction with tools that augment users' experiences using the power of machine-based intelligence. Tap into an ever-growing collection of powerful artificial intelligence algorithms for vision, speech, language, and knowledge.",
                    new CardImage(url: "https://azurecomcdn.azureedge.net/cvt-68b530dac63f0ccae8466a2610289af04bdc67ee0bfbc2d5e526b8efd10af05a/images/page/services/cognitive-services/cognitive-services.png")),
            };
        }

        private static Attachment GetHeroCard(string title, string subtitle, string text, CardImage cardImage)
        {
            var cardAction1 = new CardAction() { Type = ActionTypes.ImBack, Title = "1", Value = $" Order 1 {title}" };
            var cardAction2 = new CardAction() { Type = ActionTypes.ImBack, Title = "2", Value = $"Order 2 {title}" };
            var cardAction3 = new CardAction() { Type = ActionTypes.ImBack, Title = "3", Value = $"Order 3 {title}" };

            var heroCard = new HeroCard
            {
                Title = title,
                Subtitle = subtitle,
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