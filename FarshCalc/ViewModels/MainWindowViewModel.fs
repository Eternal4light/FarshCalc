namespace FarshCalc.ViewModels

open System
open Reactive.Bindings

type MainWindowViewModel() =
    inherit ViewModelBase()

    let mutable operandX = new ReactiveProperty<string>()
    let mutable operandY = new ReactiveProperty<string>()
    let mutable result = new ReactiveProperty<string>("Результат")
    let calculateCommand = new ReactiveCommand<string>()

    let Summ(x, y) = x+y
    let Subtract(x, y) = x-y
    let Multiply(x, y) = x*y
    let Devide(x, y) = x/y
    let Exponent(x, y) = float x ** float y

    let ParseValues(operandX : string) (operandY : string) =
            match Int32.TryParse(operandX), Int32.TryParse(operandY) with
            | (true, parsedX), (true, parsedY) -> Some (parsedX, parsedY)
            | _ -> None

    let Calculate(param) =
        let parsed = ParseValues operandX.Value operandY.Value 
        match param with
        | "+" ->            Summ (fst parsed.Value, snd parsed.Value)
        | "-" ->        Subtract (fst parsed.Value, snd parsed.Value)
        | "*" ->        Multiply (fst parsed.Value, snd parsed.Value)
        | "/" ->          Devide (fst parsed.Value, snd parsed.Value)
        | "^" -> int <| Exponent (fst parsed.Value, snd parsed.Value)
        | _ -> 0

    let SetResult(param) = 
        result.Value <- Calculate(param).ToString()

    do 
        calculateCommand.Subscribe(fun param -> 
         // let param = string objParam
          SetResult(param)
        ) |> ignore

    member _.SummButtonName = "Сложить"
    member _.SubtractButtonName = "Вычесть"
    member _.MultiplyButtonName = "Умножить"
    member _.DevideButtonName = "Разделить"
    member _.ExponentButtonName = "Возвести в степень"
    member _.CalculateCommand = calculateCommand
    member _.OperandX = operandX
    member _.OperandY = operandY
    member _.Result = result