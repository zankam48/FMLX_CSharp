namespace DocumentManagement.Context;
using DocumentManagement.State;
using DocumentManagement.StateInterface;
using DocumentManagement.Enum;

public class Document
{
    private IDocumentState? _state;
    public void SetState(IDocumentState state) => _state = state;
    public void Handle(ActionType action) => _state?.HandleAction(this, action);
}