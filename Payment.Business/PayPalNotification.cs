using Wr.API.Mailer;

namespace Payment.Business
{
    public class PayPalNotification : Wr.API.Mailer.EmailWorker
    {
       public void Send(string bodyTxt)
       {
           this.SendEmail(_mailerSettings.EmailName, bodyTxt, "PayPal Withdraw");
       }
    }
}
