using System;
using System.Threading.Tasks;

namespace SantafeApi.Services.Interfaces
{
    public interface IQualificationService
    {
        QualificationModel GetQualificationReport(int codCliente, int codControle, int codVistoria, int codItem, DateTime start, DateTime end);
    }
}