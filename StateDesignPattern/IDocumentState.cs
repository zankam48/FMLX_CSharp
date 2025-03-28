namespace DocumentManagement.StateInterface;
using DocumentManagement.Context;
using DocumentManagement.Enum;
public interface IDocumentState
{
    public void HandleAction(Document document, ActionType action);
}