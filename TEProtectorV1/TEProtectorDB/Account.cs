using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEProtectorV1.TEProtectorDB
{
    public class Account
    { 
        [Key]
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(20)]
        [Required]
        public string Password { get; set; }
        [MaxLength(30)]
        public string FullName { get; set; }
        [MaxLength(15)]
        public string Number { get; set; }
        public bool Sex { get; set; }
        [Required]
        public string Question { get; set; }
        [Required]
        public string Answer { get; set; }
        public int timeusing { get; set; }
        public int timeshutdown { get; set; }
        public int shortbreaktime { get; set; }
        public int shortbreaklock { get; set; }
        public int longbreaktimes { get; set; }
        public int longbreaklock { get; set; }
        public bool taskmanager { get; set; }
        public bool enablepass { get; set; }
        public bool enablenoti { get; set; }
        public int notitime { get; set; }
        public bool defaultnoti { get; set; }
        public bool customnoti { get; set; }
        public bool manhinhmo { get; set; }
        public bool manhinhdonsac { get; set; }
        public string mamau { get; set; } = "";
        public bool manhinhhinhanh { get; set; }
        public string linkanh { get; set; }
        public bool locksounddefault { get; set; }
        public string namelocksounddefault { get; set; }
        public bool locksoundcustom { get; set; }
        public string linksoundcustom { get; set; }
        public bool notificationsounddefault { get; set; }
        public string namenotificationsounddefault { get; set; }
        public bool notificationsoundcustom { get; set; }
        public string linknotificationsoundcustom { get; set; }

    }
    public class AccountTEDBContext : DbContext
    {
        public AccountTEDBContext() : base("AccountTEConnectionString")
        {

        }
        public DbSet<Account> Accounts { get; set; }
    }
}
