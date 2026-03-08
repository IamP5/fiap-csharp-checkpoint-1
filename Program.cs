using System.Globalization;

CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

const string appTitle = "Calculadora (Adição / Subtração / Multiplicação / Divisão)";

while (true)
{
    Console.Clear();
    Console.WriteLine(appTitle);
    Console.WriteLine();
    Console.WriteLine("Escolha uma opção:");
    Console.WriteLine("1-Adição");
    Console.WriteLine("2-Subtração");
    Console.WriteLine("3-Multiplicação");
    Console.WriteLine("4-Divisão");
    Console.WriteLine("5-Sair");
    Console.WriteLine();
    Console.Write("Opção: ");

    var optionInput = Console.ReadLine();

    if (!int.TryParse(optionInput, out var option))
    {
        ShowMessage("Opção inválida. Digite um número de 1 a 5.");
        continue;
    }

    if (option == 5)
    {
        Console.WriteLine("Encerrando... até a próxima!");
        break;
    }

    if (option is < 1 or > 4)
    {
        ShowMessage("Opção inválida. Digite um número de 1 a 5.");
        continue;
    }

    var firstNumber = ReadNumber("Digite o primeiro número: ");
    var secondNumber = ReadNumber("Digite o segundo número: ");

    var operationResult = ExecuteOperation(option, firstNumber, secondNumber);

    if (!operationResult.Success)
    {
        ShowMessage(operationResult.ErrorMessage!);
        continue;
    }

    Console.WriteLine();
    Console.WriteLine($"Resultado: {operationResult.Result}");
    Console.WriteLine();
    Console.Write("Pressione ENTER para voltar ao menu...");
    Console.ReadLine();
}

static decimal ReadNumber(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        var input = Console.ReadLine();

        if (decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out var number))
        {
            return number;
        }

        Console.WriteLine("Valor inválido. Digite um número válido (ex.: 10 ou 10.5).\n");
    }
}

static OperationResult ExecuteOperation(int option, decimal firstNumber, decimal secondNumber)
{
    return option switch
    {
        1 => OperationResult.Ok(firstNumber + secondNumber),
        2 => OperationResult.Ok(firstNumber - secondNumber),
        3 => OperationResult.Ok(firstNumber * secondNumber),
        4 when secondNumber == 0 => OperationResult.Fail("Erro: divisão por zero não é permitida."),
        4 => OperationResult.Ok(firstNumber / secondNumber),
        _ => OperationResult.Fail("Opção de operação inválida.")
    };
}

static void ShowMessage(string message)
{
    Console.WriteLine();
    Console.WriteLine(message);
    Console.WriteLine();
    Console.Write("Pressione ENTER para continuar...");
    Console.ReadLine();
}

internal sealed record OperationResult(bool Success, decimal Result, string? ErrorMessage)
{
    public static OperationResult Ok(decimal result) => new(true, result, null);
    public static OperationResult Fail(string message) => new(false, 0, message);
}
