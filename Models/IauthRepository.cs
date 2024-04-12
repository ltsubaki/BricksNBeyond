using IntexQueensSlay.Data;
using IntexQueensSlay.Models;
namespace IntexQueensSlay.Models
{
    public interface IauthRepository
    {
        public IQueryable<AspNetUsers> AspNetUserss { get; }
        //object AspNetUsers { get; set; }
    }
}
