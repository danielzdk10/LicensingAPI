namespace AimbotLicensingAPI.Models
{
    public class License
    {
        public string LicenseKey { get; set; } = Guid.NewGuid().ToString();  // Gera uma chave única
        public string UserId { get; set; }  // ID do usuário
        public DateTime? ActivationDate { get; set; }  // Data de ativação
        public DateTime? ExpirationDate { get; set; }  // Data de expiração
        public int LicenseDuration { get; set; }  // Duração da licença (em dias)
    }
}
