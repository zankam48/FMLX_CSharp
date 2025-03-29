namespace DocumentManagement.State;
using DocumentManagement.StateInterface;
using DocumentManagement.Context;

public class UnderReview : IDocumentState
{
    private static Random rnd = new Random();

    public void Edit(Document document, string content)
    {
        Console.WriteLine("Cannot edit document while Under Review.");
    }

    public void Publish(Document document)
    {
        int decision = rnd.Next(0, 3);
        if (decision == 0)
        {
            Console.WriteLine("Review requires further revisions. Staying in UnderReview state.");
        }
        else if (decision == 1)
        {
            Console.WriteLine("Document approved after Under Review.");
            document.SetState(new Approved());
        }
        else if (decision == 2)
        {
            Console.WriteLine("Document rejected after Under Review.");
            document.SetState(new Rejected());
        }
    }

    public void Archive(Document document)
    {
        Console.WriteLine("Archiving document from UnderReview state.");
        document.SetState(new Archived());
    }
}