module Parser.SkillTreeExtractor

open System
open System.Collections.Generic
open Newtonsoft.Json


type CharacterDataJson = {
    base_str: int
    base_dex: int
    base_int: int
}

type RootJson = {
    g: int
    o: int
    oidx: int
    sa: int
    da: int
    ia: int
    out: int list
    int: int list
}

type NodesJson = {
    id:int
    icon: string 
    ks: bool
    not: bool
    dn: string 
    m: bool
    isJewelSocket: bool
    isMultipleChoice: bool
    isMultipleChoiceOption: bool
    passivePointsGranted: int
    spc: string list
    sd: string list
    g: int
    o: int
    oidx: int
    sa: int
    da: int
    ia: int
    out: int list
    ``in``: int list
}

type ConstantsJson = {
    classes: Dictionary<string, int>
    characterAttributes: Dictionary<string, int>

    // Unused
    PSSCentreInnerRadius: int
    skillsPerOrbit: int list
    orbitRadii: int list
}


type TreeDataJson = {
    characterData: Dictionary<string, CharacterDataJson>
    root: RootJson
    nodes: NodesJson
    constants: ConstantsJson
    // Unused
    //groups: Object
    //extraImages: Object
    //min_x: int
    //min_y: int
    //max_x: int
    //max_y: int
    //assets: Object
    //imageRoot: string
    //skillSprites: Object
    //imageZoomLevels: float list
}


let jsonToTreeData json = 
    JsonConvert.DeserializeObject<TreeDataJson>(json)