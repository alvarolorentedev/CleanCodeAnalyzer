using System.Collections.Immutable;
using System.Linq;
using ArchiMetrics.Analysis.Metrics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Soft.Hati.CleanCode.Extension;

namespace Soft.Hati.CleanCode.Metrics
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class CodeMetricsAnalyzer : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(AnalizerDescriptor.Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            // TODO: Consider registering other actions that act on syntax instead of or in addition to symbols
            context.RegisterSymbolAction(MethodAnalizer.AnalyzeMethod, SymbolKind.Method);
        }
        
    }

    class AnalizerDescriptor
    {
        public const string DiagnosticId = "Clean Code Metrics";

        // You can change these strings in the Resources.resx file. If you do not want your analyzer to be localize-able, you can use regular strings for Title and MessageFormat.
        internal static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.AnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        internal static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        internal static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.AnalyzerDescription), Resources.ResourceManager, typeof(Resources));
        internal const string Category = "Complexity Metrics";

        internal static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);
    }

    class MethodAnalizer
    {
        public static void AnalyzeMethod(SymbolAnalysisContext context)
        {
            var namedTypeSymbol = (IMethodSymbol)context.Symbol;
            
            var syntax = context.Symbol.DeclaringSyntaxReferences.First().SyntaxTree.ToString(); //Careful, partial methods might burn you

            var calculator = new SyntaxMetricsCalculator();
            var result = calculator.Calculate(syntax);
        }
    }
}
