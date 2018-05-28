module Parser.SkillTreeExtractor

open System.Collections.Generic
open Newtonsoft.Json

type ClassId = string

type CharacterDataJson = {
    base_str: int
    base_dex: int
    base_int: int
}

type RootJson = {
    out: int list       // Starting nodes

    // Unused
    //g: int          // Group number
    //o: int          // Orbit distance
    //oidx: int       // Orbit index
    //sa: int         // Strength
    //da: int         // Dexterity
    //ia: int         // Intelligence    
    //int: int list   // In nodes
}

type NodesJson = {
    id:int              // id
    dn: string          // Name
    isJewelSocket: bool
    isMultipleChoice: bool
    isMultipleChoiceOption: bool
    passivePointsGranted: int
    sd: string list       // Effects    
    out: int list       // Out nodes
    ``in``: int list    // In nodes

    // Unused
    //spc: string list    // Class id (if class node)
    //m: bool             // Mastery
    //icon: string        // icon
    //ks: bool            // Keystone
    //not: bool           // Notable
    //g: int              // Group number
    //o: int              // Orbit distance
    //oidx: int           // Orbit index
    //sa: int             // Strength
    //da: int             // Dexteriy
    //ia: int             // Intelligence

}

/// Classes indexes
type Classes = {
    StrClass: int
    DexClass: int
    IntClass: int

    StrDexClass: int
    StrIntClass: int
    DexIntClass: int

    StrDexIntClass: int
}

/// Attribute indexes
type CharacterAttributes = {
    Strength: int
    Dexterity: int
    Intelligence: int
}

/// Record for constan
type ConstantsJson = {
    classes: Classes
    characterAttributes: CharacterAttributes

    // Unused
    //PSSCentreInnerRadius: int
    //skillsPerOrbit: int list
    //orbitRadii: int list
}


type TreeDataJson = {
    characterData: Dictionary<ClassId, CharacterDataJson>
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

type Stat = 
    | PassivePoint of int
    | IncreasedMaximumLife of int
    | Damage of int

type Node = {
    Name: string
    Adjacents: int list
    Stats: Stat list
    isJewelSocket: bool
    isMultipleChoice: bool
    isMultipleChoiceOption: bool
}

type SkillTree = {
    StartingNodes: int list
    Tree: Dictionary<int, Node>
}


let jsonToTreeData json = 
    JsonConvert.DeserializeObject<TreeDataJson>(json)

