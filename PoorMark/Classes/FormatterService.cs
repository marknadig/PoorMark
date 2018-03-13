namespace PoorMark.Classes
{
    public class FormatterService
    {
        public class Options
        {
            public string inputString { get; set; } 
            public bool reFormat { get; set; } = true;
            public string indent { get; set; } = "\t";
            public int spacesPerTab { get; set; } = 4;
            public int maxLineWidth { get; set; } = 999;
            public int statementBreaks { get; set; } = 2;
            public int clauseBreaks { get; set; } = 1;
            public bool expandCommaLists { get; set; } = true;
            public bool trailingCommas { get; set; } = false;
            public bool spaceAfterExpandedComma { get; set; } = false;
            public bool expandBooleanExpressions { get; set; } = false;
            public bool expandCaseStatements { get; set; } = true;
            public bool expandBetweenConditions { get; set; } = true;
            public bool breakJoinOnSections { get; set; } = true;
            public bool uppercaseKeywords { get; set; } = false;
            public bool coloring { get; set; } = true;
            public bool keywordStandardization { get; set; } = true; 
            public bool useParseErrorPlaceholder { get; set; } = false;
            public bool obfuscate { get; set; } = false;
            public bool randomizeColor { get; set; } = false;
            public bool randomizeLineLengths { get; set; } = false;
            public bool randomizeKeywordCase { get; set; } = false;
            public bool preserveComments { get; set; } = false;
            public bool enableKeywordSubstitution { get; set; } = false;
            public bool expandInLists { get; set; } = true;
        }

        public class Options2
        {
            public string inputString { get; set; }
            public bool? reFormat { get; set; } = true;
            //public string indent { get; set; } = "\t";
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
        }

        public string FormatTSql(string inputString)
        {
            return FormatTSqlWithOptions( new Options() { inputString = inputString });
        }

        public string FormatTSqlWithOptions(Options options)
        {
            PoorMansTSqlFormatterRedux.Interfaces.ISqlTreeFormatter formatter = null;
            if (options.reFormat)
            {
                formatter = new PoorMansTSqlFormatterRedux.Formatters.TSqlStandardFormatter(new PoorMansTSqlFormatterRedux.Formatters.TSqlStandardFormatterOptions
                {
                    IndentString = options.indent,
                    SpacesPerTab = options.spacesPerTab,
                    MaxLineWidth = options.maxLineWidth,
                    NewStatementLineBreaks = options.statementBreaks,
                    NewClauseLineBreaks = options.clauseBreaks,
                    ExpandCommaLists = options.expandCommaLists,
                    TrailingCommas = options.trailingCommas,
                    SpaceAfterExpandedComma = options.spaceAfterExpandedComma,
                    ExpandBooleanExpressions = options.expandBooleanExpressions,
                    ExpandCaseStatements = options.expandCaseStatements,
                    ExpandBetweenConditions = options.expandBetweenConditions,
                    BreakJoinOnSections = options.breakJoinOnSections,
                    UppercaseKeywords = options.uppercaseKeywords,
                    HTMLColoring = options.coloring,
                    KeywordStandardization = options.keywordStandardization,
                    ExpandInLists = options.expandInLists
                });

            }
            else if (options.obfuscate)
                formatter = new PoorMansTSqlFormatterRedux.Formatters.TSqlObfuscatingFormatter(
                    options.randomizeKeywordCase,
                    options.randomizeColor,
                    options.randomizeLineLengths,
                    options.preserveComments,
                    options.enableKeywordSubstitution
                    );
            else
                formatter = new PoorMansTSqlFormatterRedux.Formatters.TSqlIdentityFormatter(options.coloring);

            if (options.useParseErrorPlaceholder)
                formatter.ErrorOutputPrefix = "{PARSEERRORPLACEHOLDER}";

            PoorMansTSqlFormatterRedux.SqlFormattingManager fullFormatter = new PoorMansTSqlFormatterRedux.SqlFormattingManager(new PoorMansTSqlFormatterRedux.Formatters.HtmlPageWrapper(formatter));
            return fullFormatter.Format(options.inputString);
        }
    }
}
