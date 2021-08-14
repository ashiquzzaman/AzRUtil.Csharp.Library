namespace AzRUtil.Csharp.Library.Abstraction
{

    public interface IHttpAppItem<TUser, TCompany> where TUser : class where TCompany : class

    {
        TUser AppUser { get; set; }
        TCompany AppCompany { get; set; }
        string UserAgentInfo { get; }
        string UserIP { get; }

        TCompany DefaultCompany();
        TCompany SetAppCompany(TCompany appcompany = null);
    }
}
