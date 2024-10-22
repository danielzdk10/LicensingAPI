using AimbotLicensingAPI.Models;
using System.Collections.Generic;

namespace AimbotLicensingAPI.Services
{
    public class LicenseService
    {
        // Lista para armazenar as licenças em memória
        private static List<License> _licenses = new List<License>();

        // Criar uma nova licença
        public License CreateLicense(string userId, int durationInDays)
        {
            var newLicense = new License
            {
                UserId = userId,
                LicenseDuration = durationInDays
            };

            _licenses.Add(newLicense);
            return newLicense;
        }

        // Buscar licença pela chave
        public License GetLicenseByKey(string licenseKey)
        {
            return _licenses.Find(l => l.LicenseKey == licenseKey);
        }

        // Atualizar a licença
        public void UpdateLicense(License license)
        {
            var existingLicense = GetLicenseByKey(license.LicenseKey);
            if (existingLicense != null)
            {
                existingLicense.ActivationDate = license.ActivationDate;
                existingLicense.ExpirationDate = license.ExpirationDate;
            }
        }
    }
}
