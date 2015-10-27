using System.Collections.Immutable;
using System.Composition;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using CleanCode.DiagnosticAnalizers;

namespace CleanCode.CodeFixes
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(CleanCodeCodeFixProvider)), Shared]
    public class CleanCodeCodeFixProvider : CodeFixProvider
    {
        private const string title = "Refactor method";

        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get { return ImmutableArray.Create(MethodMetricsAnalizer.DiagnosticId); }
        }

        public sealed override FixAllProvider GetFixAllProvider()
        {
            return WellKnownFixAllProviders.BatchFixer;
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {

        }


    }
}