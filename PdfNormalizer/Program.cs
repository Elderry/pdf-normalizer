using System.Collections.Generic;
using System.CommandLine;

using PdfNormalizer;
using PdfNormalizer.Common;

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

rootCommand.SetHandler((dry, paths) =>
{
    PDFNormalizer normalizer = new(Utils.GetPDFPaths(paths), dry);
    normalizer.Normalize();
}, dryOption, argumentPath);

rootCommand.Invoke(args);
