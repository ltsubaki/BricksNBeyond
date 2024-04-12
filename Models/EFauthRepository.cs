using IntexQueensSlay.Data;
using System;

namespace IntexQueensSlay.Models
{
    
    public class EFauthRepository: IauthRepository
    {
        private ApplicationDbContext _appcontext;
        public EFauthRepository(ApplicationDbContext temp)
        {
            _appcontext = temp;
        }
        public IQueryable<AspNetUsers> AspNetUserss => _appcontext.AspNetUserss;



    }

}
