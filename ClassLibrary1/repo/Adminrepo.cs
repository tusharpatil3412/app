using ClassLibrary.data;
using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClassLibrary.repo
{
   
    
        public class Adminrepo : IAdminrepo // Assuming IAdminRepo is your interface for admin operations
        {
            private readonly IDataAcc _db; // Assuming IDataAcc is your data access interface

            public Adminrepo(IDataAcc db)
            {
                _db = db;
            }

            public async Task<Admin> AddAdmin(Admin admin)
            {
                try
                {
                    string query = "INSERT INTO admin (username, password) OUTPUT INSERTED.id VALUES (@Username, @Password)";
                    var id= await _db.SaveDataAndGetId(query, new { Username = admin.Username, Password = admin.Password });
                    return await GetByIdAdmin(id);
                }
                catch (Exception)
                {
                    return admin;
                }
            }

            public async Task<bool> DeleteAdmin(int id)
            {
                try
                {
                    string query = "DELETE FROM admin WHERE id = @Id";
                    await _db.SaveData(query, new { Id = id });
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public async Task<Admin> GetByIdAdmin(int id)
            {
                try
                {
                    string query = "SELECT * FROM admin WHERE id = @Id";
                    var result = await _db.GetData<Admin, dynamic>(query, new { Id = id });
                    return result.FirstOrDefault();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        public async Task<Admin> GetByIdusername(string username)
        {
            try
            {
                string query = "SELECT * FROM admin WHERE username = @Username";
                var result = await _db.GetData<Admin, dynamic>(query, new { Username = username });
                return result.FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Admin>> GetAllAdmins()
            {
                try
                {
                    string query = "SELECT * FROM admin";
                    var admins = await _db.GetData<Admin, dynamic>(query, new { });
                    return admins;
                }
                catch (Exception)
                {
                    return null;
                }
            }

            public async Task<bool> UpdateAdmin(Admin admin)
            {
                try
                {
                    string query = "UPDATE admin SET username = @Username, password = @Password WHERE id = @Id";
                    await _db.SaveData(query, admin); // Assuming SaveData can handle the entire object mapping
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

        }

        // Assuming Admin model class looks something like this:

    }

