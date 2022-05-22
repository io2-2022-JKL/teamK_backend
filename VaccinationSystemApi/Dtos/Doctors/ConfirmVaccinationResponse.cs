using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystemApi.Dtos.Doctors
{
    public record ConfirmVaccinationResponse
    {
        public bool canCertify { get; init; }
    }
}
