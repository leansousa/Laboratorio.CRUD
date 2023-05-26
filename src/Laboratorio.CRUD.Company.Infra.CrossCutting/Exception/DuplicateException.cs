namespace Laboratorio.CRUD.Company.Infra.CrossCutting.Exception
{
    public class DuplicateException : System.Exception
    {
        public DuplicateException()
        {
        }

        public DuplicateException(string message)
            : base(message)
        {
        }

        public DuplicateException(string message, System.Exception inner)
            : base(message, inner)
        {
        }
    }
}
