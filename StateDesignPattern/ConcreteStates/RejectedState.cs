namespace DocumentManagement.State;
using DocumentManagement.StateInterface;
using DocumentManagement.Context;

public class Rejected : IDocumentState
{
    public void Edit(Document document, string content)
    {
        Console.WriteLine("Editing a rejected document. Transitioning back to Draft state for revisions.");
        // document.Content = content;
        document.SetState(new Draft());
    }

    public void Publish(Document document)
    {
        Console.WriteLine("Cannot publish a rejected document directly. Please edit it to move back to Draft state.");
    }

    public void Archive(Document document)
    {
        Console.WriteLine("Archiving document from Rejected state.");
        document.SetState(new Archived());
    }
}