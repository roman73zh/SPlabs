using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Consts
{
    internal class Constants
    {
        public static readonly string[] emptySymbols = { " ", "\n", "\0", "\t", "\r" };

        public static readonly string[] keywords = {
            "if",
            "else",
            "public",
            "private",
            "protected",
            "class", 
            "for", 
            "while", 
            "return", 
            "continue", 
            "break",
            "abstract",
            "internal",
            "new",
            "override",
            "readonly",
            "try",
            "catch",
            "void",
            "case",
            "const",
            "do",
            "using",
            "null",
            "switch",
            "volatile",
            "this",
            "false",
            "true",
            "foreach",
            "operator",
            "throw",
            "goto",
            "string",
            "virtual"
        };

        public static readonly Dictionary<string, (int, string)> dataTypes = new Dictionary<string, (int, string)>() {
            { "int", (0, "int32") },
            { "uint", (1, "uint32") },
            { "long", (2, "int64") },
            { "ulong", (3, "uint64") },
            { "short", (4, "int16") },
            { "ushort", (5, "uint16") },
            { "byte", (6, "uint8") },
            { "sbyte", (7, "int8") },
            { "string", (8, "string")},
        };

        public static readonly string[] delimiters = { ";", ".", ",", "(", ")", "{", "}", "[", "]" };

        public static readonly Dictionary<string, (int, string)> operators = new Dictionary<string, (int, string)>() {
            {"=", (0, "assign_operation")},
            {"+", (1, "sum_operation")},
            {"-", (2, "sub_operation")},
            {"*", (3, "mul_operation")},
            {"/", (4, "div_operation")},
            {"%", (5, "mod_operation")},
            {"++", (6, "inc_operation")},
            {"--", (7, "dec_operation")},
            {"+=", (8, "add_amount_operation")},
            {"-=", (9, "sub_amount_operation")},
            {"*=", (10, "mul_amount_operation")},
            {"/=", (11, "div_amount_operation")},
            {"%=", (12, "mod_amount_operation")},
            {"==", (13, "equal_comparison")},
            {"!=", (14, "not_equal_comparison")},
            {">", (15, "more_comparison")},
            {"<", (16, "less_comparison")},
            {">=", (17, "more_equ_comparison")},
            {"<=", (18, "less_equ_comparison")},
            {"!", (19, "not_comparison")}
        };
    }
}
