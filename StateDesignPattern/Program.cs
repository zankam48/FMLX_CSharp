using DocumentManagement.State;
using DocumentManagement.Context;

public class Program
{
    public static void Main(string[] args)
    {
        Document doc = new Document();

        doc.Edit("Initial content in Draft.");

        doc.Publish();

        doc.Edit("Attempting to edit in Submitted state.");

        doc.Publish();

        doc.Publish();

        if (doc.GetStateName() == "Rejected")
        {
            doc.Edit("Revising rejected document, moving back to Draft.");
        }
        else
        {
            Console.WriteLine("For demonstration, forcing document into Rejected state.");
            // doc.State = DocumentState.Rejected;
            doc.Edit("Revising rejected document, moving back to Draft.");
        }

        doc.Publish();

        doc.Archive();

        Console.WriteLine("Final state of document: " + doc.GetStateName());
    }
}
