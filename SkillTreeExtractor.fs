open System.Net
open System

let loadContent (address: String) =
    use client = new WebClient()   
    client.DownloadString address

let capture (captureStart: String) (content: String) = 
    let startIdx = content.IndexOf(captureStart) + captureStart.Length
    let endIdx = content.IndexOf(";", startIdx)    
    content.Substring(startIdx, endIdx - startIdx)

let captureTreeData = capture @"var passiveSkillTreeData = "
let captureOptions = capture @"var opts = "   

[<EntryPoint>]
let main args = 
    let path = @"https://www.pathofexile.com/passive-skill-tree"

    let content = loadContent path
    
    captureOptions content
    |> printfn "%s"

    0

    