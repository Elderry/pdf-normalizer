using System.CommandLine;

using PDFHelper.Common;

namespace PDFHelper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Argument<ICollection<string>> argumentPath = new("path")
            {
                Arity = ArgumentArity.ZeroOrMore,
                Description = "Path(s) of PDF files, default to current directory if not provided."
            };

            Option<bool> dryOption = new("--dry")
            {
                Arity = ArgumentArity.ZeroOrOne,
                Description = "Whether to try fix PDF file violations."
            };

            RootCommand rootCommand = new()
            {
                Description = "Personal helper to normalize PDF files."
            };
            rootCommand.AddOption(dryOption);
            rootCommand.AddArgument(argumentPath);
            rootCommand.SetHandler(NormalizePDF, dryOption, argumentPath);
            _ = rootCommand.Invoke(args);
        }

        private static void NormalizePDF(bool dry, ICollection<string> paths)
        {
            PDFNormalizer normalizer = new(Utils.GetPDFPaths(paths), dry);
            normalizer.Normalize();
        }
    }
}
