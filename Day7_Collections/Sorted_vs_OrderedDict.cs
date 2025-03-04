var sortedDict = new SortedDictionary<string, int>();
sortedDict.Add("Three", 3);
sortedDict.Add("One", 1);
sortedDict.Add("Two", 2);

foreach (var kv in sortedDict)
{
    Console.WriteLine($"{kv.Key}: {kv.Value}");
}
// Output will be sorted by key:
// One: 1
// Three: 3
// Two: 2

var orderedDict = new OrderedDictionary();
orderedDict.Add("One", 1);
orderedDict.Add("Three", 3);
orderedDict.Add("Two", 2);
orderedDict.Add("Five", 5);
orderedDict.Add("Four", 4);

foreach (DictionaryEntry entry in orderedDict)
{
    Console.WriteLine($"{entry.Key}: {entry.Value}");
}
// Output sesuai dengan order dari codenya
// One: 1
// Three: 3
// Two: 2
// Five: 5
// Four: 4