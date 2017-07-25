namespace MultiDialogsBot.Dialogs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;

    [Serializable]
    public class TakeOrderDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            //context.Fail(new NotImplementedException("Dialog is not implemented and is instead being used to show context.Fail"));
            await context.PostAsync("I see you are ready to order. What would you like to get today?");
            //context.Wait(this.MessageReceivedAsync);
        }
    }
}