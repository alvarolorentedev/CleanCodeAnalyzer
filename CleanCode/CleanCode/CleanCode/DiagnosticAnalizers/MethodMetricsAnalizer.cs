using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using ArchiMetrics.Analysis;
using CleanCode;
using CleanCode.Helpers.Configuration;
using System;

namespace CleanCode.DiagnosticAnalizers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class MethodMetricsAnalizer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "CleanCode.Method.";

        const string Category = "Metrics";

        public const string ComplexityId = "Complexity";
        static readonly LocalizableString ComplexityTitle = new LocalizableResourceString(nameof(Resources.ComplexityAnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        static readonly LocalizableString ComplexityMessageFormat = new LocalizableResourceString(nameof(Resources.ComplexityAnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        static readonly LocalizableString ComplexityDescription = new LocalizableResourceString(nameof(Resources.ComplexityAnalyzerDescription), Resources.ResourceManager, typeof(Resources));

        public const string ParametersId = "Parameters";
        static readonly LocalizableString ParametersTitle = new LocalizableResourceString(nameof(Resources.ParametersAnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        static readonly LocalizableString ParametersMessageFormat = new LocalizableResourceString(nameof(Resources.ParametersAnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        static readonly LocalizableString ParametersDescription = new LocalizableResourceString(nameof(Resources.ParametersAnalyzerDescription), Resources.ResourceManager, typeof(Resources));

        public const string LinesId = "Lines";
        static readonly LocalizableString LinesTitle = new LocalizableResourceString(nameof(Resources.LinesAnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        static readonly LocalizableString LinesMessageFormat = new LocalizableResourceString(nameof(Resources.LinesAnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        static readonly LocalizableString LinesDescription = new LocalizableResourceString(nameof(Resources.LinesAnalyzerDescription), Resources.ResourceManager, typeof(Resources));

        static DiagnosticDescriptor ComplexityRule = new DiagnosticDescriptor(string.Format("{0}{1}", DiagnosticId, ComplexityId), ComplexityTitle, ComplexityMessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: ComplexityDescription);
        static DiagnosticDescriptor ParametersRule = new DiagnosticDescriptor(string.Format("{0}{1}", DiagnosticId, ParametersId), ParametersTitle, ParametersMessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: ParametersDescription);
        static DiagnosticDescriptor LinesRule = new DiagnosticDescriptor(string.Format("{0}{1}", DiagnosticId, LinesId), LinesTitle, LinesMessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: LinesDescription);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(ComplexityRule, ParametersRule, LinesRule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterCodeBlockAction(Analyzeblock);
        }

        async void Analyzeblock(CodeBlockAnalysisContext obj)
        {
            var metricsCalculator = new CodeMetricsCalculator();
            var metrics = await metricsCalculator.Calculate(new List<SyntaxTree> { CSharpSyntaxTree.ParseText(obj.CodeBlock.ToString()) });
            var functionMetric = metrics.ElementAt(0).TypeMetrics.ElementAt(0).MemberMetrics.ElementAt(0);
            //var settings = SettingsHelper.GetSettings(obj.Options);
            var settings = new Settings();
            CheckComplexity(obj, functionMetric.CyclomaticComplexity, settings.MethodSettings);
            CheckNumberOfParameters(obj, functionMetric.NumberOfParameters, settings.MethodSettings);
            CheckLinesOfCode(obj, functionMetric.LinesOfCode, settings.MethodSettings);
        }


        protected void CheckComplexity(CodeBlockAnalysisContext obj, int cyclomaticComplexity, IMethodSettings methodSettings)
        {
            if (cyclomaticComplexity < 10)
                return;

            var diagnostic = Diagnostic.Create(ComplexityRule, obj.CodeBlock.GetLocation(), new object[]{ "Cyclomatic Complexity", 10});
            obj.ReportDiagnostic(diagnostic);

        }

        protected void CheckNumberOfParameters(CodeBlockAnalysisContext obj, int numberOfParameters, IMethodSettings methodSettings)
        {
            if (numberOfParameters < 4)
                return;

            var diagnostic = Diagnostic.Create(ParametersRule, obj.CodeBlock.GetLocation(), new object[] { "Number Of Parameters", 3 });
            obj.ReportDiagnostic(diagnostic);
        }

        protected void CheckLinesOfCode(CodeBlockAnalysisContext obj, int linesOfCode, IMethodSettings methodSettings)
        {
            if (linesOfCode < 20)
                return;

            var diagnostic = Diagnostic.Create(LinesRule, obj.CodeBlock.GetLocation(), new object[] { "Lines Of Code", 20 });
            obj.ReportDiagnostic(diagnostic);

        }
    }
}
