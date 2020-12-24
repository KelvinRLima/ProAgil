using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        public readonly ProAgilContext _context;

        public ProAgilRepository(ProAgilContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        //GERAL
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        //EVENTO
        public async Task<List<Evento>> GetAllEventosAsync(bool incluirPalestrante = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(c => c.Lotes).Include(c => c.RedesSociais);

            if (incluirPalestrante)
            {
                query = query.Include(pe => pe.PalestrantesEventos).ThenInclude(p => p.Palestrante); //muitos p/ muitos
            }

            query = query.AsNoTracking().OrderBy(c => c.ID);

            return await query.ToListAsync();
        }

        public async Task<Evento[]> GetAllEventosAsyncByTema(string tema, bool incluirPalestrante = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(c => c.Lotes).Include(c => c.RedesSociais);

            if (incluirPalestrante)
            {
                query = query.Include(pe => pe.PalestrantesEventos).ThenInclude(p => p.Palestrante);
            }

            query = query.AsNoTracking().Where(x => x.Tema.ToLower().Contains(tema.ToLower())).OrderByDescending(c => c.DataEvento);

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoAsyncByID(int id, bool incluirPalestrante = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(c => c.Lotes).Include(c => c.RedesSociais);

            if (incluirPalestrante)
            {
                query = query.Include(pe => pe.PalestrantesEventos).ThenInclude(p => p.Palestrante);
            }

            query = query.AsNoTracking().Where(x => x.ID == id).OrderByDescending(c => c.DataEvento);

            return await query.FirstOrDefaultAsync();
        }

        //PALESTRANTE
        public async Task<Palestrante[]> GetAllPalestrantesAsyncByName(string nome, bool incluirEvento = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(c => c.RedesSociais);

            if (incluirEvento)
            {
                query = query.Include(pe => pe.PalestrantesEventos).ThenInclude(p => p.Evento);
            }

            query = query.AsNoTracking().Where(x => x.Nome.ToLower().Contains(nome.ToLower())).OrderBy(c => c.Nome);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteAsyncByID(int id, bool incluirEvento = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(c => c.RedesSociais);

            if (incluirEvento)
            {
                query = query.Include(pe => pe.PalestrantesEventos).ThenInclude(p => p.Evento);
            }

            query = query.AsNoTracking().Where(x => x.ID == id);

            return await query.FirstOrDefaultAsync();
        }
    }
}
