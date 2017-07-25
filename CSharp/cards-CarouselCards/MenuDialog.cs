namespace CarouselCardsBot
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;

    [Serializable]
    public class MenuDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Okay, here is our menu. Let me know when you ready.");

            Activity replyMenu = (Activity)context.MakeMessage();
            replyMenu.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            replyMenu.Attachments = GetCards.GetCardsAttachments();

            await context.PostAsync(replyMenu);

            context.Wait(this.MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            await context.PostAsync("Your order has been placed! Let me know if you want to order again!");

            // This will return us to RootDialog
            context.Done(true);
        }
    }
}
