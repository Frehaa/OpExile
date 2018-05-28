module Parser.Main

open System.Net
open System
open System.IO

let downloadContent (address: String) =
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

let loadJson args = 
    let root = AppDomain.CurrentDomain.BaseDirectory + @"data\"
    let treePath = root + "treeData.json"
    let optionsPath = root + "options.json"

    if Directory.Exists root |> not then 
        Directory.CreateDirectory root |> ignore
            
    let loadAndSave address = 
        printfn "Updating tree from %s" address
        
        use treeFile = File.CreateText treePath
        use optionsFile = File.CreateText optionsPath

        let content = downloadContent address
        let treeData = captureTreeData content
        let options = captureOptions content

        treeFile.Write(treeData)
        optionsFile.Write(options)

        printfn "%s" "Updating finished"
        (treeData, options)

    if Array.length args >= 2 && args.[0] = "-u" then
        loadAndSave args.[1]
    elif not (File.Exists treePath) || not (File.Exists optionsPath) then
        loadAndSave @"https://www.pathofexile.com/passive-skill-tree"
    else 
        printfn "Loading tree"
        let treeData = File.ReadAllText(treePath)
        let options = File.ReadAllText(optionsPath)

        (treeData, options)



[<EntryPoint>]
let main args =     

    //printfn "%s" AppDomain.CurrentDomain.BaseDirectory  
    let (treeDataJson, optionsJson) = loadJson args 
    let treeData = SkillTreeExtractor.jsonToTreeData treeDataJson
    //let options = OptionsExtractor.jsonToOptions optionsJson

    //OptionsExtractor.printVersion options

    printfn "%A" treeData.characterData.[Names.Scion]
    printfn "%A" treeData.root
    printfn "%A" treeData.constants
    

    for n in treeData.root.out do
        printfn "%i" n



    0

    