using System;
using System.IO;

namespace TestNinja.Mocking
{
    public interface IStatementGenerator
    {
        string SaveStatement(int housekeeperId, string housekeeperName, DateTime statementDate);
    }
    public class StatementGenerator : IStatementGenerator
    {
        public string SaveStatement(int housekeeperId, string housekeeperName, DateTime statementDate)
        {
            var report = new HousekeeperStatementReport(housekeeperId, statementDate);

            if (!report.HasData)
                return string.Empty;

            report.CreateDocument();

            var filename = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                string.Format("Sandpiper Statement {0:yyyy-MM} {1}.pdf", statementDate, housekeeperName));

            report.ExportToPdf(filename);

            return filename;
        }
    }
}