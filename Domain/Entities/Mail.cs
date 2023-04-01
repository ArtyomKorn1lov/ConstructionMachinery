using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Mail
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }

        public void CopyFrom(Mail mail)
        {
            Created = mail.Created;
            Name = mail.Name;
            Email = mail.Email; 
            Phone = mail.Phone;
            Description = mail.Description;
        }
    }
}
