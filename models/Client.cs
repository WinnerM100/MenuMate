
using MenuMate.Models.DTOs;

namespace MenuMate.Models
{
    public class Client
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Prenume { get; set; }

        public override string ToString()
        {
            return $"Client[Id:'{Id}', Name: '{Name}', Prenume: '{Prenume}']";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            if(obj is not Client)
                return base.Equals(obj);
            
            Client clientDTO = obj as Client;

            if(clientDTO.Id != null && !clientDTO.Id.Equals(Guid.Empty) && clientDTO.Id != this.Id)
                return false;
            if(!clientDTO.Name.Equals(this.Name, StringComparison.OrdinalIgnoreCase))
                return false;
            if(!clientDTO.Prenume.Equals(this.Prenume, StringComparison.OrdinalIgnoreCase))
                return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}