using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;


public class IndexModel : PageModel
{


    public List<Device> Devices { get; set; } = new();

    private readonly string connectionString = "Data Source=mssqlstud.fhict.local;Initial Catalog=dbi563236;User ID=dbi563236;Password=Zondag23!;Encrypt=False";
    public string SelectedUserName { get; set; }
    public void OnGet()
    {
        LoadDevices();

            SelectedUserName = "SelectedUser";
        
     
    }

    

    public IActionResult OnPost()
    {
        int deviceId = int.Parse(Request.Form["deviceId"]);
        string action = Request.Form["action"];

        if (action == "toggle")
        {
            ToggleDeviceStatus(deviceId);
        }

        return RedirectToPage();
    }

    private void LoadDevices()
    {
        Devices.Clear();

        using SqlConnection conn = new SqlConnection(connectionString);
        conn.Open();

        string query = "SELECT DeviceId, Name, Type, Status FROM Devices";
        using SqlCommand cmd = new SqlCommand(query, conn);
        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Devices.Add(new Device
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Type = reader.GetString(2),
                Status = reader.GetString(3)
            });
        }
    }

    private void ToggleDeviceStatus(int deviceId)
    {
        using SqlConnection conn = new SqlConnection(connectionString);
        conn.Open();

        string getStatusQuery = "SELECT Status FROM Devices WHERE DeviceId = @Id";
        string newStatus = "uit";

        using (SqlCommand getCmd = new SqlCommand(getStatusQuery, conn))
        {
            getCmd.Parameters.AddWithValue("@Id", deviceId);
            var currentStatus = (string)getCmd.ExecuteScalar();
            newStatus = currentStatus == "aan" ? "uit" : "aan";
        }

        string updateQuery = "UPDATE Devices SET Status = @Status WHERE DeviceId = @Id";
        using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
        {
            updateCmd.Parameters.AddWithValue("@Status", newStatus);
            updateCmd.Parameters.AddWithValue("@Id", deviceId);
            updateCmd.ExecuteNonQuery();
        }
    }




   
    


}

public class Device
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Status { get; set; }
}
