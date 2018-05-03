module etconfig
open FSharp.Configuration

type ETConfig = YamlConfig<"config.yaml">
let config = ETConfig()
config.Load ".\config.yaml"

let mutable uri = config.uri.ToString()


let mutable username = config.userId.ToString()
