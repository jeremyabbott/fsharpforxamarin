module GitHub.Data

open FSharp.Data

let baseUrl = "https://api.github.com/gists"

type Gists = JsonProvider<"https://api.github.com/gists">
type Gist = Gists.Root

let getGists () =
    Gists.Load(baseUrl)

let getGistsAsync () =
    Gists.AsyncLoad(sprintf "https://api.github.com/gists")