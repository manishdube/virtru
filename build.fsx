// --------------------------------------------------------------------------------------
// FAKE build script
// --------------------------------------------------------------------------------------

#r @"packages/FAKE/tools/FakeLib.dll"
open Fake
open Fake.Git
open Fake.AssemblyInfoFile
open Fake.ReleaseNotesHelper
open Fake.UserInputHelper
open System
open System.IO

// --------------------------------------------------------------------------------------
// START TODO: Provide project-specific details below
// --------------------------------------------------------------------------------------

// Information about the project are used
//  - for version and project name in generated AssemblyInfo file
//  - by the generated NuGet package
//  - to run tests and to publish documentation on GitHub gh-pages
//  - for documentation, you also need to edit info in "docs/tools/generate.fsx"

// The name of the project
// (used by attributes in AssemblyInfo, name of a NuGet package and directory in 'src')
let project = "sit"

// Short summary of the project
// (used as description in AssemblyInfo and as a short summary for NuGet package)
let summary = "Starter kit for UI automation with canopy"

// Longer description of the project
// (used as a description for NuGet package; line breaks are automatically cleaned up)
let description = "Starter kit for UI automation with canopy"

// List of author names (for NuGet package)
let authors = [ "Chris Holt" ]

// Tags for your project (for NuGet package)
let tags = "canopy uiautomation f# fsharp"

// File system information
let solutionFile  = "sit.sln"

// Pattern specifying assemblies to be tested using NUnit
let exe = "bin/sit"

// --------------------------------------------------------------------------------------
// END TODO: The rest of the file includes standard build steps
// --------------------------------------------------------------------------------------

// Helper active pattern for project types
let (|Fsproj|Csproj|Vbproj|) (projFileName:string) =
    match projFileName with
    | f when f.EndsWith("fsproj") -> Fsproj
    | f when f.EndsWith("csproj") -> Csproj
    | f when f.EndsWith("vbproj") -> Vbproj
    | _                           -> failwith (sprintf "Project file %s not supported. Unknown project type." projFileName)

// Generate assembly info files with the right version & up-to-date information
Target "AssemblyInfo" (fun _ ->
    let getAssemblyInfoAttributes projectName =
        [ Attribute.Title (projectName)
          Attribute.Product project
          Attribute.Description summary
          Attribute.Version "1.0"
          Attribute.FileVersion "1.0" ]

    let getProjectDetails projectPath =
        let projectName = System.IO.Path.GetFileNameWithoutExtension(projectPath)
        ( projectPath,
          projectName,
          System.IO.Path.GetDirectoryName(projectPath),
          (getAssemblyInfoAttributes projectName)
        )

    !! "**/*.??proj"
    |> Seq.map getProjectDetails
    |> Seq.iter (fun (projFileName, projectName, folderName, attributes) ->
        match projFileName with
        | Fsproj -> CreateFSharpAssemblyInfo (folderName @@ "AssemblyInfo.fs") attributes
        | Csproj -> CreateCSharpAssemblyInfo ((folderName @@ "Properties") @@ "AssemblyInfo.cs") attributes
        | Vbproj -> CreateVisualBasicAssemblyInfo ((folderName @@ "My Project") @@ "AssemblyInfo.vb") attributes
        )
)

// Copies binaries from default VS location to expected bin folder
// But keeps a subdirectory structure for each project in the
// src folder to support multiple project outputs
Target "CopyBinaries" (fun _ ->
    !! "**/*.??proj"
    |>  Seq.map (fun f -> ((System.IO.Path.GetDirectoryName f) @@ "bin/Debug", "bin" @@ (System.IO.Path.GetFileNameWithoutExtension f)))
    |>  Seq.iter (fun (fromDir, toDir) -> CopyDir toDir fromDir (fun _ -> true))
)

// --------------------------------------------------------------------------------------
// Rename driver for macOS or Linux

Target "RenameDrivers" (fun _ ->
    match Environment.OSVersion.Platform with
    |  PlatformID.Unix ->
         if Environment.OSVersion.VersionString.Contains("Unix 4.") //linux kernel 4.x
         then Fake.FileHelper.Rename "bin/sit/chromedriver" "bin/sit/chromedriver_linux64"
         //assume macOS (the enum for macOS actually returns Unix so we have to cheese it)
         else Fake.FileHelper.Rename "bin/sit/chromedriver" "bin/sit/chromedriver_macOS"
    |  _ -> ()
)

// --------------------------------------------------------------------------------------
// Clean build results

Target "Clean" (fun _ ->
    CleanDirs ["bin"; "temp"]
)

// --------------------------------------------------------------------------------------
// Build library & test project

Target "Build" (fun _ ->
    !! solutionFile
    |> MSBuildDebug "" "Rebuild"
    |> ignore
)

// --------------------------------------------------------------------------------------
// Run the unit tests using test runner

let run args =
  let result =
    ExecProcess (fun info ->
                 info.FileName <- (exe @@ "sit.exe")
                 info.Arguments <- args
                 ) (System.TimeSpan.FromMinutes 5.)

  if result <> 0 then failwith "Failed result from unit tests"

Target "Dev" (fun _ -> run """--browser Chrome --tag All --testtype UnderDevelopment""")

Target "All" (fun _ -> run """--browser Chrome --tag All --testtype All""")

// --------------------------------------------------------------------------------------
// Run all targets by default. Invoke 'build <Target>' to override

Target "BuildStuff" DoNothing

"Clean"
  ==> "AssemblyInfo"
  ==> "Build"
  ==> "CopyBinaries"
  ==> "RenameDrivers"
  ==> "BuildStuff"

"BuildStuff"
  ==> "All"

"BuildStuff"
  ==> "Dev"

RunTargetOrDefault "Dev"
