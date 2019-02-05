module Agents

open System
open System.Collections.Generic

type AgentMessage =
    | AddIfNoExists of id:Guid * userName:string
    | RemoveIfNoExists of id:Guid

let RegisterterUserConnection id name = ()
let DeregisterUserConnection id name = ()

type AgentOnlineUsers() =
    let agent = MailboxProcessor<AgentMessage>.Start(fun inbox ->
        let onlineUsers = Dictionary<Guid, string>()
        let rec loop() = async {
            let! msg = inbox.Receive()
            match msg with
            | AddIfNoExists(id, userName) ->
                let exists, _ = onlineUsers.TryGetValue(id)
                if not exists then
                    onlineUsers.Add(id, userName)
                    RegisterterUserConnection(id, userName) |> ignore
            | RemoveIfNoExists(id) ->
                let exists, userName = onlineUsers.TryGetValue(id)
                if exists then
                    onlineUsers.Remove(id) |> ignore
                    DeregisterUserConnection(id, userName) |> ignore
            return! loop() }
        loop())

