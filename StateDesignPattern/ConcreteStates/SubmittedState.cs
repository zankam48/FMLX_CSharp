namespace DocumentManagement.State;
using DocumentManagement.StateInterface;
using DocumentManagement.Context;

public class Submitted : IDocumentState
{
    public void Edit(Document document, string content)
    {
        Console.WriteLine("Cannot edit document in Submitted state. Editing is allowed only in Draft or after Rejection.");
    }

    public void Publish(Document document)
    {
        Console.WriteLine("Publishing from Submitted state. Transitioning to UnderReview state.");
        document.SetState(new UnderReview());
    }

    public void Archive(Document document)
    {
        Console.WriteLine("Archiving document from Submitted state.");
        document.SetState(new Archived());
    }
}