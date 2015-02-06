namespace MyWebsite.Utils
{
    public class Enum
    {
        public enum SystemLogStatus
        {
            All = -1,
            Success = 1,
            Failed = 0
        }

        public enum SystemLogActionName
        {
            View,
            Export,
            First,
            Prev,
            Next,
            Last
        }
    }
}
