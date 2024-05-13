module Hw7.Task1

open FSharp.Data

let getAllLinks =
    HtmlDocument.Parse
    >> _.Descendants([ "a" ])
    >> Seq.choose _.TryGetAttribute("href")
    >> Seq.map _.Value()
    >> Seq.filter (_.StartsWith("http://"))

let crawl link =
    async {
        let! req = Http.AsyncRequestString link

        return!
            req
            |> getAllLinks
            |> Seq.map (fun link ->
                async {
                    let! content = Http.AsyncRequestString link
                    return link, content.Length
                })
            |> Async.Parallel
    }
