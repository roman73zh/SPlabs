using Lab1;
Console.WriteLine("Hello, World!");
DirectoryInfo dirInfo = new DirectoryInfo("../../../Examples/");
FileInfo[] files = dirInfo.GetFiles("*.*");
LexemProc proc = new LexemProc();
foreach (FileInfo file in files)
{
    proc.LoadFile(file.ToString());
    Console.WriteLine("#############################################################################\nProcessing " + file.Name);
    proc.Process();
    Console.WriteLine("\n++++++++++++++++++++++++++++++++++++\nLexems:");
    foreach (var lexem in proc.GetLexems())
    {
        Console.WriteLine(lexem.ToString());
    }
    Console.WriteLine("++++++++++++++++++++++++++++++++++++++\nVariables:");
    foreach (var variable in proc.GetVariables())
    {
        Console.WriteLine(variable.ToString());
    }
    Console.WriteLine("++++++++++++++++++++++++++++++++++++++\nIdentifiers:");
    foreach (var identifier in proc.GetIdentifiers())
    {
        Console.WriteLine(identifier.ToString());
    }
    Console.WriteLine("#############################################################################");
}

