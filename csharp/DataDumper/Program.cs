using System;
using System.IO;
using System.Linq;
using Lumina;
using Lumina.Excel;
using Lumina.Excel.Sheets;

namespace DataDumper
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: DataDumper <sqpack_dir>");
                return;
            }

            var gameData = new GameData(args[0], new LuminaOptions { DefaultExcelLanguage = Lumina.Data.Language.English });

            Console.WriteLine("--- Search in Addon ---");
            var addonSheet = gameData.GetExcelSheet<Addon>();
            if (addonSheet != null)
            {
                foreach (var row in addonSheet)
                {
                    var text = row.Text.ExtractText();
                    if (text.Contains("Practice") || text.Contains("One Player per Job"))
                    {
                        Console.WriteLine($"[Addon] Id: {row.RowId}, Text: {text}");
                    }
                }
            }

            Console.WriteLine("--- Search in LogMessage ---");
            var logSheet = gameData.GetExcelSheet<LogMessage>();
            if (logSheet != null)
            {
                foreach (var row in logSheet)
                {
                    var text = row.Text.ExtractText();
                    if (text.Contains("Practice") || text.Contains("One Player per Job"))
                    {
                        Console.WriteLine($"[LogMessage] Id: {row.RowId}, Text: {text}");
                    }
                }
            }

            // PartyContentTextData 확인 (스키마가 맞는지 불확실하지만 시도)
            Console.WriteLine("--- Search in PartyContentTextData ---");
            try {
                // 동적으로 시트 로드 시도 (타입 정보 없이)
                var sheet = gameData.Excel.GetSheet("PartyContentTextData");
                if (sheet != null) {
                    foreach (var page in sheet.DataPages) {
                        foreach (var row in page.File.DataPages[0].Rows) {
                            // 단순 덤프 시도 (구조를 모르므로 한계 있음)
                            // Lumina의 Low-level API 필요할 수 있음
                        }
                    }
                }
            } catch (Exception e) {
                Console.WriteLine($"Skipped PartyContentTextData: {e.Message}");
            }
        }
    }
}
