module Parser.Main

open System.Net
open System
open Newtonsoft.Json
open System.Collections.Generic

let loadContent (address: String) =
    use client = new WebClient()   
    client.DownloadString address

let capture (captureStart: String) (content: String) = 
    let startIdx = content.IndexOf(captureStart) + captureStart.Length
    let endIdx = content.IndexOf(";", startIdx)    
    content.Substring(startIdx, endIdx - startIdx)

let captureTreeData = capture @"var passiveSkillTreeData = "
let captureOptions content = 
    let json = capture @"var opts = " content
    json.Replace("passiveSkillTreeData: passiveSkillTreeData,", "")    

[<EntryPoint>]
let main args = 
    let path = @"https://www.pathofexile.com/passive-skill-tree"

    let content = loadContent path
    
    //let options =
    //    captureOptions content 
    //    |> Parser.OptionsExtractor.jsonToOptions 
    
    //printfn "%A" options.ascClasses.[Names.Marauder].classes

    let tree = 
        captureTreeData content
        |> SkillTreeExtractor.jsonToTreeData

    printfn "%A" tree.characterData.["0"].base_dex

    0

    