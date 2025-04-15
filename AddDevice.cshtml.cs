using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartHomeApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

public class AddDeviceModel : PageModel
{
    private readonly IConfiguration _configuration;

    public AddDeviceModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [BindProperty]
    public Device Device { get; set; }

    public IActionResult OnPost()
    {
        string connectionString = "Data Source=mssqlstud.fhict.local;Initial Catalog=dbi563236;User ID=dbi563236;Password=Zondag23!;Encrypt=False";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "INSERT INTO Devices (Name, Type, Status, UserId) VALUES (@Name, @Type, @Status, @UserId)";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                
                cmd.Parameters.AddWithValue("@Name", Device.Name);
                cmd.Parameters.AddWithValue("@Type", Device.Type);
                cmd.Parameters.AddWithValue("@Status", "uit");
                cmd.Parameters.AddWithValue("@UserId",1); 


                cmd.ExecuteNonQuery();
            }
        }

        return RedirectToPage("Overview");
    }
}
