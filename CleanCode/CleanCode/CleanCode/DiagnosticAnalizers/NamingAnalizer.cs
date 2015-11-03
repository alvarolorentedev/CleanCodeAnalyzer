using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.DiagnosticAnalizers
{
    //[DiagnosticAnalyzer(LanguageNames.CSharp)]
    //public class NamingAnalizer : DiagnosticAnalyzer
    //{

        //public const string DiagnosticId = "CleanCode.Naming.";

        //const string Category = "Naming";

        //public const string ComplexityId = "Complexity";
        //static readonly LocalizableString ComplexityTitle = new LocalizableResourceString(nameof(Resources.ComplexityAnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        //static readonly LocalizableString ComplexityMessageFormat = new LocalizableResourceString(nameof(Resources.ComplexityAnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        //static readonly LocalizableString ComplexityDescription = new LocalizableResourceString(nameof(Resources.ComplexityAnalyzerDescription), Resources.ResourceManager, typeof(Resources));

        //static DiagnosticDescriptor ComplexityRule = new DiagnosticDescriptor(string.Format("{0}{1}", DiagnosticId, ComplexityId), ComplexityTitle, ComplexityMessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: ComplexityDescription);
        //public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(ComplexityRule); } }

        //public override void Initialize(AnalysisContext context)
        //{
        //    context.RegisterCodeBlockAction(Analyzeblock);
        //}

        //async void Analyzeblock(CodeBlockAnalysisContext obj)
        //{
        //    //var settings = SettingsHelper.GetSettings(obj.Options);
        //    var settings = new Settings();
        //    CheckComplexity(obj, functionMetric.CyclomaticComplexity, settings.MethodSettings);
        //}


        //protected void CheckComplexity(CodeBlockAnalysisContext obj, int cyclomaticComplexity, IMethodSettings methodSettings)
        //{
        //    if (cyclomaticComplexity < 10)
        //        return;

        //    var diagnostic = Diagnostic.Create(ComplexityRule, obj.CodeBlock.GetLocation(), new object[] { "Cyclomatic Complexity", 10 });
        //    obj.ReportDiagnostic(diagnostic);

        //}

    //}
}
