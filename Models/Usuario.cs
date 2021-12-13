using System;
namespace api.Models{
    public class Usuario{
        private Guid _id ;
        public  Guid Id{
             get => _id;
             set => _id =  Guid.NewGuid() ;
         }

         private string _Email;
        public string Email
        {
            get => _Email;
            set => _Email = value?.Trim();
        }

        private int _senha;
        public String senha
        {
            get => _senha.ToString();
            set => _senha = value.Trim().GetHashCode();
        }

    }
}