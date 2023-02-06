using RingCentral;

namespace Send_SMS
{
    internal class CreateFaxMessageRequest
    {
        internal Attachment[] attachments;
        internal MessageStoreCalleeInfoRequest[] to;
        internal string faxResolution;
        internal string coverPageText;

        public CreateFaxMessageRequest()
        {
        }
    }
}