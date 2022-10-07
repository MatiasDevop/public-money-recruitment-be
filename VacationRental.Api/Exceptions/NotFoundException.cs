namespace VacationRental.Api.Exceptions
{
    public class NotFoundException : BaseAppException
    {
        public NotFoundException(string name, object key)
           : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}
