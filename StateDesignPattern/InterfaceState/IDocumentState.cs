namespace DocumentManagement.StateInterface;
using DocumentManagement.Context;
public interface IDocumentState
{
    public void Edit(Document document, string content);
    public void Publish(Document document);
    public void Archive(Document document);
}