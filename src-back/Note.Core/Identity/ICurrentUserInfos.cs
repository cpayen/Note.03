namespace Note.Core.Identity
{
    public interface ICurrentUserInfos
    {
        string Id { get; }
        string UserName { get; }
        string FullName { get; }
        bool HasRole(string role);
    }
}
