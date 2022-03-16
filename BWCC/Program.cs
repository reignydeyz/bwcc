// See https://aka.ms/new-console-template for more information
using BWCC.Business;

string article = File.ReadAllText(
    Path.Combine(Environment.CurrentDirectory, "Input\\Article.txt"));

var words = File.ReadLines(
    Path.Combine(Environment.CurrentDirectory, "Input\\Words.txt"));

var rowHelper = new RowHelper();
var row = 1;

var txtHelper = new TextHelper(article);

var lines = new List<string>();
foreach (var word in words)
{
    var sentenceIndeces = txtHelper.GetSentenceIndices(word);
    var text = $"{rowHelper.RowNumberToRowName(row)}. {word} {{{txtHelper.GetWordCount(word)}:{string.Join(',', sentenceIndeces)}}}";

    Console.WriteLine(text);
    lines.Add(text);

    row++;
}

File.WriteAllLinesAsync("Output.txt", lines);

Console.ReadLine();
