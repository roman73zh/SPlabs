using Lab1;
Console.WriteLine("Hello, World!");
LexemProc proc = new LexemProc();
proc.LoadFile("../../../input.txt");
proc.Process();
Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\nLexems:");
foreach(var lexem in proc.GetLexems()) 
{
    Console.WriteLine(lexem.ToString());
}
Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\nVariables:");
foreach (var variable in proc.GetVariables())
{
    Console.WriteLine(variable.ToString());
}
Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\nIdentifiers:");
foreach (var identifier in proc.GetIdentifiers())
{
    Console.WriteLine(identifier.ToString());
}
Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
