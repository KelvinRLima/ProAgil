using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        //EVENTO
        Task<Evento[]> GetAllEventosAsyncByTema(string tema, bool incluirPalestrante);
        Task<List<Evento>> GetAllEventosAsync(bool incluirPalestrante);
        Task<Evento> GetEventoAsyncByID(int eventoID, bool incluirPalestrante);

        //PALESTRANTE
        Task<Palestrante[]> GetAllPalestrantesAsyncByName(string nome, bool incluirEvento);
        Task<Palestrante> GetPalestranteAsyncByID(int palestranteID, bool incluirEvento);
    }
}
