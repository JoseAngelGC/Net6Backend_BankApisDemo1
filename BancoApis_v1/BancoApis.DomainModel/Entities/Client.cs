using BancoApis.DomainModel.Commons;

namespace BancoApis.DomainModel.Entities
{
    public class Client : AuditableBaseEntity
    {
        private int _age = 0;
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public int Age
        {
            get 
            { 
                if (_age == 0)
                {
                    _age = new DateTime(DateTime.Now.Subtract(BirthDay).Ticks).Year - 1;
                }

                return _age;
            }
        }
    }
}
