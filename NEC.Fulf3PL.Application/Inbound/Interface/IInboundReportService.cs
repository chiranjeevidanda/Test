namespace NEC.Fulf3PL.Application.Inbound.Interface
{
    public interface IInboundReportService
    {
        Task SaveMappedCompareReport(string fileName, Stream inputFileStream, TextWriter outputWriter);
    }
}