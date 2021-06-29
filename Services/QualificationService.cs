using System;
using System.Threading.Tasks;
using SantafeApi.Services.Interfaces;

namespace SantafeApi.Services
{
    public class QualificationService : IQualificationService
    {
        public QualificationModel GetQualificationReport(int codCliente, int codControle, int codVistoria, int codItem, DateTime start, DateTime end)
        {
            var qualificationModel = new QualificationModel();

            return qualificationModel;
        }
    }
}