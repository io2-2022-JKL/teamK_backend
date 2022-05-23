using System;

namespace VaccinationSystemApi.Exceptions
{
    public class NoChangesInDatabaseException : Exception
    {
        public NoChangesInDatabaseException()
        {
        }

        public NoChangesInDatabaseException(string message)
            : base(message)
        {
        }

        public NoChangesInDatabaseException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
