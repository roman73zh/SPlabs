using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Lab1.Consts;
using Lab1.Models;

namespace Lab1
{
    internal class LexemProc
    {
        private string filename = "";
        private char currentChar;
        private string buffer = "";
        private int index = 0;
        private StringReader? reader;
        private List<Lexem> lexems = new List<Lexem>();
        private List<Variable> variables = new List<Variable>();
        private List<Identifier> identifiers = new List<Identifier>();

        private enum ProcState : int
        {
            Idle = 0,
            ReadingInt = 1,
            ReadingIdentifier = 2,
            ReadingLiteral = 3,
            Delimiter = 4,
            Error = 5,
            Finished = 6,
            
        }

        private ProcState state;

        public LexemProc()
        {

        }

        public void LoadFile(string filename)
        {
            this.filename = filename;
        }

        public void Process()
        {
            state = ProcState.Idle;
            using (reader = new StringReader(File.ReadAllText(filename) + "\n"))
            {
                while (state != ProcState.Finished)
                {
                    if (state == ProcState.Idle)
                    {
                        if (reader.Peek() == -1)
                        {
                            state = ProcState.Finished;
                            break;
                        }
                        if (skipEmpty())
                        {
                            continue;
                        }
                        else if (char.IsDigit(currentChar))
                        {
                            clearBuffers();
                            buffer += currentChar;
                            state = ProcState.ReadingInt;
                        }
                        else if (char.IsLetter(currentChar))
                        {
                            clearBuffers();
                            buffer += currentChar;
                            state = ProcState.ReadingIdentifier;
                        } 
                        else if (currentChar == '"')
                        {
                            clearBuffers();
                            state = ProcState.ReadingLiteral;
                        }
                        else
                        {
                            clearBuffers();
                            buffer += currentChar;
                            state = ProcState.Delimiter;
                        }

                    }
                    else if (state == ProcState.ReadingInt)
                    {
                        getNextChar();
                        if (char.IsDigit(currentChar))
                        {
                            buffer += currentChar;
                        }
                        else
                        {
                            addLexem(Lexem.Types.Constant, int.Parse(buffer), $"integer, value = {buffer}");
                            state = ProcState.Idle;
                        }
                    }
                    else if (state == ProcState.ReadingIdentifier)
                    {
                        getNextChar();
                        if (char.IsLetterOrDigit(currentChar))
                        {
                            buffer += currentChar;
                        }
                        else
                        {
                            int keywordRes = Array.FindIndex(Consts.Constants.keywords, l => l.Equals(buffer));
                            bool dataTypeRes = Consts.Constants.dataTypes.ContainsKey(buffer);
                            if (keywordRes != -1)
                            {
                                addLexem(Lexem.Types.Keyword, keywordRes, buffer);
                            }
                            else if (dataTypeRes)
                            {
                                addLexem(Lexem.Types.DataType, Consts.Constants.dataTypes[buffer].Item1, Consts.Constants.dataTypes[buffer].Item2);
                            }
                            else
                            {
                                if (variables.Any(v => v.name.Equals(buffer)))
                                {
                                    addLexem(Lexem.Types.Variable, variables.FindIndex(v => v.name == buffer), $"variable <{buffer}>");
                                }
                                else if (identifiers.Any(i => i.name.Equals(buffer)))
                                {
                                    addLexem(Lexem.Types.Identifier, variables.FindIndex(i => i.name == buffer), $"identifier <{buffer}>");
                                }
                                else
                                {
                                    var variableType = lexems.Last();
                                    if (variableType == null || variableType.type != Lexem.Types.DataType)
                                    {
                                        identifiers.Add(new Identifier(identifiers.Count, buffer));
                                        addLexem(Lexem.Types.Identifier, identifiers.Count - 1, $"identifier <{buffer}>");
                                    }
                                    else
                                    {
                                        variables.Add(new Variable(variables.Count, variableType.desc, buffer));
                                        addLexem(Lexem.Types.Variable, variables.Count - 1, $"<{variableType.desc}> variable <{buffer}>");
                                    }
                                }
                            }
                            state = ProcState.Idle;
                        }
                    }
                    else if (state == ProcState.Delimiter)
                    {
                        getNextChar();
                        int delimitersRes = Array.FindIndex(Consts.Constants.delimiters, l => l.Equals(buffer));
                        bool operatorsRes = Consts.Constants.operators.ContainsKey(buffer);
                        if (delimitersRes != -1)
                        {
                            addLexem(Lexem.Types.Delimeter, delimitersRes, buffer);
                            state = ProcState.Idle;
                        }
                        else if (operatorsRes)
                        {
                            if (Consts.Constants.operators.ContainsKey(buffer + char.ToString(currentChar)))
                            {
                                addLexem(Lexem.Types.Operation, Consts.Constants.operators[buffer + char.ToString(currentChar)].Item1,
                                    Consts.Constants.operators[buffer + char.ToString(currentChar)].Item2);
                                state = ProcState.Idle;
                                getNextChar();
                            }
                            else
                            {
                                addLexem(Lexem.Types.Operation, Consts.Constants.operators[buffer].Item1,
                                    Consts.Constants.operators[buffer].Item2);
                                state = ProcState.Idle;
                            }
                        }
                        else
                        {
                            addLexem(Lexem.Types.ParsingError, -1, $"Error at {index}: Could not parse {buffer}!");
                            state = ProcState.Error;
                        }
                    }
                    else if (state == ProcState.ReadingLiteral)
                    {
                        getNextChar();
                        if (currentChar != '"')
                        {
                            buffer += currentChar;
                        }
                        else
                        {
                            addLexem(Lexem.Types.Literal, 0, buffer);
                            state = ProcState.Idle;
                            getNextChar();
                        }
                    }
                    else if (state == ProcState.Error)
                    {
                        break;
                    }
                }
            }
        }
        private void getNextChar()
        {
            if (reader == null)
                return;
            currentChar = (char)reader.Read();
            index++;
        }

        private bool skipEmpty()
        {
            if (Array.FindIndex(Consts.Constants.emptySymbols, l => l.Equals(Char.ToString(currentChar))) != -1)
            {
                getNextChar();
                return true;
            }
            return false;
        }

        private void clearBuffers()
        {
            buffer = "";
        }

        private void addLexem(Lexem.Types type, int val, string desc)
        {
            lexems.Add(new Lexem(type, val, desc));
        }

        public List<Lexem> GetLexems()
        {
            return lexems;
        }
        public List<Variable> GetVariables()
        {
            return variables;
        }
        public List<Identifier> GetIdentifiers()
        {
            return identifiers;
        }

    }
}
