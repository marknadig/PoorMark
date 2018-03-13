namespace PoorMark.Classes
{
    public class FormatterService
    {
        public class Options
        {
            public string inputString;
            public bool reFormat = true;
            public string indent = "\t";
            public int spacesPerTab = 4;
            public int maxLineWidth = 999;
            public int statementBreaks = 2;
            public int clauseBreaks = 1;
            public bool expandCommaLists = true;
            public bool trailingCommas = false;
            public bool spaceAfterExpandedComma = false;
            public bool expandBooleanExpressions = false;
            public bool expandCaseStatements = true;
            public bool expandBetweenConditions = true;
            public bool breakJoinOnSections = true;
            public bool uppercaseKeywords = false;
            public bool coloring = true;
            public bool keywordStandardization = true; 
            public bool useParseErrorPlaceholder = false;
            public bool obfuscate = false;
            public bool randomizeColor = false;
            public bool randomizeLineLengths = false;
            public bool randomizeKeywordCase = false;
            public bool preserveComments = false;
            public bool enableKeywordSubstitution = false;
            public bool expandInLists = true;
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
