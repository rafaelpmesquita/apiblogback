using System;
namespace api.Models
{
    public class NavBar
    {
        private Guid _id;
        public Guid Id
        {
            get => _id;
            set => _id = Guid.NewGuid();
        }

         private string _texto;
        public string Texto
        {
            get => _texto;
            set => _texto = value?.Trim();
        }
        
         private string _rota;
        public string Rota
        {
            get => _rota;
            set => _rota = value?.Trim();
        }
        

    }
}
