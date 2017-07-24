namespace CarouselCardsBot
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;

    public enum MenuSequence
    {
        Initial = 0,
        ShownMenu,
        OrderTaken
    };

    [Serializable]
    public class CarouselCardsDialog : IDialog<object>
    {
        public MenuSequence menuSequence = MenuSequence.Initial;
        public string order = "";
        public string size = "";

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(this.MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result as Activity;
            var reply = context.MakeMessage();

            if(menuSequence == MenuSequence.Initial && (activity.Text.Contains("menu") || activity.Text.Contains("menus")))
            {
                reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                reply.Attachments = GetCardsAttachments();
                reply.Text = "Here is our menu. Let me know what you want to order!";
                menuSequence = MenuSequence.ShownMenu;
            }
            else if(menuSequence <= MenuSequence.ShownMenu && activity.Text.Contains("tea"))
            {
                reply.Text = "What size tea would you like?";
                order = "tea";
                menuSequence = MenuSequence.OrderTaken;
            }
            else if(menuSequence == MenuSequence.OrderTaken && (activity.Text.Contains("small") || activity.Text.Contains("medium") || activity.Text.Contains("large")))
            {
                size = activity.Text;
                reply.Text = $"One {size} {order} coming up! Your total is $1 million dollars. Bye bye!";
                menuSequence = MenuSequence.Initial;
            }
            else
            {
                if(menuSequence == MenuSequence.Initial)
                {
                    reply.Text = "Type menu to see our menu!";
                }
                else if(menuSequence == MenuSequence.ShownMenu)
                {
                    reply.Text = "Order a tea!";
                }
                else
                {
                    reply.Text = "Small, medium, or large?";
                }
            }

            await context.PostAsync(reply);
            context.Wait(this.MessageReceivedAsync);
        }

        private static IList<Attachment> GetCardsAttachments()
        {
            return new List<Attachment>()
            {
                GetHeroCard(
                    "Azure Storage",
                    "Offload the heavy lifting of data center management",
                    "Store and help protect your data. Get durable, highly available data storage across the globe and pay only for what you use.",
                    new CardImage(url: "https://docs.microsoft.com/en-us/azure/storage/media/storage-introduction/storage-concepts.png"),
                    new CardAction(ActionTypes.OpenUrl, "Learn more", value: "https://azure.microsoft.com/en-us/services/storage/")),
                GetThumbnailCard(
                    "DocumentDB",
                    "Blazing fast, planet-scale NoSQL",
                    "NoSQL service for highly available, globally distributed apps—take full advantage of SQL and JavaScript over document and key-value data without the hassles of on-premises or virtual machine-based cloud database options.",
                    new CardImage(url: "https://docs.microsoft.com/en-us/azure/documentdb/media/documentdb-introduction/json-database-resources1.png"),
                    new CardAction(ActionTypes.OpenUrl, "Learn more", value: "https://azure.microsoft.com/en-us/services/documentdb/")),
                GetHeroCard(
                    "Azure Functions",
                    "Process events with a serverless code architecture",
                    "An event-based serverless compute experience to accelerate your development. It can scale based on demand and you pay only for the resources you consume.",
                    new CardImage(url: "https://azurecomcdn.azureedge.net/cvt-5daae9212bb433ad0510fbfbff44121ac7c759adc284d7a43d60dbbf2358a07a/images/page/services/functions/01-develop.png"),
                    new CardAction(ActionTypes.OpenUrl, "Learn more", value: "https://azure.microsoft.com/en-us/services/functions/")),
                GetThumbnailCard(
                    "Cognitive Services",
                    "Build powerful intelligence into your applications to enable natural and contextual interactions",
                    "Enable natural and contextual interaction with tools that augment users' experiences using the power of machine-based intelligence. Tap into an ever-growing collection of powerful artificial intelligence algorithms for vision, speech, language, and knowledge.",
                    new CardImage(url: "https://azurecomcdn.azureedge.net/cvt-68b530dac63f0ccae8466a2610289af04bdc67ee0bfbc2d5e526b8efd10af05a/images/page/services/cognitive-services/cognitive-services.png"),
                    new CardAction(ActionTypes.OpenUrl, "Learn more", value: "https://azure.microsoft.com/en-us/services/cognitive-services/")),
            };
        }

        private static Attachment GetHeroCard(string title, string subtitle, string text, CardImage cardImage, CardAction cardAction)
        {
            var heroCard = new HeroCard
            {
                Title = title,
                Subtitle = subtitle,
                Text = text,
                Images = new List<CardImage>() { cardImage },
                Buttons = new List<CardAction>() { cardAction },
            };

            return heroCard.ToAttachment();
        }

        private static Attachment GetThumbnailCard(string title, string subtitle, string text, CardImage cardImage, CardAction cardAction)
        {
            var heroCard = new ThumbnailCard
            {
                Title = title,
                Subtitle = subtitle,
                Text = text,
                Images = new List<CardImage>() { cardImage },
                Buttons = new List<CardAction>() { cardAction },
            };

            return heroCard.ToAttachment();
        }
    }
}
