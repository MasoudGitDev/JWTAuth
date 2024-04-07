namespace Infra.Auth.Contexts;
public static class DbSettings {

    public static readonly string connectionString =
    @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AuthDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

}
