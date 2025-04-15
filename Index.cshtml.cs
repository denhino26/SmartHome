using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using SmartHomeApp.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartHomeApp.Pages
{
    public class SelectUserModel : PageModel
    {
        public List<User> Users { get; set; } = new List<User>();

        [BindProperty]
        [Required(ErrorMessage = "Selecteer een gebruiker.")]
        public int SelectedUserId { get; set; }


        [BindProperty]
        public string NewUserName { get; set; }

        private readonly string connectionString = "Data Source=mssqlstud.fhict.local;Initial Catalog=dbi563236;User ID=dbi563236;Password=Zondag23!;Encrypt=False";

        public void OnGet()
        {
            LoadUsers();
        }

        public IActionResult OnPost()
        {
           
                TempData["SelectedUser"] = SelectedUserId;
           

            ModelState.AddModelError("", "Selecteer eerst een gebruiker.");
            LoadUsers(); // Herlaad voor formulier opnieuw
            return RedirectToPage("/Overview");
        }

        public IActionResult OnPostAddUser()
        {
            if (!string.IsNullOrWhiteSpace(NewUserName))
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var cmd = new SqlCommand("INSERT INTO Users (Name) VALUES (@name)", conn);
                    cmd.Parameters.AddWithValue("@name", NewUserName);
                    cmd.ExecuteNonQuery();
                }
                LoadUsers();
            }

            return RedirectToPage();
        }

        private void LoadUsers()
        {
            Users.Clear();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var command = new SqlCommand("SELECT UserId,Name FROM Users", conn);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Users.Add(new User
                    {
                        UserId = Convert.ToInt32(reader["UserId"]),
                        Name = reader["Name"].ToString()
                    });
                }
            }
        }
    }
}

