using System;
namespace api.Models{
    public class Especialidades{
        private Guid _id ;
        public  Guid Id{
             get => _id;
             set => _id =  Guid.NewGuid() ;
         }

         private string _lang;
        public string Lang
        {
            get => _lang;
            set => _lang = value?.Trim().ToUpper();
        }

        private string _img;
        public string Img
        {
            get => _img;
            set => _img = value?.Trim();
        }

         private string _desc;
        public string Desc
        {
            get => _desc;
            set => _desc = value?.Trim();
        }
    }
}