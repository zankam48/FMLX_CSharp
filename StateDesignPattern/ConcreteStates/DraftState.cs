namespace DocumentManagement.State;
using DocumentManagement.StateInterface;
using DocumentManagement.Context;

public class Draft : IDocumentState
{
    public void Edit(Document document, string content)
    {
        // document.Content = content;
        Console.WriteLine("Document edited in Draft state. Content updated.");
    }

    public void Publish(Document document)
    {
        Console.WriteLine("Publishing from Draft state. Transitioning to Submitted state.");
        document.SetState(new Submitted());
    }

    public void Archive(Document document)
    {
        Console.WriteLine("Archiving document from Draft state.");
        document.SetState(new Archived());
    }

}