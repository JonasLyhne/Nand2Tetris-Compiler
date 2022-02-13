// See https://aka.ms/new-console-template for more information

using CompilerProject.Data;
using CompilerProject.Tokenization;

TokenTable tokenTable = TokenTableLoader.LoadTokenTable();
Tokenizer tokenizer = new Tokenizer(tokenTable);

StartUp();

void StartUp()
{
    while (true)
    {
        Console.WriteLine("Please enter filepath:");
        var path = Console.ReadLine();
        if (File.Exists(path))
        {
            Console.WriteLine("You have entered the path of a single file..");
            Console.WriteLine("Press any key to continue.");
            if (path.Contains(".jack"))
            {
                ProcessSingleFile(path);
            }
            else
            {
                Console.WriteLine("Can only translate .jack files");
                continue;
            }
            Console.WriteLine("Translation done.");
            Console.ReadKey();
        }
        else if (Directory.Exists(path))
        {
            Console.WriteLine("You have entered the path of a directory..");
            Console.WriteLine("Will try to translate all .jack files.");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            ProcessDirectory(path);
            Console.WriteLine("Translation done.");
            Console.ReadKey();
        }
        else if (path == "cls")
        {
            Console.Clear();
        }
        else if (path == "exit")
        {
            break;
        }
        else
        {
            Console.WriteLine("'{0}' is an invalid path.", path);
        }
    }
}

void ProcessSingleFile(string path)
{
    var file = StreamIO.ReadFile(path);
    var tokens = tokenizer.Tokenize(file);
    path = path.Replace(".jack", ".xml");
    path = NextAvailableFilename(path);
    var xmlFile = StreamIO.WriteToXml(tokens);
    StreamIO.SaveXmlDocument(xmlFile, path);
}

void ProcessDirectory(string path)
{
    var entries = Directory.GetFiles(path).ToList();
    foreach (var entry in entries.Where(entry => entry.Contains(".jack")))
    {
        ProcessSingleFile(entry);
    }
}

const string numberPattern = " ({0})";

static string NextAvailableFilename(string path)
{
    // Short-cut if already available
    if (!File.Exists(path))
        return path;

    // If path has extension then insert the number pattern just before the extension and return next filename
    return Path.HasExtension(path) ? GetNextFilename(path.Insert(path.LastIndexOf(Path.GetExtension(path), StringComparison.Ordinal), numberPattern)) : GetNextFilename(path + numberPattern);

    // Otherwise just append the pattern to the path and return next filename
}

static string GetNextFilename(string pattern)
{
    string tmp = string.Format(pattern, 1);
    if (tmp == pattern)
        throw new ArgumentException("The pattern must include an index place-holder", nameof(pattern));

    if (!File.Exists(tmp))
        return tmp; // short-circuit if no matches

    int min = 1, max = 2; // min is inclusive, max is exclusive/untested

    while (File.Exists(string.Format(pattern, max)))
    {
        min = max;
        max *= 2;
    }

    while (max != min + 1)
    {
        var pivot = (max + min) / 2;
        if (File.Exists(string.Format(pattern, pivot)))
            min = pivot;
        else
            max = pivot;
    }

    return string.Format(pattern, max);
}

