using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassLibrary.Models;

namespace ClassLibrary.repo
{
    public interface IAdminrepo
    {
        Task<bool> AddAdmin(Admin admin);
        Task<bool> DeleteAdmin(int id);
        Task<Admin> GetByIdAdmin(int id);

        Task<Admin> GetByIdusername(string username);
        Task<IEnumerable<Admin>> GetAllAdmins();
        Task<bool> UpdateAdmin(Admin admin);
        
    }
    }



