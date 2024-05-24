namespace API.Helpers
{
    public static class Helpers
    {
        public static string ConstructEmailBody(string eventName, int quantity, decimal amountPaid)
        {
            string emailBody = $@"
                <html>
                <body>
                    <h2>Your Payment is Confirmed - {eventName} Ticket</h2>
                    <p>Dear Customer,</p>
                    <p>We are thrilled to inform you that your payment for the {eventName} has been successfully processed! Thank you for your purchase.</p>
                    <h3>Event Details:</h3>
                    <ul>
                        <li><strong>Event Name:</strong> {eventName}</li>
                        <li><strong>Quantity:</strong> {quantity}</li>
                    </ul>
                    <h3>Payment Details:</h3>
                    <ul>
                        <li><strong>Amount Paid:</strong> {amountPaid}</li>
                    </ul>
                    <p>Please bring this email and a valid ID to the event for entry.</p>
                    <p>If you have any questions or need further assistance, feel free to contact our support team.</p>
                    <p>Thank you for choosing to attend {eventName}. We look forward to seeing you there!</p>
                    <p>Best regards,<br/>
                    Event Ticketing System<br/>

                </body>
                </html>";

            return emailBody;
        }
    }
}
