// Learn more about F# at http://fsharp.org

open System

// let buildMessage (nome : string) : string =
//     if nome.Length > 5 then
//         "ola " + nome + "seu nome é longo"
//     else
//          "ola " + nome + "seu nome é curto"

// [<EntryPoint>]
// let main argv =
//     printfn "Digite seu nome"
//     let  nome : string = Console.ReadLine()
  
//     printf "%s\n" (buildMessage nome) 
//     0 // return an integer exit code

type Person ={
    name: string
    age: int
}


               

let OldestPerson people : Person =
    people |> List.maxBy( fun p -> p.age)

let Youngger people =
    people |> List.minBy( fun p -> p.age)

//curryng implicito
let getPageAbleContent pagenumber (people : Person List)   = 
    let mutable maxpage = people.Length / 3
    if people.Length % 3 <> 0 then
        maxpage <- maxpage + 1
    
    if pagenumber > maxpage then
        failwith "pagina nao encontrada"

    people |> List.sortBy(fun pp -> pp.name)
           |> List.skip((pagenumber - 1) * 3 )
           |> List.truncate(3)

//curryng explicito
// let pagesgetlist people =
//     let pagesQauntity   pagenumber =
//         people |> List.sortBy(fun pp -> pp.name)
//            |> List.skip((pagenumber - 1) * 3 )
//            |> List.truncate(3)
//     pagesQauntity


[<EntryPoint>]
let main argv =
    let mutable nome = ""
    printf "digite 'sair' para sair \n"
    let names = [ 
        while nome <> "sair" do  
            printf "digite um nome \n"
            nome <- Console.ReadLine()
            if nome <> "sair" then
                printf "digite a idade \n"
                let age = int(Console.ReadLine())
                yield {name = nome; age = age}          
               
    ]

    printf "voce tem %d amigos \n" names.Length
   

    names |> List.map( fun n -> "olá " + n.name)
          |> List.iter(fun wel -> printf "%s \n" wel)

    printf "digite o termo de busca por nome \n"
    let searcharg = Console.ReadLine()

    printf "Resultados \n"
    names |> List.where(fun person ->  person.name.Contains(searcharg))
          |> List.iter(fun n -> printf "%s \n" n.name) 

    
    printf "===========================\n"
    printf "Sumario\n"

    let oldestP = names |> OldestPerson

    let younngerP = names |> Youngger
    

    printf "seu amigo mais velho é %s, idade é %d \n" oldestP.name oldestP.age
    printf "seu amigo mais novo é %s, idade é %d \n" younngerP.name younngerP.age
    printf "a media de idade é %d" ((names |> List.sumBy(fun s -> s.age)) / names.Length)
    printf "===========================\n"


    let mutable pageNumber = 1
    while pageNumber <> 0 do 
        printf "digite a pagina desejada \n"
        pageNumber <- int(Console.ReadLine())
        //curring implicito
        try
            getPageAbleContent  pageNumber names |> List.iter(fun p -> printf "nome: %s idade: %d \n" p.name p.age)
        with
            | Failure message -> printf "houve um erro %s \n" message
        // curring explicito 
        // ((pagesgetlist names) pageNumber ) |> List.iter(fun p -> printf "nome: %s idade: %d \n" p.name p.age)
      
    0

    
