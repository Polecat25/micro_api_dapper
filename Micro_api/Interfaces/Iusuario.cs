namespace Micro_api.Interfaces
{
    public interface Iusuario
    {
        public Task<microHelper_data> login(AuthUserPeti usuario);
    }
}
