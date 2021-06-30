using System;
using System.Threading.Tasks;
using System.Linq;
using SantafeApi.Infraestrucutre.Data;
using SantafeApi.Services.Interfaces;
using System.Globalization;

namespace SantafeApi.Services
{
    public class QualificationService : IQualificationService
    {
        private readonly SantafeApiContext _dbContext;
        public QualificationService(SantafeApiContext dbContext)

        {
            _dbContext = dbContext;
        }
        public QualificationModel GetQualificationReport(int codCliente, DateTime start, DateTime end)
        {
            var codControleOs = GetAllControleOs(codCliente, start, end);
            Console.WriteLine($"codControleOs: {codControleOs.Length}");
            var vistorias = _dbContext.Vistorias.Where(vistoria => codControleOs.Contains(vistoria.CodControle));

            var qualificationModel = vistorias.AsEnumerable().Select(vistoria =>
            {
                var itensConformes = vistorias.Where(vistoriaConforme => vistoriaConforme.Conformidade == Conformidade.CONFORME && vistoriaConforme.CodItem == vistoria.CodItem).Count();
                var itensNaoConformes = vistorias.Where(vistoriaConforme => vistoriaConforme.Conformidade == Conformidade.NAO_CONFORME && vistoriaConforme.CodItem == vistoria.CodItem).Count();
                return new QualificationModel
                {
                    CodItem = vistoria.CodItem,
                    ItemName = vistoria.NomeItem,
                    Conforme = itensConformes,
                    NaoConforme = itensNaoConformes
                };
            }).First();


            return qualificationModel;
        }

        private int[] GetAllControleOs(int codCliente, DateTime start, DateTime end)
        {
            var clientOs = _dbContext.ControleOs.Where(x => x.CodCliente == codCliente).ToList();
            clientOs.ForEach(c => Console.WriteLine(c.DataVistoria));
            var allCodOs = clientOs.Where(cos =>
            {
                var dataVistoria = Convert.ToDateTime(cos.DataVistoria);
                return dataVistoria > start && dataVistoria < end;
            })
            .Select(cos => cos.Cod).ToArray();

            return allCodOs;
        }

        public static class Conformidade
        {
            public const string CONFORME = "C";
            public const string NAO_CONFORME = "NC";
        }
    }
}