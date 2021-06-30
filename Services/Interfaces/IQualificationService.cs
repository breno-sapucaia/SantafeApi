using System;
using System.Threading.Tasks;

namespace SantafeApi.Services.Interfaces
{
    public interface IQualificationService
    {
        QualificationModel GetQualificationReport(int codCliente, DateTime start, DateTime end);
    }
}