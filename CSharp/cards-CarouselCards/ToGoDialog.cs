namespace MultiDialogsBot.Dialogs
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
    using Microsoft.Bot.Builder.FormFlow;
    using CarouselCardsBot;
    using System.Collections.Generic;

    [Serializable]
    public class ToGoDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("I see, let me take down your delivery info now.");

            var hotelsFormDialog = FormDialog.FromForm(this.BuildPersonalInfoForm, FormOptions.PromptInStart);

            context.Call(hotelsFormDialog, this.ResumeAfterFormDialog);
        }

        private IForm<PersonalInfoQuery> BuildPersonalInfoForm()
        {
            OnCompletionAsyncDelegate<PersonalInfoQuery> processHotelsSearch = async (context, state) =>
            {
                await context.PostAsync($"Ok. Your address is {state.Address}. Phone number is {state.Phone}. Email is {state.Email}.");
            };

            return new FormBuilder<PersonalInfoQuery>()
                .Field(nameof(PersonalInfoQuery.Address))
                .Message("Looking for hotels in {Address}...")
                .AddRemainingFields()
                .OnCompletion(processHotelsSearch)
                .Build();
        }

        private async Task ResumeAfterFormDialog(IDialogContext context, IAwaitable<PersonalInfoQuery> result)
        {
            try
            {
                var searchQuery = await result;
                await context.PostAsync($"Here is our menu");

                var resultMessage = context.MakeMessage();
                resultMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                resultMessage.Attachments = GetCards.GetCardsAttachments();
                await context.PostAsync(resultMessage);

                context.Call(new TakeOrderDialog(), this.ResumeAfterOptionDialog);
            }
            catch (FormCanceledException ex)
            {
                string reply;

                if (ex.InnerException == null)
                {
                    reply = "You have canceled the operation. Quitting from the TogoDialog";
                }
                else
                {
                    reply = $"Oops! Something went wrong :( Technical Details: {ex.InnerException.Message}";
                }

                await context.PostAsync(reply);
            }
            finally
            {
                context.Done<object>(null);
            }
        }

        private async Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                var message = await result;
            }
            catch (Exception ex)
            {
                await context.PostAsync($"Failed with message: {ex.Message}");
            }
            finally
            {
                context.Done<object>(null);
            }
        }
    }
}