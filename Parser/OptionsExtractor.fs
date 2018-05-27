module Parser.OptionsExtractor

open System
open System.Collections.Generic
open Newtonsoft.Json

type AscendencyJson = { name: String; displayName: String }
type ClassJson = {name: String; classes: Dictionary<string, AscendencyJson>}
type OptionsJson = { ascClasses: Dictionary<string, ClassJson>; version: string }

let jsonToOptions json = 
    JsonConvert.DeserializeObject<OptionsJson>(json)