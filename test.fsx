//open System
//open System.IO

//let rnd = System.Random()
////let zeroOrOne = System.Random().Next(0, 1).ToString()
////let twoThroughSix = System.Random().Next(2, 6).ToString()
////let twoThroughSeven = System.Random().Next(2, 7).ToString()

//let a = rnd.Next(2,7).ToString()      
//let b = rnd.Next(2,7).ToString() 

////let readAllLines filePath = File.ReadLines("C:\Selenium\Automation_framework\BCBSA_Canopy_CI\sit\features\MyBlue_DMIP_Notification.feature")


//let ParseFile = 
//    let ParseFile = File.ReadAllLines("C:\\Selenium\\Automation_framework\\BCBSA_Canopy_CI\\sit\\features\\MyBlue_DMIP_Notification.feature")
    
//    let ParseFileForErrors = 
//        ParseFile
//        |> Seq.filter(fun x -> if x.Contains("_warning_") then false else true)
    
//    let ParseFileForWarnings = 
//        ParseFile
//        |> Seq.filter(fun x -> if x.Contains("_warning_") then true else false)

//    let myFileSet = 
//        let mySubFileSet = Seq.append ParseFileForErrors ParseFileForWarnings    
//        let FileSummary = "Number of Entries : " + ParseFile.Length.ToString() + " Number of Errors : " + (ParseFileForErrors |> Seq.length).ToString()
//        Seq.append ( FileSummary |> Seq.singleton) mySubFileSet
    
//    File.WriteAllLines("C:\\Selenium", myFileSet)        

//ParseFile
//printfn "Done"

//let printChar ch =
//      match ch with
//      | Digit -> printfn "%A is a Digit" ch
//      | Letter -> printfn "%A is a Letter" ch
//      | Whitespace -> printfn "%A is a Whitespace" ch
//      | _ -> printfn "%c is something else" ch

//    // print a list
//['a'; 'b'; '1'; ' '; '-'; 'c'] |> List.iter printChar

//let inOrder = [1; 2]
//              |> List.append <| [3; 4]

////let mutable ddItems1 = [] 
////  |> List.append ( element "#BcbsaFep_VisionExam" |> elementsWithin ("option"))  



