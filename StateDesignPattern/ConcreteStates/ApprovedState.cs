namespace DocumentManagement.State;
using DocumentManagement.StateInterface;
using DocumentManagement.Context;
public class Approved : IDocumentState
{
    public void Edit(Document document, string content)
    {
        Console.WriteLine("Cannot edit document. It is already approved and finalized.");
    }

    public void Publish(Document document)
    {
        Console.WriteLine("Document is already approved and finalized. No further publishing allowed.");
    }

    public void Archive(Document document)
    {
        Console.WriteLine("Cannot archive an approved document. It is already finalized.");
    }
}