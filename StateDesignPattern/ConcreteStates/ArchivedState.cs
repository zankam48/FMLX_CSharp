namespace DocumentManagement.State;
using DocumentManagement.StateInterface;
using DocumentManagement.Context;

public class Archived : IDocumentState
{
    public void Edit(Document document, string content)
    {
        Console.WriteLine("Cannot edit an archived document.");
    }

    public void Publish(Document document)
    {
        Console.WriteLine("Cannot publish an archived document.");
    }

    public void Archive(Document document)
    {
        Console.WriteLine("Document is already archived.");
    }
}