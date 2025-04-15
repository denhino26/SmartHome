namespace SmartHomeApp.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Status { get; set; } = "off";

        public string UserId { get; set; }

        public void TurnOn() => Status = "on";
        public void TurnOff() => Status = "off";
        public void ToggleStatus() => Status = Status == "on" ? "off" : "on";
    }
}
