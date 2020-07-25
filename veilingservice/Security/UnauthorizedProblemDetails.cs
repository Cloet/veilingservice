namespace veilingservice.Security
{
    internal class UnauthorizedProblemDetails : IProblemDetails
    {
        public string Message => "Unauthorized client.";
    }
}