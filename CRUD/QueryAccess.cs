using ProjectManagement.Models;

namespace ProjectManagement.CRUD
{
    public class QueryAccess
    {
        public static Access GetAccessTypeByID(int id)
        {
            return new Access { };
        }

        public static Access[] GetAllAccessTypes()
        {
            return new Access[] { };
        }

        public static int AddAccessType()
        {
            return 1;
        }

        public static int UpdateAccess(int id)
        {
            return 1;
        }

        public static int SoftDeleteAccessType()
        {
            return 1;
        }

        public static int HardDeleteAccessType()
        {
            return 1;
        }
    }
}
