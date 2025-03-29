// using System;

// namespace DocumentManagementNoStatePattern
// {
//     public enum DocumentState
//     {
//         Draft,
//         Submitted,
//         UnderReview,
//         Approved,
//         Rejected,
//         Archived
//     }

//     public class Document
//     {
//         public int ID { get; set; }
//         public string Content { get; set; }
//         public DocumentState State { get; set; }
//         private Random rnd;

//         public Document(int id)
//         {
//             ID = id;
//             Content = "";
//             State = DocumentState.Draft;
//             rnd = new Random();
//             Console.WriteLine("Document created in Draft state.");
//         }

//         public void Edit(string content)
//         {
//             switch (State)
//             {
//                 case DocumentState.Draft:
//                     Content = content;
//                     Console.WriteLine("Document edited in Draft state. Content updated.");
//                     break;
//                 case DocumentState.Submitted:
//                     Console.WriteLine("Cannot edit document in Submitted state. Editing is only allowed in Draft or after Rejection.");
//                     break;
//                 case DocumentState.UnderReview:
//                     Console.WriteLine("Cannot edit document while Under Review.");
//                     break;
//                 case DocumentState.Approved:
//                     Console.WriteLine("Cannot edit document. It is already approved and finalized.");
//                     break;
//                 case DocumentState.Rejected:
//                     Content = content;
//                     Console.WriteLine("Editing a rejected document. Moving back to Draft state for revisions.");
//                     State = DocumentState.Draft;
//                     break;
//                 case DocumentState.Archived:
//                     Console.WriteLine("Cannot edit an archived document.");
//                     break;
//                 default:
//                     Console.WriteLine("Invalid state.");
//                     break;
//             }
//         }

//         public void Publish()
//         {
//             switch (State)
//             {
//                 case DocumentState.Draft:
//                     Console.WriteLine("Publishing from Draft state. Transitioning to Submitted state.");
//                     State = DocumentState.Submitted;
//                     break;
//                 case DocumentState.Submitted:
//                     Console.WriteLine("Publishing from Submitted state. Transitioning to UnderReview state.");
//                     State = DocumentState.UnderReview;
//                     break;
//                 case DocumentState.UnderReview:
//                     int decision = rnd.Next(0, 3);
//                     if (decision == 0)
//                     {
//                         Console.WriteLine("Review requires further revisions. Remaining in UnderReview state.");
//                     }
//                     else if (decision == 1)
//                     {
//                         Console.WriteLine("Document approved after UnderReview.");
//                         State = DocumentState.Approved;
//                     }
//                     else if (decision == 2)
//                     {
//                         Console.WriteLine("Document rejected after UnderReview.");
//                         State = DocumentState.Rejected;
//                     }
//                     break;
//                 case DocumentState.Approved:
//                     Console.WriteLine("Document is already approved and finalized. No further publishing allowed.");
//                     break;
//                 case DocumentState.Rejected:
//                     Console.WriteLine("Cannot publish a rejected document directly. Please edit to move it back to Draft.");
//                     break;
//                 case DocumentState.Archived:
//                     Console.WriteLine("Cannot publish an archived document.");
//                     break;
//                 default:
//                     Console.WriteLine("Invalid state.");
//                     break;
//             }
//         }

//         public void Archive()
//         {
//             switch (State)
//             {
//                 case DocumentState.Draft:
//                 case DocumentState.Submitted:
//                 case DocumentState.UnderReview:
//                 case DocumentState.Rejected:
//                     Console.WriteLine($"Archiving document from {State} state.");
//                     State = DocumentState.Archived;
//                     break;
//                 case DocumentState.Approved:
//                     Console.WriteLine("Cannot archive an approved document. It is already finalized.");
//                     break;
//                 case DocumentState.Archived:
//                     Console.WriteLine("Document is already archived.");
//                     break;
//                 default:
//                     Console.WriteLine("Invalid state.");
//                     break;
//             }
//         }

//         public string GetStateName()
//         {
//             return State.ToString();
//         }
//     }

//     public class Program
//     {
//         public static void Main(string[] args)
//         {
//             Document doc = new Document(1);

//             doc.Edit("Initial content in Draft.");

//             doc.Publish();

//             doc.Edit("Attempting to edit in Submitted state.");

//             doc.Publish();

//             doc.Publish();

//             if (doc.GetStateName() == "Rejected")
//             {
//                 doc.Edit("Revising rejected document, moving back to Draft.");
//             }
//             else
//             {
//                 Console.WriteLine("For demonstration, forcing document into Rejected state.");
//                 doc.State = DocumentState.Rejected;
//                 doc.Edit("Revising rejected document, moving back to Draft.");
//             }

//             doc.Publish();

//             doc.Archive();

//             Console.WriteLine("Final state of document: " + doc.GetStateName());
//         }
//     }
// }
