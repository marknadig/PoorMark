namespace PoorMark.Classes
{
    public class FormatterService
    {    
        public class Options
        {
            public string inputString { get; set; }
            public bool? reFormat { get; set; } = true;
            public string indent { get; set; } = "\t";
            public int? spacesPerTab { get; set; } = 4;
            public int? maxLineWidth { get; set; } = 999;
            public int? statementBreaks { get; set; } = 2;
            public int? clauseBreaks { get; set; } = 1;
            public bool? expandCommaLists { get; set; } = true;
            public bool? trailingCommas { get; set; } = false;
            public bool? spaceAfterExpandedComma { get; set; } = false;
            public bool? expandBooleanExpressions { get; set; } = false;
            public bool? expandCaseStatements { get; set; } = true;
            public bool? expandBetweenConditions { get; set; } = true;
            public bool? breakJoinOnSections { get; set; } = true;
            public bool? uppercaseKeywords { get; set; } = false;
            public bool? coloring { get; set; } = true;
            public bool? keywordStandardization { get; set; } = true;
            public bool? useParseErrorPlaceholder { get; set; } = false;
            public bool? obfuscate { get; set; } = false;
            public bool? randomizeColor { get; set; } = false;
            public bool? randomizeLineLengths { get; set; } = false;
            public bool? randomizeKeywordCase { get; set; } = false;
            public bool? preserveComments { get; set; } = false;
            public bool? enableKeywordSubstitution { get; set; } = false;
            public bool? expandInLists { get; set; } = true;
            public string format { get; set; } = "html";
        }

        public string FormatTSql(string inputString)
        {
            return FormatTSqlWithOptions( new Options() { inputString = inputString });
        }

        public string FormatTSqlWithOptions(Options options)
        {
            PoorMansTSqlFormatterRedux.Interfaces.ISqlTreeFormatter formatter = null;
            if (options.reFormat.Value)
            {
                formatter = new PoorMansTSqlFormatterRedux.Formatters.TSqlStandardFormatter(new PoorMansTSqlFormatterRedux.Formatters.TSqlStandardFormatterOptions
                {
                    IndentString = options.indent,
                    SpacesPerTab = options.spacesPerTab.Value,
                    MaxLineWidth = options.maxLineWidth.Value,
                    NewStatementLineBreaks = options.statementBreaks.Value,
                    NewClauseLineBreaks = options.clauseBreaks.Value,
                    ExpandCommaLists = options.expandCommaLists.Value,
                    TrailingCommas = options.trailingCommas.Value,
                    SpaceAfterExpandedComma = options.spaceAfterExpandedComma.Value,
                    ExpandBooleanExpressions = options.expandBooleanExpressions.Value,
                    ExpandCaseStatements = options.expandCaseStatements.Value,
                    ExpandBetweenConditions = options.expandBetweenConditions.Value,
                    BreakJoinOnSections = options.breakJoinOnSections.Value,
                    UppercaseKeywords = options.uppercaseKeywords.Value,
                    HTMLColoring = options.coloring.Value,
                    KeywordStandardization = options.keywordStandardization.Value,
                    ExpandInLists = options.expandInLists.Value
                });

            }
            else if (options.obfuscate.Value)
                formatter = new PoorMansTSqlFormatterRedux.Formatters.TSqlObfuscatingFormatter(
                    options.randomizeKeywordCase.Value,
                    options.randomizeColor.Value,
                    options.randomizeLineLengths.Value,
                    options.preserveComments.Value,
                    options.enableKeywordSubstitution.Value
                    );
            else
                formatter = new PoorMansTSqlFormatterRedux.Formatters.TSqlIdentityFormatter(options.coloring.Value);

            if (options.useParseErrorPlaceholder.Value)
                formatter.ErrorOutputPrefix = "{PARSEERRORPLACEHOLDER}";

            PoorMansTSqlFormatterRedux.Interfaces.ISqlTreeFormatter wrapper = formatter;
            if (string.Equals("html", options.format, System.StringComparison.OrdinalIgnoreCase))
            {
                wrapper = new PoorMansTSqlFormatterRedux.Formatters.HtmlPageWrapper(formatter);
            }
            PoorMansTSqlFormatterRedux.SqlFormattingManager fullFormatter = new PoorMansTSqlFormatterRedux.SqlFormattingManager(wrapper);
            return fullFormatter.Format(options.inputString);
        }
    }
}
