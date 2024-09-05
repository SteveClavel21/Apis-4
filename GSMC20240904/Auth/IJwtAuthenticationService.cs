namespace GSMC20240904.Auth
{
    public interface IJwtAuthenticationService
    {
        String Authenticate(string userName);
    }
}
