
using System;
namespace api.Models{
    public class TimeLine{
       
         private Guid _id ;
        public  Guid Id{
             get => _id;
             set => _id =  Guid.NewGuid() ;
         }         
         private string _titulo;
         public string Titulo
        {
            get => _titulo;
            set => _titulo = value?.Trim();
        }
         private string _desc;
        public string Desc
        {
            get => _desc;
            set => _desc = value?.Trim();
        }
         private string _lado;
        public string lado
        {
            get => _lado;
            set => _lado = value?.Trim().ToLower();
        }
        // Multi-tenant segregation key (e.g., domain)
        private string _tenant;
        public string Tenant
        {
            get => _tenant;
            set => _tenant = value?.Trim().ToLower();
        }
     
    }
}