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
            await context.PostAsync("Let me take down your delivery info now.");

            var formDialog = FormDialog.FromForm(this.BuildPersonalInfoForm, FormOptions.PromptInStart);

            context.Call(formDialog, this.ResumeAfterFormDialog);
        }

        private IForm<PersonalInfoQuery> BuildPersonalInfoForm()
        {
            OnCompletionAsyncDelegate<PersonalInfoQuery> compilePersonalInfo = async (context, state) =>
            {
                await context.PostAsync($"Great, we will deliver your order to {state.Address}. We will contact you at {state.Phone} or {state.Email}.");
            };

            return new FormBuilder<PersonalInfoQuery>()
                .Field(nameof(PersonalInfoQuery.Address))
                .AddRemainingFields()
                .OnCompletion(compilePersonalInfo)
                .Build();
        }

        private async Task ResumeAfterFormDialog(IDialogContext context, IAwaitable<PersonalInfoQuery> result)
        {
            try
            {
                context.Call(new MenuDialog(), this.ResumeAfterOptionDialog);
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