namespace DocumentManagement.Context;
using DocumentManagement.State;
using DocumentManagement.StateInterface;

public class Document
{
    private IDocumentState? _state;

    public Document()
    {
        _state = new Draft();
    }
    public void SetState(IDocumentState state) => _state = state;
    public string GetStateName()
    {
        return _state.GetType().Name;
    }

    public void Edit(string content)
    {
        _state.Edit(this, content);
    }

    public void Publish()
    {
        _state.Publish(this);
    }

    public void Archive()
    {
        _state.Archive(this);
    }
}