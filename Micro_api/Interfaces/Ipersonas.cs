namespace Micro_api.Interfaces
{
    public interface Ipersonas
    {
        Task<microHelper_data> getAllPErsonas();
        Task<microHelper_data> getAllPErsonas2(long cuenta);
        Task<microHelper_data> getPersona(int  id);
        Task<microHelper_data> addPersona(personas persona);
        Task<microHelper_data> UpdatePersonas(personas persona);
        Task<microHelper_data> membresiaPersona(long cuenta);
    }
}
