using KooliProjekt.Data; //loodud 05.11 T3 raames. 
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class RealEstatesService: IRealEstatesService
    {
        private readonly ApplicationDbContext _context;
        public RealEstatesService(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<PagedResult<RealEstate>> List(int page, int pageSize) // pole kindel mis sisemisesse <> l'heb 05.11
        {
            var result = await _context.RealEstates.GetPagedAsync(page, pageSize: 3);

            return result;
        }
        public async Task<RealEstate> GetById(int id)
        {
            var result = await _context.RealEstates
                .FirstOrDefaultAsync(m => m.RealEstateId == id);

            return result;
        }
        public async Task Save(RealEstate realEstate)
        {
            if(realEstate.RealEstateId == null)
            {
                _context.Add(realEstate);
            }
            else
            {
                _context.Update(realEstate);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id) 
        {
            var realEstate = await _context.RealEstates.FindAsync(id);
            if (realEstate != null)
            {
                _context.RealEstates.Remove(realEstate);
            }
            await _context.SaveChangesAsync();
        }

        public bool RealEstateExists(int id)
        {
            return (_context.RealEstates?.Any(e => e.RealEstateId == id)).GetValueOrDefault();
            
        }
    }   

}
