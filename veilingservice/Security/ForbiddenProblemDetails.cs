namespace veilingservice.Security
{
    internal class ForbiddenProblemDetails : IProblemDetails
    {
        public string Message => "Forbidden for client.";
    }
}