namespace BlogCenter.WebAPI.Dtos.Enums
{
    public class Enums
    {
        public enum BlogStatus
        {
            Draft = 1,
            SendForApproval = 2,
            Approved = 3,
            Rejected = 4,
            Deleted = 5
        }

        public enum BlogTableColumn
        {
            title = 1,
            content = 2,
            status = 3
        }
        public enum UserStatus
        {
            Active = 1,
            Deactive = 2,
            Deleted = 3
        }


    }
}
