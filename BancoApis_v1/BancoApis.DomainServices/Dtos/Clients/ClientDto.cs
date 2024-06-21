namespace BancoApis.DomainServices.Dtos.Clients
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
    }
}
