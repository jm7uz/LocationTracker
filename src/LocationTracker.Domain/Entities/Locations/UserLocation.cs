using LocationTracker.Domain.Commons;

namespace LocationTracker.Domain.Entities.Locations;

public class UserLocation : Auditable<long>
{
    public long UserId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    //Is Verifed 1 soat ichidagi statusga % chiqarish uchun belgilandi. Foydalanuvchi hisobotga qo'shishdan oldin harakatlarini tahlil qilish uchun.
    public bool IsVerified { get; set; }
}
