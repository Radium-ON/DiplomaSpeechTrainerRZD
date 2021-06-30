
namespace SpeechTrainer.Core
{
    public static class Session
    {
        public static int StudentId { get; private set; }
        public static Status Status { get; private set; }

        public static void SetId(int value)
        {
            StudentId = value;
            Status = Status.Authorized;
        }

        public static void DeleteStatus()
        {
            StudentId = -1;
            Status = Status.NotAuthorized;
        }
    }
}
